/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 14.03.2017
 * Время: 9:46
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Aggregator.Data;

namespace Aggregator.Database.Server
{
	/// <summary>
	/// Description of HistoryRefreshSqlServer.
	/// </summary>
	public struct Table {
			public String name;
			public String represent;
			public String datetime;
			public String error;
			public String user;
	}
	
	public class HistoryRefreshSqlServer
	{
		List<Table> tables;
		SqlServer sqlServer;
		
		public HistoryRefreshSqlServer()
		{
			sqlServer = new SqlServer();
			sqlServer.sqlCommandSelect.CommandText = "SELECT [id], [name], [represent], [datetime], [error], [user] FROM History";
			sqlServer.sqlCommandUpdate.CommandText = "UPDATE History SET " +
					"[name] = @name, " +
					"[represent] = @represent, " +
					"[datetime] = @datetime, " +
					"[error] = @error, " +
					"[user] = @user " +
					"WHERE ([id] = @id)";
			sqlServer.sqlCommandUpdate.Parameters.Add("@name", SqlDbType.VarChar, 255, "name");
			sqlServer.sqlCommandUpdate.Parameters.Add("@represent", SqlDbType.VarChar, 255, "represent");
			sqlServer.sqlCommandUpdate.Parameters.Add("@datetime", SqlDbType.VarChar, 255, "datetime");
			sqlServer.sqlCommandUpdate.Parameters.Add("@error", SqlDbType.VarChar, 255, "error");
			sqlServer.sqlCommandUpdate.Parameters.Add("@user", SqlDbType.VarChar, 255, "user");
			sqlServer.sqlCommandUpdate.Parameters.Add("@id", SqlDbType.Int, 10, "id");
			if(sqlServer.ExecuteFill("History")){
				tables = new List<Table>();
				Table table;
				foreach(DataRow row in sqlServer.dataSet.Tables["History"].Rows)
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
		
		public void MonitoringStart()
		{
			if(sqlServer.dataSet.Tables.Count == 0) return;
			
			SqlDependency.Start(DataConfig.serverConnection);
			using (SqlCommand command = new SqlCommand("SELECT [id], [name], [represent], [datetime], [error], [user] FROM History", sqlServer.sqlConnection))
    		{
        		SqlDependency dependency=new SqlDependency(command);
        		dependency.OnChange += new OnChangeEventHandler(OnDependencyChange);
		        using (SqlDataReader reader = command.ExecuteReader())
		        {
		        	Utilits.Console.Log("[MonitoringStart] " + reader.ToString());
		        }
			}

		}
		
		void OnDependencyChange(object sender, SqlNotificationEventArgs e)
		{
			Utilits.Console.Log("[OnDependencyChange] " + e.ToString());
		}

		public void MonitoringStop()
		{
			SqlDependency.Stop(DataConfig.serverConnection);
		}
	}
}
