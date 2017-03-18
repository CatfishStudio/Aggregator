/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 15.03.2017
 * Время: 10:40
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Data;
using Aggregator.Database.Local;
using Aggregator.Database.Server;

namespace Aggregator.Client.Directories
{
	/// <summary>
	/// Description of FormNomenclature.
	/// </summary>
	public partial class FormNomenclature : Form
	{
		public FormNomenclature()
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
			oleDb.dataSet.DataSetName = "Nomenclature";
			if(actionFolder == "") {
				oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Nomenclature WHERE (type = 'folder') ORDER BY name ASC";
			}else{
				oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Nomenclature WHERE (type = 'folder' AND name = '" + actionFolder + "') ORDER BY name ASC";
			}
			if(oleDb.ExecuteFill("Nomenclature")){
				foldersTable = oleDb.dataSet.Tables["Nomenclature"].Copy();
			}else{
				Utilits.Console.Log("[ОШИБКА] Ошибка выполнения запроса к таблице Номенклатура при отборе папок.");
				oleDb.Error();
				return;
			}
			// Файлы			
			oleDb.dataSet.Clear();
			oleDb.dataSet.DataSetName = "Nomenclature";
			if(actionFolder == "" && folderExplore == true) {
				oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Nomenclature WHERE (type = 'file' AND parent = '') ORDER BY name ASC";
			}else{
				if(folderExplore == false) oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Counteragents WHERE (type = 'file') ORDER BY name ASC";
				else oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Nomenclature WHERE (type = 'file' AND parent = '" + actionFolder + "') ORDER BY name ASC";
			}
			if(oleDb.ExecuteFill("Nomenclature")){
				filesTable = oleDb.dataSet.Tables["Nomenclature"].Copy();
			}else{
				Utilits.Console.Log("[ОШИБКА] Ошибка выполнения запроса к таблице Номенклатура при отборе файлов.");
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
				listView1.Items.Add(ListViewItem_add);
			}
			// ВЫБОР: выдиляем ранее выбранный элемент.
			listView1.SelectedIndices.IndexOf(selectTableLine);
		}
		
		void TableRefreshServer(String actionFolder)
		{
			listView1.Items.Clear();
			// Папки
			sqlServer = new SqlServer();
			sqlServer.dataSet.Clear();
			sqlServer.dataSet.DataSetName = "Nomenclature";
			if(actionFolder == "") {
				sqlServer.sqlCommandSelect.CommandText = "SELECT * FROM Nomenclature WHERE (type = 'folder') ORDER BY name ASC";
			}else{
				sqlServer.sqlCommandSelect.CommandText = "SELECT * FROM Nomenclature WHERE (type = 'folder' AND name = '" + actionFolder + "') ORDER BY name ASC";
			}
			if(sqlServer.ExecuteFill("Nomenclature")){
				foldersTable = sqlServer.dataSet.Tables["Nomenclature"].Copy();
			}else{
				Utilits.Console.Log("[ОШИБКА] Ошибка выполнения запроса к таблице Номенклатура при отборе папок.");
				sqlServer.Error();
				return;
			}
			// Файлы			
			sqlServer.dataSet.Clear();
			sqlServer.dataSet.DataSetName = "Nomenclature";
			if(actionFolder == "" && folderExplore == true) {
				sqlServer.sqlCommandSelect.CommandText = "SELECT * FROM Nomenclature WHERE (type = 'file' AND parent = '') ORDER BY name ASC";
			}else{
				if(folderExplore == false) sqlServer.sqlCommandSelect.CommandText = "SELECT * FROM Counteragents WHERE (type = 'file') ORDER BY name ASC";
				else sqlServer.sqlCommandSelect.CommandText = "SELECT * FROM Nomenclature WHERE (type = 'file' AND parent = '" + actionFolder + "') ORDER BY name ASC";
			}
			if(sqlServer.ExecuteFill("Nomenclature")){
				filesTable = sqlServer.dataSet.Tables["Nomenclature"].Copy();
			}else{
				Utilits.Console.Log("[ОШИБКА] Ошибка выполнения запроса к таблице Номенклатура при отборе файлов.");
				sqlServer.Error();
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
				listView1.Items.Add(ListViewItem_add);
			}
			// ВЫБОР: выдиляем ранее выбранный элемент.
			listView1.SelectedIndices.IndexOf(selectTableLine);
		}
		
		void search()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL && DataConfig.typeDatabase == DataConstants.TYPE_OLEDB) {
				// OLEDB
				try{
					searchLocal();
					Utilits.Console.Log("Журнал Номенклатура: поиск завершен.");
				}catch(Exception ex){
					oleDb.Error();
					Utilits.Console.Log("[ОШИБКА]: " + ex.Message.ToString(), false, true);
				}
			} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER && DataConfig.typeDatabase == DataConstants.TYPE_MSSQL){
				// MSSQL SERVER
				try{
					searchServer();
					Utilits.Console.Log("Журнал Номенклатура: поиск завершен.");
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
			oleDb.dataSet.DataSetName = "Nomenclature";
			oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Nomenclature WHERE (name LIKE '%" + comboBox1.Text + "%') ORDER BY name ASC";
			if(oleDb.ExecuteFill("Nomenclature")){
				table = oleDb.dataSet.Tables["Nomenclature"];
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
			DataTable table;
			sqlServer = new SqlServer();
			sqlServer.dataSet.Clear();
			sqlServer.dataSet.DataSetName = "Nomenclature";
			sqlServer.sqlCommandSelect.CommandText = "SELECT * FROM Nomenclature WHERE (name LIKE '%" + comboBox1.Text + "%') ORDER BY name ASC";
			if(sqlServer.ExecuteFill("Nomenclature")){
				table = sqlServer.dataSet.Tables["Nomenclature"];
			}else{
				Utilits.Console.Log("[ОШИБКА] Ошибка поиска.");
				sqlServer.Error();
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
		
		void hierarchy() // иерархическое отображение
		{
			if(folderExplore){
				folderExplore = false;
				Utilits.Console.Log("Номенклатура: группирование отключено.");
				TableRefresh(""); // отображается всё содержимое
			}else{
				folderExplore = true;
				Utilits.Console.Log("Номенклатура: группирование включено.");
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
			FormNomenclatureFile FNomenclatureFile = new FormNomenclatureFile();
			FNomenclatureFile.MdiParent = DataForms.FClient;
			FNomenclatureFile.ID = null;
			FNomenclatureFile.ParentFolder = openFolder;
			FNomenclatureFile.Show();
		}
		
		void editFile()
		{
			if(listView1.SelectedIndices.Count > 0){
				if(listView1.Items[listView1.SelectedIndices[0]].SubItems[2].Text.ToString() == "" 
				   && listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text.ToString() != ".." 
				   && listView1.SelectedItems[0].StateImageIndex == 1){
					FormNomenclatureFile FNomenclatureFile = new FormNomenclatureFile();
					FNomenclatureFile.MdiParent = DataForms.FClient;
					FNomenclatureFile.ID = listView1.Items[listView1.SelectedIndices[0]].SubItems[3].Text.ToString();
					FNomenclatureFile.ParentFolder = openFolder;
					FNomenclatureFile.Show();
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
					
					if(MessageBox.Show("Удалить безвозвратно '" + fileName + "'?"  ,"Вопрос:", MessageBoxButtons.YesNo) == DialogResult.Yes){
						if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL) {
							// OLEDB
							QueryOleDb query = new QueryOleDb(DataConfig.localDatabase);
							query.SetCommand("DELETE FROM Nomenclature WHERE (id = " + fileID + ")");
							if(query.Execute()){
								DataForms.FClient.updateHistory("Nomenclature");
								Utilits.Console.Log("Номенклатура '" + fileName + "' успешно удалена.");
							}else{
								Utilits.Console.Log("[ОШИБКА] Номенклатура '" + fileName + "' не удалось удалить!");
							}
						} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
							// MSSQL SERVER
							QuerySqlServer query = new QuerySqlServer();
							query.SetCommand("DELETE FROM Nomenclature WHERE (id = " + fileID + ")");
							if(query.Execute()){
								Utilits.Console.Log("Номенклатура '" + fileName + "' успешно удалена.");
								DataForms.FClient.updateHistory("Nomenclature");
							}else{
								Utilits.Console.Log("[ОШИБКА] Номенклатура '" + fileName + "' не удалось удалить!");
							}
						}
					}					
				}
			}
		}
		
		void addFolder()
		{
			FormNomenclatureFolder FNomenclatureFolder = new FormNomenclatureFolder();
			FNomenclatureFolder.MdiParent = DataForms.FClient;
			FNomenclatureFolder.ID = null;
			FNomenclatureFolder.Show();
		}
		
		void editFolder()
		{
			if(listView1.SelectedIndices.Count > 0){
				if(listView1.Items[listView1.SelectedIndices[0]].SubItems[2].Text.ToString() == "Папка" 
				   && listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text.ToString() != ".." 
				   && listView1.SelectedItems[0].StateImageIndex == 0){
					FormNomenclatureFolder FNomenclatureFolder = new FormNomenclatureFolder();
					FNomenclatureFolder.MdiParent = DataForms.FClient;
					FNomenclatureFolder.ID = listView1.Items[listView1.SelectedIndices[0]].SubItems[3].Text.ToString();
					FNomenclatureFolder.Show();
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
							query = new QueryOleDb(DataConfig.localDatabase);
							query.SetCommand("DELETE FROM Nomenclature WHERE (parent ='" + folderName +"')");
							if(query.Execute()){
								query = new QueryOleDb(DataConfig.localDatabase);
								query.SetCommand("DELETE FROM Nomenclature WHERE (id = " + folderID +")");
								if(query.Execute()){
									DataForms.FClient.updateHistory("Nomenclature");
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
							
						}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
							// MSSQL SERVER
							QuerySqlServer query = new QuerySqlServer();
							query = new QuerySqlServer();
							query.SetCommand("DELETE FROM Nomenclature WHERE (parent ='" + folderName +"')");
							if(query.Execute()){
								query = new QuerySqlServer();
								query.SetCommand("DELETE FROM Nomenclature WHERE (id = " + folderID +")");
								if(query.Execute()){
									DataForms.FClient.updateHistory("Nomenclature");
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
					}
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
		void FormNomenclatureLoad(object sender, EventArgs e)
		{
			TableRefresh(""); // Загрузка данных из базы данных
			Utilits.Console.Log("Журнал Номенклатура: отркыт.");
		}
		void FormNomenclatureFormClosed(object sender, FormClosedEventArgs e)
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL && oleDb != null) oleDb.Dispose();
			if(DataConfig.typeConnection == DataConstants.CONNETION_SERVER && sqlServer != null) sqlServer.Dispose();
			Dispose();
			DataForms.FNomenclature = null;
			DataForms.FClient.messageInStatus("...");
			Utilits.Console.Log("Журнал Контрагенты: закрыт.");
		}
		void FormNomenclatureActivated(object sender, EventArgs e)
		{
			DataForms.FClient.messageInStatus("Номенклатура");
		}
		void AddButtonClick(object sender, EventArgs e)
		{
			if(DataConfig.userPermissions == "guest"){
				MessageBox.Show("У вас недостаточно прав чтобы выполнить данное действие.", "Сообщение");
				return;
			}
			addFile();
		}
		void AddFolderButtonClick(object sender, EventArgs e)
		{
			if(DataConfig.userPermissions == "guest"){
				MessageBox.Show("У вас недостаточно прав чтобы выполнить данное действие.", "Сообщение");
				return;
			}
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
			if(DataConfig.userPermissions == "guest" || DataConfig.userPermissions == "user"){
				MessageBox.Show("У вас недостаточно прав чтобы выполнить данное действие.", "Сообщение");
				return;
			}
			deleteFile();
		}
		void DeleteFolderButtonClick(object sender, EventArgs e)
		{
			if(DataConfig.userPermissions == "guest" || DataConfig.userPermissions == "user"){
				MessageBox.Show("У вас недостаточно прав чтобы выполнить данное действие.", "Сообщение");
				return;
			}
			deleteFolder();
		}
		void СоздатьПапкуToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(DataConfig.userPermissions == "guest"){
				MessageBox.Show("У вас недостаточно прав чтобы выполнить данное действие.", "Сообщение");
				return;
			}
			addFolder();
		}
		void ИзменитьПапкуToolStripMenuItemClick(object sender, EventArgs e)
		{
			editFolder();
		}
		void УдалитьПапкуToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(DataConfig.userPermissions == "guest" || DataConfig.userPermissions == "user"){
				MessageBox.Show("У вас недостаточно прав чтобы выполнить данное действие.", "Сообщение");
				return;
			}
			deleteFolder();
		}
		void СоздатьЗаписьToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(DataConfig.userPermissions == "guest"){
				MessageBox.Show("У вас недостаточно прав чтобы выполнить данное действие.", "Сообщение");
				return;
			}
			addFile();
		}
		void ИзменитьЗаписьToolStripMenuItemClick(object sender, EventArgs e)
		{
			editFile();
		}
		void УдалитьЗаписьToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(DataConfig.userPermissions == "guest" || DataConfig.userPermissions == "user"){
				MessageBox.Show("У вас недостаточно прав чтобы выполнить данное действие.", "Сообщение");
				return;
			}
			deleteFile();
		}
		void ButtonLoadingClick(object sender, EventArgs e)
		{
			if(panelLoading.Visible) panelLoading.Visible = false;
			else panelLoading.Visible = true;
		}
		void Button3Click(object sender, EventArgs e)
		{
			
		}
		
	}
}
