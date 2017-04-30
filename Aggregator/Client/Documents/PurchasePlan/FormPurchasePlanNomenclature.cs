﻿/*
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
using Aggregator.Data;
using Aggregator.Database.Local;
using Aggregator.Utilits;

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
		
		public ListView ListViewPrices;
		public ListView ListViewReturnValue;
		public int SelectTableLine;
		SearchNomenclatureOleDb searchNomenclatureOleDb;
		
		public void LoadNomenclature(List<Nomenclature> nomenclatureList)
		{
			DateTime dt;
			ListViewItem ListViewItem_add;
			foreach(Nomenclature nomenclature in nomenclatureList){
				ListViewItem_add = new ListViewItem();
				ListViewItem_add.SubItems.Add(nomenclature.Name);
				ListViewItem_add.StateImageIndex = 0;
				ListViewItem_add.SubItems.Add(nomenclature.Price.ToString());
				ListViewItem_add.SubItems.Add(nomenclature.Manufacturer);
				ListViewItem_add.SubItems.Add(nomenclature.Remainder.ToString());
				dt = new DateTime();
				DateTime.TryParse(nomenclature.Term.ToString(), out dt);
				ListViewItem_add.SubItems.Add(dt.ToString("dd.MM.yyyy"));
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
			String value;
			if(listView1.SelectedIndices.Count > 0){
				ListViewReturnValue.Items[SelectTableLine].StateImageIndex = 1;
				ListViewReturnValue.Items[SelectTableLine].SubItems[6].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[1].Text;
				value = Conversion.StringToMoney(Conversion.StringToDouble(listView1.Items[listView1.SelectedItems[0].Index].SubItems[2].Text).ToString());
				ListViewReturnValue.Items[SelectTableLine].SubItems[7].Text= value;
				ListViewReturnValue.Items[SelectTableLine].SubItems[8].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[3].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[9].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[4].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[10].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[5].Text;
				value = Conversion.StringToMoney(Conversion.StringToDouble(listView1.Items[listView1.SelectedItems[0].Index].SubItems[6].Text).ToString());
				ListViewReturnValue.Items[SelectTableLine].SubItems[11].Text = value;
				value = Conversion.StringToMoney(Conversion.StringToDouble(listView1.Items[listView1.SelectedItems[0].Index].SubItems[7].Text).ToString());
				ListViewReturnValue.Items[SelectTableLine].SubItems[12].Text = value;
				value = Conversion.StringToMoney(Conversion.StringToDouble(listView1.Items[listView1.SelectedItems[0].Index].SubItems[8].Text).ToString());
				ListViewReturnValue.Items[SelectTableLine].SubItems[13].Text = value;
				value = Conversion.StringToMoney(Conversion.StringToDouble(listView1.Items[listView1.SelectedItems[0].Index].SubItems[9].Text).ToString());
				ListViewReturnValue.Items[SelectTableLine].SubItems[14].Text = value;
				ListViewReturnValue.Items[SelectTableLine].SubItems[15].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[10].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[16].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[11].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[17].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[12].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[18].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[13].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[19].Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[14].Text;
				ListViewReturnValue.Items[SelectTableLine].SubItems[20].Text = "";
				return true;
			}
			return false;
		}
		
		void loadALLNomenclature()
		{
			while(listView1.Items.Count > 0){
				listView1.Items[0].Remove();
			}
			
			List<Nomenclature> nomenclatureList;
			searchNomenclatureOleDb = new SearchNomenclatureOleDb();
			searchNomenclatureOleDb.setPrices(ListViewPrices);
			nomenclatureList = searchNomenclatureOleDb.getAllNomenclature();
			LoadNomenclature(nomenclatureList);
		}
		
		void search()
		{
			String str;
			for(int i = 0; i < listView1.Items.Count; i++){
				str = listView1.Items[i].SubItems[1].Text;
				if(str.Contains(textBox1.Text)){
					listView1.FocusedItem = listView1.Items[i];
					listView1.Items[i].Selected = true;
					listView1.Select();
					listView1.EnsureVisible(i);
					break;
				}
			}
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */	
		void FormPurchasePlanNomenclatureLoad(object sender, EventArgs e)
		{
			Utilits.Console.Log(Text);
		}
		void FormPurchasePlanNomenclatureFormClosed(object sender, FormClosedEventArgs e)
		{
			Dispose();
			DataForms.FClient.messageInStatus("...");
			Utilits.Console.Log(Text + ": закрыт.");
		}
		void ButtonCancelClick(object sender, EventArgs e)
		{
			Close();
		}
		void ButtonSaveClick(object sender, EventArgs e)
		{
			if(returnValue()) Close();
		}
		void ВыбратьНоменклатуруToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(returnValue()) Close();
		}
		void ПоказатьВесьПереченьToolStripMenuItemClick(object sender, EventArgs e)
		{
			loadALLNomenclature();
		}
		void FindButtonClick(object sender, EventArgs e)
		{
			search();
		}
		void TextBox1KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyData == Keys.Enter){
				search();
			}
		}
		void ListView1SelectedIndexChanged(object sender, EventArgs e)
		{
			if(listView1.SelectedItems.Count > 0){
				label1.Text = listView1.Items[listView1.SelectedItems[0].Index].SubItems[1].Text;
			}
		}
		void FormPurchasePlanNomenclatureActivated(object sender, EventArgs e)
		{
			DataForms.FClient.messageInStatus(this.Text);
		}

	}
}
