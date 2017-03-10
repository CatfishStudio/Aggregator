/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 27.02.2017
 * Time: 9:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Aggregator.Data;
using Aggregator.Database.Local;

namespace Aggregator.Database.Base
{
	/// <summary>
	/// Description of CreateBaseTables.
	/// </summary>
	public static class CreateBaseTables
	{
		public static void TableUsers()
		{
			String sqlCommand;
			QueryOleDb query;
			query = new QueryOleDb(DataConfig.localDatabase);
			
			sqlCommand = "CREATE TABLE Users (" +
				"[id] COUNTER PRIMARY KEY, " +
				"[name] VARCHAR DEFAULT '' UNIQUE, " +
				"[pass] VARCHAR DEFAULT '', " +
				"[permissions] VARCHAR DEFAULT '', " +
				"[info] TEXT" +
				")";
			query.SetCommand(sqlCommand);
			if(!query.Execute()) Utilits.Console.Log("[ОШИБКА][CreateBaseTables:TableUsers] ошибка создания таблицы Users.", false, true);
			
			sqlCommand = "INSERT INTO Users (" +
				"[name], [pass], [permissions], [info]) " +
				"VALUES ('Администратор', '', 'admin', '')";
			query.SetCommand(sqlCommand);
			if(!query.Execute()) Utilits.Console.Log("[ОШИБКА][CreateBaseTables:TableUsers] ошибка добавления данных в таблицу Users.", false, true);
			
			sqlCommand = "INSERT INTO Users (" +
				"[name], [pass], [permissions], [info]) " +
				"VALUES ('Оператор', '', 'operator', '')";
			query.SetCommand(sqlCommand);
			if(!query.Execute()) Utilits.Console.Log("[ОШИБКА][CreateBaseTables:TableUsers] ошибка добавления данных в таблицу Users.", false, true);
			
			sqlCommand = "INSERT INTO Users (" +
				"[name], [pass], [permissions], [info]) " +
				"VALUES ('Пользователь', '', 'user', '')";
			query.SetCommand(sqlCommand);
			if(!query.Execute()) Utilits.Console.Log("[ОШИБКА][CreateBaseTables:TableUsers] ошибка добавления данных в таблицу Users.", false, true);
			
			sqlCommand = "INSERT INTO Users (" +
				"[name], [pass], [permissions], [info]) " +
				"VALUES ('Гость', '', 'guest', '')";
			query.SetCommand(sqlCommand);
			if(!query.Execute()) Utilits.Console.Log("[ОШИБКА][CreateBaseTables:TableUsers] ошибка добавления данных в таблицу Users.", false, true);
			
			query.Dispose();
		}
		
		public static void TableCounteragents()
		{
			String sqlCommand;
			QueryOleDb query;
			query = new QueryOleDb(DataConfig.localDatabase);
			
			sqlCommand = "CREATE TABLE Counteragents (" +
				"[id] COUNTER PRIMARY KEY, " +
				"[type] VARCHAR DEFAULT '', " +
				"[organization_name] VARCHAR DEFAULT '' UNIQUE, " +
				"[organization_address] VARCHAR DEFAULT '', " +
				"[organization_phone] VARCHAR DEFAULT '', " +
				"[organization_site] VARCHAR DEFAULT '', " +
				"[organization_email] VARCHAR DEFAULT '', " +
				"[contact_fullname] VARCHAR DEFAULT '', " +
				"[contact_post] VARCHAR DEFAULT '', " +
				"[contact_phone] VARCHAR DEFAULT '', " +
				"[contact_skype] VARCHAR DEFAULT '', " +
				"[contact_email] VARCHAR DEFAULT '', " +
				"[information] TEXT DEFAULT '', " +
				"[excel_filename] TEXT DEFAULT '', " +
				"[excel_date] VARCHAR DEFAULT '', " +
				"[excel_column_name] INTEGER DEFAULT 0, " +
				"[excel_column_code] INTEGER DEFAULT 0, " +
				"[excel_column_series] INTEGER DEFAULT 0, " +
				"[excel_column_article] INTEGER DEFAULT 0, " +
				"[excel_column_remainder] INTEGER DEFAULT 0, " +
				"[excel_column_manufacturer] INTEGER DEFAULT 0, " +
				"[excel_column_price] INTEGER DEFAULT 0, " +
				"[excel_column_discount_1] INTEGER DEFAULT 0, " +
				"[excel_column_discount_2] INTEGER DEFAULT 0, " +
				"[excel_column_discount_3] INTEGER DEFAULT 0, " +
				"[excel_column_discount_4] INTEGER DEFAULT 0, " +
				"[excel_column_term] INTEGER DEFAULT 0, " +
				"[excel_table_id] VARCHAR DEFAULT '', " +
				"[parent] VARCHAR DEFAULT ''" +
				")";
			query.SetCommand(sqlCommand);
			if(!query.Execute()) Utilits.Console.Log("[ОШИБКА][CreateBaseTables:TableCounteragents] ошибка создания таблицы Counteragents.", false, true);
		}
		
		public static void TableHistory()
		{
			String sqlCommand;
			QueryOleDb query;
			query = new QueryOleDb(DataConfig.localDatabase);
			
			sqlCommand = "CREATE TABLE History (" +
				"[id] COUNTER PRIMARY KEY, " +
				"[name] VARCHAR DEFAULT '' UNIQUE, " +
				"[represent] VARCHAR DEFAULT '', " +
				"[datetime] VARCHAR DEFAULT '', " +
				"[error] VARCHAR DEFAULT '', " +
				"[user] VARCHAR DEFAULT ''" +
				")";
			query.SetCommand(sqlCommand);
			if(!query.Execute()) Utilits.Console.Log("[ОШИБКА][CreateBaseTables:TableHistory] ошибка создания таблицы History.", false, true);
			
			sqlCommand = "INSERT INTO History (" +
				"[name], [represent], [datetime], [error], [user]) " +
				"VALUES ('Users', 'Пользователи', '" + DateTime.Now.ToString() + "', '', '" + DataConfig.userName + "')";
			query.SetCommand(sqlCommand);
			if(!query.Execute()) Utilits.Console.Log("[ОШИБКА][CreateBaseTables:TableHistory] ошибка добавления данных в таблицу History.", false, true);
			
			sqlCommand = "INSERT INTO History (" +
				"[name], [represent], [datetime], [error], [user]) " +
				"VALUES ('Counteragents', 'Контрагенты', '" + DateTime.Now.ToString() + "', '', '" + DataConfig.userName + "')";
			query.SetCommand(sqlCommand);
			if(!query.Execute()) Utilits.Console.Log("[ОШИБКА][CreateBaseTables:TableHistory] ошибка добавления данных в таблицу History.", false, true);
		}
	}
}
