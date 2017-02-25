/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 23.02.2017
 * Time: 10:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Aggregator.Data
{
	/// <summary>
	/// Description of DataConfig.
	/// </summary>
	public static class DataConfig
	{
		/* Программа */	
		public static String programPath = "";			// адрес программы
		public static String resource = "";				// адрес папки ресурсов
		public static String configFile = "";			// адрес и имя файла базы данных config.mdb
		/* Пользователь */
		public static String userName = "";				// имя
		public static String userPass = "";				// пароль
		public static String userPermissions = "";		// права
		
		/* Локальная база данных */		
		public static String oledbConnectLineBegin = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
		public static String oledbConnectLineEnd = ";Jet OLEDB:Database Password=";
		public static String oledbConnectPass = "12345";
		
		/* Настройки подключения к базе данных */		
		public static String localDatabase = "";		// адрес и имя файла базы данных database.mdb
		public static String typeConnection = "";		// тип подключения к базе данных (local/servel)
		public static String typeDatabase = "";			// тип провайдера данных (oledb/mssql)
		public static String server = "";				// адрес сервера MS SQLServer
		public static String serverUser = "";			// пользователь сервера MS SQLServer
		public static String serverDatabase = "";		// база данных на сервере MS SQLServer
	}
}
