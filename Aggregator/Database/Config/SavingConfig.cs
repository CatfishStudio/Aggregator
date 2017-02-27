/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 25.02.2017
 * Time: 13:40
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
	/// Description of SavingConfig.
	/// </summary>
	public static class SavingConfig
	{
		public static void SaveSettings()
		{
			OleDb oleDb;
			oleDb = new OleDb(DataConfig.configFile);
			try{
				oleDb.oleDbCommandSelect.CommandText = "SELECT id, name, localDatabase, typeDatabase, typeConnection, server, serverUser, serverDatabase FROM DatabaseSettings";
				oleDb.oleDbCommandUpdate.CommandText = "UPDATE DatabaseSettings SET " +
					"name = @name, " +
					"localDatabase = @localDatabase, " +
					"typeDatabase = @typeDatabase, " +
					"typeConnection = @typeConnection, " +
					"server = @server, " +
					"serverUser = @serverUser, " +
					"serverDatabase = @serverDatabase " +
					"WHERE (id = @id)";
				oleDb.oleDbCommandUpdate.Parameters.Add("@name", OleDbType.VarChar, 255, "name");
				oleDb.oleDbCommandUpdate.Parameters.Add("@localDatabase", OleDbType.VarChar, 255, "localDatabase");
				oleDb.oleDbCommandUpdate.Parameters.Add("@typeDatabase", OleDbType.VarChar, 255, "typeDatabase");
				oleDb.oleDbCommandUpdate.Parameters.Add("@typeConnection", OleDbType.VarChar, 255, "typeConnection");
				oleDb.oleDbCommandUpdate.Parameters.Add("@server", OleDbType.VarChar, 255, "server");
				oleDb.oleDbCommandUpdate.Parameters.Add("@serverUser", OleDbType.VarChar, 255, "serverUser");
				oleDb.oleDbCommandUpdate.Parameters.Add("@serverDatabase", OleDbType.VarChar, 255, "serverDatabase");
				oleDb.oleDbCommandUpdate.Parameters.Add("@id", OleDbType.Integer, 10, "id");
				oleDb.ExecuteFill("DatabaseSettings");
				
				oleDb.dataSet.Tables["DatabaseSettings"].Rows[0]["localDatabase"] = DataConfig.localDatabase;
				oleDb.dataSet.Tables["DatabaseSettings"].Rows[0]["typeDatabase"] = DataConfig.typeDatabase;
				oleDb.dataSet.Tables["DatabaseSettings"].Rows[0]["typeConnection"] = DataConfig.typeConnection;
				oleDb.dataSet.Tables["DatabaseSettings"].Rows[0]["server"] = DataConfig.server;
				oleDb.dataSet.Tables["DatabaseSettings"].Rows[0]["serverUser"] = DataConfig.serverUser;
				oleDb.dataSet.Tables["DatabaseSettings"].Rows[0]["serverDatabase"] = DataConfig.serverDatabase;
				oleDb.ExecuteUpdate("DatabaseSettings");
				
				Utilits.Console.Log("Сохранение настроек соединения с базой данных прошло успешно.");
			}catch(Exception ex){
				oleDb.Error();
				Utilits.Console.Log("ОШИБКА: Сохранение настроек соединения с базой данных. " + ex.ToString(), false, true);
			}
		}
	}
}
