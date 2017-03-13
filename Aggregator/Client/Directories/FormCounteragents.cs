/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 04.03.2017
 * Time: 18:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Data;
using Aggregator.Database.Local;
using Aggregator.Database.Server;

namespace Aggregator.Client.Directories
{
	/// <summary>
	/// Description of FormCounteragents.
	/// </summary>
	public partial class FormCounteragents : Form
	{
		public FormCounteragents()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public TextBox TextBoxReturnValue;	// объект принимаемый значение
		
		OleDb oleDb;
		SqlServer sqlServer;
		DataTable foldersTable;			// папки
		DataTable filesTable; 			// файлы
		String openFolder = ""; 		// открытая папка
		bool folderExplore = true; 		// флаг отображения элементов в папках
		int selectTableLine = 0;		// выбранная строка в таблице
		
		public void TableRefresh(String actionFolder = null)
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL) {
				// OLEDB
				try{
					if(actionFolder == null) TableRefreshLocal(openFolder);
					else TableRefreshLocal(actionFolder);
				}catch(Exception ex){
					oleDb.Error();
					Utilits.Console.Log("[ОШИБКА]: " + ex.Message.ToString(), false, true);
				}
			} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				try{
					if(actionFolder == null) TableRefreshServer(openFolder);
					else TableRefreshServer(actionFolder);
				}catch(Exception ex){
					sqlServer.Error();
					Utilits.Console.Log("[ОШИБКА]: " + ex.Message.ToString(), false, true);
				}
			}
		}
		
		void TableRefreshLocal(String actionFolder)
		{
			listView1.Items.Clear();
			// Папки
			oleDb = new OleDb(DataConfig.localDatabase);
			oleDb.dataSet.Clear();
			oleDb.dataSet.DataSetName = "Counteragents";
			if(actionFolder == "") {
				oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Counteragents WHERE (type = 'folder') ORDER BY name ASC";
			}else{
				oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Counteragents WHERE (type = 'folder' AND name = '" + actionFolder + "') ORDER BY name ASC";
			}
			if(oleDb.ExecuteFill("Counteragents")){
				foldersTable = oleDb.dataSet.Tables["Counteragents"].Copy();
			}else{
				Utilits.Console.Log("[ОШИБКА] Ошибка выполнения запроса к таблице Контрагентов при отборе папок.");
				oleDb.Error();
				return;
			}
			// Файлы			
			oleDb.dataSet.Clear();
			oleDb.dataSet.DataSetName = "Counteragents";
			if(actionFolder == "" && folderExplore == true) {
				oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Counteragents WHERE (type = 'file' AND parent = '') ORDER BY name ASC";
			}else{
				if(folderExplore == false) oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Counteragents WHERE (type = 'file') ORDER BY name ASC";
				else oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Counteragents WHERE (type = 'file' AND parent = '" + actionFolder + "') ORDER BY name ASC";
			}
			if(oleDb.ExecuteFill("Counteragents")){
				filesTable = oleDb.dataSet.Tables["Counteragents"].Copy();
			}else{
				Utilits.Console.Log("[ОШИБКА] Ошибка выполнения запроса к таблице Контрагентов при отборе файлов.");
				oleDb.Error();
				return;
			}
			// ОТОБРАЖЕНИЕ: "Папок"
			foreach(DataRow rowFolder in foldersTable.Rows)
    		{
				ListViewItem ListViewItem_add = new ListViewItem();
				if(actionFolder == "") ListViewItem_add.SubItems.Add(rowFolder["name"].ToString());
				else ListViewItem_add.SubItems.Add("..");
				ListViewItem_add.StateImageIndex = 0;
				ListViewItem_add.SubItems.Add("Папка");
				ListViewItem_add.SubItems.Add(rowFolder["id"].ToString());
				ListViewItem_add.SubItems.Add(rowFolder["excel_table_id"].ToString());
				listView1.Items.Add(ListViewItem_add);
			}
			// ОТОБРАЖЕНИЕ "Файлов"
			foreach(DataRow rowElement in filesTable.Rows)
    		{
				ListViewItem ListViewItem_add = new ListViewItem();
				ListViewItem_add.SubItems.Add(rowElement["name"].ToString());
				ListViewItem_add.StateImageIndex = 1;
				ListViewItem_add.SubItems.Add("");
				ListViewItem_add.SubItems.Add(rowElement["id"].ToString());
				ListViewItem_add.SubItems.Add(rowElement["excel_table_id"].ToString());
				listView1.Items.Add(ListViewItem_add);
			}
			// ВЫБОР: выдиляем ранее выбранный элемент.
			listView1.SelectedIndices.IndexOf(selectTableLine);
		}
		
		void TableRefreshServer(String actionFolder)
		{
			sqlServer = new SqlServer();
			// ...
		}
		
		void search()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL && DataConfig.typeDatabase == DataConstants.TYPE_OLEDB) {
				// OLEDB
				try{
					searchLocal();
					Utilits.Console.Log("Журнал Контрагентов: поиск завершен.");
				}catch(Exception ex){
					oleDb.Error();
					Utilits.Console.Log("[ОШИБКА]: " + ex.Message.ToString(), false, true);
				}
			} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER && DataConfig.typeDatabase == DataConstants.TYPE_MSSQL){
				// MSSQL SERVER
				try{
					searchServer();
					Utilits.Console.Log("Журнал Контрагентов: поиск завершен.");
				}catch(Exception ex){
					sqlServer.Error();
					Utilits.Console.Log("[ОШИБКА]: " + ex.Message.ToString(), false, true);
				}
			}
			comboBox1.Items.Add(comboBox1.Text);
		}
		
		void searchLocal()
		{
			DataTable table;
			oleDb = new OleDb(DataConfig.localDatabase);
			oleDb.dataSet.Clear();
			oleDb.dataSet.DataSetName = "Counteragents";
			oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Counteragents WHERE (name LIKE '%" + comboBox1.Text + "%') ORDER BY name ASC";
			if(oleDb.ExecuteFill("Counteragents")){
				table = oleDb.dataSet.Tables["Counteragents"];
			}else{
				Utilits.Console.Log("[ОШИБКА] Ошибка поиска.");
				oleDb.Error();
				return;
			}
			listView1.Items.Clear();
			foreach(DataRow row in table.Rows)
        	{
				ListViewItem ListViewItem_add = new ListViewItem();
				ListViewItem_add.SubItems.Add(row["name"].ToString());
				if(row["type"].ToString() == "folder"){
					ListViewItem_add.StateImageIndex = 0;
					ListViewItem_add.SubItems.Add("Папка");
				}else{
					ListViewItem_add.StateImageIndex = 1;
					ListViewItem_add.SubItems.Add("");
				}
				ListViewItem_add.SubItems.Add(row["id"].ToString());
				listView1.Items.Add(ListViewItem_add);
			}
		}
		
		void searchServer()
		{
			sqlServer = new SqlServer();
			// ...
		}
		
		void hierarchy() // иерархическое отображение
		{
			if(folderExplore){
				folderExplore = false;
				Utilits.Console.Log("Контрагенты: группирование отключено.");
				TableRefresh(""); // отображается всё содержимое
			}else{
				folderExplore = true;
				Utilits.Console.Log("Контрагенты: группирование включено.");
				TableRefresh(openFolder); //возвращаемся в последнюю активную папку.
			}
		}
		
		void showOpenCloseFolder() // показать открытую папку
		{
			if(listView1.Items[listView1.SelectedIndices[0]].SubItems[2].Text.ToString() == "Папка" && folderExplore){
				if(listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text.ToString() != ".."){
					openFolder = listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text.ToString();
					TableRefresh(openFolder);
				}else {
					openFolder = "";
					TableRefresh(openFolder);
				}
			}	
		}
		
		void addFile()
		{
			FormCounteragentFile FCounteragentEdit = new FormCounteragentFile();
			FCounteragentEdit.MdiParent = DataForms.FClient;
			FCounteragentEdit.ID = null;
			FCounteragentEdit.ParentFolder = openFolder;
			FCounteragentEdit.Show();
		}
		
		void editFile()
		{
			if(listView1.SelectedIndices.Count > 0){
				if(listView1.Items[listView1.SelectedIndices[0]].SubItems[2].Text.ToString() == "" 
				   && listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text.ToString() != ".." 
				   && listView1.SelectedItems[0].StateImageIndex == 1){
					FormCounteragentFile FCounteragentEdit = new FormCounteragentFile();
					FCounteragentEdit.MdiParent = DataForms.FClient;
					FCounteragentEdit.ID = listView1.Items[listView1.SelectedIndices[0]].SubItems[3].Text.ToString();
					FCounteragentEdit.ParentFolder = openFolder;
					FCounteragentEdit.ExcelTableID = listView1.Items[listView1.SelectedIndices[0]].SubItems[4].Text.ToString();
					FCounteragentEdit.Show();
				}
			}
		}
		
		void deleteFile()
		{
			if(listView1.SelectedIndices.Count > 0){
				if(listView1.Items[listView1.SelectedIndices[0]].SubItems[2].Text.ToString() == "" 
				   && listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text.ToString() != ".." 
				   && listView1.SelectedItems[0].StateImageIndex == 1){
					
					String fileID = listView1.Items[listView1.SelectedIndices[0]].SubItems[3].Text.ToString();
					String fileName = listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text.ToString();
					String priceName = listView1.Items[listView1.SelectedIndices[0]].SubItems[4].Text.ToString();
					
					if(MessageBox.Show("Удалить безвозвратно контрагента '" + fileName + "'" + Environment.NewLine + "и его прайс '" + priceName + "' ?"  ,"Вопрос:", MessageBoxButtons.YesNo) == DialogResult.Yes){
						if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL) {
							// OLEDB
							QueryOleDb query = new QueryOleDb(DataConfig.localDatabase);
							query.SetCommand("DELETE FROM Counteragents WHERE (id = " + fileID + ")");
							if(query.Execute()){
								query = new QueryOleDb(DataConfig.localDatabase);
								query.SetCommand("DROP TABLE " + priceName);
								if(query.Execute()){
									Utilits.Console.Log("Контрагент '" + fileName + "' успешно удалён. Прайс '" + priceName + "' успешно удалён.");
								}else{
									Utilits.Console.Log("[ОШИБКА] Прайс '" + priceName + "' не удалось удалить!");
								}
								DataForms.FClient.updateHistory("Counteragents");
							}else{
								Utilits.Console.Log("[ОШИБКА] Контрагент '" + fileName + "' не удалось удалить!");
							}
						} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
							// MSSQL SERVER
							
						}
					}					
					
				}
			}
		}
		
		void addFolder()
		{
			FormCounteragentFolder FCounteragentFolder = new FormCounteragentFolder();
			FCounteragentFolder.MdiParent = DataForms.FClient;
			FCounteragentFolder.ID = null;
			FCounteragentFolder.Show();
		}
		
		void editFolder()
		{
			if(listView1.SelectedIndices.Count > 0){
				if(listView1.Items[listView1.SelectedIndices[0]].SubItems[2].Text.ToString() == "Папка" 
				   && listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text.ToString() != ".." 
				   && listView1.SelectedItems[0].StateImageIndex == 0){
					FormCounteragentFolder FCounteragentFolder = new FormCounteragentFolder();
					FCounteragentFolder.MdiParent = DataForms.FClient;
					FCounteragentFolder.ID = listView1.Items[listView1.SelectedIndices[0]].SubItems[3].Text.ToString();
					FCounteragentFolder.Show();
				}
			}
		}
		
		void deleteFolder()
		{
			if(listView1.SelectedIndices.Count > 0){
				if(listView1.Items[listView1.SelectedIndices[0]].SubItems[2].Text.ToString() == "Папка" 
				   && listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text.ToString() != ".." 
				   && listView1.SelectedItems[0].StateImageIndex == 0){
					String folderID = listView1.Items[listView1.SelectedIndices[0]].SubItems[3].Text.ToString();
					String folderName = listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text.ToString();
					
					if(MessageBox.Show("Удалить безвозвратно папку '" + folderName + "' и всё её содержимое ?"  ,"Вопрос:", MessageBoxButtons.YesNo) == DialogResult.Yes){
						if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL) {
							// OLEDB
							QueryOleDb query = new QueryOleDb(DataConfig.localDatabase);
							oleDb = new OleDb(DataConfig.localDatabase);
							oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Counteragents WHERE(parent = '" + folderName + "')";
							if(oleDb.ExecuteFill("Counteragents")){
								foreach(DataRow row in oleDb.dataSet.Tables[0].Rows){
									query.SetCommand("DROP TABLE " + row["excel_table_id"].ToString());
									if(query.Execute()){
										Utilits.Console.Log("Прайс лист '" + row["excel_table_id"].ToString() + "' успешно удалён.");
									}else{
										Utilits.Console.Log("[ОШИБКА] Прайс лист '" + row["excel_table_id"].ToString() + "' не удалось удалить!", false, true);
									}
								}
								query = new QueryOleDb(DataConfig.localDatabase);
								query.SetCommand("DELETE FROM Counteragents WHERE (parent ='" + folderName +"')");
								if(query.Execute()){
									query = new QueryOleDb(DataConfig.localDatabase);
									query.SetCommand("DELETE FROM Counteragents WHERE (id = " + folderID +")");
									if(query.Execute()){
										DataForms.FClient.updateHistory("Counteragents");
										query.Dispose();
										Utilits.Console.Log("Удаление папки '" + folderName + "' прошло успешно.");
									}else{
										query.Dispose();
										Utilits.Console.Log("Папку '" + folderName + "' не удалось удалить!", false, true);
									}
								}else{
									query.Dispose();
									Utilits.Console.Log("[ОШИБКА] Ошибка удаления файлов в папке '" + folderName + "'", false, true);
								}
							}
						} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
							// MSSQL SERVER
							
						}
					}
					
				}
			}
		}
		
		void openPrice()
		{
			if(listView1.SelectedIndices.Count > 0){
				if(listView1.Items[listView1.SelectedIndices[0]].SubItems[2].Text.ToString() == "" 
				   && listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text.ToString() != ".." 
				   && listView1.SelectedItems[0].StateImageIndex == 1){
					FormCounteragentPrice FCounteragentPrice = new FormCounteragentPrice();
					FCounteragentPrice.MdiParent = DataForms.FClient;
					FCounteragentPrice.Text = "Прайс-лист контрагента: " + listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text.ToString();
					FCounteragentPrice.PriceName = listView1.Items[listView1.SelectedIndices[0]].SubItems[4].Text.ToString();
					FCounteragentPrice.Show();
				}
			}
		}
		
		void returnValue()
		{
			if(listView1.Items[listView1.SelectedIndices[0]].SubItems[2].Text.ToString() != "Папка" && listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text.ToString() != ".."){
				TextBoxReturnValue.Text = listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text.ToString();
				this.Close();
			}
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */	
		void FormCounteragentsLoad(object sender, EventArgs e)
		{
			TableRefresh(""); // Загрузка данных из базы данных
			Utilits.Console.Log("Журнал Контрагентов: отркыт.");
		}
		void FormCounteragentsFormClosed(object sender, FormClosedEventArgs e)
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL && oleDb != null) oleDb.Dispose();
			if(DataConfig.typeConnection == DataConstants.CONNETION_SERVER && sqlServer != null) sqlServer.Dispose();
			Dispose();
			DataForms.FCounteragents = null;
			Utilits.Console.Log("Журнал Контрагентов: закрыт.");
		}
		void AddButtonClick(object sender, EventArgs e)
		{
			addFile();
		}
		void AddFolderButtonClick(object sender, EventArgs e)
		{
			addFolder();
		}
		void EditFolderButtonClick(object sender, EventArgs e)
		{
			editFolder();
		}
		void ButtonCloseClick(object sender, EventArgs e)
		{
			Close();
		}
		void ViewButtonClick(object sender, EventArgs e)
		{
			hierarchy();  // иерархическое отображение
		}
		void ListView1DoubleClick(object sender, EventArgs e)
		{
			showOpenCloseFolder(); // показать открытую папку
		}
		void ListView1SelectedIndexChanged(object sender, EventArgs e)
		{
			// выбранная строка таблицы
			if(listView1.SelectedItems.Count > 0) selectTableLine = listView1.SelectedItems[0].Index; // индекс выбраной строки
		}
		void FindButtonClick(object sender, EventArgs e)
		{
			openFolder = "";
			search(); // поиск
		}
		void ComboBox1KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyData == Keys.Enter){
				openFolder = "";
				search(); // поиск
			}
		}
		void Button1Click(object sender, EventArgs e)
		{
			returnValue(); // возвращает выбраные данные
		}
		void RefreshButtonClick(object sender, EventArgs e)
		{
			TableRefresh(openFolder);
		}
		void EditButtonClick(object sender, EventArgs e)
		{
			editFile();
		}
		void DeleteButtonClick(object sender, EventArgs e)
		{
			deleteFile();
		}
		void DeleteFolderButtonClick(object sender, EventArgs e)
		{
			deleteFolder();
		}
		void PriceButtonClick(object sender, EventArgs e)
		{
			openPrice();
		}
		
	}
}
