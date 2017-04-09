/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 23.02.2017
 * Time: 11:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Client.OpenFiles;
using Aggregator.Data;
using Aggregator.Database.Constants;
using Aggregator.Database.Local;
using Aggregator.Admin;
using Aggregator.Database.Server;
using Aggregator.User;
using Aggregator.Client.Settings;
using Aggregator.Client.Directories;
using Aggregator.Client.Documents.PurchasePlan;
using Aggregator.Client.Documents.Order;

namespace Aggregator.Client
{
	/// <summary>
	/// Description of FormClient.
	/// </summary>
	public partial class FormClient : Form
	{
		public FormClient()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: ПРОЦЕДУР И ФУНКЦИЙ
		 * =================================================================================================
		 */	
		HistoryRefreshOleDb historyRefreshOleDb;
		HistoryRefreshSqlServer historyRefreshSqlServer;
		
		/* Применить права пользователя */
		void applyPermissions()
		{
			if(DataConfig.userPermissions == "admin"){
				администраторToolStripMenuItem.Visible = true;
			}else if(DataConfig.userPermissions == "operator"){
				администраторToolStripMenuItem.Visible = false;
			}else if(DataConfig.userPermissions == "user"){
				администраторToolStripMenuItem.Visible = false;
			}else if(DataConfig.userPermissions == "guest"){
				администраторToolStripMenuItem.Visible = false;
			}
		}
		
		/* Открыть окно пользователей */
		void usersShow()
		{
			if(DataForms.FUsers == null){
				DataForms.FUsers = new FormUsers();
				DataForms.FUsers.MdiParent = DataForms.FClient;
				DataForms.FUsers.Show();
			}
		}
		
		/* Открыть окно настройки подключения к базе данных */
		void databaseSettingsShow()
		{
			if(DataForms.FSettingsDatabase == null){
				DataForms.FSettingsDatabase = new FormSettingsDatabase();
				DataForms.FSettingsDatabase.MdiParent = DataForms.FClient;
				DataForms.FSettingsDatabase.Show();
			}
		}
		
		/* Открыть окно настроек */
		void settingsShow()
		{
			if(DataForms.FSettings == null) {
				DataForms.FSettings = new FormSettings();
				DataForms.FSettings.MdiParent = DataForms.FClient;
				DataForms.FSettings.Show();
			}
		}
		
		/* Отобразить консоль */
		void consoleVisible()
		{
			if(consolePanel.Visible) consolePanel.Visible = false;
			else consolePanel.Visible = true;
		}
		
		/* Открыть консоль запросов */
		void consoleQuery()
		{
			if(DataForms.FConsoleQuery == null){
				DataForms.FConsoleQuery = new FormConsoleQuery();
				DataForms.FConsoleQuery.MdiParent = DataForms.FClient;
				DataForms.FConsoleQuery.Show();
			}
		}
		
		/* Открыть Excel файл */
		void openFileExcel()
		{
			FormOpenExcel FOpenExcel;
			if(openFileDialog1.ShowDialog() == DialogResult.OK){
				FOpenExcel = new FormOpenExcel();
				FOpenExcel.FileName = openFileDialog1.FileName;
				FOpenExcel.MdiParent = DataForms.FClient;
				FOpenExcel.Show();
			}
		}
		
		/* О программе */
		void about()
		{
			MessageBox.Show("Программа: Catfish Aggregator" + System.Environment.NewLine +
			                "Лиценция: Freeware" + System.Environment.NewLine +
			                "Версия: 1.0.0" + System.Environment.NewLine +
			                "Дата: 01.03.2017" + System.Environment.NewLine +
			                "Автор: Сомов Евгений Павлович (Catfish Studio)" + System.Environment.NewLine +
			                "Сайт: http://somov.hol.es/", "О программе");
		}
		
		/* Открыть окно констант */
		void constantsShow()
		{
			if(DataForms.FConstants == null) {
				DataForms.FConstants = new FormConstants();
				DataForms.FConstants.MdiParent = DataForms.FClient;
				DataForms.FConstants.Show();
			}
		}
		
		/* Открыть окно контрагентов */
		void counteragentsShow()
		{
			if(DataForms.FCounteragents == null) {
				DataForms.FCounteragents = new FormCounteragents();
				DataForms.FCounteragents.MdiParent = DataForms.FClient;
				DataForms.FCounteragents.Show();
			}
		}
		
		/* Открыть окно номенклатуры */
		void nomenclatureShow()
		{
			if(DataForms.FNomenclature == null){
				DataForms.FNomenclature = new FormNomenclature();
				DataForms.FNomenclature.MdiParent = DataForms.FClient;
				DataForms.FNomenclature.Show();
			}
		}
		
		void unitsShow()
		{
			if(DataForms.FUnits == null){
				DataForms.FUnits = new FormUnits();
				DataForms.FUnits.MdiParent = DataForms.FClient;
				DataForms.FUnits.Show();
			}
		}
		
		void purchasePlanJournalShow()
		{
			if(DataForms.FPurchasePlanJournal == null){
				DataForms.FPurchasePlanJournal = new FormPurchasePlanJournal();
				DataForms.FPurchasePlanJournal.MdiParent = DataForms.FClient;
				DataForms.FPurchasePlanJournal.Show();
			}
		}
		
		void purchasePlanDoclShow()
		{
			FormPurchasePlanDoc FPurchasePlanDoc = new FormPurchasePlanDoc();
			FPurchasePlanDoc.MdiParent = DataForms.FClient;
			FPurchasePlanDoc.ID = null;
			FPurchasePlanDoc.Show();
		}
		
		void orderJournalShow()
		{
			if(DataForms.FOrderJournal == null){
				DataForms.FOrderJournal = new FormOrderJournal();
				DataForms.FOrderJournal.MdiParent = DataForms.FClient;
				DataForms.FOrderJournal.Show();
			}
		}
		
		/* Сообщение в статусе */
		public void messageInStatus(String message) {
			toolStripStatusLabel2.Text = message;
		}
		
		/* Включить/отключить работу авто обновления таблиц (только для OleDb)*/
		public void autoUpdateOn()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				toolStripStatusLabel1.ImageIndex = 1;
				toolStripStatusLabel1.Visible = true;
				timer1.Start();
			}else{
				toolStripStatusLabel1.Visible = false;
				timer1.Stop();
			}
		}
		
		public void autoUpdateOff()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				toolStripStatusLabel1.ImageIndex = 2;
				toolStripStatusLabel1.Visible = true;
				timer1.Stop();
			}else{
				toolStripStatusLabel1.Visible = false;
				timer1.Stop();
			}
		}
		
		/* Обновть данные в Истории */
		public void updateHistory(String tableName)
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){	// OLEDB
				if(DataConfig.autoUpdate && historyRefreshOleDb != null) historyRefreshOleDb.update(tableName);
				if(!DataConfig.autoUpdate && historyRefreshOleDb != null) historyRefreshOleDb.refresh(tableName, tableName);
			}else{	// MSSQL
				//..
			}
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */		
		
		void FormClientLoad(object sender, EventArgs e)
		{
			statusStrip1.ImageList = imageList1;
			
			ReadingConstants readingConstants = new ReadingConstants();
			readingConstants.read();
			readingConstants.Dispose();
			
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){ // OLEDB
				historyRefreshOleDb = new HistoryRefreshOleDb();
				if(DataConfig.autoUpdate) autoUpdateOn();
				else autoUpdateOff();
			}else if(DataConfig.typeConnection == DataConstants.CONNETION_SERVER){	// MSSQL
				toolStripStatusLabel1.Visible = false;
				historyRefreshSqlServer = new HistoryRefreshSqlServer();
				historyRefreshSqlServer.MonitoringStart("database");
			}
			applyPermissions();
			Utilits.Console.Log("Программа успешно запущена!");
		}
		void FormClientFormClosing(object sender, FormClosingEventArgs e)
		{
			if(MessageBox.Show("Вы хотите выйти из программы?","Вопрос:", MessageBoxButtons.YesNo) == DialogResult.Yes){
				e.Cancel = false;
			}else{
				e.Cancel = true;
			}
		}
		void Timer1Tick(object sender, EventArgs e)
		{
			if(toolStripStatusLabel1.ImageIndex == 1) toolStripStatusLabel1.ImageIndex = 0;
			else toolStripStatusLabel1.ImageIndex = 1;
			historyRefreshOleDb.check();
		}
		void FormClientFormClosed(object sender, FormClosedEventArgs e)
		{
			timer1.Stop();
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL && historyRefreshOleDb != null) {
				historyRefreshOleDb.Dispose();
			}else if(DataConfig.typeConnection == DataConstants.CONNETION_SERVER && historyRefreshSqlServer != null){
				historyRefreshSqlServer.MonitoringStop();
			}			
			Dispose();
			Application.Exit();
		}
		void КонсольСообщенийToolStripMenuItemClick(object sender, EventArgs e)
		{
			consoleVisible();
		}
		void ToolStripButton1Click(object sender, EventArgs e)
		{
			consoleVisible();
		}
		void БазаДанныхToolStripMenuItemClick(object sender, EventArgs e)
		{
			databaseSettingsShow();
		}
		void КонсольЗапросовToolStripMenuItemClick(object sender, EventArgs e)
		{
			consoleQuery();
		}
		void ПользователиToolStripMenuItemClick(object sender, EventArgs e)
		{
			usersShow();
		}
		void НастройкиToolStripMenuItemClick(object sender, EventArgs e)
		{
			settingsShow();
		}
		void ExcelФайлФормат2007ToolStripMenuItemClick(object sender, EventArgs e)
		{
			openFileExcel();
		}
		void ВыходToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}
		void ОПрограммеToolStripMenuItemClick(object sender, EventArgs e)
		{
			about();
		}
		void КонстантыToolStripMenuItemClick(object sender, EventArgs e)
		{
			constantsShow();
		}
		void КонтрагентыToolStripMenuItemClick(object sender, EventArgs e)
		{
			counteragentsShow();
		}
		void ПанельИнструментовToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(панельИнструментовToolStripMenuItem.Checked){
				панельИнструментовToolStripMenuItem.Checked = false;
				toolStrip1.Visible = false;
			}else{
				панельИнструментовToolStripMenuItem.Checked = true;
				toolStrip1.Visible = true;
			}
		}
		void ToolStripButton2Click(object sender, EventArgs e)
		{
			constantsShow();
		}
		void ToolStripButton3Click(object sender, EventArgs e)
		{
			counteragentsShow();
		}
		void КалькуляторToolStripMenuItemClick(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("calc.exe");
		}
		void БлокнотToolStripMenuItemClick(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("notepad.exe");
		}
		void WordPadToolStripMenuItemClick(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("wordpad.exe");
		}
		void PaintToolStripMenuItemClick(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("mspaint.exe");
		}
		void ExplorerToolStripMenuItemClick(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("explorer.exe");
		}
		void КоманданяСтрокаToolStripMenuItemClick(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("cmd.exe");
		}
		void НоменклатураToolStripMenuItemClick(object sender, EventArgs e)
		{
			nomenclatureShow();
		}
		void ToolStripButton4Click(object sender, EventArgs e)
		{
			nomenclatureShow();
		}
		void ЕдинициИзмеренияToolStripMenuItemClick(object sender, EventArgs e)
		{
			unitsShow();
		}
		void ToolStripButton5Click(object sender, EventArgs e)
		{
			unitsShow();
		}
		void ПланЗакупокToolStripMenuItemClick(object sender, EventArgs e)
		{
			purchasePlanDoclShow();
		}
		void ЖурналЗакупокToolStripMenuItemClick(object sender, EventArgs e)
		{
			purchasePlanJournalShow();
		}
		void ToolStripButton10Click(object sender, EventArgs e)
		{
			purchasePlanJournalShow();
		}
		void ToolStripButton6Click(object sender, EventArgs e)
		{
			purchasePlanDoclShow();
		}
		void ЖурналЗаказовToolStripMenuItemClick(object sender, EventArgs e)
		{
			orderJournalShow();
		}
		void ToolStripButton9Click(object sender, EventArgs e)
		{
			orderJournalShow();
		}
		
	}
}
