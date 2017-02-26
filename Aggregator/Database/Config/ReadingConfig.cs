/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 25.02.2017
 * Time: 11:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using Aggregator.Data;
using Aggregator.Database.Local;

namespace Aggregator.Database.Config
{
	/// <summary>
	/// Description of ReadingConfig.
	/// </summary>
	public static class ReadingConfig
	{
		public static void ReadSettings()
		{
			OleDb oleDb;
			oleDb = new OleDb(DataConfig.configFile);
			try{
				oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Settings";
				oleDb.ExecuteFill("Settings");
				
				DataConfig.localDatabase = oleDb.dataSet.Tables["Settings"].Rows[0]["localDatabase"].ToString();
				DataConfig.typeDatabase = oleDb.dataSet.Tables["Settings"].Rows[0]["typeDatabase"].ToString();
				DataConfig.typeConnection = oleDb.dataSet.Tables["Settings"].Rows[0]["typeConnection"].ToString();
				DataConfig.server = oleDb.dataSet.Tables["Settings"].Rows[0]["server"].ToString();
				DataConfig.serverUser = oleDb.dataSet.Tables["Settings"].Rows[0]["serverUser"].ToString();
				DataConfig.serverDatabase = oleDb.dataSet.Tables["Settings"].Rows[0]["serverDatabase"].ToString();
				
				Utilits.Console.Log("Настройки соединения с базой данных успешно загружены.");
			}catch(Exception ex){
				oleDb.Error();
				MessageBox.Show(ex.ToString());
				Application.Exit();
			}
		}
	}
}
