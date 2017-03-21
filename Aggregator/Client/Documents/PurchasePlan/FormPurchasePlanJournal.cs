﻿/*
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
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Data;
using Aggregator.Database.Local;
using Aggregator.Database.Server;

namespace Aggregator.Client.Documents.PurchasePlan
{
	/// <summary>
	/// Description of FormPurchasePlanJournal.
	/// </summary>
	public partial class FormPurchasePlanJournal : Form
	{
		public FormPurchasePlanJournal()
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
			oleDb.dataSet.DataSetName = "PurchasePlan";
			oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM PurchasePlan WHERE (docDate BETWEEN '" + 
				dateTimePicker1.Text + "' AND '" + dateTimePicker2.Text + 
				"' AND (docNumber LIKE '%" + comboBox1.Text + "%' OR docTotal LIKE '%" + comboBox1.Text + "%' OR docAutor LIKE '%" + comboBox1.Text + 
				"%')) ORDER BY docDate ASC";
			
			if(oleDb.ExecuteFill("PurchasePlan")){
				listView1.Items.Clear();
				foreach(DataRow rowElement in oleDb.dataSet.Tables[0].Rows)
	    		{
					ListViewItem ListViewItem_add = new ListViewItem();
					ListViewItem_add.SubItems.Add(rowElement["docDate"].ToString());
					ListViewItem_add.StateImageIndex = 0;
					ListViewItem_add.SubItems.Add(rowElement["docNumber"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["docName"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["docTotal"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["docAutor"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["id"].ToString());
					listView1.Items.Add(ListViewItem_add);
				}
			}else{
				Utilits.Console.Log("[ОШИБКА] Ошибка выполнения запроса к таблице План закупок.");
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
			sqlServer.dataSet.DataSetName = "PurchasePlan";
			sqlServer.sqlCommandSelect.CommandText = "SELECT * FROM PurchasePlan WHERE (docDate BETWEEN '" + 
				dateTimePicker1.Text + "' AND '" + dateTimePicker2.Text + 
				"' AND (docNumber LIKE '%" + comboBox1.Text + "%' OR docTotal LIKE '%" + comboBox1.Text + "%' OR docAutor LIKE '%" + comboBox1.Text + 
				"%')) ORDER BY docDate ASC";
			
			if(sqlServer.ExecuteFill("PurchasePlan")){
				listView1.Items.Clear();
				foreach(DataRow rowElement in sqlServer.dataSet.Tables[0].Rows)
	    		{
					ListViewItem ListViewItem_add = new ListViewItem();
					ListViewItem_add.SubItems.Add(rowElement["docDate"].ToString());
					ListViewItem_add.StateImageIndex = 0;
					ListViewItem_add.SubItems.Add(rowElement["docNumber"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["docName"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["docTotal"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["docAutor"].ToString());
					ListViewItem_add.SubItems.Add(rowElement["id"].ToString());
					listView1.Items.Add(ListViewItem_add);
				}
			}else{
				Utilits.Console.Log("[ОШИБКА] Ошибка выполнения запроса к таблице План закупок.");
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
					Utilits.Console.Log("Журнал План закупок: поиск завершен.");
				}catch(Exception ex){
					oleDb.Error();
					Utilits.Console.Log("[ОШИБКА]: " + ex.Message.ToString(), false, true);
				}
			} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER && DataConfig.typeDatabase == DataConstants.TYPE_MSSQL){
				// MSSQL SERVER
				try{
					TableRefreshServer();
					Utilits.Console.Log("Журнал План закупок: поиск завершен.");
				}catch(Exception ex){
					sqlServer.Error();
					Utilits.Console.Log("[ОШИБКА]: " + ex.Message.ToString(), false, true);
				}
			}
			comboBox1.Items.Add(comboBox1.Text);
		}
		
		void addFile()
		{
			FormPurchasePlanDoc FPurchasePlanDoc = new FormPurchasePlanDoc();
			FPurchasePlanDoc.MdiParent = DataForms.FClient;
			FPurchasePlanDoc.ID = null;
			FPurchasePlanDoc.Show();
		}
		
		void editFile()
		{
			if(listView1.SelectedIndices.Count > 0){
				FormPurchasePlanDoc FPurchasePlanDoc = new FormPurchasePlanDoc();
				FPurchasePlanDoc.MdiParent = DataForms.FClient;
				FPurchasePlanDoc.ID = listView1.Items[listView1.SelectedIndices[0]].SubItems[6].Text.ToString();
				FPurchasePlanDoc.Show();
			}
		}
		
		void deleteFile()
		{
			if(listView1.SelectedIndices.Count > 0){
				/*
				if(MessageBox.Show("Удалить безвозвратно '" + fileName + "'?"  ,"Вопрос:", MessageBoxButtons.YesNo) == DialogResult.Yes){
					if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL) {
						// OLEDB
						
					} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
						// MSSQL SERVER
						
					}
				}
				*/					
			}
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */	
		void FormPurchasePlanJournalLoad(object sender, EventArgs e)
		{
			getPeriod();
			TableRefresh(); // Загрузка данных из базы данных
			Utilits.Console.Log("Журнал закупок: отркыт.");
		}
		void FormPurchasePlanJournalFormClosed(object sender, FormClosedEventArgs e)
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL && oleDb != null) oleDb.Dispose();
			if(DataConfig.typeConnection == DataConstants.CONNETION_SERVER && sqlServer != null) sqlServer.Dispose();
			Dispose();
			DataForms.FPurchasePlanJournal = null;
			DataForms.FClient.messageInStatus("...");
			Utilits.Console.Log("Журнал закупок: закрыт.");
		}
		void FindButtonClick(object sender, EventArgs e)
		{
			search();
		}
		void RefreshButtonClick(object sender, EventArgs e)
		{
			TableRefresh();
		}
		void DateButtonClick(object sender, EventArgs e)
		{
			TableRefresh();
		}
		void AddButtonClick(object sender, EventArgs e)
		{
			if(DataConfig.userPermissions == "guest"){
				MessageBox.Show("У вас недостаточно прав чтобы выполнить данное действие.", "Сообщение");
				return;
			}
			addFile();
		}
		void EditButtonClick(object sender, EventArgs e)
		{
			editFile();
		}
		void DeleteButtonClick(object sender, EventArgs e)
		{
			if(DataConfig.userPermissions == "guest" || DataConfig.userPermissions == "user"){
				MessageBox.Show("У вас недостаточно прав чтобы выполнить данное действие.", "Сообщение");
				return;
			}
			deleteFile();
		}
		
	}
}