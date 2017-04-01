/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 01.04.2017
 * Время: 12:40
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Database.Local;

namespace Aggregator.Client.Documents.PurchasePlan
{
	/// <summary>
	/// Description of FormPurchasePlanNomenclature.
	/// </summary>
	public partial class FormPurchasePlanNomenclature : Form
	{
		public FormPurchasePlanNomenclature()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public void LoadNomenclature(List<Nomenclature> nomenclatureList)
		{
			ListViewItem ListViewItem_add;
			foreach(Nomenclature nomenclature in nomenclatureList){
				ListViewItem_add = new ListViewItem();
				ListViewItem_add.SubItems.Add(nomenclature.Name);
				ListViewItem_add.StateImageIndex = 0;
				ListViewItem_add.SubItems.Add(nomenclature.Price.ToString());
				ListViewItem_add.SubItems.Add(nomenclature.Manufacturer);
				ListViewItem_add.SubItems.Add(nomenclature.Remainder.ToString());
				ListViewItem_add.SubItems.Add(nomenclature.Term.ToString());
				ListViewItem_add.SubItems.Add(nomenclature.Discount1.ToString());
				ListViewItem_add.SubItems.Add(nomenclature.Discount2.ToString());
				ListViewItem_add.SubItems.Add(nomenclature.Discount3.ToString());
				ListViewItem_add.SubItems.Add(nomenclature.Discount4.ToString());
				ListViewItem_add.SubItems.Add(nomenclature.Code);
				ListViewItem_add.SubItems.Add(nomenclature.Series);
				ListViewItem_add.SubItems.Add(nomenclature.Article);
				ListViewItem_add.SubItems.Add(nomenclature.CounteragentName);
				ListViewItem_add.SubItems.Add(nomenclature.CounteragentPrice);
				listView1.Items.Add(ListViewItem_add);
			}

		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */	
		void FormPurchasePlanNomenclatureLoad(object sender, EventArgs e)
		{
	
		}
		void FormPurchasePlanNomenclatureFormClosed(object sender, FormClosedEventArgs e)
		{
	
		}
	}
}
