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
		public static void Create()
		{
			String sqlCommand;
			QueryOleDb query;
			query = new QueryOleDb(DataConfig.configFile);
			
			/* Таблица USERS */			
			sqlCommand = "CREATE TABLE Users (" +
				"[ID] COUNTER PRIMARY KEY, " +
				"[Name] VARCHAR DEFAULT '' UNIQUE, " +
				"[Pass] VARCHAR DEFAULT '', " +
				"[Permissions] VARCHAR DEFAULT 'user'" +
				")";
			query.SetCommand(sqlCommand);
			query.Execute();
			
			sqlCommand = "INSERT INTO Users (" +
				"[Name], [Pass], [Permissions]) " +
				"VALUES ('Администратор', '', 'admin')";
			query.SetCommand(sqlCommand);
			query.Execute();
			
			sqlCommand = "INSERT INTO Users (" +
				"[Name], [Pass], [Permissions]) " +
				"VALUES ('Пользователь', '', 'user')";
			query.SetCommand(sqlCommand);
			query.Execute();
			
			/* Таблица SETTINGS */
			DataConfig.localDatabase = DataConfig.resource + "\\database.mdb";
			DataConfig.typeConnection = DataConstants.CONNETION_LOCAL;
			DataConfig.typeDatabase = DataConstants.TYPE_OLEDB;
			DataConfig.server = "localhost";
			DataConfig.serverUser = "sa";
			DataConfig.serverDatabase = "database";
			
			sqlCommand = "CREATE TABLE Settings (" +
				"[ID] COUNTER PRIMARY KEY, " +
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
			
			sqlCommand = "INSERT INTO Settings (" +
				"[name], [localDatabase], [typeDatabase], [typeConnection], " +
				"[server], [serverUser], [serverDatabase]) " +
				"VALUES ('database', '" + DataConfig.localDatabase + "', '" 
				+ DataConfig.typeDatabase +"', '" + DataConfig.typeConnection + "', '"
				+ DataConfig.server + "', '" + DataConfig.serverUser + "', '" 
				+ DataConfig.serverDatabase + "')";
			query.SetCommand(sqlCommand);
			query.Execute();

		}
	}
}
