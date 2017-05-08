/*
 * Создано в SharpDevelop.
 * Пользователь: Somov Studio
 * Дата: 08.05.2017
 * Время: 5:53
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Data;
using Aggregator.Database.Local;
using Aggregator.Database.Server;
using Aggregator.Utilits;

namespace Aggregator.Client.Documents
{
	/// <summary>
	/// Description of FormFullJournal.
	/// </summary>
	public partial class FormFullJournal : Form
	{
		public FormFullJournal()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		/*
		OleDbConnection oleDbConnection;
		OleDbCommand oleDbCommandSelect;
		OleDbDataAdapter oleDbDataAdapter;
		
		SqlConnection sqlConnection;
		SqlCommand sqlCommandSelect;
		SqlDataAdapter sqlDataAdapter;
		
		DataSet dataSet;
		*/
		
		OleDb oleDb;
		SqlServer sqlServer;
		
		int selectTableLine = 0;		// выбранная строка в таблице
		
		void getPeriod()
		{
			if(DataConfig.period == DataConstants.TODAY){
				dateTimePicker1.Value = DateTime.Today.Date;
				dateTimePicker2.Value = DateTime.Today.Date;
			}else if(DataConfig.period == DataConstants.YESTERDAY){
				dateTimePicker1.Value = DateTime.Now.AddDays(-1);
				dateTimePicker2.Value = DateTime.Now.Date;
			}else if(DataConfig.period == DataConstants.WEEK){
				dateTimePicker1.Value = DateTime.Now.AddDays(-7);
				dateTimePicker2.Value = DateTime.Now.Date;
			}else if(DataConfig.period == DataConstants.MONTH){
				var yr = DateTime.Today.Year;
				var mth = DateTime.Today.Month;
				var firstDay = new DateTime(yr, mth, 1);
				var lastDay = new DateTime(yr, mth, 1).AddMonths(1).AddDays(-1);
				dateTimePicker1.Value = firstDay;
				dateTimePicker2.Value = lastDay;
			}else if(DataConfig.period == DataConstants.YEAR){
				var yr = DateTime.Today.Year;
				var firstDay = new DateTime(yr, 1, 1);
				var lastDay = new DateTime(yr+1, 1, 1).AddDays(-1);
				dateTimePicker1.Value = firstDay;
				dateTimePicker2.Value = lastDay;
			}
		}
		
		public void TableRefresh()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL) {
				// OLEDB
				try{
					TableRefreshLocal();
				}catch(Exception ex){
					disposeDb();
					Utilits.Console.Log("[ОШИБКА]: " + ex.Message, false, true);
				}
			} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				try{
					TableRefreshServer();
				}catch(Exception ex){
					disposeDb();
					Utilits.Console.Log("[ОШИБКА]: " + ex.Message, false, true);
				}
			}
		}
		
		void TableRefreshLocal()
		{
			oleDb = new OleDb(DataConfig.localDatabase);
			oleDb.dataSet.Clear();
			
			oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM PurchasePlan WHERE (docDate BETWEEN #" + 
				dateTimePicker1.Value.ToString("dd.MM.yyyy").Replace(".", "/") + "# AND #" + 
				dateTimePicker2.Value.ToString("dd.MM.yyyy").Replace(".", "/") + "#) " +
				"AND (docNumber LIKE '%" + toolStripComboBox1.Text + 
				"%' OR docTotal LIKE '%" + toolStripComboBox1.Text + 
				"%' OR docAutor LIKE '%" + toolStripComboBox1.Text +
				"%') ORDER BY docDate DESC";
			oleDb.ExecuteFill("PurchasePlan");
			
			oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Orders WHERE (docDate BETWEEN #" + 
				dateTimePicker1.Value.ToString("dd.MM.yyyy").Replace(".", "/") + "# AND #" + 
				dateTimePicker2.Value.ToString("dd.MM.yyyy").Replace(".", "/") + "#) " +
				"AND (docNumber LIKE '%" + toolStripComboBox1.Text + 
				"%' OR docSum LIKE '%" + toolStripComboBox1.Text + 
				"%' OR docVat LIKE '%" + toolStripComboBox1.Text + 
				"%' OR docTotal LIKE '%" + toolStripComboBox1.Text + 
				"%' OR docAutor LIKE '%" + toolStripComboBox1.Text +
				"%' OR docCounteragent LIKE '%" + toolStripComboBox1.Text +
				"%') ORDER BY docDate DESC";
			oleDb.ExecuteFill("Orders");
			
			if(oleDb.dataSet.Tables.Count > 0){
				listView1.Items.Clear();
				DateTime dt;
				ListViewItem listViewItemAdd;
				
				DataSet dataSet = new DataSet();
				oleDb.dataSet.Tables[0].DefaultView.Sort = "docDate DESC";
				if(oleDb.dataSet.Tables.Count > 1) oleDb.dataSet.Tables[0].Merge(oleDb.dataSet.Tables[1]);	
				dataSet.Tables.Add(oleDb.dataSet.Tables[0].DefaultView.ToTable());
				
				foreach(DataRow row in dataSet.Tables[0].Rows)
	    		{
					listViewItemAdd = new ListViewItem();
					dt = new DateTime();
					DateTime.TryParse(row["docDate"].ToString(), out dt);
					listViewItemAdd.SubItems.Add(dt.ToString("dd.MM.yyyy"));
					
					if(row["docName"].ToString() == "План закупок") listViewItemAdd.StateImageIndex = 1;
					else if(row["docName"].ToString() == "Заказ") listViewItemAdd.StateImageIndex = 0;
					
					listViewItemAdd.SubItems.Add(row["docNumber"].ToString());
					listViewItemAdd.SubItems.Add(row["docName"].ToString());
					listViewItemAdd.SubItems.Add(Conversion.StringToMoney(Conversion.StringToDouble(row["docTotal"].ToString()).ToString()));
					
					if(row["docName"].ToString() == "Заказ") listViewItemAdd.SubItems.Add(row["docCounteragent"].ToString());
					else if(row["docName"].ToString() == "План закупок") listViewItemAdd.SubItems.Add("");
					
					listViewItemAdd.SubItems.Add(row["docAutor"].ToString());
					listViewItemAdd.SubItems.Add(row["id"].ToString());
					listView1.Items.Add(listViewItemAdd);
				}
				
				listView1.SelectedIndices.IndexOf(selectTableLine);
			}
		}
		
		void TableRefreshServer()
		{
			sqlServer = new SqlServer();
			sqlServer.dataSet.Clear();
			
			sqlServer.sqlCommandSelect.CommandText = "SELECT * FROM PurchasePlan WHERE (docDate BETWEEN '" + 
				dateTimePicker1.Text + "' AND '" + 
				dateTimePicker2.Text +
				"' AND (docNumber LIKE '%" + toolStripComboBox1.Text + 
				"%' OR docTotal LIKE '%" + toolStripComboBox1.Text + 
				"%' OR docAutor LIKE '%" + toolStripComboBox1.Text +
				"%')) ORDER BY docDate DESC";
			sqlServer.ExecuteFill("PurchasePlan");
			
			sqlServer.sqlCommandSelect.CommandText = "SELECT * FROM Orders WHERE (docDate BETWEEN '" + 
				dateTimePicker1.Text + "' AND '" + 
				dateTimePicker2.Text + "') " +
				"AND (docNumber LIKE '%" + toolStripComboBox1.Text + 
				"%' OR docSum LIKE '%" + toolStripComboBox1.Text + 
				"%' OR docVat LIKE '%" + toolStripComboBox1.Text + 
				"%' OR docTotal LIKE '%" + toolStripComboBox1.Text + 
				"%' OR docAutor LIKE '%" + toolStripComboBox1.Text +
				"%' OR docCounteragent LIKE '%" + toolStripComboBox1.Text +
				"%') ORDER BY docDate DESC";
			sqlServer.ExecuteFill("Orders");
			
			if(sqlServer.dataSet.Tables.Count > 0){
				listView1.Items.Clear();
				DateTime dt;
				ListViewItem listViewItemAdd;
				
				DataSet dataSet = new DataSet();
				sqlServer.dataSet.Tables[0].DefaultView.Sort = "docDate DESC";
				if(sqlServer.dataSet.Tables.Count > 1) sqlServer.dataSet.Tables[0].Merge(sqlServer.dataSet.Tables[1]);	
				dataSet.Tables.Add(sqlServer.dataSet.Tables[0].DefaultView.ToTable());
				
				foreach(DataRow row in dataSet.Tables[0].Rows)
	    		{
					listViewItemAdd = new ListViewItem();
					dt = new DateTime();
					DateTime.TryParse(row["docDate"].ToString(), out dt);
					listViewItemAdd.SubItems.Add(dt.ToString("dd.MM.yyyy"));
					
					if(row["docName"].ToString() == "План закупок") listViewItemAdd.StateImageIndex = 1;
					else if(row["docName"].ToString() == "Заказ") listViewItemAdd.StateImageIndex = 0;
					
					listViewItemAdd.SubItems.Add(row["docNumber"].ToString());
					listViewItemAdd.SubItems.Add(row["docName"].ToString());
					listViewItemAdd.SubItems.Add(Conversion.StringToMoney(Conversion.StringToDouble(row["docTotal"].ToString()).ToString()));
					
					if(row["docName"].ToString() == "Заказ") listViewItemAdd.SubItems.Add(row["docCounteragent"].ToString());
					else if(row["docName"].ToString() == "План закупок") listViewItemAdd.SubItems.Add("");
					
					listViewItemAdd.SubItems.Add(row["docAutor"].ToString());
					listViewItemAdd.SubItems.Add(row["id"].ToString());
					listView1.Items.Add(listViewItemAdd);
				}
				
				listView1.SelectedIndices.IndexOf(selectTableLine);
			}
		}
		
		void disposeDb()
		{
			if(oleDb != null) oleDb.Dispose();
			if(sqlServer != null) sqlServer.Dispose();
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */
		void FormFullJournalLoad(object sender, EventArgs e)
		{
			getPeriod();
			TableRefresh(); // Загрузка данных из базы данных
			Utilits.Console.Log(this.Text + ": открыт");
		}
		void FormFullJournalFormClosed(object sender, FormClosedEventArgs e)
		{
			disposeDb();
			DataForms.FClient.messageInStatus("...");
			Utilits.Console.Log(this.Text + ": закрыт");
			Dispose();
			DataForms.FFullJournal = null;
		}
		void FormFullJournalActivated(object sender, EventArgs e)
		{
			DataForms.FClient.messageInStatus(this.Text);
		}
	}
}
