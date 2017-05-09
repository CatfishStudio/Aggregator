/*
 * Создано в SharpDevelop.
 * Пользователь: Somov Studio
 * Дата: 09.05.2017
 * Время: 6:58
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Windows.Forms;
using Aggregator.Data;
using Microsoft.Win32;

namespace Aggregator.Trial
{
	/// <summary>
	/// Description of CheckTrial.
	/// </summary>
	public class CheckTrial
	{
		
		public CheckTrial()
		{
			try{
				RegistryKey currentUserKey = Registry.CurrentUser;
				RegistryKey aggregatorKey = currentUserKey.OpenSubKey("Aggregator");
				if(aggregatorKey == null){
					aggregatorKey = currentUserKey.CreateSubKey("Aggregator");
					aggregatorKey.SetValue("datetime", DateTime.Today.ToString("dd.MM.yyyy"));
					aggregatorKey.SetValue("activated", "0");
				}
				
				DataConfig.dateStart = aggregatorKey.GetValue("datetime").ToString();
				DataConfig.activated = aggregatorKey.GetValue("activated").ToString();
				aggregatorKey.Close();
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Ошибка");
				Application.Exit();
			}
		}
		
		public bool Check()
		{
			if(DataConfig.activated == "0"){
				FormTrial FTrial = new FormTrial();
				FTrial.ShowDialog();
				return true;
			}else{
				return true;
			}
		}
		
	}
}
