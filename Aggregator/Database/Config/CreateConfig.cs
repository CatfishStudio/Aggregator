/*
 * Создано в SharpDevelop.
 * Пользователь: Catfish
 * Дата: 25.02.2017
 * Время: 10:52
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Windows.Forms;
using Aggregator.Data;

namespace Aggregator.Database.Config
{
	/// <summary>
	/// Description of CreateConfig.
	/// </summary>
	public static class CreateConfig
	{
		public static void Create()
		{
			/* Создание файла базы данных */
			CreateDatabase createDataBase;
			try{
				DataConfig.oledbConnectLineBegin = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
				DataConfig.oledbConnectLineEnd = ";Jet OLEDB:Database Password=";
				DataConfig.oledbConnectPass = "12345";
				createDataBase = new CreateDatabase(DataConfig.configFile, DataConstants.TYPE_OLEDB);
			
			}catch(Exception ex){
				MessageBox.Show(ex.ToString(), "Ошибка:");
				Application.Exit();
			}
			/* Создание таблиц */
			CreateConfigTables.TableDatabaseSettings();
			CreateConfigTables.TableSettings();
		}
	}
}
