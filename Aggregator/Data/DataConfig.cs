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
		// адрес программы
		public static String programPath = "";
		// папка ресурсов
		public static String resource = "";
		// файл локальной базы
		public static String configFile = "";
		public static String oledbFileBase = "";
		// OleDb подключение:
		public static String oledbConnectLineBegin = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
		public static String oledbConnectLineEnd = ";Jet OLEDB:Database Password=";
		public static String oledbConnectPass = "12345";
		// Информация пользователя
		public static String userName = "";
		public static String userPass = "";
		public static String userPermissions = "";
	}
}
