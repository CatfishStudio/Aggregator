/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 27.02.2017
 * Time: 10:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Aggregator.Database.Local;

namespace Aggregator.Data
{
	/// <summary>
	/// Description of DataServerUpdate.
	/// </summary>
	
	public struct Table {
			public String name;
			public String represent;
			public String datetime;
			public String error;
			public String user;
	}
	
	public class DataServerUpdate
	{
		private OleDb oleDb;
		private List<Table> tables;
		
		public DataServerUpdate()
		{
			oleDb = new OleDb(DataConfig.localDatabase);
			oleDb.oleDbCommandSelect.CommandText = "SELECT id, name, represent, datetime, error, user FROM History";
			oleDb.oleDbCommandUpdate.CommandText = "UPDATE History SET " +
					"name = @name, " +
					"represent = @represent, " +
					"datetime = @datetime, " +
					"error = @error, " +
					"user = @user " +
					"WHERE (id = @id)";
			oleDb.oleDbCommandUpdate.Parameters.Add("@name", OleDbType.VarChar, 255, "name");
			oleDb.oleDbCommandUpdate.Parameters.Add("@represent", OleDbType.VarChar, 255, "represent");
			oleDb.oleDbCommandUpdate.Parameters.Add("@datetime", OleDbType.VarChar, 255, "datetime");
			oleDb.oleDbCommandUpdate.Parameters.Add("@error", OleDbType.VarChar, 255, "error");
			oleDb.oleDbCommandUpdate.Parameters.Add("@user", OleDbType.VarChar, 255, "user");
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
				Utilits.Console.Log("ПРЕДУПРЕЖДЕНИЕ: Служба истории обновлений базы данных не запущена!!!");
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
				Utilits.Console.Log("ПРЕДУПРЕЖДЕНИЕ: Служба истории обновлений базы данных не удалось получить обновленные данные!");
			}
		}
		
		/* Отметить обновление данных в базе данных */
		public void update(int index)
		{
			oleDb.dataSet.Tables["History"].Rows[index]["datetime"] = DateTime.Now.ToString();
			oleDb.ExecuteUpdate("History");
		}
		
		/* Обновить таблицы новыми данными */
		private void refresh(String tableName, String tableRepresent)
		{
			try{
				if(tableName == "Users" && DataForms.FUsers != null) DataForms.FUsers.TableRefresh();
			
				Utilits.Console.Log("ИСТОРИЯ: Таблица " + tableRepresent + " была успешно обновлена.");
			}catch(Exception ex){
				oleDb.Error();
				Utilits.Console.Log("ОШИБКА: Обновление таблицы "+ tableRepresent + "! " + ex.ToString(), false, true);
			}
		}
		
		public void Dispose()
		{
			oleDb.Dispose();
			tables.Clear();
			tables = null;
		}
	}
}
