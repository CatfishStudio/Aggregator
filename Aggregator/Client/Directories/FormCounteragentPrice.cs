﻿/*
 * Создано в SharpDevelop.
 * Пользователь: Catfish
 * Дата: 13.03.2017
 * Время: 21:45
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using Aggregator.Data;
using Aggregator.Database.Local;
using Aggregator.Database.Server;

namespace Aggregator.Client.Directories
{
	/// <summary>
	/// Description of FormCounteragentPrice.
	/// </summary>
	public partial class FormCounteragentPrice : Form
	{
		public FormCounteragentPrice()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public String PriceName;
		OleDb oleDb;
		SqlServer sqlServer;
		
		void inicColumn()
		{
			if(oleDb.dataSet != null){
				oleDb.dataSet.Tables[0].Columns[0].Caption = "№ п/п";
				oleDb.dataSet.Tables[0].Columns[1].Caption = "Наименование";
				oleDb.dataSet.Tables[0].Columns[2].Caption = "Код";
				oleDb.dataSet.Tables[0].Columns[3].Caption = "Серия";
				oleDb.dataSet.Tables[0].Columns[4].Caption = "Артикул";
				oleDb.dataSet.Tables[0].Columns[5].Caption = "Остаток";
				oleDb.dataSet.Tables[0].Columns[6].Caption = "Производитель";
				oleDb.dataSet.Tables[0].Columns[7].Caption = "Цена отпускная";
				oleDb.dataSet.Tables[0].Columns[8].Caption = "Цена со скидкой 1";
				oleDb.dataSet.Tables[0].Columns[9].Caption = "Цена со скидкой 2";
				oleDb.dataSet.Tables[0].Columns[10].Caption = "Цена со скидкой 3";
				oleDb.dataSet.Tables[0].Columns[11].Caption = "Цена со скидкой 4";
				oleDb.dataSet.Tables[0].Columns[12].Caption = "Срок годности";
			}
		}
		
		void readPrice()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL) {
				// OLEDB
				oleDb = new OleDb(DataConfig.localDatabase);
				oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM " + PriceName;
				if(oleDb.ExecuteFill(PriceName)){
					oleDb.dataSet.DataSetName = PriceName;
					inicColumn();
					dataGrid1.CaptionText = PriceName;
					dataGrid1.DataSource = oleDb.dataSet;
					dataGrid1.DataMember = oleDb.dataSet.Tables[0].TableName;
					dataGrid1.Enabled = true;
					Utilits.Console.Log("Прайс " + PriceName + " был успешно загружен.");
				}else{
					oleDb.Error();
					Utilits.Console.Log("[ПРЕДУПРЕЖДЕНИЕ] Прайс " + PriceName + " не удалось открыть.");
				}
			} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER

			}
		}
		
		void savePrice()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL) {
				// OLEDB
				oleDb.oleDbCommandUpdate.CommandText = "UPDATE " + PriceName + " SET " +
					"[name] = @name, " +
					"[code] = @code, " +
					"[series] = @series, " +
					"[article] = @article, " +
					"[remainder] = @remainder, " +
					"[manufacturer] = @manufacturer, " +
					"[price] = @price, " +
					"[discount1] = @discount1, " +
					"[discount2] = @discount2, " +
					"[discount3] = @discount3, " +
					"[discount4] = @discount4, " +
					"[term] = @term " +
					"WHERE ([id] = @id)";
				oleDb.oleDbCommandUpdate.Parameters.Add("@name", OleDbType.VarChar, 255, "name");
				oleDb.oleDbCommandUpdate.Parameters.Add("@code", OleDbType.VarChar, 255, "code");
				oleDb.oleDbCommandUpdate.Parameters.Add("@series", OleDbType.VarChar, 255, "series");
				oleDb.oleDbCommandUpdate.Parameters.Add("@article", OleDbType.VarChar, 255, "article");
				oleDb.oleDbCommandUpdate.Parameters.Add("@remainder", OleDbType.Double, 15, "remainder");
				oleDb.oleDbCommandUpdate.Parameters.Add("@manufacturer", OleDbType.VarChar, 255, "manufacturer");
				oleDb.oleDbCommandUpdate.Parameters.Add("@price", OleDbType.Double, 15, "price");
				oleDb.oleDbCommandUpdate.Parameters.Add("@discount1", OleDbType.Double, 15, "discount1");
				oleDb.oleDbCommandUpdate.Parameters.Add("@discount2", OleDbType.Double, 15, "discount2");
				oleDb.oleDbCommandUpdate.Parameters.Add("@discount3", OleDbType.Double, 15, "discount3");
				oleDb.oleDbCommandUpdate.Parameters.Add("@discount4", OleDbType.Double, 15, "discount4");
				oleDb.oleDbCommandUpdate.Parameters.Add("@term", OleDbType.Date, 15, "term");
				oleDb.oleDbCommandUpdate.Parameters.Add("@id", OleDbType.Integer, 10, "id");
				if(oleDb.ExecuteUpdate(PriceName)){
					Utilits.Console.Log("Прайс " + PriceName + " был успешно сохранён.");
					Close();
				}else{
					oleDb.Error();
					Utilits.Console.Log("[ПРЕДУПРЕЖДЕНИЕ] Прайс " + PriceName + " не удалось сохранить.");
				}
			} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER

			}
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */	
		void FormCounteragentPriceLoad(object sender, EventArgs e)
		{
			readPrice();
		}
		void FormCounteragentPriceFormClosed(object sender, FormClosedEventArgs e)
		{
			if(oleDb != null) oleDb.Dispose();
			if(sqlServer != null) sqlServer.Dispose();
			Dispose();
		}
		void ButtonCancelClick(object sender, EventArgs e)
		{
			Close();
		}
		void ButtonSaveClick(object sender, EventArgs e)
		{
			savePrice();
		}
	}
}
