/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 02.03.2017
 * Time: 7:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using Aggregator.Data;
using Aggregator.Database.Local;

namespace Aggregator.User
{
	/// <summary>
	/// Description of FormUsersEdit.
	/// </summary>
	public partial class FormUsersEdit : Form
	{
		public FormUsersEdit()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public String ID;
		OleDb oleDb;
		
		String getPermissions(String value)
		{
			if(value == "admin") return "администратор";
			if(value == "operator") return "оператор";
			if(value == "user") return "пользователь";
			if(value == "guest") return "гость";
			return "";
		}
		
		String setPermissions(String value)
		{
			if(value == "администратор") return "admin";
			if(value == "оператор") return "operator";
			if(value == "пользователь") return "user";
			if(value == "гость") return "guest";
			return "";
		}
		
		void saveNew()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL && DataConfig.typeDatabase == DataConstants.TYPE_OLEDB){
				// OLEDB
				oleDb.oleDbCommandSelect.CommandText = "SELECT id, name, pass, permissions, info FROM Users WHERE (id = 0)";
				oleDb.ExecuteFill("Users");				
				
				DataRow newRow = oleDb.dataSet.Tables["Users"].NewRow();
				newRow["name"] = nameTextBox.Text;
				newRow["pass"] = passTextBox1.Text;
				newRow["permissions"] = setPermissions(permissionsComboBox.Text);
				newRow["info"] = infoTextBox.Text;
				oleDb.dataSet.Tables["Users"].Rows.Add(newRow);
				
				oleDb.oleDbCommandInsert.CommandText = "INSERT INTO Users (name, pass, permissions, info) VALUES (@name, @pass, @permissions, @info)";
				oleDb.oleDbCommandInsert.Parameters.Add("@name", OleDbType.VarChar, 255, "name");
				oleDb.oleDbCommandInsert.Parameters.Add("@pass", OleDbType.VarChar, 255, "pass");
				oleDb.oleDbCommandInsert.Parameters.Add("@permissions", OleDbType.VarChar, 255, "permissions");
				oleDb.oleDbCommandInsert.Parameters.Add("@info", OleDbType.LongVarChar, 0, "info");
				if(oleDb.ExecuteUpdate("Users")){
					DataForms.FClient.updateData(0);
					Close();
				}
				
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER && DataConfig.typeDatabase == DataConstants.TYPE_MSSQL){
				// MSSQL SERVER
				
			}
			Utilits.Console.Log("Создан новый пользователь.");
		}
		
		void saveEdit()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL && DataConfig.typeDatabase == DataConstants.TYPE_OLEDB){
				// OLEDB

			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER && DataConfig.typeDatabase == DataConstants.TYPE_MSSQL){
				// MSSQL SERVER
				
			}
			Utilits.Console.Log("Изменены данные пользователя ID:" + ID);
		}
		
		void open()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL && DataConfig.typeDatabase == DataConstants.TYPE_OLEDB){
				// OLEDB
				oleDb.oleDbCommandSelect.CommandText = "SELECT id, name, pass, permissions, info FROM Users WHERE (id = " + ID + ")";
				oleDb.ExecuteFill("Users");
				nameTextBox.Text = oleDb.dataSet.Tables["Users"].Rows[0]["name"].ToString();
				passTextBox1.Text = oleDb.dataSet.Tables["Users"].Rows[0]["pass"].ToString();
				passTextBox2.Text = oleDb.dataSet.Tables["Users"].Rows[0]["pass"].ToString();
				permissionsComboBox.Text = getPermissions(oleDb.dataSet.Tables["Users"].Rows[0]["permissions"].ToString());
				infoTextBox.Text = oleDb.dataSet.Tables["Users"].Rows[0]["info"].ToString();
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER && DataConfig.typeDatabase == DataConstants.TYPE_MSSQL){
				// MSSQL SERVER
				
			}
		}
		
		bool check()
		{
			if(nameTextBox.Text == "") return false;
			if(permissionsComboBox.Text != "администратор" 
			   && permissionsComboBox.Text != "оператор" 
			   && permissionsComboBox.Text != "пользователь" 
			   && permissionsComboBox.Text != "гость"){
				return false;
			}
			return true;
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */
		void FormUsersEditLoad(object sender, EventArgs e)
		{
			oleDb = new OleDb(DataConfig.localDatabase);
			if(ID == null){
				Text = "Новый";
			}else{
				Text = "Изменить";
				open();
			}
		}
		void FormUsersEditFormClosed(object sender, FormClosedEventArgs e)
		{
			oleDb.Dispose();
			Dispose();
		}
		void Button1Click(object sender, EventArgs e)
		{
			nameTextBox.Clear();
		}
		void Button2Click(object sender, EventArgs e)
		{
			passTextBox1.Clear();
		}
		void Button3Click(object sender, EventArgs e)
		{
			passTextBox2.Clear();
		}
		void Button5Click(object sender, EventArgs e)
		{
			Close();
		}
		void Button4Click(object sender, EventArgs e)
		{
			if(check()){
				if(ID == null) saveNew();
				else saveEdit();
			}else{
				MessageBox.Show("Не корректно заполнена форма.", "Сообщение:");
			}
		}
		void PermissionsComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			switch(permissionsComboBox.Text){
				case "администратор": 
					checkBox1.Checked = true;
					checkBox2.Checked = true;
					checkBox3.Checked = true;
					checkBox4.Checked = true;
					checkBox5.Checked = true;
					break;
				case "оператор": 
					checkBox1.Checked = false;
					checkBox2.Checked = true;
					checkBox3.Checked = true;
					checkBox4.Checked = true;
					checkBox5.Checked = true;
					break;
				case "пользователь": 
					checkBox1.Checked = false;
					checkBox2.Checked = true;
					checkBox3.Checked = true;
					checkBox4.Checked = false;
					checkBox5.Checked = false;
					break;
				case "гость": 
					checkBox1.Checked = false;
					checkBox2.Checked = false;
					checkBox3.Checked = true;
					checkBox4.Checked = false;
					checkBox5.Checked = false;
					break;
				default:
					checkBox1.Checked = false;
					checkBox2.Checked = false;
					checkBox3.Checked = false;
					checkBox4.Checked = false;
					checkBox5.Checked = false;
					break;
			}
			
		}
		
	}
}
