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
using Aggregator.Data;
using Aggregator.Admin;
using Aggregator.User;
using Aggregator.Client.Settings;

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
		DataServerUpdate dataServerUpdate;
		
		void usersShow()
		{
			if(DataForms.FUsers == null){
				DataForms.FUsers = new FormUsers();
				DataForms.FUsers.MdiParent = DataForms.FClient;
				DataForms.FUsers.Show();
			}
		}
		
		void databaseSettingsShow()
		{
			if(DataForms.FSettingsDatabase == null){
				DataForms.FSettingsDatabase = new FormSettingsDatabase();
				DataForms.FSettingsDatabase.MdiParent = DataForms.FClient;
				DataForms.FSettingsDatabase.Show();
			}
		}
		
		void settingsShow()
		{
			if(DataForms.FSettings == null) {
				DataForms.FSettings = new FormSettings();
				DataForms.FSettings.MdiParent = DataForms.FClient;
				DataForms.FSettings.Show();
			}
		}
		
		void consoleVisible()
		{
			if(consolePanel.Visible) consolePanel.Visible = false;
			else consolePanel.Visible = true;
		}
		
		void consoleQuery()
		{
			if(DataForms.FConsoleQuery == null){
				DataForms.FConsoleQuery = new FormConsoleQuery();
				DataForms.FConsoleQuery.MdiParent = DataForms.FClient;
				DataForms.FConsoleQuery.Show();
			}
		}
		
		public void messageInStatus(String message) {
			toolStripStatusLabel2.Text = message;
		}
		
		public void autoUpdateOn()
		{
			dataServerUpdate = new DataServerUpdate();
			toolStripStatusLabel1.ImageIndex = 1;
			timer1.Start();
		}
		
		public void autoUpdateOff()
		{
			timer1.Stop();
			toolStripStatusLabel1.ImageIndex = 2;
			if(dataServerUpdate != null) {
				dataServerUpdate.Dispose();
				dataServerUpdate = null;
			}
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */		
		
		void FormClientLoad(object sender, EventArgs e)
		{
			statusStrip1.ImageList = imageList1;
			if(DataConfig.autoUpdate == true) autoUpdateOn();
			else autoUpdateOff();
			Utilits.Console.Log("Программа успешно загрущена!");
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
			dataServerUpdate.checkUpdate();
		}
		void FormClientFormClosed(object sender, FormClosedEventArgs e)
		{
			timer1.Stop();
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
		
	}
}
