/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 27.02.2017
 * Time: 9:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Aggregator.Data;
using Aggregator.Database.Local;

namespace Aggregator.Database.Base
{
	/// <summary>
	/// Description of CreateBaseTables.
	/// </summary>
	public static class CreateBaseTables
	{
		public static void TableUsers()
		{
			String sqlCommand;
			QueryOleDb query;
			query = new QueryOleDb(DataConfig.localDatabase);
			
			sqlCommand = "CREATE TABLE Users (" +
				"[id] COUNTER PRIMARY KEY, " +
				"[name] VARCHAR DEFAULT '' UNIQUE, " +
				"[pass] VARCHAR DEFAULT '', " +
				"[permissions] VARCHAR DEFAULT 'user'" +
				")";
			query.SetCommand(sqlCommand);
			query.Execute();
			
			sqlCommand = "INSERT INTO Users (" +
				"[name], [pass], [permissions]) " +
				"VALUES ('Администратор', '', 'admin')";
			query.SetCommand(sqlCommand);
			query.Execute();
			
			sqlCommand = "INSERT INTO Users (" +
				"[name], [pass], [permissions]) " +
				"VALUES ('Пользователь', '', 'user')";
			query.SetCommand(sqlCommand);
			query.Execute();
			query.Dispose();
		}
		
		public static void TableHistory()
		{
			String sqlCommand;
			QueryOleDb query;
			query = new QueryOleDb(DataConfig.localDatabase);
			
			sqlCommand = "CREATE TABLE History (" +
				"[id] COUNTER PRIMARY KEY, " +
				"[name] VARCHAR DEFAULT '' UNIQUE, " +
				"[represent] VARCHAR DEFAULT '', " +
				"[datetime] VARCHAR DEFAULT '', " +
				"[error] VARCHAR DEFAULT '', " +
				"[user] VARCHAR DEFAULT ''" +
				")";
			query.SetCommand(sqlCommand);
			query.Execute();
			
			sqlCommand = "INSERT INTO History (" +
				"[name], [represent], [datetime], [error], [user]) " +
				"VALUES ('Users', 'Пользователи', '" + DateTime.Now.ToString() + "', '', '" + DataConfig.userName + "')";
			query.SetCommand(sqlCommand);
			query.Execute();
		}
	}
}
