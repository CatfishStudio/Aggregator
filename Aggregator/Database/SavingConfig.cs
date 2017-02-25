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
			              "serverUser='" + DataConfig.server + "', " +
			              "serverDatabase='" + DataConfig.server + "' WHERE ID = 0");
			query.Execute();
			Utilits.Console.Log("Новые настройки базы данных успешно сохранены.");
			ReadingConfig.ReadSettings();
			
			/*
			OleDb oleDb;
			oleDb = new OleDb();
			try{
				oleDb.DataTableClear();
				oleDb.DataTableColumnAdd("ID", Type.GetType("System.Int32"));
				oleDb.DataTableColumnAdd("name", Type.GetType("System.String"));
				oleDb.DataTableColumnAdd("localDatabase", Type.GetType("System.String"));
				oleDb.DataTableColumnAdd("typeDatabase", Type.GetType("System.String"));
				oleDb.DataTableColumnAdd("typeConnection", Type.GetType("System.String"));
				oleDb.DataTableColumnAdd("server", Type.GetType("System.String"));
				oleDb.DataTableColumnAdd("serverUser", Type.GetType("System.String"));
				oleDb.DataTableColumnAdd("serverDatabase", Type.GetType("System.String"));
				oleDb.SetSelectCommand("SELECT * FROM Settings");				
				oleDb.SetInsertCommand("INSERT INTO Settings ([ID], [name], [localDatabase], [typeDatabase], " +
				                       "[typeConnection],[server],[serverUser],[serverDatabase]) VALUES (?, ?, ?, ?, ?, ?, ?, ?)");
				oleDb.SetUpdateCommand("UPDATE Settings SET [ID] = ?, [name] = ?, [localDatabase] = ?, [typeDatabase] = ?, " +
				                       "[typeConnection] = ?, [server] = ?, [serverUser] = ?, [serverDatabase] = ? " +
				                       "WHERE ([ID] = ?) " +
		                               "AND (name = ? OR ? IS NULL AND name IS NULL) " +
		                               "AND (localDatabase = ? OR ? IS NULL AND localDatabase IS NULL) " +
		                               "AND (typeDatabase = ? OR ? IS NULL AND typeDatabase IS NULL) " +
		                               "AND (typeConnection = ? OR ? IS NULL AND typeConnection IS NULL) " +
		                               "AND (server = ? OR ? IS NULL AND server IS NULL) " +
		                               "AND (serverUser = ? OR ? IS NULL AND serverUser IS NULL) " +
		                               "AND (serverDatabase = ? OR ? IS NULL AND serverDatabase IS NULL)");
				oleDb.SetDeleteCommand("DELETE FROM Settings WHERE ([ID] = ?) " +
	                                   "AND (name = ? OR ? IS NULL AND name IS NULL) " +
		                               "AND (localDatabase = ? OR ? IS NULL AND localDatabase IS NULL) " +
		                               "AND (typeDatabase = ? OR ? IS NULL AND typeDatabase IS NULL) " +
		                               "AND (typeConnection = ? OR ? IS NULL AND typeConnection IS NULL) " +
		                               "AND (server = ? OR ? IS NULL AND server IS NULL) " +
		                               "AND (serverUser = ? OR ? IS NULL AND serverUser IS NULL) " +
	                                   "AND (serverDatabase = ? OR ? IS NULL AND serverDatabase IS NULL)");
				oleDb.Fill("Settings");
				
				oleDb.EditValue("Settings", 0, "localDatabase", DataConfig.localDatabase);
				oleDb.EditValue("Settings", 0, "typeDatabase", DataConfig.typeDatabase);
				oleDb.EditValue("Settings", 0, "typeConnection", DataConfig.typeConnection);
				oleDb.EditValue("Settings", 0, "server", DataConfig.server);
				oleDb.EditValue("Settings", 0, "serverUser", DataConfig.serverUser);
				oleDb.EditValue("Settings", 0, "serverDatabase", DataConfig.serverDatabase);
				oleDb.Update("Settings");				
				Utilits.Console.Log("Новые настройки базы данных успешно сохранены.");
				
			}catch(Exception ex){
				oleDb.Error();
				Utilits.Console.Log("ОШИБКА СОХРАНЕНИЯ ДАННЫХ: " + ex.ToString(), false, true);
			}
			*/
			
		}
	}
}
