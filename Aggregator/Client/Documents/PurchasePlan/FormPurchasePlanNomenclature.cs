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
		
		public ListView ListViewReturnValue;
		public int SelectTableLine;
		
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
		
		bool returnValue()
		{
			if(listView1.SelectedIndices.Count > 0){
				ListViewReturnValue.Items[SelectTableLine].StateImageIndex = 1;
				ListViewReturnValue.Items[SelectTableLine].SubItems[6].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[1].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[7].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[2].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[8].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[3].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[9].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[4].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[10].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[5].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[11].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[6].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[12].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[7].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[13].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[8].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[14].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[9].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[15].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[10].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[16].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[11].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[17].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[12].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[18].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[13].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[19].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[14].Text;
				return true;
			}
			return false;
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
			Dispose();
		}
		void ButtonCancelClick(object sender, EventArgs e)
		{
			Close();
		}
		void ButtonSaveClick(object sender, EventArgs e)
		{
			if(returnValue()) Close();
		}
	}
}
