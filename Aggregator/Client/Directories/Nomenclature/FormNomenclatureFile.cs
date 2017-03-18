/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 17.03.2017
 * Время: 9:44
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Data;
using Aggregator.Utilits;

namespace Aggregator.Client.Directories
{
	/// <summary>
	/// Description of FormNomenclatureFile.
	/// </summary>
	public partial class FormNomenclatureFile : Form
	{
		public FormNomenclatureFile()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public String ID;
		public String ParentFolder;
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */	
		void FormNomenclatureFileLoad(object sender, EventArgs e)
		{
	
		}
		void FormNomenclatureFileFormClosed(object sender, FormClosedEventArgs e)
		{
			
		}
		void PriceTextBoxKeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab){
				String Value = priceTextBox.Text;
				priceTextBox.Clear();
				priceTextBox.Text = Conversion.StringToMoney(Math.Round(Conversion.StringToDouble(Value), 2).ToString());
				if(priceTextBox.Text == "" || Conversion.checkString(priceTextBox.Text) == false) priceTextBox.Text = "0,00";
			}
		}
		void PriceTextBoxTextChanged(object sender, EventArgs e)
		{
			if(priceTextBox.Text == "" || Conversion.checkString(priceTextBox.Text) == false) priceTextBox.Text = "0,00";
		}
		void PriceTextBoxTextLostFocus(object sender, EventArgs e)
		{
			String Value = priceTextBox.Text;
			priceTextBox.Clear();
			priceTextBox.Text = Conversion.StringToMoney(Math.Round(Conversion.StringToDouble(Value), 2).ToString());
			if(priceTextBox.Text == "" || Conversion.checkString(priceTextBox.Text) == false) priceTextBox.Text = "0,00";
		}
		void Button7Click(object sender, EventArgs e)
		{
			Calculator Calc = new Calculator();
			Calc.TextBoxReturnValue = priceTextBox;
			Calc.MdiParent = DataForms.FClient;
			Calc.Show();
		}
		
	}
}
