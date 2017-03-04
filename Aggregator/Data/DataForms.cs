/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 23.02.2017
 * Time: 10:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Aggregator.Admin;
using Aggregator.Client;
using Aggregator.Client.Directories;
using Aggregator.Client.Settings;
using Aggregator.User;

namespace Aggregator.Data
{
	/// <summary>
	/// Description of DataForms.
	/// </summary>
	public static class DataForms
	{
		public static MainForm FMain;
		public static FormCheckUser FCheckUser;
		/* Клиент */
		public static FormClient FClient;
		public static FormSettings FSettings;
		public static FormConstants FConstants;
		/*Администратор*/
		public static FormSettingsDatabase FSettingsDatabase;
		public static FormConsoleQuery FConsoleQuery;
		public static FormUsers FUsers;
	}
}
