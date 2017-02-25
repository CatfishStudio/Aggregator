/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 25.02.2017
 * Time: 13:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Aggregator.Data;

namespace Aggregator.Database
{
	/// <summary>
	/// Description of SavingConfig.
	/// </summary>
	public static class SavingConfig
	{
		public static void SaveSettings()
		{
			QueryOleDb query;
			query = new QueryOleDb(DataConfig.configFile);
			query.SetCommand("UPDATE Settings SET " +
			              "localDatabase='" + DataConfig.localDatabase + "', " + 
			              "typeDatabase='" + DataConfig.typeDatabase + "', " + 
			              "typeConnection='" + DataConfig.typeConnection + "', " + 
			              "server='" + DataConfig.server + "', " +
			              "serverUser='" + DataConfig.serverUser + "', " +
			              "serverDatabase='" + DataConfig.serverDatabase + "' WHERE ID = 0");
			query.Execute();
			Utilits.Console.Log("Новые настройки базы данных успешно сохранены.");
			ReadingConfig.ReadSettings();
		}
	}
}
