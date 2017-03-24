/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 19.03.2017
 * Время: 10:27
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
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
		SqlServer sqlServer;
		int selectTableLine = 0;
		String docNumber;
		
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
		
		void openPrice()
		{
			if(listView1.SelectedIndices.Count > 0){
				FormCounteragentPrice FCounteragentPrice = new FormCounteragentPrice();
				FCounteragentPrice.MdiParent = DataForms.FClient;
				FCounteragentPrice.Text = "Прайс-лист контрагента: " + listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text.ToString();
				FCounteragentPrice.PriceName = listView1.Items[listView1.SelectedIndices[0]].SubItems[2].Text.ToString();
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
						Utilits.Console.Log("Документ 'План закупок' успешно создан.");
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
				for(int i = 0; i < listView1.Items.Count; i++){
					newRow = oleDb.dataSet.Tables["PurchasePlanPriceLists"].NewRow();
					newRow["counteragentName"] = listView1.Items[i].SubItems[1].Text.ToString();
					newRow["counteragentPricelist"] = listView1.Items[i].SubItems[2].Text.ToString();
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
				
			}
			return false;
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */	
		void FormPurchasePlanDocLoad(object sender, EventArgs e)
		{
			//if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL) oleDb = new OleDb(DataConfig.localDatabase);
			//if(DataConfig.typeConnection == DataConstants.CONNETION_SERVER) sqlServer = new SqlServer();
			if(ID == null){
				Text = "Создать";
				dateTimePicker1.Value = DateTime.Today.Date;
				autorLabel.Text = "Автор: " + DataConfig.userName;
			}else{
				Text = "Изменить";
				//open();
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
				DataForms.FCounteragents.ListViewReturnValue = listView1;
				DataForms.FCounteragents.TypeReturnValue = "name&price";
				DataForms.FCounteragents.ShowMenuReturnValue();
				DataForms.FCounteragents.Show();
			}	
		}
		void DeleteButtonClick(object sender, EventArgs e)
		{
			if(listView1.SelectedItems.Count > 0) listView1.Items[listView1.SelectedItems[0].Index].Remove();
		}
		void PriceButtonClick(object sender, EventArgs e)
		{
			openPrice();
		}
		void ButtonSaveClick(object sender, EventArgs e)
		{
			if(DataConfig.userPermissions == "guest"){
				MessageBox.Show("У вас недостаточно прав чтобы выполнить данное действие.", "Сообщение");
				return;
			}
			saveNew();
			/*
			if(check()){
				if(ID == null) saveNew();
				else saveEdit();
			}else{
				MessageBox.Show("Некорректно заполнены поля формы.", "Сообщение:");
			}
			*/
		}

		
	}
}
