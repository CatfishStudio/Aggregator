/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 21.02.2017
 * Time: 15:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Data;
using Aggregator.Database.Local;
using Aggregator.Client;
using Aggregator.Utilits;

namespace Aggregator.User
{
	/// <summary>
	/// Description of FormSelectUser.
	/// </summary>
	public partial class FormCheckUser : Form
	{
		public FormCheckUser()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		private Boolean programClose;	//флаг закрытия приложения
		private OleDb oleDb;
		
		void loadData()
		{
			//Подключение локальной базы данных (список серверов)
			try{
				if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL && DataConfig.typeDatabase == DataConstants.TYPE_OLEDB) {
					// OLEDB
					oleDb = new OleDb(DataConfig.localDatabase);
					oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Users";
					oleDb.ExecuteFill("Users");
				} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER && DataConfig.typeDatabase == DataConstants.TYPE_MSSQL){
					// MSSQL SERVER
					
					
				}
				
			}catch(Exception ex){
				oleDb.Error();
				MessageBox.Show(ex.ToString());
				Application.Exit();
			}
			readData();
		}
		
		void readData()
		{
			foreach(DataRow row in oleDb.dataSet.Tables["Users"].Rows)
			{
				comboBox1.Items.Add(row[1].ToString());
			}
		}
		
		void checkUser()
		{
			//Проверка логина и пароля
			try{
				if(comboBox1.Text == ""){
					MessageBox.Show("Вы не выбрали пользователя!","Сообщение:");
				}else{
					String pass = oleDb.dataSet.Tables["Users"].Rows[comboBox1.SelectedIndex]["pass"].ToString();
					if(textBox1.Text == pass){
						Visible = false;
						DataConfig.userName = oleDb.dataSet.Tables["Users"].Rows[comboBox1.SelectedIndex]["name"].ToString();
						DataConfig.userPass = oleDb.dataSet.Tables["Users"].Rows[comboBox1.SelectedIndex]["pass"].ToString();
						DataConfig.userPermissions = oleDb.dataSet.Tables["Users"].Rows[comboBox1.SelectedIndex]["permissions"].ToString(); 
						DataForms.FMain.Visible = false;
						programClose = false;
						DataForms.FClient = new FormClient();
						DataForms.FClient.Show();
						Utilits.Console.Log("Пользователь успешно авторизовался!");
						Close();
					}else{
						MessageBox.Show("Вы ввели не верный пароль.","Сообщение:");
					}
				}
			}catch{
				MessageBox.Show("Ошибка ввода логина и пароля.","Сообщение:");
			}
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */
		void FormSelectUserFormClosed(object sender, FormClosedEventArgs e)
		{
			if(programClose) Application.Exit();
			oleDb.Dispose();
		}
		void Button2Click(object sender, EventArgs e)
		{
			Close();
		}
		void FormSelectUserLoad(object sender, EventArgs e)
		{
			programClose = true;
			loadData();
		}
		void Button1Click(object sender, EventArgs e)
		{
			checkUser();
		}
		
	}
}
