/*
 * Создано в SharpDevelop.
 * Пользователь: Catfish
 * Дата: 25.02.2017
 * Время: 10:52
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Windows.Forms;
using System.Data.OleDb;
using Aggregator.Data;
using Aggregator.Database.Local;

namespace Aggregator.Database.Config
{
	/// <summary>
	/// Description of CreateConfig.
	/// </summary>
	public static class CreateConfig
	{
		public static void Create()
		{
			/* Создание файла базы данных */
			CreateDatabase createDataBase;
			try{
				createDataBase = new CreateDatabase(DataConfig.configFile, DataConstants.TYPE_OLEDB);
			}catch(Exception ex){
				MessageBox.Show(ex.ToString(), "Ошибка:");
				Application.Exit();
			}
			
			/* Создание таблицы пользователей */
			CreateTable createTable;
			createTable = new CreateTable("Users", DataConfig.configFile, DataConstants.TYPE_OLEDB);
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
			
			DataConfig.localDatabase = DataConfig.resource + "\\database.mdb";
			DataConfig.typeConnection = DataConstants.CONNETION_LOCAL;
			
			/* Создание таблицы настроек */
			createTable = new CreateTable("Settings", DataConfig.configFile, DataConstants.TYPE_OLEDB);
			createTable.СolumnAdd("ID", true, "COUNTER");
			createTable.СolumnAdd("name");
			createTable.СolumnAdd("localDatabase");
			createTable.СolumnAdd("typeDatabase");
			createTable.СolumnAdd("typeConnection");
			createTable.СolumnAdd("server");
			createTable.СolumnAdd("serverUser");
			createTable.СolumnAdd("serverDatabase");
			
			try{
				createTable.Execute();
				createTable.InsertValue("0, 'database', '" + DataConfig.localDatabase 
				                        + "', '" + DataConstants.TYPE_OLEDB 
				                        + "', '" + DataConfig.typeConnection 
				                        + "', 'localhost', 'sa', 'database'"); 
			}catch(Exception ex){
				createTable.Error();
				MessageBox.Show(ex.ToString(), "Ошибка:");
				Application.Exit();
			}
		}
	}
}
