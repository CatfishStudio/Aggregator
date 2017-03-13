/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 11.03.2017
 * Время: 16:29
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Aggregator.Data;
using Aggregator.Database.Local;


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
		List<Table> tables;
		
		public HistoryRefreshOleDb()
		{
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
			if(oleDb.ExecuteFill("History") == true){
				tables = new List<Table>();
				Table table;
				foreach(DataRow row in oleDb.dataSet.Tables["History"].Rows)
				{
					table.name = row["name"].ToString();
					table.represent = row["represent"].ToString();
					table.datetime = row["datetime"].ToString();
					table.error = row["error"].ToString();
					table.user = row["user"].ToString();
					tables.Add(table);
				}
			}else{
				Utilits.Console.Log("[ПРЕДУПРЕЖДЕНИЕ] Служба истории обновлений базы данных не запущена!!!");
			}
		}
		
		/* Проверка обновленных таблиц в базе данных */
		public void check()
		{
			oleDb.dataSet.Clear();
			if(oleDb.ExecuteFill("History") == true){
				Table table;
				for(int i = 0; i < tables.Count; i++){
					if(tables[i].datetime != oleDb.dataSet.Tables["History"].Rows[i]["datetime"].ToString()){
						refresh(tables[i].name, tables[i].represent);
						table = tables[i];
						table.datetime = oleDb.dataSet.Tables["History"].Rows[i]["datetime"].ToString();
						tables[i] = table;
					}
				}
			}else{
				Utilits.Console.Log("[ПРЕДУПРЕЖДЕНИЕ] Служба истории обновлений базы данных не удалось получить обновленные данные!");
			}
		}
		
		/* Отметить обновление данных в базе данных */
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
			return -1;
		}
		
		public void Dispose()
		{
			oleDb.Dispose();
			tables.Clear();
			tables = null;
		}
	}
}
