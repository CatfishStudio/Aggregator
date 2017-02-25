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

namespace Aggregator.Database
{
	/// <summary>
	/// Description of ReadingConfig.
	/// </summary>
	public static class ReadingConfig
	{
		public static void ReadSettings()
		{
			OleDb oleDb;
			oleDb = new OleDb();
			try{
				oleDb.DataTableClear();
				oleDb.DataTableColumnAdd("name", Type.GetType("System.String"));
				oleDb.DataTableColumnAdd("localDatabase", Type.GetType("System.String"));
				oleDb.DataTableColumnAdd("typeDatabase", Type.GetType("System.String"));
				oleDb.DataTableColumnAdd("typeConnection", Type.GetType("System.String"));
				oleDb.DataTableColumnAdd("server", Type.GetType("System.String"));
				oleDb.DataTableColumnAdd("serverUser", Type.GetType("System.String"));
				oleDb.DataTableColumnAdd("serverDatabase", Type.GetType("System.String"));
				oleDb.SetSelectCommand("SELECT * FROM Settings");
				oleDb.Fill("Settings");
				
				DataConfig.localDatabase = oleDb.GetValue("Settings", 0, "localDatabase").ToString();
				DataConfig.typeDatabase = oleDb.GetValue("Settings", 0, "typeDatabase").ToString();
				DataConfig.typeConnection = oleDb.GetValue("Settings", 0, "typeConnection").ToString();
				DataConfig.server = oleDb.GetValue("Settings", 0, "server").ToString();
				DataConfig.serverUser = oleDb.GetValue("Settings", 0, "serverUser").ToString();
				DataConfig.serverDatabase = oleDb.GetValue("Settings", 0, "serverDatabase").ToString();
				
			}catch(Exception ex){
				oleDb.Error();
				MessageBox.Show(ex.ToString());
				Application.Exit();
			}
		}
	}
}
