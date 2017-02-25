/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 24.02.2017
 * Time: 8:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.OleDb;
using Aggregator.Data;


namespace Aggregator.Database
{
	/// <summary>
	/// Description of CreateTable.
	/// 
	/// Column - структура колонки
	/// CreateTable - конструктор класса
	/// СolumnAdd - добавить колонку в таблицу
	/// Execute - выполнить запрос который создаст таблицу в базе данных
	/// createTableOleDb - создаёт таблицу в базе данных OleDb
	/// createTableMsSql - создаёт таблицу в базе данных MSSQL
	/// InsertValue - вставить данные в таблицу
	/// Error - в случае ошибки прервать соединение с базой данных
	/// 
	/// </summary>
	
	public struct Column {
			public String columnName;
			public Boolean primaryKey;
			public String columnType;
	}

	public class CreateTable
	{
		private String name;
		private String path;
		private String type;
		private List<Column> columns;
		
		private OleDbConnection oleDbConnection;
		private OleDbCommand oleDbCommand;
		
		public CreateTable(String tableName, String fileName, String baseType)
		{
			name = tableName;
			path = fileName;
			type = baseType;
			columns = new List<Column>();
		}
		
		public void СolumnAdd(String columnName, Boolean primaryKey = false, String columnType = "VARCHAR DEFAULT")
		{
			Column col;
			col.columnName = columnName;
			col.primaryKey = primaryKey;
			col.columnType = columnType;
			columns.Add(col);
		}
		
		public void Execute()
		{
			if(type == DataConstants.TYPE_OLEDB){
				createTableOleDb();
			}else if(type == DataConstants.TYPE_MSSQL){
				createTableMsSql();
			}
		}
		
		/* OleDB */
		private void createTableOleDb()
		{
			String sqlCommand;
			sqlCommand = "CREATE TABLE " + name + " (";
			foreach(Column col in columns){
				if(col.primaryKey){
					sqlCommand += "[" + col.columnName + "] " + col.columnType + " PRIMARY KEY, ";
				}else{
					sqlCommand += "[" + col.columnName + "] " + col.columnType + " \"" + "\"" + ", ";
				}
			}
			sqlCommand = sqlCommand.Remove(sqlCommand.Length - 2, 2);
			sqlCommand += ")";

			try{
				oleDbConnection = new OleDbConnection();
				oleDbConnection.ConnectionString = DataConfig.oledbConnectLineBegin + path + DataConfig.oledbConnectLineEnd + DataConfig.oledbConnectPass;
				oleDbConnection.Open();
								
				oleDbCommand = new OleDbCommand(sqlCommand, oleDbConnection);
				oleDbCommand.ExecuteNonQuery();	//выполнение запроса				
				
				oleDbConnection.Close();
			}catch(Exception ex){
				MessageBox.Show(ex.ToString(), "Ошибка:");
				Application.Exit();
			}
		}
		
		/* MSSQL */
		private void createTableMsSql()
		{
			// ...
		}
		
		public void InsertValue(String values)
		{
			String sqlCommand;
			sqlCommand = "INSERT INTO " + name + "(";
			foreach(Column col in columns){
				sqlCommand += "[" + col.columnName + "], ";
			}
			sqlCommand = sqlCommand.Remove(sqlCommand.Length - 2, 2);
			sqlCommand += ")";
			sqlCommand += " VALUES (" + values + ")";
				
			oleDbConnection = new OleDbConnection();
			oleDbConnection.ConnectionString = DataConfig.oledbConnectLineBegin + path + DataConfig.oledbConnectLineEnd + DataConfig.oledbConnectPass;
			oleDbConnection.Open();
			
			oleDbCommand = new OleDbCommand(sqlCommand, oleDbConnection);
			oleDbCommand.ExecuteNonQuery();	//выполнение запроса
			
			oleDbConnection.Close();
		}
		
		/* Отключание соединения в случае ошибки */
		public void Error()
		{
			oleDbConnection.Close();//отключение соединения
		}
	}
}
