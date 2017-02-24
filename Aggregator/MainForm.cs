/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 21.02.2017
 * Time: 14:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data.OleDb;
using ADOX;
using Aggregator.Data;
using Aggregator.User;
using Aggregator.Database;

namespace Aggregator
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			timer1.Start();
		}
		void Timer1Tick(object sender, EventArgs e)
		{
			timer1.Stop();
			
			initLocalPath();
			initResource();
			initLocalBase();
		}
		
		void initLocalPath()
		{
			//определяем расположение программы (путь)
			DataConfig.programPath = Environment.CurrentDirectory + "\\";
			//расположение папки ресурсов
			DataConfig.resource = DataConfig.programPath + "resource";
		}
		
		void initResource()
		{
			//Проверка существования папки
			if(!Directory.Exists(DataConfig.resource)){
				//папки нет, она будет создана заново
				Directory.CreateDirectory(DataConfig.resource);
			}
		}
		
		void initLocalBase()
		{
			//поиск локальной базы данный
			DataConfig.configFile = DataConfig.resource + "\\config.mdb";
			if(!File.Exists(DataConfig.configFile)){ //файл не найден, он будет создан
				
				CreateDatabase createDataBase;
				try{
					createDataBase = new CreateDatabase(DataConfig.configFile, DataConstants.BASE_TYPE_OLEDB);
				}catch(Exception ex){
					MessageBox.Show(ex.ToString(), "Ошибка:");
					Application.Exit();
				}
				
				CreateTable createTable;
				createTable = new CreateTable("Users", DataConfig.configFile, DataConstants.BASE_TYPE_OLEDB);
				createTable.СolumnAdd("ID", true, "COUNTER");
				createTable.СolumnAdd("Name");
				createTable.СolumnAdd("Pass");
				createTable.СolumnAdd("Permissions");
				try{
					createTable.Execute();
					createTable.InsertValue("0, 'Администратор', '', 'admin'");
					createTable.InsertValue("1, 'Пользователь', '', 'user'");
				}catch(Exception ex){
					createTable.Error();
					MessageBox.Show(ex.ToString(), "Ошибка:");
					Application.Exit();
				}
			}
			createFormSelectUser();
		}
		
		void createFormSelectUser()
		{
			// Открываем окно выбора пользователя
			DataForms.FSelectUser = new FormSelectUser();
			DataForms.FSelectUser.Show();
			DataForms.FMain = this;
		}
	}
}
