/*
 * Создано в SharpDevelop.
 * Пользователь: Catfish
 * Дата: 23.02.2017
 * Время: 21:08
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Data;
using System.Data.OleDb;
using Aggregator.Data;

namespace Aggregator.Data
{
	/// <summary>
	/// Description of DataOleDb.
	/// </summary>
	public class DataOleDb
	{
		private OleDbConnection oleDbConnection;
		private OleDbCommand oleDbCommandSelect;
		private OleDbCommand oleDbCommandDelete;
		private OleDbCommand oleDbCommandUpdate;
		private OleDbCommand oleDbCommandInsert;
		private OleDbDataAdapter oleDbDataAdapter;
		private DataSet oleDbDataSet;
		private DataTable oleDbDataTable;
		
		public DataOleDb()
		{
			oleDbConnection = new OleDbConnection();
			oleDbConnection.ConnectionString = DataConfig.oledbConnectLineBegin + DataConfig.configFile + DataConfig.oledbConnectLineEnd + DataConfig.oledbConnectPass;
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
		public void DataTableAdd(String columnName, Type columnType)
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
