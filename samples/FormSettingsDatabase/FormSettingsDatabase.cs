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
using System.Data.SqlClient;
using Aggregator.Data;
using Aggregator.Database.Config;

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
			ReadingConfig.ReadDatabaseSettings();
			typeConnectionСomboBox.Items.Add(DataConstants.CONNETION_LOCAL);
			typeConnectionСomboBox.Items.Add(DataConstants.CONNETION_SERVER);
			
			typeConnectionСomboBox.Text = DataConfig.typeConnection;
			typeDatabaseTextBox.Text = DataConfig.typeDatabase;
			localDatabaseTextBox.Text = DataConfig.localDatabase;
			serverTextBox.Text = DataConfig.serverConnection;
			Utilits.Console.Log("Настройки базы данных: открыт.");
		}
		void ButtonCloseClick(object sender, EventArgs e)
		{
			Close();
		}
		void FormSettingsFormClosed(object sender, FormClosedEventArgs e)
		{
			Dispose();
			DataForms.FClient.messageInStatus("...");
			Utilits.Console.Log("Настройки базы данных: закрыт.");
			DataForms.FSettingsDatabase = null;
		}
		void TypeConnectionСomboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			if(typeConnectionСomboBox.Text == DataConstants.CONNETION_LOCAL){
				typeDatabaseTextBox.Text = DataConstants.TYPE_OLEDB;
				groupBox1.Enabled = true;
				groupBox2.Enabled = false;
			}else if(typeConnectionСomboBox.Text == DataConstants.CONNETION_SERVER){
				typeDatabaseTextBox.Text = DataConstants.TYPE_MSSQL;
				groupBox1.Enabled = false;
				groupBox2.Enabled = true;
			}
		}
		void Button1Click(object sender, EventArgs e)
		{
			if(openFileDialog1.ShowDialog() == DialogResult.OK){
				localDatabaseTextBox.Text = openFileDialog1.FileName;
			}
		}
		void ButtonSaveClick(object sender, EventArgs e)
		{
			if(typeConnectionСomboBox.Text != DataConstants.CONNETION_LOCAL && typeConnectionСomboBox.Text != DataConstants.CONNETION_SERVER){
				MessageBox.Show("Указан не верный тип подключения!", "Сообщение:");
				return;
			}
			DataConfig.localDatabase = localDatabaseTextBox.Text;
			DataConfig.typeConnection = typeConnectionСomboBox.Text;
			DataConfig.typeDatabase = typeDatabaseTextBox.Text;
			DataConfig.serverConnection = serverTextBox.Text;
			SavingConfig.SaveDatabaseSettings();
			Close();
			MessageBox.Show("Чтобы изменения вступили в силу, необходимо перезапустить программу.", "Сообщение:");
			DataForms.FClient.Close();
		}
		void TestConnectButtonClick(object sender, EventArgs e)
		{
			SqlConnection conn = null;
			try{
				conn = new SqlConnection(serverTextBox.Text);
				conn.Open();
				MessageBox.Show("Состояние соединения: " + conn.State.ToString());
				//if(conn.State.ToString() == "open") MessageBox.Show("Проверка прошла успешно!");
				//else MessageBox.Show("Не удалось подключиться к базе данных.");
				conn.Close();
			}catch(Exception ex){
				conn.Close();
				MessageBox.Show(ex.ToString());
			}
		}
		void FormSettingsDatabaseActivated(object sender, EventArgs e)
		{
			DataForms.FClient.messageInStatus(this.Text);
		}
	}
}
