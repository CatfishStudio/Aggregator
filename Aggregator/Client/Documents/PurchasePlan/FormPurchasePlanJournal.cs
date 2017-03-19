/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 19.03.2017
 * Время: 10:27
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Data;
using Aggregator.Database.Local;
using Aggregator.Database.Server;

namespace Aggregator.Client.Documents.PurchasePlan
{
	/// <summary>
	/// Description of FormPurchasePlanJournal.
	/// </summary>
	public partial class FormPurchasePlanJournal : Form
	{
		public FormPurchasePlanJournal()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		OleDb oleDb;
		SqlServer sqlServer;
		
		void getPeriod()
		{
			if(DataConfig.period == DataConstants.TODAY){
				dateTimePicker1.Value = DateTime.Today.Date;
				dateTimePicker2.Value = DateTime.Today.Date;
			}else if(DataConfig.period == DataConstants.YESTERDAY){
				dateTimePicker1.Value = DateTime.Now.AddDays(-1);
				dateTimePicker2.Value = DateTime.Now.Date;
			}else if(DataConfig.period == DataConstants.WEEK){
				dateTimePicker1.Value = DateTime.Now.AddDays(-7);
				dateTimePicker2.Value = DateTime.Now.Date;
			}else if(DataConfig.period == DataConstants.MONTH){
				var yr = DateTime.Today.Year;
				var mth = DateTime.Today.Month;
				var firstDay = new DateTime(yr, mth, 1);
				var lastDay = new DateTime(yr, mth, 1).AddMonths(1).AddDays(-1);
				dateTimePicker1.Value = firstDay;
				dateTimePicker2.Value = lastDay;
			}else if(DataConfig.period == DataConstants.YEAR){
				var yr = DateTime.Today.Year;
				var firstDay = new DateTime(yr, 1, 1);
				var lastDay = new DateTime(yr+1, 1, 1).AddDays(-1);
				dateTimePicker1.Value = firstDay;
				dateTimePicker2.Value = lastDay;
			}
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */	
		void FormPurchasePlanJournalLoad(object sender, EventArgs e)
		{
			getPeriod();
			Utilits.Console.Log("Журнал закупок: отркыт.");
		}
		void FormPurchasePlanJournalFormClosed(object sender, FormClosedEventArgs e)
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL && oleDb != null) oleDb.Dispose();
			if(DataConfig.typeConnection == DataConstants.CONNETION_SERVER && sqlServer != null) sqlServer.Dispose();
			Dispose();
			DataForms.FPurchasePlanJournal = null;
			DataForms.FClient.messageInStatus("...");
			Utilits.Console.Log("Журнал закупок: закрыт.");
		}
	}
}
