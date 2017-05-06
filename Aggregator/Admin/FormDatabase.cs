/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 06.05.2017
 * Время: 10:19
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using Aggregator.Data;
using Aggregator.Database.Config;

namespace Aggregator.Admin
{
	/// <summary>
	/// Description of FormDatabase.
	/// </summary>
	public partial class FormDatabase : Form
	{
		public FormDatabase()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void FormDatabaseLoad(object sender, EventArgs e)
		{
			ReadingConfig.ReadDatabaseSettings();
			
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				localRadioButton.Checked = true;
			}else if(DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				serverRadioButton.Checked = true;
			}
			
			localDatabaseTextBox.Text = DataConfig.localDatabase;
			serverTextBox.Text = DataConfig.serverConnection;
			Utilits.Console.Log("Настройки базы данных: открыт.");
		}
		void FormDatabaseFormClosed(object sender, FormClosedEventArgs e)
		{
			Dispose();
			DataForms.FClient.messageInStatus("...");
			Utilits.Console.Log("Настройки базы данных: закрыт.");
			DataForms.FDatabase = null;
		}
		void FormDatabaseActivated(object sender, EventArgs e)
		{
			DataForms.FClient.messageInStatus(this.Text);
		}
		void ButtonCloseClick(object sender, EventArgs e)
		{
			Close();
		}
		void ButtonSaveClick(object sender, EventArgs e)
		{
			if(DataConfig.typeConnection != DataConstants.CONNETION_LOCAL && DataConfig.typeConnection != DataConstants.CONNETION_SERVER){
				MessageBox.Show("Указан не верный тип подключения!", "Сообщение:");
				return;
			}
			if(localRadioButton.Checked){
				DataConfig.typeConnection = DataConstants.CONNETION_LOCAL;
				DataConfig.typeDatabase = DataConstants.TYPE_OLEDB;
			}else if(serverRadioButton.Checked){
				DataConfig.typeConnection = DataConstants.CONNETION_SERVER;
				DataConfig.typeDatabase = DataConstants.TYPE_MSSQL;
			}
			DataConfig.localDatabase = localDatabaseTextBox.Text;
			DataConfig.serverConnection = serverTextBox.Text;
			SavingConfig.SaveDatabaseSettings();
			Close();
			MessageBox.Show("Чтобы изменения вступили в силу, необходимо перезапустить программу.", "Сообщение:");
			Application.Exit();
		}
		void Button1Click(object sender, EventArgs e)
		{
			if(openFileDialog1.ShowDialog() == DialogResult.OK){
				localDatabaseTextBox.Text = openFileDialog1.FileName;
			}
		}
		void Button4Click(object sender, EventArgs e)
		{
			OleDbConnection conn = null;
			try{
				conn = new OleDbConnection(DataConfig.oledbConnectLineBegin + localDatabaseTextBox.Text);
				conn.Open();
				MessageBox.Show("Состояние соединения: " + conn.State.ToString(), "Результат проверки");
				conn.Close();
			}catch(Exception ex){
				conn.Close();
				MessageBox.Show("Ошибка: " + ex.Message, "Результат проверки");
			}
		}
		void Button2Click(object sender, EventArgs e)
		{
			FormCreateAccessDB FCreateAccessDB = new FormCreateAccessDB();
			FCreateAccessDB.MdiParent = DataForms.FClient;
			FCreateAccessDB.Show();
		}
		void TestConnectButtonClick(object sender, EventArgs e)
		{
			SqlConnection conn = null;
			try{
				conn = new SqlConnection(serverTextBox.Text);
				conn.Open();
				MessageBox.Show("Состояние соединения: " + conn.State.ToString(), "Результат проверки");
				conn.Close();
			}catch(Exception ex){
				conn.Close();
				MessageBox.Show("Ошибка: " + ex.Message, "Результат проверки");
			}
		}
		void Button3Click(object sender, EventArgs e)
		{
	
		}
		void RadioButtonsCheckedChanged(object sender, EventArgs e)
		{
			if(localRadioButton.Checked){
				groupBox1.Enabled = true;
				groupBox2.Enabled = false;
			}else if(serverRadioButton.Checked){
				groupBox1.Enabled = false;
				groupBox2.Enabled = true;
			}
		}
		
		
	}
}
