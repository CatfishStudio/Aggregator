/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 02.03.2017
 * Time: 18:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Aggregator.Data;

namespace Aggregator.Database.Server
{
	/// <summary>
	/// Description of QuerySqlServer.
	/// </summary>
	public class QuerySqlServer
	{
		SqlConnection sqlConnection;
		SqlCommand sqlCommand;		
		
		public QuerySqlServer()
		{
			sqlConnection = new SqlConnection();
			sqlConnection.ConnectionString = "server=" + DataConfig.server + ";uid=" + DataConfig.serverUser + ";database=" + DataConfig.serverDatabase;
		}
		
		public void SetCommand(String sqlTextCommand)
		{
			sqlCommand = new SqlCommand(sqlTextCommand, sqlConnection);
		}
		
		public void Execute()
		{
			try{
				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();	//выполнение запроса				
				sqlConnection.Close();
			}catch(Exception ex){
				sqlConnection.Close();
				Utilits.Console.Log("ОШИБКА ВЫПОЛНЕНИЯ ЗАПРОСА: " + ex.ToString());
			}
		}
		
		public void Dispose()
		{
			sqlCommand.Dispose();
			sqlConnection.Dispose();
		}
	}
}
