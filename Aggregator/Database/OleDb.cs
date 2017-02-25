/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 24.02.2017
 * Time: 7:53
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
	/// Description of OleDb.
	/// 
	/// OleDb - конструктор класса
	/// SetSelectCommand - создать команду запрос Select
	/// GetSelectCommand - получить команду запроса
	/// SetInsertCommand - создать команду запрос Insert
	/// GetInsertCommand - получить команду запроса
	/// SetUpdateCommand - создать команду запрос Update
	/// GetUpdateCommand - получить команду запроса
	/// SetDeleteCommand - создать команду запрос Delete
	/// GetDeleteCommand - получить команду запроса
	/// DataTableClear - очистить таблицу
	/// DataTableColumnAdd - добавить в таблицу колонку
	/// GetTables - получить коллекцию таблиц
	/// GetTable - получить таблицу
	/// GetValue - получить данные из таблицы
	/// Fill - заполнить адаптор данными в соответствии с запросами
	/// Error - в случае ошибки прервать соединение с базой данных
	/// Clear - очистить все переменные класса
	/// 
	/// </summary>
	public class OleDb
	{
		private OleDbConnection oleDbConnection;
		private OleDbCommand oleDbCommandSelect;
		private OleDbCommand oleDbCommandDelete;
		private OleDbCommand oleDbCommandUpdate;
		private OleDbCommand oleDbCommandInsert;
		private OleDbDataAdapter oleDbDataAdapter;
		private DataSet oleDbDataSet;
		private DataTable oleDbDataTable;
		
		public OleDb(String fileBase)
		{
			oleDbConnection = new OleDbConnection();
			oleDbConnection.ConnectionString = DataConfig.oledbConnectLineBegin + fileBase + DataConfig.oledbConnectLineEnd + DataConfig.oledbConnectPass;
			oleDbDataAdapter = new OleDbDataAdapter();
			oleDbDataSet = new DataSet();
			oleDbDataTable = new DataTable();
		}
		
		/* Команда Select */
		public void SetSelectCommand(String commandText)
		{
			oleDbCommandSelect = new OleDbCommand(commandText, oleDbConnection);
			oleDbDataAdapter.SelectCommand = oleDbCommandSelect;
		}
		
		public OleDbCommand GetSelectCommand()
		{
			return oleDbCommandSelect;
		}
		
		/* Команда Insert */
		public void SetInsertCommand(String commandText)
		{
			oleDbCommandInsert = new OleDbCommand(commandText, oleDbConnection);
			oleDbDataAdapter.InsertCommand = oleDbCommandInsert;
		}
		
		public OleDbCommand GetInsertCommand()
		{
			return oleDbCommandInsert;
		}
		
		/* Команда Update */
		public void SetUpdateCommand(String commandText)
		{
			oleDbCommandUpdate = new OleDbCommand(commandText, oleDbConnection);
			oleDbDataAdapter.UpdateCommand = oleDbCommandUpdate;
		}
		
		public OleDbCommand GetUpdateCommand(){
			return oleDbCommandUpdate;
		}
		
		/* Команда Delete */
		public void SetDeleteCommand(String commandText)
		{
			oleDbCommandDelete = new OleDbCommand(commandText, oleDbConnection);
			oleDbDataAdapter.DeleteCommand = oleDbCommandDelete;
		}
		
		public OleDbCommand GetDeleteCommand()
		{
			return oleDbCommandDelete;
		}
		
		/* Очистка таблицы */
		public void DataTableClear(bool caseSensitive = false)
		{
			oleDbDataTable.Clear();
			oleDbDataTable.CaseSensitive = caseSensitive;
		}
		
		/* Добавить колонку в таблицу */
		public void DataTableColumnAdd(String columnName, Type columnType)
		{
			oleDbDataTable.Columns.Add(columnName, columnType);
		}
		
		/* Получить все таблицы */
		public DataTableCollection GetTables()
		{
			return oleDbDataSet.Tables;
		}
		
		/* Получить таблицу */
		public DataTable GetTable(String tableName)
		{
			return oleDbDataSet.Tables[tableName];
		}
		
		/* Получить значение из таблицы */
		public object GetValue(String tableName, int index, String columnName)
		{
			return oleDbDataSet.Tables[tableName].Rows[index][columnName];
		}
		
		/* Изменить значение таблицы */
		public void EditValue(String tableName, int index, String columnName, object value)
		{
			//oleDbDataSet.Tables[tableName].Rows[index][columnName] = value;
		}
		
		public void NewValues(String tableName, int index, String columnName, DataRow newDataRow)
		{
			/*
			DataRow dataRow;
			dataRow = GetTable(tableName).NewRow();
			dataRow["fName"] = "John";
    		dataRow["lName"] = "Smith";
    		oleDbDataSet.Tables[tableName].Rows.Add(dataRow);
    		*/
		}
		
		
		/* Выполнить запрос */
		public void Fill(String tableName)
		{
			oleDbDataSet.Clear();
			oleDbDataSet.Tables.Add(oleDbDataTable);
			oleDbDataSet.DataSetName = tableName;
			
			oleDbConnection.Open(); //соединение с базой
			oleDbDataAdapter.Fill(oleDbDataSet, tableName);
			oleDbConnection.Close();//отключение соединения
		}
		
		public void Update()
		{
			oleDbConnection.Open(); //соединение с базой
			oleDbDataAdapter.Update(oleDbDataSet);
			oleDbConnection.Close();//отключение соединения
		}
		
		/* Отключание соединения в случае ошибки */
		public void Error()
		{
			oleDbConnection.Close();//отключение соединения
		}
		
		/* Очистка */
		public void Clear()
		{
			oleDbConnection.Close();
			oleDbConnection = null;
			oleDbCommandSelect = null;
			oleDbCommandDelete = null;
			oleDbCommandUpdate = null;
			oleDbCommandInsert = null;
			oleDbDataAdapter = null;
			oleDbDataSet.Clear();
			oleDbDataSet = null;
			oleDbDataTable.Clear();
			oleDbDataTable = null;
		}
	}
}
