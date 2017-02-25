/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 25.02.2017
 * Time: 15:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.Data.OleDb;
using Aggregator.Data;

namespace Aggregator.Database
{
	/// <summary>
	/// Description of OleDb2.
	/// </summary>
	public class OleDb2
	{
		private OleDbConnection oleDbConnection;
		private OleDbCommand oleDbCommandSelect;
		private OleDbCommand oleDbCommandInsert;
		private OleDbCommand oleDbCommandUpdate;
		private OleDbCommand oleDbCommandDelete;
		private OleDbDataAdapter oleDbDataAdapter;
		private String sqlCommandSelect;
		private String sqlCommandUpdate;
		private String sqlCommandInsert;
		private String sqlCommandDelete;
		public DataSet dataSet;
		
		public OleDb2(String fileBase)
		{
			oleDbConnection = new OleDbConnection();
			oleDbConnection.ConnectionString = DataConfig.oledbConnectLineBegin + fileBase + DataConfig.oledbConnectLineEnd + DataConfig.oledbConnectPass;
			oleDbCommandSelect = new OleDbCommand("", oleDbConnection);
			oleDbCommandInsert = new OleDbCommand("", oleDbConnection);
			oleDbCommandUpdate = new OleDbCommand("", oleDbConnection);
			oleDbCommandDelete = new OleDbCommand("", oleDbConnection);
			oleDbDataAdapter = new OleDbDataAdapter();
			dataSet = new DataSet();
		}
		
		//свойства ------------------------
		public String SelectCommand
		{
			get {return sqlCommandSelect;}
			set {sqlCommandSelect = value;}
		}
		
		public String InsertCommand
		{
			get {return sqlCommandInsert;}
			set {sqlCommandInsert = value;}
		}
		
		public String UpdateCommand
		{
			get {return sqlCommandUpdate;}
			set {sqlCommandUpdate = value;}
		}
		
		public String DeleteCommand
		{
			get {return sqlCommandDelete;}
			set {sqlCommandDelete = value;}
		}
		
		//методы --------------------------
		public void ExecuteFill(String tableName){
			try{
				oleDbConnection.Open();

				oleDbCommandSelect.CommandText = sqlCommandSelect;
				oleDbCommandInsert.CommandText = sqlCommandInsert;
				oleDbCommandUpdate.CommandText = sqlCommandUpdate;
				oleDbCommandDelete.CommandText = sqlCommandDelete;
				
				oleDbDataAdapter.SelectCommand = oleDbCommandSelect;
				oleDbDataAdapter.InsertCommand = oleDbCommandInsert;
				oleDbDataAdapter.UpdateCommand = oleDbCommandUpdate;
				oleDbDataAdapter.DeleteCommand = oleDbCommandDelete;
				oleDbDataAdapter.Fill(dataSet, tableName);
				
				oleDbConnection.Close();

			}catch(Exception ex){
				oleDbConnection.Close();
				Utilits.Console.Log("ОШИБКА ЗАГРУЗКИ ДАННЫХ " + ex.ToString(), false, true);
			}
		}
		
		public void ExecuteUpdate(String tableName){
			dataSet = new DataSet();
			dataSet.Clear();
			try{
				oleDbConnection.Open();

				oleDbCommandSelect.CommandText = sqlCommandSelect;
				oleDbCommandInsert.CommandText = sqlCommandInsert;
				oleDbCommandUpdate.CommandText = sqlCommandUpdate;
				oleDbCommandDelete.CommandText = sqlCommandDelete;				

				oleDbDataAdapter.SelectCommand = oleDbCommandSelect;
				oleDbDataAdapter.InsertCommand = oleDbCommandInsert;
				oleDbDataAdapter.UpdateCommand = oleDbCommandUpdate;
				oleDbDataAdapter.DeleteCommand = oleDbCommandDelete;
				oleDbDataAdapter.Update(dataSet, tableName);
				
				oleDbConnection.Close();

			}catch(Exception ex){
				oleDbConnection.Close();
				Utilits.Console.Log("ОШИБКА ОБНОВЛЕНИЯ ДАННЫХ " + ex.ToString(), false, true);
			}
		}
		
		public void SelectParametersAdd(String parameterName, OleDbType dbType, int size, String sourceColumn, UpdateRowSource urs)
		{
			oleDbCommandSelect.Parameters.Add(parameterName, dbType, size, sourceColumn);
			//oleDbCommandSelect.UpdatedRowSource = urs;
		}
		
		public void InsertParametersAdd(String parameterName, OleDbType dbType, int size, String sourceColumn, UpdateRowSource urs)
		{
			oleDbCommandInsert.Parameters.Add(parameterName, dbType, size, sourceColumn);
			//oleDbCommandInsert.UpdatedRowSource = urs;
		}
		
		public void UpdateParametersAdd(String parameterName, OleDbType dbType, int size, String sourceColumn, UpdateRowSource urs)
		{
			oleDbCommandUpdate.Parameters.Add(parameterName, dbType, size, sourceColumn);
			//oleDbCommandUpdate.UpdatedRowSource = urs;			
		}
		
		public void DeleteParametersAdd(String parameterName, OleDbType dbType, int size, String sourceColumn, UpdateRowSource urs)
		{
			oleDbCommandDelete.Parameters.Add(parameterName, dbType, size, sourceColumn);
			//oleDbCommandDelete.UpdatedRowSource = urs;
		}
		
		
		
		
	}
}
