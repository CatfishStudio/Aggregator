/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 09.04.2017
 * Время: 13:43
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Data;
using Aggregator.Database.Local;
using Aggregator.Database.Server;
using Aggregator.Utilits;

namespace Aggregator.Client.Documents.Order
{
	/// <summary>
	/// Description of FormOrderJournal.
	/// </summary>
	public partial class FormOrderJournal : Form
	{
		public FormOrderJournal()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
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
					oleDb.Error();
					Utilits.Console.Log("[ОШИБКА]: " + ex.Message.ToString(), false, true);
				}
			} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				try{
					TableRefreshServer();
				}catch(Exception ex){
					sqlServer.Error();
					Utilits.Console.Log("[ОШИБКА]: " + ex.Message.ToString(), false, true);
				}
			}
		}
		
		void TableRefreshLocal()
		{
			oleDb = new OleDb(DataConfig.localDatabase);
			oleDb.dataSet.Clear();
			oleDb.dataSet.DataSetName = "Orders";
			
			//Дата в формате: BETWEEN #22/03/2017# AND #22/03/2017#
			oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Orders WHERE (docDate BETWEEN #" + 
				dateTimePicker1.Value.ToString("dd.MM.yyyy").Replace(".", "/") + "# AND #" + 
				dateTimePicker2.Value.ToString("dd.MM.yyyy").Replace(".", "/") + "#) " +
				"AND (docNumber LIKE '%" + comboBox1.Text + 
				"%' OR docSum LIKE '%" + comboBox1.Text + 
				"%' OR docVat LIKE '%" + comboBox1.Text + 
				"%' OR docTotal LIKE '%" + comboBox1.Text + 
				"%' OR docAutor LIKE '%" + comboBox1.Text +
				"%' OR docCounteragent LIKE '%" + comboBox1.Text +
				"%') ORDER BY docDate DESC";
			
			if(oleDb.ExecuteFill("Orders")){
				listView1.Items.Clear();
				DateTime dt;
				foreach(DataRow rowElement in oleDb.dataSet.Tables[0].Rows)
	    		{
					ListViewItem ListViewItem_add = new ListViewItem();
					dt = new DateTime();
					DateTime.TryParse(rowElement["docDate"].ToString(), out dt);
					ListViewItem_add.SubItems.Add(dt.ToString("dd.MM.yyyy"));
					ListViewItem_add.StateImageIndex = 0;
					ListViewItem_add.SubItems.Add(rowElement["docNumber"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["docName"].ToString());
					ListViewItem_add.SubItems.Add(Conversion.StringToMoney(Conversion.StringToDouble(rowElement["docSum"].ToString()).ToString()));
					ListViewItem_add.SubItems.Add(Conversion.StringToMoney(Conversion.StringToDouble(rowElement["docVat"].ToString()).ToString()));
					ListViewItem_add.SubItems.Add(Conversion.StringToMoney(Conversion.StringToDouble(rowElement["docTotal"].ToString()).ToString()));
					ListViewItem_add.SubItems.Add(rowElement["docCounteragent"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["docAutor"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["docPurchasePlan"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["id"].ToString());
					listView1.Items.Add(ListViewItem_add);
				}
			}else{
				Utilits.Console.Log("[ОШИБКА] Ошибка выполнения запроса к таблице Заказы.");
				oleDb.Error();
				return;
			}
			// ВЫБОР: выдиляем ранее выбранный элемент.
			listView1.SelectedIndices.IndexOf(selectTableLine);
		}
		
		void TableRefreshServer()
		{
			sqlServer = new SqlServer();
			sqlServer.dataSet.Clear();
			sqlServer.dataSet.DataSetName = "Orders";
			sqlServer.sqlCommandSelect.CommandText = "SELECT * FROM Orders WHERE (docDate BETWEEN '" + 
				dateTimePicker1.Text + "' AND '" + 
				dateTimePicker2.Text + "') " +
				"AND (docNumber LIKE '%" + comboBox1.Text + 
				"%' OR docSum LIKE '%" + comboBox1.Text + 
				"%' OR docVat LIKE '%" + comboBox1.Text + 
				"%' OR docTotal LIKE '%" + comboBox1.Text + 
				"%' OR docAutor LIKE '%" + comboBox1.Text +
				"%' OR docCounteragent LIKE '%" + comboBox1.Text +
				"%') ORDER BY docDate DESC";
			
			if(sqlServer.ExecuteFill("Orders")){
				listView1.Items.Clear();
				DateTime dt;
				foreach(DataRow rowElement in sqlServer.dataSet.Tables[0].Rows)
	    		{
					ListViewItem ListViewItem_add = new ListViewItem();
					dt = new DateTime();
					DateTime.TryParse(rowElement["docDate"].ToString(), out dt);
					ListViewItem_add.SubItems.Add(dt.ToString("dd.MM.yyyy"));
					ListViewItem_add.StateImageIndex = 0;
					ListViewItem_add.SubItems.Add(rowElement["docNumber"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["docName"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["docSum"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["docVat"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["docTotal"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["docCounteragent"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["docAutor"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["docPurchasePlan"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["id"].ToString());
					listView1.Items.Add(ListViewItem_add);
				}
			}else{
				Utilits.Console.Log("[ОШИБКА] Ошибка выполнения запроса к таблице Заказы.");
				sqlServer.Error();
				return;
			}
			// ВЫБОР: выдиляем ранее выбранный элемент.
			listView1.SelectedIndices.IndexOf(selectTableLine);
		}
		
		void search()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL && DataConfig.typeDatabase == DataConstants.TYPE_OLEDB) {
				// OLEDB
				try{
					TableRefreshLocal();
					Utilits.Console.Log("Журнал Заказы: поиск завершен.");
				}catch(Exception ex){
					oleDb.Error();
					Utilits.Console.Log("[ОШИБКА]: " + ex.Message.ToString(), false, true);
				}
			} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER && DataConfig.typeDatabase == DataConstants.TYPE_MSSQL){
				// MSSQL SERVER
				try{
					TableRefreshServer();
					Utilits.Console.Log("Журнал Заказы: поиск завершен.");
				}catch(Exception ex){
					sqlServer.Error();
					Utilits.Console.Log("[ОШИБКА]: " + ex.Message.ToString(), false, true);
				}
			}
			if(comboBox1.Text != "")  comboBox1.Items.Add(comboBox1.Text);
		}
		
		void addFile()
		{
			FormOrderDoc FOrderDoc = new FormOrderDoc();
			FOrderDoc.MdiParent = DataForms.FClient;
			FOrderDoc.ID = null;
			FOrderDoc.Show();
		}
		
		void editFile()
		{
			if(listView1.SelectedIndices.Count > 0){
				FormOrderDoc FOrderDoc = new FormOrderDoc();
				FOrderDoc.MdiParent = DataForms.FClient;
				FOrderDoc.ID = listView1.Items[listView1.SelectedIndices[0]].SubItems[10].Text;
				FOrderDoc.ParentDoc = listView1.Items[listView1.SelectedIndices[0]].SubItems[9].Text;
				FOrderDoc.Show();
			}
		}
		
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */
		void FormOrderJournalLoad(object sender, EventArgs e)
		{
			getPeriod();
			TableRefresh(); // Загрузка данных из базы данных
			Utilits.Console.Log("Журнал закупок: отркыт.");
		}
		void FormOrderJournalFormClosed(object sender, FormClosedEventArgs e)
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL && oleDb != null) oleDb.Dispose();
			if(DataConfig.typeConnection == DataConstants.CONNETION_SERVER && sqlServer != null) sqlServer.Dispose();
			Dispose();
			DataForms.FOrderJournal = null;
			DataForms.FClient.messageInStatus("...");
			Utilits.Console.Log("Журнал заказов: закрыт.");
		}
		void AddButtonClick(object sender, EventArgs e)
		{
			addFile();
		}
		void EditButtonClick(object sender, EventArgs e)
		{
			editFile();
		}
		void DeleteButtonClick(object sender, EventArgs e)
		{
	
		}
	}
}
