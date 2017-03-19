﻿/*
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
		public static void ReadDatabaseSettings()
		{
			OleDb oleDb;
			oleDb = new OleDb(DataConfig.configFile);
			try{
				oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM DatabaseSettings";
				oleDb.ExecuteFill("DatabaseSettings");
				
				DataConfig.localDatabase = oleDb.dataSet.Tables["DatabaseSettings"].Rows[0]["localDatabase"].ToString();
				DataConfig.typeDatabase = oleDb.dataSet.Tables["DatabaseSettings"].Rows[0]["typeDatabase"].ToString();
				DataConfig.typeConnection = oleDb.dataSet.Tables["DatabaseSettings"].Rows[0]["typeConnection"].ToString();
				DataConfig.serverConnection = oleDb.dataSet.Tables["DatabaseSettings"].Rows[0]["serverConnection"].ToString();
				oleDb.Dispose();
				Utilits.Console.Log("Настройки соединения с базой данных успешно загружены.");
			}catch(Exception ex){
				oleDb.Error();
				MessageBox.Show("[ReadDatabaseSettings]: " + ex.ToString(), "Ошибка");
				Application.Exit();
			}
		}
		
		public static void ReadSettings()
		{
			OleDb oleDb;
			oleDb = new OleDb(DataConfig.configFile);
			try{
				oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM Settings";
				oleDb.ExecuteFill("Settings");
				
				if(oleDb.dataSet.Tables["Settings"].Rows[0]["autoUpdate"].ToString() == "True") DataConfig.autoUpdate = true;
				else DataConfig.autoUpdate = false;
				DataConfig.period = oleDb.dataSet.Tables["Settings"].Rows[0]["period"].ToString();
				oleDb.Dispose();
				Utilits.Console.Log("[Настройки]: Настройки программы успешно загружены.");
			}catch(Exception ex){
				oleDb.Error();
				MessageBox.Show("[Настройки]: " + ex.ToString(), "Ошибка");
				Application.Exit();
			}
		}
	}
}
