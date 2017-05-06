/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 06.05.2017
 * Время: 12:28
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Aggregator.Database.Server;

namespace Aggregator.Database
{
	/// <summary>
	/// Description of CreateDatabaseMSSQL.
	/// </summary>
	public class CreateDatabaseMSSQL
	{
		String newDBName;
		String serverName;
		String dbName;
		String userName;
		String pass;
		
		String connectionString;
		
		SqlConnection connection = null;
		SqlCommand command = null;
		
		public CreateDatabaseMSSQL(String newDatabase, String server, String database, String user, String password)
		{
			newDBName = newDatabase;
			serverName = server;
			dbName = database;
			userName = user;
			pass = password;
			connectionString = "Server=" + serverName + ";Database=" + dbName + ";User Id=" + userName + ";Password=" + pass;
			connection = new SqlConnection(connectionString);
		}
		
		public bool CreateDB()
		{
			try{
				command = new SqlCommand("CREATE DATABASE " + newDBName, connection);
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
				
				/* Создание таблиц */
				connectionString = "Server=" + serverName + ";Database=" + newDBName + ";User Id=" + userName + ";Password=" + pass;
				tableUsers();
				
				
				Utilits.Console.Log("Базу данных " + newDBName + " успешно создана!");
				MessageBox.Show("База данных успешно создана!", "Сообщение");
				return true;
			}catch(Exception ex){
				connection.Close();
				Utilits.Console.Log("Не удалось создать базу данных " + newDBName, false, true);
				MessageBox.Show("Не удалось создать базу данных "+ ex.Message, "Ошибка");
				return false;
			}			
		}
		
		/* =========================================================================================
		 * Создание таблиц в базе данных 
		 * =========================================================================================
		 */
		void tableUsers()
		{
			String sqlCommand;
			QuerySqlServer query;
			query = new QuerySqlServer(connectionString);
			
			sqlCommand = "CREATE TABLE Users (" +
				"[id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
				"[name] VARCHAR(255) DEFAULT '' UNIQUE, " +
				"[pass] VARCHAR(255) DEFAULT '', " +
				"[permissions] VARCHAR(255) DEFAULT '', " +
				"[info] TEXT" +
				")";
			query.SetCommand(sqlCommand);
			if(!query.Execute()) Utilits.Console.Log("[ОШИБКА] ошибка создания таблицы Пользователи.", false, true);
			
			sqlCommand = "INSERT INTO Users (" +
				"[name], [pass], [permissions], [info]) " +
				"VALUES ('Администратор', '', 'admin', '')";
			query.SetCommand(sqlCommand);
			if(!query.Execute()) Utilits.Console.Log("[ОШИБКА] ошибка добавления данных в таблицу Пользователи.", false, true);
			
			sqlCommand = "INSERT INTO Users (" +
				"[name], [pass], [permissions], [info]) " +
				"VALUES ('Оператор', '', 'operator', '')";
			query.SetCommand(sqlCommand);
			if(!query.Execute()) Utilits.Console.Log("[ОШИБКА] ошибка добавления данных в таблицу Пользователи.", false, true);
			
			sqlCommand = "INSERT INTO Users (" +
				"[name], [pass], [permissions], [info]) " +
				"VALUES ('Пользователь', '', 'user', '')";
			query.SetCommand(sqlCommand);
			if(!query.Execute()) Utilits.Console.Log("[ОШИБКА] ошибка добавления данных в таблицу Пользователи.", false, true);
			
			sqlCommand = "INSERT INTO Users (" +
				"[name], [pass], [permissions], [info]) " +
				"VALUES ('Гость', '', 'guest', '')";
			query.SetCommand(sqlCommand);
			if(!query.Execute()) Utilits.Console.Log("[ОШИБКА] ошибка добавления данных в таблицу Пользователи.", false, true);
			
			query.Dispose();
		}
		
		
	}
}
