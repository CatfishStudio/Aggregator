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
		
		void settings()
		{
			if(DataForms.FSettingsDatabase == null){
				DataForms.FSettingsDatabase = new FormSettingsDatabase();
				DataForms.FSettingsDatabase.MdiParent = DataForms.FClient;
				DataForms.FSettingsDatabase.Show();
			}
		}
		
		void consoleVisible()
		{
			if(consolePanel.Visible) consolePanel.Visible = false;
			else consolePanel.Visible = true;
		}
		
		void consoleQuery()
		{
			if(DataForms.FQueryDesigner == null){
				DataForms.FQueryDesigner = new FormQueryDesigner();
				DataForms.FQueryDesigner.MdiParent = DataForms.FClient;
				DataForms.FQueryDesigner.Show();
			}
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */		
		
		void FormClientLoad(object sender, EventArgs e)
		{
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
		void FormClientFormClosed(object sender, FormClosedEventArgs e)
		{
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
			settings();
		}
		void КонсольЗапросовToolStripMenuItemClick(object sender, EventArgs e)
		{
			consoleQuery();
		}
	}
}
