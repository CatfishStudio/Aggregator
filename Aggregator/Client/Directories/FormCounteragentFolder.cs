/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 09.03.2017
 * Время: 20:03
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Aggregator.Data;
using Aggregator.Database.Local;
using Aggregator.Database.Server;

namespace Aggregator.Client.Directories
{
	/// <summary>
	/// Description of FormCounteragentFolder.
	/// </summary>
	public partial class FormCounteragentFolder : Form
	{
		public FormCounteragentFolder()
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
		SqlServer sqlServer;
		String folderName;
		
		void saveNew()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				oleDb.oleDbCommandSelect.CommandText = "SELECT id, name, type FROM Counteragents WHERE (id = 0)";
				oleDb.ExecuteFill("Counteragents");				
				
				DataRow newRow = oleDb.dataSet.Tables["Counteragents"].NewRow();
				newRow["name"] = nameTextBox.Text;
				newRow["type"] = "folder";
				oleDb.dataSet.Tables["Counteragents"].Rows.Add(newRow);
				
				oleDb.oleDbCommandInsert.CommandText = "INSERT INTO Counteragents (name, type) VALUES (@name, @type)";
				oleDb.oleDbCommandInsert.Parameters.Add("@name", OleDbType.VarChar, 255, "name");
				oleDb.oleDbCommandInsert.Parameters.Add("@type", OleDbType.VarChar, 255, "type");
				if(oleDb.ExecuteUpdate("Counteragents")){
					DataForms.FClient.updateHistory("Counteragents");
					Close();
				}
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				
			}
			Utilits.Console.Log("Создан новый пользователь.");
		}
		
		void saveEdit()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["name"] = nameTextBox.Text;
				oleDb.oleDbCommandUpdate.CommandText = "UPDATE Counteragents SET " +
					"[name] = @name " +
					"WHERE ([id] = @id)";
				oleDb.oleDbCommandUpdate.Parameters.Add("@name", OleDbType.VarChar, 255, "name");
				oleDb.oleDbCommandUpdate.Parameters.Add("@id", OleDbType.Integer, 10, "id");
				if(oleDb.ExecuteUpdate("Counteragents")){
					moveFilesInRenameFolder();
					DataForms.FClient.updateHistory("Counteragents");					
					Utilits.Console.Log("Папка успешно переименована.");
					Close();
				}				
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				
			}
			
		}
		
		void moveFilesInRenameFolder()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				QueryOleDb query = new QueryOleDb(DataConfig.localDatabase);
				query.SetCommand("UPDATE Counteragents SET parent='" + nameTextBox.Text + "' WHERE(parent = '" + folderName + "')");
				query.Execute();
				query.Dispose();
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				
			}
			
			
		}
		
		void open()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				oleDb.oleDbCommandSelect.CommandText = "SELECT id, name, type FROM Counteragents WHERE (id = " + ID + ")";
				oleDb.ExecuteFill("Counteragents");
				codeTextBox.Text = oleDb.dataSet.Tables["Counteragents"].Rows[0]["id"].ToString();
				nameTextBox.Text = oleDb.dataSet.Tables["Counteragents"].Rows[0]["name"].ToString();
				folderName = oleDb.dataSet.Tables["Counteragents"].Rows[0]["name"].ToString();
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				
			}
		}
		
		bool check()
		{
			if(nameTextBox.Text == "") return false;
			return true;
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */	
		void FormCounteragentFolderLoad(object sender, EventArgs e)
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL) oleDb = new OleDb(DataConfig.localDatabase);
			if(DataConfig.typeConnection == DataConstants.CONNETION_SERVER) sqlServer = new SqlServer();
			if(ID == null){
				Text = "Создать";
			}else{
				Text = "Изменить";
				open();
			}
		}
		void FormCounteragentFolderFormClosed(object sender, FormClosedEventArgs e)
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL && oleDb != null) oleDb.Dispose();
			if(DataConfig.typeConnection == DataConstants.CONNETION_SERVER && sqlServer != null) sqlServer.Dispose();
			Dispose();
		}
		void ButtonCancelClick(object sender, EventArgs e)
		{
			Close();
		}
		void Button2Click(object sender, EventArgs e)
		{
			nameTextBox.Clear();
		}
		void ButtonSaveClick(object sender, EventArgs e)
		{
			if(check()){
				if(ID == null) saveNew();
				else saveEdit();
			}else{
				MessageBox.Show("Некорректно заполнены поля формы.", "Сообщение:");
			}
		}
	}
}
