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
	public partial class FormSelectUser : Form
	{
		public FormSelectUser()
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
		
		void loadData()
		{
			//Подключение локальной базы данных (список серверов)
			try{
				oleDb = new OleDb(DataConfig.configFile);
				oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Users";
				oleDb.ExecuteFill("Users");
				
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
					String pass = oleDb.dataSet.Tables["Users"].Rows[comboBox1.SelectedIndex]["Pass"].ToString();
					if(textBox1.Text == pass){
						DataConfig.userName = oleDb.dataSet.Tables["Users"].Rows[comboBox1.SelectedIndex]["Name"].ToString();
						DataConfig.userPass = oleDb.dataSet.Tables["Users"].Rows[comboBox1.SelectedIndex]["Pass"].ToString();
						DataConfig.userPermissions = oleDb.dataSet.Tables["Users"].Rows[comboBox1.SelectedIndex]["Permissions"].ToString(); 
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
		
		void Button1Click(object sender, EventArgs e)
		{
			checkUser();
		}
		
	}
}
