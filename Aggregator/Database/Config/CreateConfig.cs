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
using System.Data.OleDb;
using Aggregator.Data;
using Aggregator.Database.Local;

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
				createDataBase = new CreateDatabase(DataConfig.configFile, DataConstants.TYPE_OLEDB);
			}catch(Exception ex){
				MessageBox.Show(ex.ToString(), "Ошибка:");
				Application.Exit();
			}
			
			CreateConfigTables.Create();
		}
	}
}
