/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 25.02.2017
 * Time: 15:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data.OleDb;
using Aggregator.Data;

namespace Aggregator.Database.Local
{
	/// <summary>
	/// Description of QueryOleDb.
	/// </summary>
	public class QueryOleDb
	{
		private OleDbConnection oleDbConnection;
		private OleDbCommand oleDbCommand;
		
		public QueryOleDb(String fileBase)
		{
			oleDbConnection = new OleDbConnection();
			oleDbConnection.ConnectionString = DataConfig.oledbConnectLineBegin + fileBase + DataConfig.oledbConnectLineEnd + DataConfig.oledbConnectPass;
		}
		
		public void SetCommand(String sqlCommand)
		{
			oleDbCommand = new OleDbCommand(sqlCommand, oleDbConnection);
		}
		
		public void Execute()
		{
			try{
				oleDbConnection.Open();
				oleDbCommand.ExecuteNonQuery();	//выполнение запроса				
				oleDbConnection.Close();
			}catch(Exception ex){
				oleDbConnection.Close();
				Utilits.Console.Log("ОШИБКА ВЫПОЛНЕНИЯ ЗАПРОСА: " + ex.ToString());
			}
		}
		
	}
}
