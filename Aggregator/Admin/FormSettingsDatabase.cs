/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 25.02.2017
 * Time: 11:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Data;

namespace Aggregator.Admin
{
	/// <summary>
	/// Description of Settings.
	/// </summary>
	public partial class FormSettingsDatabase : Form
	{
		public FormSettingsDatabase()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void FormSettingsLoad(object sender, EventArgs e)
		{
			typeConnectionСomboBox.Items.Add(DataConstants.CONNETION_LOCAL);
			typeConnectionСomboBox.Items.Add(DataConstants.CONNETION_SERVER);
			typeConnectionСomboBox.Text = DataConfig.typeConnection;
			
			typeDatabaseСomboBox.Items.Add(DataConstants.TYPE_OLEDB);
			typeDatabaseСomboBox.Items.Add(DataConstants.TYPE_MSSQL);
			typeDatabaseСomboBox.Text = DataConfig.typeDatabase;
			
			localDatabaseTextBox.Text = DataConfig.localDatabase;
			serverTextBox.Text = DataConfig.server;
			serverUserTextBox.Text = DataConfig.serverUser;
			serverDatabaseTextBox.Text = DataConfig.serverDatabase;
		}
		void ButtonCloseClick(object sender, EventArgs e)
		{
			Close();
		}
		void FormSettingsFormClosed(object sender, FormClosedEventArgs e)
		{
			Dispose();
			DataForms.FSettingsDatabase = null;
		}
	}
}
