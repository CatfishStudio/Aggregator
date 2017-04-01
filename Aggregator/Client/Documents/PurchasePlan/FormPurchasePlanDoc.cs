/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 19.03.2017
 * Время: 10:27
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Data;
using Aggregator.Database.Local;
using Aggregator.Database.Server;
using Aggregator.Client.Directories;
using Aggregator.Utilits;

namespace Aggregator.Client.Documents.PurchasePlan
{
	/// <summary>
	/// Description of FormPurchasePlanDoc.
	/// </summary>
	public partial class FormPurchasePlanDoc : Form
	{
		public FormPurchasePlanDoc()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public String ID;
		OleDb oleDb;
		SearchNomenclatureOleDb searchNomenclatureOleDb;
		SqlServer sqlServer;
		String docNumber;
		int selectTableLine = 0;		// выбранная строка в таблице
		
		String getDocNumber()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL) {
				// OLEDB
				OleDbConnection oleDbConnection = new OleDbConnection();
				oleDbConnection.ConnectionString = DataConfig.oledbConnectLineBegin + DataConfig.localDatabase + DataConfig.oledbConnectLineEnd + DataConfig.oledbConnectPass;
				try{
					
					OleDbCommand oleDbCommand = new OleDbCommand("SELECT MAX(id) FROM PurchasePlan", oleDbConnection);
					oleDbConnection.Open();
					var order_id = oleDbCommand.ExecuteScalar();
					oleDbConnection.Close();
					
					int num;
					if (order_id.ToString() == "") num = 1;
					else num = (int)order_id + 1;
					String idStr = num.ToString();
					String numStr = "ПЗ-0000000";
					numStr = numStr.Remove((numStr.Length - idStr.Length));
					numStr += idStr;
					return numStr;
				}catch(Exception ex){
					oleDbConnection.Close();
					//Utilits.Console.Log("[ОШИБКА]: " + ex.Message.ToString(), false, true);
					Utilits.Console.Log("[ОШИБКА]: " + ex.ToString(), false, true);
				}
			} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				SqlConnection sqlConnection = new SqlConnection(DataConfig.serverConnection);
				try{
					SqlCommand sqlCommand = new SqlCommand("SELECT SCOPE_IDENTITY(PurchasePlan)", sqlConnection);
					sqlConnection.Open();
					var order_id = sqlCommand.ExecuteScalar();
					sqlConnection.Close();
					
					int num;
					if (order_id.ToString() == "") num = 1;
					else num = (int)order_id + 1;
					String idStr = num.ToString();
					String numStr = "ПЗ-0000000";
					numStr = numStr.Remove((numStr.Length - idStr.Length));
					numStr += idStr;
					return numStr;
				}catch(Exception ex){
					sqlConnection.Close();
					Utilits.Console.Log("[ОШИБКА]: " + ex.Message.ToString(), false, true);
				}
			}
			return null;
		}
		
		void editPrice()
		{
			if(listViewPrices.SelectedIndices.Count > 0){
				FormCounteragentPrice FCounteragentPrice = new FormCounteragentPrice();
				FCounteragentPrice.MdiParent = DataForms.FClient;
				FCounteragentPrice.Text = "Прайс-лист контрагента: " + listViewPrices.Items[listViewPrices.SelectedIndices[0]].SubItems[1].Text.ToString();
				FCounteragentPrice.PriceName = listViewPrices.Items[listViewPrices.SelectedIndices[0]].SubItems[2].Text.ToString();
				FCounteragentPrice.Show();
			}
		}
		
		void saveNew()
		{
			docNumber = getDocNumber();
			if(docNumber == null) {
				Utilits.Console.Log("[ОШИБКА] автонумерация не смогла назначить номер для документа.");
				return;
			}
			
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				oleDb = new OleDb(DataConfig.localDatabase);
				oleDb.oleDbCommandSelect.CommandText = "SELECT id, docDate, docNumber, docName, docAutor, docSum, docVat, docTotal FROM PurchasePlan WHERE (id = 0)";
				oleDb.ExecuteFill("PurchasePlan");				
				
				DataRow newRow = oleDb.dataSet.Tables["PurchasePlan"].NewRow();
				newRow["docDate"] = dateTimePicker1.Value;
				newRow["docNumber"] = docNumber;
				newRow["docName"] = "План закупок";
				newRow["docAutor"] = DataConfig.userName;
				newRow["docSum"] = 0;
				newRow["docVat"] = 0;
				newRow["docTotal"] = 0;
				oleDb.dataSet.Tables["PurchasePlan"].Rows.Add(newRow);
				
				oleDb.oleDbCommandInsert.CommandText = "INSERT INTO PurchasePlan (docDate, docNumber, docName, docAutor, docSum, docVat, docTotal) " +
					"VALUES (@docDate, @docNumber, @docName, @docAutor, @docSum, @docVat, @docTotal)";
				oleDb.oleDbCommandInsert.Parameters.Add("@docDate", OleDbType.Date, 255, "docDate");
				oleDb.oleDbCommandInsert.Parameters.Add("@docNumber", OleDbType.VarChar, 255, "docNumber");
				oleDb.oleDbCommandInsert.Parameters.Add("@docName", OleDbType.VarChar, 255, "docName");
				oleDb.oleDbCommandInsert.Parameters.Add("@docAutor", OleDbType.VarChar, 255, "docAutor");
				oleDb.oleDbCommandInsert.Parameters.Add("@docSum", OleDbType.Double, 15, "docSum");
				oleDb.oleDbCommandInsert.Parameters.Add("@docVat", OleDbType.Double, 15, "docVat");
				oleDb.oleDbCommandInsert.Parameters.Add("@docTotal", OleDbType.Double, 15, "docTotal");
				if(oleDb.ExecuteUpdate("PurchasePlan")){
					DataForms.FClient.updateHistory("PurchasePlan");
					if(saveNewChangesPriceLists()){
						Utilits.Console.Log("Документ План закупок №" + docNumber + ": успешно создан.");
						Close();
					}else{
						Utilits.Console.Log("[ПРЕДУПРЕЖДЕНИЕ] Документ План закупок №" + docNumber + ": не удалось сохранить список выбранных прайс листов.", false, true);
					}
				}
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				sqlServer = new SqlServer();
				sqlServer.sqlCommandSelect.CommandText = "SELECT id, docDate, docNumber, docName, docAutor, docSum, docVat, docTotal FROM PurchasePlan WHERE (id = 0)";
				sqlServer.ExecuteFill("PurchasePlan");				
				
				DataRow newRow = oleDb.dataSet.Tables["PurchasePlan"].NewRow();
				newRow["docDate"] = dateTimePicker1.Value;
				newRow["docNumber"] = docNumber;
				newRow["docName"] = "План закупок";
				newRow["docAutor"] = DataConfig.userName;
				newRow["docSum"] = 0;
				newRow["docVat"] = 0;
				newRow["docTotal"] = 0;
				sqlServer.dataSet.Tables["PurchasePlan"].Rows.Add(newRow);
				
				sqlServer.sqlCommandInsert.CommandText = "INSERT INTO PurchasePlan (docDate, docNumber, docName, docAutor, docSum, docVat, docTotal) " +
					"VALUES (@docDate, @docNumber, @docName, @docAutor, @docSum, @docVat, @docTotal)";
				sqlServer.sqlCommandInsert.Parameters.Add("@docDate", SqlDbType.Date, 255, "docDate");
				sqlServer.sqlCommandInsert.Parameters.Add("@docNumber", SqlDbType.VarChar, 255, "docNumber");
				sqlServer.sqlCommandInsert.Parameters.Add("@docName", SqlDbType.VarChar, 255, "docName");
				sqlServer.sqlCommandInsert.Parameters.Add("@docAutor", SqlDbType.VarChar, 255, "docAutor");
				sqlServer.sqlCommandInsert.Parameters.Add("@docSum", SqlDbType.Float, 15, "docSum");
				sqlServer.sqlCommandInsert.Parameters.Add("@docVat", SqlDbType.Float, 15, "docVat");
				sqlServer.sqlCommandInsert.Parameters.Add("@docTotal", SqlDbType.Float, 15, "docTotal");
				if(sqlServer.ExecuteUpdate("PurchasePlan")){
					DataForms.FClient.updateHistory("PurchasePlan");
					if(saveNewChangesPriceLists()){
						Utilits.Console.Log("Документ План закупок №" + docNumber + ": успешно создан.");
						Close();
					}else{
						Utilits.Console.Log("[ПРЕДУПРЕЖДЕНИЕ] Документ План закупок №" + docNumber + ": не удалось сохранить список выбранных прайс листов.", false, true);
					}
				}
			}
		}
		
		bool saveNewChangesPriceLists()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				oleDb = new OleDb(DataConfig.localDatabase);
				oleDb.oleDbCommandSelect.CommandText = "SELECT counteragentName, counteragentPricelist, docID FROM PurchasePlanPriceLists WHERE (id = 0)";
				oleDb.ExecuteFill("PurchasePlanPriceLists");				
				
				DataRow newRow = null;
				for(int i = 0; i < listViewPrices.Items.Count; i++){
					newRow = oleDb.dataSet.Tables["PurchasePlanPriceLists"].NewRow();
					newRow["counteragentName"] = listViewPrices.Items[i].SubItems[1].Text.ToString();
					newRow["counteragentPricelist"] = listViewPrices.Items[i].SubItems[2].Text.ToString();
					newRow["docID"] = docNumber;
					oleDb.dataSet.Tables["PurchasePlanPriceLists"].Rows.Add(newRow);
				}
				
				oleDb.oleDbCommandInsert.CommandText = "INSERT INTO PurchasePlanPriceLists (counteragentName, counteragentPricelist, docID) " +
					"VALUES (@counteragentName, @counteragentPricelist, @docID)";
				oleDb.oleDbCommandInsert.Parameters.Add("@counteragentName", OleDbType.VarChar, 255, "counteragentName");
				oleDb.oleDbCommandInsert.Parameters.Add("@counteragentPricelist", OleDbType.VarChar, 255, "counteragentPricelist");
				oleDb.oleDbCommandInsert.Parameters.Add("@docID", OleDbType.VarChar, 255, "docID");
				if(oleDb.ExecuteUpdate("PurchasePlanPriceLists")){
					return true;
				}else{
					return false;
				}
				
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				sqlServer = new SqlServer();
				sqlServer.sqlCommandSelect.CommandText = "SELECT counteragentName, counteragentPricelist, docID FROM PurchasePlanPriceLists WHERE (id = 0)";
				sqlServer.ExecuteFill("PurchasePlanPriceLists");
				
				DataRow newRow = null;
				for(int i = 0; i < listViewPrices.Items.Count; i++){
					newRow = oleDb.dataSet.Tables["PurchasePlanPriceLists"].NewRow();
					newRow["counteragentName"] = listViewPrices.Items[i].SubItems[1].Text.ToString();
					newRow["counteragentPricelist"] = listViewPrices.Items[i].SubItems[2].Text.ToString();
					newRow["docID"] = docNumber;
					sqlServer.dataSet.Tables["PurchasePlanPriceLists"].Rows.Add(newRow);
				}
				
				sqlServer.sqlCommandInsert.CommandText = "INSERT INTO PurchasePlanPriceLists (counteragentName, counteragentPricelist, docID) " +
					"VALUES (@counteragentName, @counteragentPricelist, @docID)";
				sqlServer.sqlCommandInsert.Parameters.Add("@counteragentName", SqlDbType.VarChar, 255, "counteragentName");
				sqlServer.sqlCommandInsert.Parameters.Add("@counteragentPricelist", SqlDbType.VarChar, 255, "counteragentPricelist");
				sqlServer.sqlCommandInsert.Parameters.Add("@docID", SqlDbType.VarChar, 255, "docID");
				if(sqlServer.ExecuteUpdate("PurchasePlanPriceLists")){
					return true;
				}else{
					return false;
				}
			}
			return false;
		}
		
		void open()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				oleDb = new OleDb(DataConfig.localDatabase);
				oleDb.oleDbCommandSelect.CommandText = "SELECT id, docDate, docNumber, docName, docAutor, docSum, docVat, docTotal FROM PurchasePlan WHERE (id = " + ID + ")";
				if(oleDb.ExecuteFill("PurchasePlan")){
					docNumber = oleDb.dataSet.Tables["PurchasePlan"].Rows[0]["docNumber"].ToString();
					docNumberTextBox.Text = docNumber;
					dateTimePicker1.Value = (DateTime)oleDb.dataSet.Tables["PurchasePlan"].Rows[0]["docDate"];
					autorLabel.Text = "Автор: " + oleDb.dataSet.Tables["PurchasePlan"].Rows[0]["docAutor"].ToString();
					openPrices();
				}else{
					Utilits.Console.Log("[ОШИБКА] программа не смогла открыть документ план закупок.", false, true);
				}
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				sqlServer = new SqlServer();
				sqlServer.sqlCommandSelect.CommandText = "SELECT id, docDate, docNumber, docName, docAutor, docSum, docVat, docTotal FROM PurchasePlan WHERE (id = " + ID + ")";
				if(sqlServer.ExecuteFill("PurchasePlan")){
					docNumber = sqlServer.dataSet.Tables["PurchasePlan"].Rows[0]["docNumber"].ToString();
					docNumberTextBox.Text = docNumber;
					dateTimePicker1.Value = (DateTime)sqlServer.dataSet.Tables["PurchasePlan"].Rows[0]["docDate"];
					autorLabel.Text = "Автор: " + sqlServer.dataSet.Tables["PurchasePlan"].Rows[0]["docAutor"].ToString();
					openPrices();
				}else{
					Utilits.Console.Log("[ОШИБКА] программа не смогла открыть документ план закупок.", false, true);
				}
			}
		}
		
		void openPrices()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				oleDb = new OleDb(DataConfig.localDatabase);
				oleDb.oleDbCommandSelect.CommandText = "SELECT counteragentName, counteragentPricelist, docID FROM PurchasePlanPriceLists WHERE (docID = '" + docNumber + "')";
				if(oleDb.ExecuteFill("PurchasePlanPriceLists")){
					ListViewItem ListViewItem_add;
					foreach(DataRow row in oleDb.dataSet.Tables["PurchasePlanPriceLists"].Rows){
						ListViewItem_add = new ListViewItem();
						ListViewItem_add.SubItems.Add(row["counteragentName"].ToString());
						ListViewItem_add.StateImageIndex = 1;
						ListViewItem_add.SubItems.Add(row["counteragentPricelist"].ToString());
						listViewPrices.Items.Add(ListViewItem_add);
					}
				}else{
					Utilits.Console.Log("[ПРЕДУПРЕЖДЕНИЕ] программа не смогла загрузить прайс листы документа план закупок №" + docNumber, false, true);
				}
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				sqlServer = new SqlServer();
				sqlServer.sqlCommandSelect.CommandText = "SELECT counteragentName, counteragentPricelist, docID FROM PurchasePlanPriceLists WHERE (docID = '" + docNumber + "')";
				if(sqlServer.ExecuteFill("PurchasePlanPriceLists")){
					ListViewItem ListViewItem_add;
					foreach(DataRow row in sqlServer.dataSet.Tables["PurchasePlanPriceLists"].Rows){
						ListViewItem_add = new ListViewItem();
						ListViewItem_add.SubItems.Add(row["counteragentName"].ToString());
						ListViewItem_add.StateImageIndex = 1;
						ListViewItem_add.SubItems.Add(row["counteragentPricelist"].ToString());
						listViewPrices.Items.Add(ListViewItem_add);
					}
				}else{
					Utilits.Console.Log("[ПРЕДУПРЕЖДЕНИЕ] программа не смогла загрузить прайс листы документа план закупок №" + docNumber, false, true);
				}
			}
		}
		
		bool saveEdit()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				oleDb = new OleDb(DataConfig.localDatabase);
				oleDb.oleDbCommandSelect.CommandText = "SELECT id, docDate, docNumber, docName, docAutor, docSum, docVat, docTotal FROM PurchasePlan WHERE (id = " + ID + ")";
				if(oleDb.ExecuteFill("PurchasePlan")){
					oleDb.dataSet.Tables["PurchasePlan"].Rows[0]["docDate"] = dateTimePicker1.Value;
					oleDb.dataSet.Tables["PurchasePlan"].Rows[0]["docAutor"] = DataConfig.userName;
					oleDb.oleDbCommandUpdate.CommandText = "UPDATE PurchasePlan SET " +
					"[docDate] = @docDate, [docNumber] = @docNumber, " +
					"[docName] = @docName, [docAutor] = @docAutor, [docSum] = @docSum, " +
					"[docVat] = @docVat, [docTotal] = @docTotal " +
					"WHERE ([id] = @id)";
					oleDb.oleDbCommandUpdate.Parameters.Add("@docDate", OleDbType.Date, 255, "docDate");
					oleDb.oleDbCommandUpdate.Parameters.Add("@docNumber", OleDbType.VarChar, 255, "docNumber");
					oleDb.oleDbCommandUpdate.Parameters.Add("@docName", OleDbType.VarChar, 255, "docName");
					oleDb.oleDbCommandUpdate.Parameters.Add("@docAutor", OleDbType.VarChar, 255, "docAutor");
					oleDb.oleDbCommandUpdate.Parameters.Add("@docSum", OleDbType.Double, 15, "docSum");
					oleDb.oleDbCommandUpdate.Parameters.Add("@docVat", OleDbType.Double, 15, "docVat");
					oleDb.oleDbCommandUpdate.Parameters.Add("@docTotal", OleDbType.Double, 15, "docTotal");
					oleDb.oleDbCommandUpdate.Parameters.Add("@id", OleDbType.Integer, 10, "id");
					if(oleDb.ExecuteUpdate("PurchasePlan")){
						return true;
					}else{
						Utilits.Console.Log("[ОШИБКА] программа не смогла сохранить основные данные документа план закупок №" + docNumber, false, true);
					}
				}else{
					Utilits.Console.Log("[ОШИБКА] программа не смогла загрузить данные документа план закупок №" + docNumber, false, true);
				}			
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				sqlServer = new SqlServer();
				sqlServer.sqlCommandSelect.CommandText = "SELECT id, docDate, docNumber, docName, docAutor, docSum, docVat, docTotal FROM PurchasePlan WHERE (id = " + ID + ")";
				if(sqlServer.ExecuteFill("PurchasePlan")){
					sqlServer.dataSet.Tables["PurchasePlan"].Rows[0]["docDate"] = dateTimePicker1.Value;
					sqlServer.dataSet.Tables["PurchasePlan"].Rows[0]["docAutor"] = DataConfig.userName;
					sqlServer.sqlCommandUpdate.CommandText = "UPDATE PurchasePlan SET " +
					"[docDate] = @docDate, [docNumber] = @docNumber, " +
					"[docName] = @docName, [docAutor] = @docAutor, [docSum] = @docSum, " +
					"[docVat] = @docVat, [docTotal] = @docTotal " +
					"WHERE ([id] = @id)";
					sqlServer.sqlCommandUpdate.Parameters.Add("@docDate", SqlDbType.Date, 255, "docDate");
					sqlServer.sqlCommandUpdate.Parameters.Add("@docNumber", SqlDbType.VarChar, 255, "docNumber");
					sqlServer.sqlCommandUpdate.Parameters.Add("@docName", SqlDbType.VarChar, 255, "docName");
					sqlServer.sqlCommandUpdate.Parameters.Add("@docAutor", SqlDbType.VarChar, 255, "docAutor");
					sqlServer.sqlCommandUpdate.Parameters.Add("@docSum", SqlDbType.Float, 15, "docSum");
					sqlServer.sqlCommandUpdate.Parameters.Add("@docVat", SqlDbType.Float, 15, "docVat");
					sqlServer.sqlCommandUpdate.Parameters.Add("@docTotal", SqlDbType.Float, 15, "docTotal");
					sqlServer.sqlCommandUpdate.Parameters.Add("@id", SqlDbType.Int, 10, "id");
					if(sqlServer.ExecuteUpdate("PurchasePlan")){
						return true;
					}else{
						Utilits.Console.Log("[ОШИБКА] программа не смогла сохранить основные данные документа план закупок №" + docNumber, false, true);
					}
				}else{
					Utilits.Console.Log("[ОШИБКА] программа не смогла загрузить данные документа план закупок №" + docNumber, false, true);
				}
			}
			return false;
		}
		
		bool saveEditPrices()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				QueryOleDb query = new QueryOleDb(DataConfig.localDatabase);
				query.SetCommand("DELETE FROM PurchasePlanPriceLists WHERE (docID = '" + docNumber + "')");
				if(query.Execute()){
					if(saveNewChangesPriceLists()){
						return true;
					}else{
						Utilits.Console.Log("[ОШИБКА] программа не смогла записать ищменённый выбор прайс-листов в документ план закупок №" + docNumber, false, true);
					}
				}else{
					Utilits.Console.Log("[ОШИБКА] программа не смогла удалить прайс-листы из документа план закупок №" + docNumber, false, true);
				}
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				QuerySqlServer query = new QuerySqlServer();
				query.SetCommand("DELETE FROM PurchasePlanPriceLists WHERE (docID = '" + docNumber + "')");
				if(query.Execute()){
					if(saveNewChangesPriceLists()){
						return true;
					}else{
						Utilits.Console.Log("[ОШИБКА] программа не смогла записать ищменённый выбор прайс-листов в документ план закупок №" + docNumber, false, true);
					}
				}else{
					Utilits.Console.Log("[ОШИБКА] программа не смогла удалить прайс-листы из документа план закупок №" + docNumber, false, true);
				}
			}
			return false;
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */	
		void FormPurchasePlanDocLoad(object sender, EventArgs e)
		{
			if(ID == null){
				Text = "Создать";
				dateTimePicker1.Value = DateTime.Today.Date;
				autorLabel.Text = "Автор: " + DataConfig.userName;
			}else{
				Text = "Изменить";
				open();
			}
		}
		void FormPurchasePlanDocFormClosed(object sender, FormClosedEventArgs e)
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL && oleDb != null) oleDb.Dispose();
			if(DataConfig.typeConnection == DataConstants.CONNETION_SERVER && sqlServer != null) sqlServer.Dispose();
			Dispose();
		}
		void ButtonCancelClick(object sender, EventArgs e)
		{
			Close();
		}
		void AddButtonClick(object sender, EventArgs e)
		{
			if(DataForms.FCounteragents != null) DataForms.FCounteragents.Close();
			if(DataForms.FCounteragents == null) {
				DataForms.FCounteragents = new FormCounteragents();
				DataForms.FCounteragents.MdiParent = DataForms.FClient;
				DataForms.FCounteragents.ListViewReturnValue = listViewPrices;
				DataForms.FCounteragents.TypeReturnValue = "name&price";
				DataForms.FCounteragents.ShowMenuReturnValue();
				DataForms.FCounteragents.Show();
			}	
		}
		void DeleteButtonClick(object sender, EventArgs e)
		{
			if(listViewPrices.SelectedItems.Count > 0) listViewPrices.Items[listViewPrices.SelectedItems[0].Index].Remove();
		}
		void PriceButtonClick(object sender, EventArgs e)
		{
			editPrice();
		}
		void ButtonSaveClick(object sender, EventArgs e)
		{
			if(DataConfig.userPermissions == "guest"){
				MessageBox.Show("У вас недостаточно прав чтобы выполнить данное действие.", "Сообщение");
				return;
			}
			if(ID == null){
				saveNew();
			}else{
				if(saveEdit() && saveEditPrices()){
					DataForms.FClient.updateHistory("PurchasePlan");
					Utilits.Console.Log("Документ План закупок №" + docNumber + ": успешно изменён и сохранён.");
					Close();
				}else{
					Utilits.Console.Log("[ПРЕДУПРЕЖДЕНИЕ] Документ План закупок №" + docNumber + ": не удалось сохранить изменения.", false, true);
				}
			}
			
			/*
			if(check()){
				if(ID == null) saveNew();
				else saveEdit();
			}else{
				MessageBox.Show("Некорректно заполнены поля формы.", "Сообщение:");
			}
			*/
		}
		void ButtonNomenclatureAddClick(object sender, EventArgs e)
		{
			if(DataForms.FNomenclature != null) DataForms.FNomenclature.Close();
			if(DataForms.FNomenclature == null) {
				DataForms.FNomenclature = new FormNomenclature();
				DataForms.FNomenclature.MdiParent = DataForms.FClient;
				DataForms.FNomenclature.ListViewReturnValue = listViewNomenclature;
				DataForms.FNomenclature.TypeReturnValue = "file";
				DataForms.FNomenclature.ShowMenuReturnValue();
				DataForms.FNomenclature.Show();
			}
		}
		void ButtonNomenclatureDeleteClick(object sender, EventArgs e)
		{
			if(listViewNomenclature.SelectedItems.Count > 0) listViewNomenclature.Items[listViewNomenclature.SelectedItems[0].Index].Remove();
		}
		void ButtonNomenclaturesAddClick(object sender, EventArgs e)
		{
			if(DataForms.FNomenclature != null) DataForms.FNomenclature.Close();
			if(DataForms.FNomenclature == null) {
				DataForms.FNomenclature = new FormNomenclature();
				DataForms.FNomenclature.MdiParent = DataForms.FClient;
				DataForms.FNomenclature.ListViewReturnValue = listViewNomenclature;
				DataForms.FNomenclature.TypeReturnValue = "folder";
				DataForms.FNomenclature.ShowMenuReturnValue();
				DataForms.FNomenclature.Show();
			}
		}
		void ButtonNomenclaturesDeleteClick(object sender, EventArgs e)
		{
			while(listViewNomenclature.Items.Count > 0){
				listViewNomenclature.Items[0].Remove();
			}
		}
		void Button4Click(object sender, EventArgs e)
		{
			if(listViewPrices.Items.Count == 0){
				MessageBox.Show("Вы не добавили не одного прайса.", "Сообщение");
				return;
			}
			
			if(listViewNomenclature.SelectedItems.Count > 0){
				List<Nomenclature> nomenclatureList;
				
				String nID = listViewNomenclature.Items[listViewNomenclature.SelectedItems[0].Index].SubItems[2].Text;
				searchNomenclatureOleDb = new SearchNomenclatureOleDb();
				searchNomenclatureOleDb.setPrices(listViewPrices);
				nomenclatureList = searchNomenclatureOleDb.getFindNomenclature(nID);
				if(nomenclatureList.Count > 0){
					
					FormPurchasePlanNomenclature FPurchasePlanNomenclature = new FormPurchasePlanNomenclature();
					FPurchasePlanNomenclature.MdiParent = DataForms.FClient;
					FPurchasePlanNomenclature.LoadNomenclature(nomenclatureList);
					FPurchasePlanNomenclature.Show();
				}
			}
		}
		void ListViewNomenclatureSelectedIndexChanged(object sender, EventArgs e)
		{
			if(listViewNomenclature.SelectedItems.Count > 0){
				selectTableLine = listViewNomenclature.SelectedItems[0].Index;
				textBox1.Text = listViewNomenclature.Items[selectTableLine].SubItems[1].Text;
				textBox2.Text = listViewNomenclature.Items[selectTableLine].SubItems[4].Text;
				textBox3.Text = listViewNomenclature.Items[selectTableLine].SubItems[3].Text;
			}
		}
		void Button8Click(object sender, EventArgs e)
		{
			if(DataForms.FUnits != null) DataForms.FUnits.Close();
			if(DataForms.FUnits == null) {
				DataForms.FUnits = new FormUnits();
				DataForms.FUnits.MdiParent = DataForms.FClient;
				DataForms.FUnits.TextBoxReturnValue = textBox3;
				DataForms.FUnits.ShowMenuReturnValue();
				DataForms.FUnits.Show();
			}
		}
		void Button9Click(object sender, EventArgs e)
		{
			textBox3.Clear();
		}
		void TextBox3TextChanged(object sender, EventArgs e)
		{
			if(listViewNomenclature.Items.Count > 0){
				listViewNomenclature.Items[selectTableLine].SubItems[3].Text = textBox3.Text;
			}
		}
		void TextBox2KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab){
				String Value = textBox2.Text;
				textBox2.Clear();
				textBox2.Text = Conversion.StringToMoney(Conversion.StringToDouble(Value).ToString());
				if(textBox2.Text == "" || Conversion.checkString(textBox2.Text) == false) textBox2.Text = "0,00";
			}
		}
		void TextBox2TextLostFocus(object sender, EventArgs e)
		{
			String Value = textBox2.Text;
			textBox2.Clear();
			textBox2.Text = Conversion.StringToMoney(Conversion.StringToDouble(Value).ToString());
			if(textBox2.Text == "" || Conversion.checkString(textBox2.Text) == false) textBox2.Text = "0,00";
		}
		void TextBox2TextChanged(object sender, EventArgs e)
		{
			if(textBox2.Text == "" || Conversion.checkString(textBox2.Text) == false) textBox2.Text = "0,00";
			if(listViewNomenclature.Items.Count > 0){
				listViewNomenclature.Items[selectTableLine].SubItems[4].Text = textBox2.Text;
			}
		}
		void Button7Click(object sender, EventArgs e)
		{
			Calculator Calc = new Calculator();
			Calc.TextBoxReturnValue = textBox2;
			Calc.MdiParent = DataForms.FClient;
			Calc.Show();
		}
		
	}
}
