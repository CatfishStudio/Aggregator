/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 26.02.2017
 * Time: 12:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data.OleDb;
using Aggregator.Data;
using Aggregator.Database.Local;

namespace Aggregator.Database.Config
{
	/// <summary>
	/// Description of CreateConfigTables.
	/// </summary>
	public static class CreateConfigTables
	{
		public static void TableUsers()
		{
			String sqlCommand;
			QueryOleDb query;
			query = new QueryOleDb(DataConfig.configFile);
			
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
		
		public static void TableDatabaseSettings()
		{
			DataConfig.localDatabase = DataConfig.resource + "\\database.mdb";
			DataConfig.typeConnection = DataConstants.CONNETION_LOCAL;
			DataConfig.typeDatabase = DataConstants.TYPE_OLEDB;
			DataConfig.server = "localhost";
			DataConfig.serverUser = "sa";
			DataConfig.serverDatabase = "database";
			
			String sqlCommand;
			QueryOleDb query;
			query = new QueryOleDb(DataConfig.configFile);
			
			sqlCommand = "CREATE TABLE DatabaseSettings (" +
				"[id] COUNTER PRIMARY KEY, " +
				"name VARCHAR(50) DEFAULT '' UNIQUE, " +
				"localDatabase TEXT DEFAULT '', " +
				"typeDatabase VARCHAR(50) DEFAULT '', " +
				"typeConnection VARCHAR(50) DEFAULT '', " +
				"server VARCHAR(255) DEFAULT '', " +
				"serverUser VARCHAR(255) DEFAULT '', " +
				"serverDatabase VARCHAR(255) DEFAULT ''" +
				")";
			query.SetCommand(sqlCommand);
			query.Execute();
			
			sqlCommand = "INSERT INTO DatabaseSettings (" +
				"[name], [localDatabase], [typeDatabase], [typeConnection], " +
				"[server], [serverUser], [serverDatabase]) " +
				"VALUES ('database', '" + DataConfig.localDatabase + "', '" 
				+ DataConfig.typeDatabase +"', '" + DataConfig.typeConnection + "', '"
				+ DataConfig.server + "', '" + DataConfig.serverUser + "', '" 
				+ DataConfig.serverDatabase + "')";
			query.SetCommand(sqlCommand);
			query.Execute();
			query.Dispose();
		}
		
		public static void TableSettings()
		{
			String sqlCommand;
			QueryOleDb query;
			query = new QueryOleDb(DataConfig.configFile);
			
			sqlCommand = "CREATE TABLE Settings (" +
				"[id] COUNTER PRIMARY KEY, " +
				"[autoUpdate] VARCHAR(10) DEFAULT '', " +
				"[period] VARCHAR(25) DEFAULT ''" +
				")";
			query.SetCommand(sqlCommand);
			query.Execute();
			
			sqlCommand = "INSERT INTO Settings (" +
				"[autoUpdate], [period]) " +
				"VALUES ('true', 'last_day')";
			query.SetCommand(sqlCommand);
			query.Execute();
		}
	}
}
