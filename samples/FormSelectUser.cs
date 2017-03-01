/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 21.02.2017
 * Time: 15:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
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
		
		private Boolean programClose;//флаг закрытия приложения
		private OleDbConnection oleDbConnection;
		private OleDbCommand oleDbCommandSelect;
		private OleDbCommand oleDbCommandDelete;
		private OleDbCommand oleDbCommandUpdate;
		private OleDbCommand oleDbCommandInsert;
		private OleDbDataAdapter oleDbDataAdapter1;
		private System.Data.DataSet oleDbDataSet;
		private System.Data.DataTable oleDbDataTable;
		
		void FormSelectUserFormClosed(object sender, FormClosedEventArgs e)
		{
			if(programClose) Application.Exit();
		}
		void Button2Click(object sender, EventArgs e)
		{
			Close();
		}
		void FormSelectUserLoad(object sender, EventArgs e)
		{
			init();
			loadData();
		}
		
		void init()
		{
			programClose = true;
			oleDbConnection = new OleDbConnection();
			oleDbDataAdapter1 = new OleDbDataAdapter();
			oleDbDataSet = new System.Data.DataSet();
			oleDbDataTable = new System.Data.DataTable();
		}
		
		void loadData()
		{
			//Подключение локальной базы данных (список серверов)
			try{
				oleDbConnection.ConnectionString = DataConfig.oledbConnectLineBegin + DataConfig.configFile + DataConfig.oledbConnectLineEnd + DataConfig.oledbConnectPass;
				oleDbConnection.Open(); //соединение с базой
				
				//Создание таблицы DataTable
				oleDbDataTable.Clear();
				oleDbDataTable.CaseSensitive = false;
				oleDbDataTable.Columns.Add("Name", Type.GetType("System.String"));
				oleDbDataTable.Columns.Add("Pass", Type.GetType("System.String"));
				oleDbDataTable.Columns.Add("Permissions", Type.GetType("System.String"));
				//Вставка таблицы в DataSet
				oleDbDataSet.Clear();
				oleDbDataSet.Tables.Add(oleDbDataTable);
				oleDbDataSet.DataSetName = "ListUsers";

				oleDbCommandSelect = new OleDbCommand("SELECT * FROM Users", oleDbConnection);
				oleDbCommandDelete = new OleDbCommand("DELETE FROM Users WHERE ([ID] = ?) " +
				                                      "AND (Name = ? OR ? IS NULL AND Name IS NULL) " +
				                                      "AND (Pass = ? OR ? IS NULL AND Pass IS NULL) " +
				                                      "AND (Permissions = ? OR ? IS NULL AND Permissions IS NULL)", oleDbConnection);
				oleDbCommandUpdate = new OleDbCommand("UPDATE Users SET [Name] = ?, [Pass] = ?, [Permissions] = ? WHERE ([ID] = ?) " +
				                                      "AND (Name = ? OR ? IS NULL AND Name IS NULL) " +
				                                      "AND (Pass = ? OR ? IS NULL AND Pass IS NULL) " +
				                                      "AND (Permissions = ? OR ? IS NULL AND Permissions IS NULL)", oleDbConnection);
				oleDbCommandInsert = new OleDbCommand("INSERT INTO Users ([Name], [Pass], [Permissions]) VALUES (?, ?, ?)", oleDbConnection);
				oleDbDataAdapter1.SelectCommand = oleDbCommandSelect;
				oleDbDataAdapter1.DeleteCommand = oleDbCommandDelete;
				oleDbDataAdapter1.UpdateCommand = oleDbCommandUpdate;
				oleDbDataAdapter1.InsertCommand = oleDbCommandInsert;
				oleDbDataAdapter1.Fill(oleDbDataSet, "Users");
				
				oleDbConnection.Close();//отключение соединения
				
			}catch(Exception ex){
				MessageBox.Show(ex.ToString());
				oleDbConnection.Close();
				Application.Exit();
			}
			readData();
		}
		
		void readData()
		{
			foreach(System.Data.DataTable table in oleDbDataSet.Tables)
			{
				foreach(System.Data.DataRow row in table.Rows)
    			{
					comboBox1.Items.Add(row[1].ToString());
				}
		 	}
		}
		
		void checkUser()
		{
			//Проверка логина и пароля
			try{
				if(comboBox1.Text == ""){
					MessageBox.Show("Вы не выбрали пользователя!","Сообщение:");
				}else{
					String Pass = oleDbDataSet.Tables["Users"].Rows[comboBox1.SelectedIndex]["Pass"].ToString();
					if(textBox1.Text == Pass){
						DataConfig.userName = oleDbDataSet.Tables["Users"].Rows[comboBox1.SelectedIndex]["Name"].ToString();
						DataConfig.userPass = oleDbDataSet.Tables["Users"].Rows[comboBox1.SelectedIndex]["Pass"].ToString();
						DataConfig.userPermissions = oleDbDataSet.Tables["Users"].Rows[comboBox1.SelectedIndex]["Permissions"].ToString();
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
