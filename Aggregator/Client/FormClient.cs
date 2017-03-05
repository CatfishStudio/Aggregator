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
using Aggregator.Database.Local;
using Aggregator.Admin;
using Aggregator.User;
using Aggregator.Client.Settings;
using Aggregator.Client.Directories;

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
		AutoUpdateLocalDatabase autoUpdateLocalDatabase;
		
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
				константыToolStripMenuItem.Visible = false;
				toolStripSeparator3.Visible = false;
				документыToolStripMenuItem.Visible = false;
				настройкиToolStripMenuItem.Visible = false;
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
		
		/* Открыть XLS файл */
		void openFileExcel(String fileFormat)
		{
			FormOpenExcel FOpenExcel;
			if(fileFormat == "xls"){
				if(openFileDialogXLS.ShowDialog() == DialogResult.OK){
					FOpenExcel = new FormOpenExcel();
					FOpenExcel.FileName = openFileDialogXLS.FileName;
					FOpenExcel.FileFormat = fileFormat;
					FOpenExcel.MdiParent = DataForms.FClient;
					FOpenExcel.Show();
				}
			}else if(fileFormat == "xlsx"){
				if(openFileDialogXLSX.ShowDialog() == DialogResult.OK){
					FOpenExcel = new FormOpenExcel();
					FOpenExcel.FileName = openFileDialogXLSX.FileName;
					FOpenExcel.FileFormat = fileFormat;
					FOpenExcel.MdiParent = DataForms.FClient;
					FOpenExcel.Show();
				}
			}
		}
		
		/* О программе */
		void about()
		{
			MessageBox.Show("Программа: Catfish Aggregator" + System.Environment.NewLine +
			                "Лиценция: Freeware" + System.Environment.NewLine +
			                "Версия: 1.0" + System.Environment.NewLine +
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
		
		/* Сообщение в статусе */
		public void messageInStatus(String message) {
			toolStripStatusLabel2.Text = message;
		}
		
		/* Включить/отключить работу авто обновления таблиц */
		public void autoUpdateOn()
		{
			toolStripStatusLabel1.ImageIndex = 1;
			timer1.Start();
		}
		
		public void autoUpdateOff()
		{
			timer1.Stop();
			toolStripStatusLabel1.ImageIndex = 2;
		}
		
		/* Обновть данные в Истории */
		public void updateHistory(String tableName)
		{
			autoUpdateLocalDatabase.update(tableName);
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */		
		
		void FormClientLoad(object sender, EventArgs e)
		{
			statusStrip1.ImageList = imageList1;
			autoUpdateLocalDatabase = new AutoUpdateLocalDatabase();
			if(DataConfig.autoUpdate) autoUpdateOn();
			else autoUpdateOff();
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
			autoUpdateLocalDatabase.check();
		}
		void FormClientFormClosed(object sender, FormClosedEventArgs e)
		{
			timer1.Stop();
			autoUpdateLocalDatabase.Dispose();
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
		void ExcelФайлФормат972003ToolStripMenuItemClick(object sender, EventArgs e)
		{
			openFileExcel("xls");
		}
		void ExcelФайлФормат2007ToolStripMenuItemClick(object sender, EventArgs e)
		{
			openFileExcel("xlsx");
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
		
	}
}
