/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 04.03.2017
 * Time: 18:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
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
		
		void TableUpdate(String actionFolder)
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL && DataConfig.typeDatabase == DataConstants.TYPE_OLEDB) {
				// OLEDB
				try{
					TableUpdateLocal(actionFolder);
				}catch(Exception ex){
					oleDb.Error();
					Utilits.Console.Log("[ОШИБКА]: " + ex.ToString(), false, true);
				}
			} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER && DataConfig.typeDatabase == DataConstants.TYPE_MSSQL){
				// MSSQL SERVER
				try{
					TableUpdateServer(actionFolder);
				}catch(Exception ex){
					sqlServer.Error();
					Utilits.Console.Log("[ОШИБКА]: " + ex.ToString(), false, true);
				}
			}
		}
		
		void TableUpdateLocal(String actionFolder)
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
			if(actionFolder == "" && folderExplore) {
				oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Counteragents WHERE (type = 'file' AND parent = '') ORDER BY name ASC";
			}else{
				if(folderExplore) oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Counteragents WHERE (type = 'file') ORDER BY name ASC";
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
		
		void TableUpdateServer(String actionFolder)
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
					Utilits.Console.Log("[ОШИБКА]: " + ex.ToString(), false, true);
				}
			} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER && DataConfig.typeDatabase == DataConstants.TYPE_MSSQL){
				// MSSQL SERVER
				try{
					searchServer();
					Utilits.Console.Log("Журнал Контрагентов: поиск завершен.");
				}catch(Exception ex){
					sqlServer.Error();
					Utilits.Console.Log("[ОШИБКА]: " + ex.ToString(), false, true);
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
				TableUpdate(""); // отображается всё содержимое
			}else{
				folderExplore = true;
				Utilits.Console.Log("Контрагенты: группирование включено.");
				TableUpdate(openFolder); //возвращаемся в последнюю активную папку.
			}
		}
		
		void showOpenCloseFolder() // показать открытую папку
		{
			if(listView1.Items[listView1.SelectedIndices[0]].SubItems[2].Text.ToString() == "Папка" && folderExplore){
				if(listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text.ToString() != ".."){
					openFolder = listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text.ToString();
					TableUpdate(openFolder);
				}else {
					openFolder = "";
					TableUpdate(openFolder);
				}
			}	
		}
		
		void addFile()
		{
			FormCounteragentFile FCounteragentEdit = new FormCounteragentFile();
			FCounteragentEdit.MdiParent = DataForms.FClient;
			FCounteragentEdit.ID = null;
			FCounteragentEdit.Show();
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
			FormCounteragentFolder FCounteragentFolder = new FormCounteragentFolder();
			FCounteragentFolder.MdiParent = DataForms.FClient;
			FCounteragentFolder.ID = listView1.Items[listView1.SelectedIndices[0]].SubItems[3].Text.ToString();
			FCounteragentFolder.Show();
		}
		
		void returnValue()
		{
			if(listView1.Items[listView1.SelectedIndices[0]].SubItems[2].Text.ToString() != "Папка" && listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text.ToString() != ".."){
				TextBoxReturnValue.Text = listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text.ToString();
				this.Close();
			}
		}
		
		public void TableRefresh()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL && DataConfig.typeDatabase == DataConstants.TYPE_OLEDB){
				// OLEDB
				TableUpdate(openFolder);
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER && DataConfig.typeDatabase == DataConstants.TYPE_MSSQL){
				// MSSQL SERVER
			}
		}
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */	
		void FormCounteragentsLoad(object sender, EventArgs e)
		{
			TableUpdate(""); // Загрузка данных из базы данных
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
			search(); // поиск
		}
		void Button1Click(object sender, EventArgs e)
		{
			returnValue(); // возвращает выбраные данные
		}
		void RefreshButtonClick(object sender, EventArgs e)
		{
			TableRefresh();
		}
		
	}
}
