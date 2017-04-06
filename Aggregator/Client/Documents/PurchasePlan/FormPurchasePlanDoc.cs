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
using System.Text.RegularExpressions;
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
		int selectTableLine = -1;		// выбранная строка в таблице
		
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
		
		void search()
		{
			String str;
			for(int i = 0; i < listViewNomenclature.Items.Count; i++){
				str = listViewNomenclature.Items[i].SubItems[2].Text;
				if(str.Contains(comboBox1.Text)){
					listViewNomenclature.FocusedItem = listViewNomenclature.Items[i];
					listViewNomenclature.Items[i].Selected = true;
					listViewNomenclature.Select();
					listViewNomenclature.EnsureVisible(i);
					Utilits.Console.Log(str);
					break;
				}
			}
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
			selectTableLine = -1;
			textBox1.Clear();
			textBox3.Clear();
			textBox2.Text = "0,00";
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
			selectTableLine = -1;
			textBox1.Clear();
			textBox3.Clear();
			textBox2.Text = "0,00";
				
		}
		void Button4Click(object sender, EventArgs e)
		{
			if(listViewPrices.Items.Count == 0){
				MessageBox.Show("Вы не добавили не одного прайса.", "Сообщение");
				return;
			}
			
			if(listViewNomenclature.SelectedItems.Count > 0){
				List<Nomenclature> nomenclatureList;
				
				String nID = listViewNomenclature.Items[listViewNomenclature.SelectedItems[0].Index].SubItems[1].Text;
				searchNomenclatureOleDb = new SearchNomenclatureOleDb();
				searchNomenclatureOleDb.setPrices(listViewPrices);
				nomenclatureList = searchNomenclatureOleDb.getFindNomenclature(nID);
				if(nomenclatureList.Count > 0){
					
					FormPurchasePlanNomenclature FPurchasePlanNomenclature = new FormPurchasePlanNomenclature();
					FPurchasePlanNomenclature.MdiParent = DataForms.FClient;
					FPurchasePlanNomenclature.ListViewReturnValue = listViewNomenclature;
					FPurchasePlanNomenclature.SelectTableLine = selectTableLine;
					FPurchasePlanNomenclature.LoadNomenclature(nomenclatureList);
					FPurchasePlanNomenclature.Show();
				}
			}
		}
		void ListViewNomenclatureSelectedIndexChanged(object sender, EventArgs e)
		{
			if(listViewNomenclature.SelectedItems.Count > 0){
				selectTableLine = listViewNomenclature.SelectedItems[0].Index;
				textBox1.Text = listViewNomenclature.Items[selectTableLine].SubItems[2].Text;
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
			if(listViewNomenclature.Items.Count > 0 && selectTableLine > -1){
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
			if(listViewNomenclature.Items.Count > 0 && selectTableLine > -1){
				listViewNomenclature.Items[selectTableLine].SubItems[4].Text = textBox2.Text;
			}
		}
		void Button6Click(object sender, EventArgs e)
		{
			textBox2.Text = "0,00";
		}
		void Button7Click(object sender, EventArgs e)
		{
			Calculator Calc = new Calculator();
			Calc.TextBoxReturnValue = textBox2;
			Calc.MdiParent = DataForms.FClient;
			Calc.Show();
		}
		void Button1Click(object sender, EventArgs e)
		{
			if(listViewPrices.Items.Count == 0){
				MessageBox.Show("Вы не добавили не одного прайса.", "Сообщение");
				return;
			}
			searchNomenclatureOleDb = new SearchNomenclatureOleDb();
			searchNomenclatureOleDb.setPrices(listViewPrices);
			searchNomenclatureOleDb.autoFindNomenclature(listViewNomenclature);
		}
		void ButtonPrintPreviewClick(object sender, EventArgs e)
		{
			PrintPreviewDialog ppd = new PrintPreviewDialog();
			ppd.Document = printDocument1;
			ppd.MdiParent = DataForms.FClient;
			ppd.Show();	
		}
		void ButtonPrintClick(object sender, EventArgs e)
		{
			if(printDialog1.ShowDialog() == DialogResult.OK)
			{
				printDocument1.PrinterSettings = printDialog1.PrinterSettings;
				printDocument1.Print();
			}	
		}
		void PrintDocument1PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			// Заголовок документа
			e.Graphics.DrawString("ПЛАН ЗАКУПОК № " + docNumberTextBox.Text + "   дата: " + dateTimePicker1.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, 20, 20);
			
		}
		void ДобавитьПрайслистToolStripMenuItemClick(object sender, EventArgs e)
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
		void УдалитьПрайслистToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(listViewPrices.SelectedItems.Count > 0) listViewPrices.Items[listViewPrices.SelectedItems[0].Index].Remove();
		}
		void ПросмотретьПрайслистToolStripMenuItemClick(object sender, EventArgs e)
		{
			editPrice();
		}
		void ДобавитьНоменклатуруToolStripMenuItemClick(object sender, EventArgs e)
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
		void ДобавитьМножествоНоменклатурыToolStripMenuItemClick(object sender, EventArgs e)
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
		void УдалитьВыбраннуюНоменклатуруToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(listViewNomenclature.SelectedItems.Count > 0) listViewNomenclature.Items[listViewNomenclature.SelectedItems[0].Index].Remove();
			selectTableLine = -1;
			textBox1.Clear();
			textBox3.Clear();
			textBox2.Text = "0,00";
		}
		void УдалитьВесьПереченьНоменклатурыToolStripMenuItemClick(object sender, EventArgs e)
		{
			while(listViewNomenclature.Items.Count > 0){
				listViewNomenclature.Items[0].Remove();
			}
			selectTableLine = -1;
			textBox1.Clear();
			textBox3.Clear();
			textBox2.Text = "0,00";
		}
		void ПодобратьНоменклатуруToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(listViewPrices.Items.Count == 0){
				MessageBox.Show("Вы не добавили не одного прайса.", "Сообщение");
				return;
			}
			
			if(listViewNomenclature.SelectedItems.Count > 0){
				List<Nomenclature> nomenclatureList;
				
				String nID = listViewNomenclature.Items[listViewNomenclature.SelectedItems[0].Index].SubItems[1].Text;
				searchNomenclatureOleDb = new SearchNomenclatureOleDb();
				searchNomenclatureOleDb.setPrices(listViewPrices);
				nomenclatureList = searchNomenclatureOleDb.getFindNomenclature(nID);
				if(nomenclatureList.Count > 0){
					
					FormPurchasePlanNomenclature FPurchasePlanNomenclature = new FormPurchasePlanNomenclature();
					FPurchasePlanNomenclature.MdiParent = DataForms.FClient;
					FPurchasePlanNomenclature.ListViewReturnValue = listViewNomenclature;
					FPurchasePlanNomenclature.SelectTableLine = selectTableLine;
					FPurchasePlanNomenclature.LoadNomenclature(nomenclatureList);
					FPurchasePlanNomenclature.Show();
				}
			}
		}
		void АвтоподборНоменклатурыToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(listViewPrices.Items.Count == 0){
				MessageBox.Show("Вы не добавили не одного прайса.", "Сообщение");
				return;
			}
			searchNomenclatureOleDb = new SearchNomenclatureOleDb();
			searchNomenclatureOleDb.setPrices(listViewPrices);
			searchNomenclatureOleDb.autoFindNomenclature(listViewNomenclature);
		}
		void FindButtonClick(object sender, EventArgs e)
		{
			if(comboBox1.Text != "") comboBox1.Items.Add(comboBox1.Text);
			search();
		}
		void ComboBox1KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyData == Keys.Enter){
				if(comboBox1.Text != "") comboBox1.Items.Add(comboBox1.Text);
				search();
			}
		}
		void Button3Click(object sender, EventArgs e)
		{
			textBox4.Text = "0,00";
		}
		void Button2Click(object sender, EventArgs e)
		{
			Calculator Calc = new Calculator();
			Calc.TextBoxReturnValue = textBox4;
			Calc.MdiParent = DataForms.FClient;
			Calc.Show();
		}
		void TextBox4TextLostFocus(object sender, EventArgs e)
		{
			String Value = textBox4.Text;
			textBox4.Clear();
			textBox4.Text = Conversion.StringToMoney(Math.Round(Conversion.StringToDouble(Value), 2).ToString());
			if(textBox4.Text == "" || Conversion.checkString(textBox4.Text) == false) textBox4.Text = "0,00";
		}
		void TextBox4KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab){
				String Value = textBox4.Text;
				textBox4.Clear();
				textBox4.Text = Conversion.StringToMoney(Math.Round(Conversion.StringToDouble(Value), 2).ToString());
				if(textBox4.Text == "" || Conversion.checkString(textBox4.Text) == false) textBox4.Text = "0,00";
			}
		}
		void TextBox4TextChanged(object sender, EventArgs e)
		{
			if(textBox4.Text == "" || Conversion.checkString(textBox4.Text) == false) textBox4.Text = "0,00";
		}
		void Button10Click(object sender, EventArgs e)
		{
			textBox5.Text = "0,00";
		}
		void Button5Click(object sender, EventArgs e)
		{
			Calculator Calc = new Calculator();
			Calc.TextBoxReturnValue = textBox5;
			Calc.MdiParent = DataForms.FClient;
			Calc.Show();
		}
		void TextBox5TextLostFocus(object sender, EventArgs e)
		{
			String Value = textBox5.Text;
			textBox5.Clear();
			textBox5.Text = Conversion.StringToMoney(Math.Round(Conversion.StringToDouble(Value), 2).ToString());
			if(textBox5.Text == "" || Conversion.checkString(textBox5.Text) == false) textBox5.Text = "0,00";
		}
		void TextBox5TextChanged(object sender, EventArgs e)
		{
			if(textBox5.Text == "" || Conversion.checkString(textBox5.Text) == false) textBox5.Text = "0,00";
		}
		void TextBox5KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab){
				String Value = textBox5.Text;
				textBox5.Clear();
				textBox5.Text = Conversion.StringToMoney(Math.Round(Conversion.StringToDouble(Value), 2).ToString());
				if(textBox5.Text == "" || Conversion.checkString(textBox5.Text) == false) textBox5.Text = "0,00";
			}
		}
		void Button12Click(object sender, EventArgs e)
		{
			textBox6.Text = "0,00";
		}
		void Button11Click(object sender, EventArgs e)
		{
			Calculator Calc = new Calculator();
			Calc.TextBoxReturnValue = textBox6;
			Calc.MdiParent = DataForms.FClient;
			Calc.Show();
		}
		void TextBox6TextLostFocus(object sender, EventArgs e)
		{
			String Value = textBox6.Text;
			textBox6.Clear();
			textBox6.Text = Conversion.StringToMoney(Math.Round(Conversion.StringToDouble(Value), 2).ToString());
			if(textBox6.Text == "" || Conversion.checkString(textBox6.Text) == false) textBox6.Text = "0,00";
		}
		void TextBox6TextChanged(object sender, EventArgs e)
		{
			if(textBox6.Text == "" || Conversion.checkString(textBox6.Text) == false) textBox6.Text = "0,00";
		}
		void TextBox6KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab){
				String Value = textBox6.Text;
				textBox6.Clear();
				textBox6.Text = Conversion.StringToMoney(Math.Round(Conversion.StringToDouble(Value), 2).ToString());
				if(textBox6.Text == "" || Conversion.checkString(textBox6.Text) == false) textBox6.Text = "0,00";
			}
		}
		
		
		
		
		
	}
}
