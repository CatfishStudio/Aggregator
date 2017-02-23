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
using System.Windows.Forms.VisualStyles;
using System.Data.OleDb;
using Aggregator.Data;

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
		private DataOleDb dataOleDb;
		
		void FormSelectUserFormClosed(object sender, FormClosedEventArgs e)
		{
			if(programClose) Application.Exit();
			dataOleDb.Clear();
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
				dataOleDb = new DataOleDb();
				dataOleDb.DataTableClear();
				dataOleDb.DataTableAdd("Name", Type.GetType("System.String"));
				dataOleDb.DataTableAdd("Pass", Type.GetType("System.String"));
				dataOleDb.DataTableAdd("Permissions", Type.GetType("System.String"));
				dataOleDb.SetSelectCommand("SELECT * FROM Users");
				dataOleDb.SetInsertCommand("INSERT INTO Users ([Name], [Pass], [Permissions]) VALUES (?, ?, ?)");
				dataOleDb.SetUpdateCommand("UPDATE Users SET [Name] = ?, [Pass] = ?, [Permissions] = ? WHERE ([ID] = ?) " +
		                                  	"AND (Name = ? OR ? IS NULL AND Name IS NULL) " +
		                                  	"AND (Pass = ? OR ? IS NULL AND Pass IS NULL) " +
		                                  	"AND (Permissions = ? OR ? IS NULL AND Permissions IS NULL)");
				dataOleDb.SetDeleteCommand("DELETE FROM Users WHERE ([ID] = ?) " +
		                                  	"AND (Name = ? OR ? IS NULL AND Name IS NULL) " +
		                                  	"AND (Pass = ? OR ? IS NULL AND Pass IS NULL) " +
		                                  	"AND (Permissions = ? OR ? IS NULL AND Permissions IS NULL)");
				dataOleDb.Fill("Users");
				
			}catch(Exception ex){
				MessageBox.Show(ex.ToString());
				dataOleDb.Error();
				Application.Exit();
			}
			readData();
		}
		
		void readData()
		{
			foreach(DataRow row in dataOleDb.GetTable("Users").Rows)
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
					String Pass = dataOleDb.GetValue("Users", comboBox1.SelectedIndex, "Pass").ToString(); //oleDbDataSet.Tables["Users"].Rows[comboBox1.SelectedIndex]["Pass"].ToString();
					if(textBox1.Text == Pass){
						DataConfig.userName = dataOleDb.GetValue("Users", comboBox1.SelectedIndex, "Name").ToString(); // oleDbDataSet.Tables["Users"].Rows[comboBox1.SelectedIndex]["Name"].ToString();
						DataConfig.userPass = dataOleDb.GetValue("Users", comboBox1.SelectedIndex, "Pass").ToString(); //oleDbDataSet.Tables["Users"].Rows[comboBox1.SelectedIndex]["Pass"].ToString();
						DataConfig.userPermissions = dataOleDb.GetValue("Users", comboBox1.SelectedIndex, "Permissions").ToString(); //oleDbDataSet.Tables["Users"].Rows[comboBox1.SelectedIndex]["Permissions"].ToString();
						DataForms.FMain.Visible = false;
						
						MessageBox.Show("DATA: " + DataConfig.userName + " " + DataConfig.userPass + " " + DataConfig.userPermissions, "Сообщение:");
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
