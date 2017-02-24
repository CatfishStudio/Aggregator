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
		void FormClientLoad(object sender, EventArgs e)
		{
	
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
		void ПанельИнструментовToolStripMenuItemClick(object sender, EventArgs e)
		{
	
		}
		
		void consoleVisible()
		{
			if(consolePanel.Visible) consolePanel.Visible = false;
			else consolePanel.Visible = true;
		}
		void КонсольСообщенийToolStripMenuItemClick(object sender, EventArgs e)
		{
			consoleVisible();
		}
		void ToolStripButton1Click(object sender, EventArgs e)
		{
			consoleVisible();
		}
	}
}
