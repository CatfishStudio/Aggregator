/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 27.03.2017
 * Время: 11:02
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Data;
using System.Data.Odbc;
using System.Collections.Generic;
using System.Data.OleDb;
using Aggregator.Data;

namespace Aggregator.Database.Local
{
	/// <summary>
	/// Description of HistoryRefreshOleDb.
	/// </summary>
	
	public struct Table {
			public String name;
			public String represent;
			public String datetime;
			public String error;
			public String user;
	}
	
	public class HistoryRefreshOleDb
	{
		OleDb oleDb;
		OleDbConnection oleDbConnection;
		OleDbCommand oleDbCommand;
		OleDbDataReader oleDbDataReader;
		
		List<Table> tables;		
		
		public HistoryRefreshOleDb()
		{
			tables = new List<Table>();
			Table table;
			
			oleDbConnection = new OleDbConnection();
			oleDbConnection.ConnectionString = DataConfig.oledbConnectLineBegin + DataConfig.localDatabase + DataConfig.oledbConnectLineEnd + DataConfig.oledbConnectPass;
			oleDbConnection.Open();
			oleDbCommand = new OleDbCommand("SELECT [id], [name], [represent], [datetime], [error], [user] FROM History", oleDbConnection);
			oleDbDataReader = oleDbCommand.ExecuteReader();
			while (oleDbDataReader.Read())
	        {
				table.name = oleDbDataReader["name"].ToString();
				table.represent = oleDbDataReader["represent"].ToString();
				table.datetime = oleDbDataReader["datetime"].ToString();
				table.error = oleDbDataReader["error"].ToString();
				table.user = oleDbDataReader["user"].ToString();
				tables.Add(table);
			}
			oleDbDataReader.Close();
			oleDbConnection.Close();
			
			oleDb = new OleDb(DataConfig.localDatabase);
			oleDb.oleDbCommandSelect.CommandText = "SELECT [id], [name], [represent], [datetime], [error], [user] FROM History";
			oleDb.oleDbCommandUpdate.CommandText = "UPDATE History SET " +
					"[name] = @name, " +
					"[represent] = @represent, " +
					"[datetime] = @datetime, " +
					"[error] = @error, " +
					"[user] = @user " +
					"WHERE ([id] = @id)";
			oleDb.oleDbCommandUpdate.Parameters.Add("@name", OleDbType.VarChar, 255, "name");
			oleDb.oleDbCommandUpdate.Parameters.Add("@represent", OleDbType.VarChar, 255, "represent");
			oleDb.oleDbCommandUpdate.Parameters.Add("@datetime", OleDbType.VarChar, 255, "datetime");
			oleDb.oleDbCommandUpdate.Parameters.Add("@error", OleDbType.VarChar, 255, "error");
			oleDb.oleDbCommandUpdate.Parameters.Add("@user", OleDbType.VarChar, 255, "user");
			oleDb.oleDbCommandUpdate.Parameters.Add("@id", OleDbType.Integer, 10, "id");
			oleDb.ExecuteFill("History");
		}
		
		/* Проверить обновления */
		public void check()
		{
			Table table;
			
			oleDbConnection.Open();
			oleDbCommand = new OleDbCommand("SELECT [id], [name], [represent], [datetime], [error], [user] FROM History", oleDbConnection);
			oleDbDataReader = oleDbCommand.ExecuteReader();
			
			int i = 0;
			while (oleDbDataReader.Read())
	        {
				table.name = oleDbDataReader["name"].ToString();
				table.represent = oleDbDataReader["represent"].ToString();
				table.datetime = oleDbDataReader["datetime"].ToString();
				table.error = oleDbDataReader["error"].ToString();
				table.user = oleDbDataReader["user"].ToString();

				if(tables[i].datetime != table.datetime){
					refresh(tables[i].name, tables[i].represent);
					tables[i] = table;
				}
				i++;
			}
			oleDbDataReader.Close();
			oleDbConnection.Close();
		}
		
		/* Обновить */
		public void update(String tableName)
		{
			try{
				oleDb.dataSet.Tables["History"].Rows[getTableIndex(tableName)]["user"] = DataConfig.userName;
				oleDb.dataSet.Tables["History"].Rows[getTableIndex(tableName)]["datetime"] = DateTime.Now.ToString();
				
				if(!oleDb.ExecuteUpdate("History")) Utilits.Console.Log("[ОШИБКА] ошибка обновления данных.", false, true);
			}catch(Exception ex){
				oleDb.Error();
				Utilits.Console.Log("[ОШИБКА] " + ex.Message.ToString(), false, true);
			}
		}
		
		/* Обновить таблицы новыми данными */
		public void refresh(String tableName, String tableRepresent)
		{
			try{
				if(tableName == "Users" && DataForms.FUsers != null) DataForms.FUsers.TableRefresh();
				if(tableName == "Counteragents" && DataForms.FCounteragents != null) DataForms.FCounteragents.TableRefresh();
				if(tableName == "Nomenclature" && DataForms.FNomenclature != null) DataForms.FNomenclature.TableRefresh();
				if(tableName == "Units" && DataForms.FUnits != null) DataForms.FUnits.TableRefresh();
				if(tableName == "PurchasePlan" && DataForms.FPurchasePlanJournal != null) DataForms.FPurchasePlanJournal.TableRefresh();
				
				Utilits.Console.Log("[ИСТОРИЯ] Таблица " + tableRepresent + " была успешно обновлена.");
			}catch(Exception ex){
				oleDb.Error();
				Utilits.Console.Log("[ОШИБКА] Обновление таблицы "+ tableRepresent + "! " + ex.Message.ToString(), false, true);
			}
		}
		
		int getTableIndex(String tableName)
		{
			if(tableName == "Users") return 0;
			if(tableName == "Counteragents") return 1;
			if(tableName == "Nomenclature") return 2;
			if(tableName == "Units") return 3;
			if(tableName == "PurchasePlan") return 3;
			return -1;
		}
		
		public void Dispose()
		{
			oleDb.Dispose();
			oleDbDataReader.Close();
			oleDbCommand.Dispose();
			oleDbConnection.Dispose();
			tables.Clear();
			tables = null;
		}
	}
}
