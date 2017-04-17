/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 09.04.2017
 * Время: 15:00
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Data;
using Aggregator.Database.Local;
using Aggregator.Database.Server;
using Aggregator.Client.Directories;
using Aggregator.Utilits;

namespace Aggregator.Client.Documents.Order
{
	/// <summary>
	/// Description of FormOrderDoc.
	/// </summary>
	public partial class FormOrderDoc : Form
	{
		public FormOrderDoc()
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
		OleDb oleDb;
		SqlServer sqlServer;
		String docNumber;
		int selectTableLine = -1;		// выбранная строка в таблице
		
		String getDocNumber()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL) {
				// OLEDB
				OleDbConnection oleDbConnection = new OleDbConnection();
				oleDbConnection.ConnectionString = DataConfig.oledbConnectLineBegin + DataConfig.localDatabase + DataConfig.oledbConnectLineEnd + DataConfig.oledbConnectPass;
				try{
					
					OleDbCommand oleDbCommand = new OleDbCommand("SELECT MAX(id) FROM Orders", oleDbConnection);
					oleDbConnection.Open();
					var order_id = oleDbCommand.ExecuteScalar();
					oleDbConnection.Close();
					
					int num;
					if (order_id.ToString() == "") num = 1;
					else num = (int)order_id + 1;
					String idStr = num.ToString();
					String numStr = "ЗА-0000000";
					numStr = numStr.Remove((numStr.Length - idStr.Length));
					numStr += idStr;
					return numStr;
				}catch(Exception ex){
					oleDbConnection.Close();
					Utilits.Console.Log("[ОШИБКА]: " + ex.ToString(), false, true);
				}
			} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				SqlConnection sqlConnection = new SqlConnection(DataConfig.serverConnection);
				try{
					SqlCommand sqlCommand = new SqlCommand("SELECT SCOPE_IDENTITY(Orders)", sqlConnection);
					sqlConnection.Open();
					var order_id = sqlCommand.ExecuteScalar();
					sqlConnection.Close();
					
					int num;
					if (order_id.ToString() == "") num = 1;
					else num = (int)order_id + 1;
					String idStr = num.ToString();
					String numStr = "ЗА-0000000";
					numStr = numStr.Remove((numStr.Length - idStr.Length));
					numStr += idStr;
					return numStr;
				}catch(Exception ex){
					sqlConnection.Close();
					Utilits.Console.Log("[ОШИБКА]: " + ex.Message.ToString(), false, true);
				}
			}
			return null;
		}
		
		void listViewClear()
		{
			while(listViewNomenclature.Items.Count > 0){
				listViewNomenclature.Items[0].Remove();
			}
			selectTableLine = -1;
			calculate();
		}
		
		void calculate()
		{
			if(listViewNomenclature.Items.Count > 0){
				double sum = 0;
				double amount = 0;
				double price = 0;
				double vat = 0;
				double total = 0;
				int count = listViewNomenclature.Items.Count;
				for(int i = 0; i < count; i++){
					if(listViewNomenclature.Items[i].SubItems[3].Text != "") amount = Convert.ToDouble(listViewNomenclature.Items[i].SubItems[3].Text);
					else amount = 0;
					if(listViewNomenclature.Items[i].SubItems[4].Text != "") price = Convert.ToDouble(listViewNomenclature.Items[i].SubItems[4].Text);
					else price = 0;
					sum += (price * amount);
					
					vat = Math.Round(price * amount, 2) * DataConstants.ConstFirmVAT / 100;
					vat = Math.Round(vat, 2);
					listViewNomenclature.Items[i].SubItems[5].Text = Conversion.StringToMoney(Conversion.StringToDouble(vat.ToString()).ToString());
					total = Math.Round(price * amount, 2) + vat;
					listViewNomenclature.Items[i].SubItems[6].Text = Conversion.StringToMoney(Conversion.StringToDouble(total.ToString()).ToString());
				}
				sum = Math.Round(sum, 2);
				vat = sum * DataConstants.ConstFirmVAT / 100;
				vat = Math.Round(vat, 2);
				total = sum + vat;
				total = Math.Round(total, 2);
				
				sumTextBox.Text = Conversion.StringToMoney(Conversion.StringToDouble(sum.ToString()).ToString());
				vatTextBox.Text = Conversion.StringToMoney(Conversion.StringToDouble(vat.ToString()).ToString());
				totalTextBox.Text = Conversion.StringToMoney(Conversion.StringToDouble(total.ToString()).ToString());
			}else{
				sumTextBox.Text = "0,00";
				vatTextBox.Text = "0,00";
				totalTextBox.Text = "0,00";
			}
		}
		
		void search()
		{
			String str;
			for(int i = 0; i < listViewNomenclature.Items.Count; i++){
				str = listViewNomenclature.Items[i].SubItems[1].Text;
				if(str.Contains(comboBox1.Text)){
					listViewNomenclature.FocusedItem = listViewNomenclature.Items[i];
					listViewNomenclature.Items[i].Selected = true;
					listViewNomenclature.Select();
					listViewNomenclature.EnsureVisible(i);
					break;
				}
			}
		}
		
		void addAllNomenclatures()
		{
			DateTime dt;
			ListViewItem ListViewItem_add;
			String priceName = "";
			
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL) {
				// OLEDB
				OleDbConnection oleDbConnection = new OleDbConnection();
				oleDbConnection.ConnectionString = DataConfig.oledbConnectLineBegin + 
													DataConfig.localDatabase + 
													DataConfig.oledbConnectLineEnd + 
													DataConfig.oledbConnectPass;
				oleDbConnection.Open();
				OleDbCommand oleDbCommand = new OleDbCommand("SELECT * FROM Counteragents WHERE (name = '" + counteragentTextBox.Text + "')", oleDbConnection);
				OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
				if(oleDbDataReader.Read()){
					priceName = oleDbDataReader["excel_table_id"].ToString();
				}else{
					MessageBox.Show("Контрагент " + "\"" + counteragentTextBox.Text + "\"" + " не существует в справочнике контрагентов.", "Сообщение");
				}
				oleDbDataReader.Close();
				
				if(priceName != ""){
					oleDbCommand = new OleDbCommand("SELECT * FROM " + priceName + " ", oleDbConnection);
					oleDbDataReader = oleDbCommand.ExecuteReader();
					while (oleDbDataReader.Read())
		        	{
						ListViewItem_add = new ListViewItem();
						ListViewItem_add.SubItems.Add(oleDbDataReader["name"].ToString());
						ListViewItem_add.StateImageIndex = 0;
						ListViewItem_add.SubItems.Add(DataConstants.ConstFirmUnits);
						ListViewItem_add.SubItems.Add("0,00");
						ListViewItem_add.SubItems.Add(oleDbDataReader["price"].ToString());
						ListViewItem_add.SubItems.Add("0,00");
						ListViewItem_add.SubItems.Add("0,00");
						ListViewItem_add.SubItems.Add(oleDbDataReader["manufacturer"].ToString());
						ListViewItem_add.SubItems.Add(oleDbDataReader["remainder"].ToString());
						dt = new DateTime();
						DateTime.TryParse(oleDbDataReader["term"].ToString(), out dt);
						ListViewItem_add.SubItems.Add(dt.ToString("dd.MM.yyyy"));
						ListViewItem_add.SubItems.Add(oleDbDataReader["discount1"].ToString());
						ListViewItem_add.SubItems.Add(oleDbDataReader["discount2"].ToString());
						ListViewItem_add.SubItems.Add(oleDbDataReader["discount3"].ToString());
						ListViewItem_add.SubItems.Add(oleDbDataReader["discount4"].ToString());
						ListViewItem_add.SubItems.Add(oleDbDataReader["code"].ToString());
						ListViewItem_add.SubItems.Add(oleDbDataReader["series"].ToString());
						ListViewItem_add.SubItems.Add(oleDbDataReader["article"].ToString());
						ListViewItem_add.SubItems.Add(counteragentTextBox.Text);
						ListViewItem_add.SubItems.Add(priceName);
						listViewNomenclature.Items.Add(ListViewItem_add);
					}
					oleDbDataReader.Close();
				}else{
					MessageBox.Show("Контрагент " + counteragentTextBox.Text + " не содержит прайс-листа.", "Сообщение");
				}
				oleDbConnection.Close();
				
			} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				SqlConnection sqlConnection;
				SqlCommand sqlCommand;
				SqlDataReader sqlDataReader;
				
				sqlConnection = new SqlConnection(DataConfig.serverConnection);
				sqlConnection.Open();
				
				sqlCommand = new SqlCommand("SELECT * FROM Counteragents WHERE (name = '" + counteragentTextBox.Text + "')", sqlConnection);
				sqlDataReader = sqlCommand.ExecuteReader();
				if(sqlDataReader.Read()){
					priceName = sqlDataReader["excel_table_id"].ToString();
				}else{
					MessageBox.Show("Контрагент " + "\"" + counteragentTextBox.Text + "\"" + " не существует в справочнике контрагентов.", "Сообщение");
				}
				sqlDataReader.Close();
				
				if(priceName != ""){
					sqlCommand = new SqlCommand("SELECT * FROM " + priceName + " ", sqlConnection);
					sqlDataReader = sqlCommand.ExecuteReader();
					while (sqlDataReader.Read())
		        	{
						ListViewItem_add = new ListViewItem();
						ListViewItem_add.SubItems.Add(sqlDataReader["name"].ToString());
						ListViewItem_add.StateImageIndex = 0;
						ListViewItem_add.SubItems.Add(DataConstants.ConstFirmUnits);
						ListViewItem_add.SubItems.Add("0,00");
						ListViewItem_add.SubItems.Add(sqlDataReader["price"].ToString());
						ListViewItem_add.SubItems.Add("0,00");
						ListViewItem_add.SubItems.Add("0,00");
						ListViewItem_add.SubItems.Add(sqlDataReader["manufacturer"].ToString());
						ListViewItem_add.SubItems.Add(sqlDataReader["remainder"].ToString());
						dt = new DateTime();
						DateTime.TryParse(sqlDataReader["term"].ToString(), out dt);
						ListViewItem_add.SubItems.Add(dt.ToString("dd.MM.yyyy"));
						ListViewItem_add.SubItems.Add(sqlDataReader["discount1"].ToString());
						ListViewItem_add.SubItems.Add(sqlDataReader["discount2"].ToString());
						ListViewItem_add.SubItems.Add(sqlDataReader["discount3"].ToString());
						ListViewItem_add.SubItems.Add(sqlDataReader["discount4"].ToString());
						ListViewItem_add.SubItems.Add(sqlDataReader["code"].ToString());
						ListViewItem_add.SubItems.Add(sqlDataReader["series"].ToString());
						ListViewItem_add.SubItems.Add(sqlDataReader["article"].ToString());
						ListViewItem_add.SubItems.Add(counteragentTextBox.Text);
						ListViewItem_add.SubItems.Add(priceName);
						listViewNomenclature.Items.Add(ListViewItem_add);
					}
					sqlDataReader.Close();
				}else{
					MessageBox.Show("Контрагент " + counteragentTextBox.Text + " не содержит прайс-листа.", "Сообщение");
				}				
				sqlConnection.Close();
			}
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */	
		void FormOrderDocLoad(object sender, EventArgs e)
		{
	
		}
		void FormOrderDocFormClosed(object sender, FormClosedEventArgs e)
		{
			
		}
		void Button1Click(object sender, EventArgs e)
		{
			if(DataForms.FCounteragents != null) DataForms.FCounteragents.Close();
			if(DataForms.FCounteragents == null) {
				DataForms.FCounteragents = new FormCounteragents();
				DataForms.FCounteragents.MdiParent = DataForms.FClient;
				DataForms.FCounteragents.TextBoxReturnValue = counteragentTextBox;
				DataForms.FCounteragents.TypeReturnValue = "name";
				DataForms.FCounteragents.ShowMenuReturnValue();
				DataForms.FCounteragents.Show();
			}
		}
		void Button2Click(object sender, EventArgs e)
		{
			counteragentTextBox.Clear();
			listViewClear();
		}
		void CounteragentTextBoxTextChanged(object sender, EventArgs e)
		{
			listViewClear();
		}
		void ButtonNomenclatureAddClick(object sender, EventArgs e)
		{
			FormOrderNomenclature FOrderNomenclature = new FormOrderNomenclature();
			FOrderNomenclature.MdiParent = DataForms.FClient;
			FOrderNomenclature.ListViewReturnValue = listViewNomenclature;
			FOrderNomenclature.Counteragent = counteragentTextBox.Text;
			FOrderNomenclature.loadNomenclature();
			FOrderNomenclature.Show();
		}
		void ButtonNomenclaturesAddClick(object sender, EventArgs e)
		{
			addAllNomenclatures();
		}
		void ButtonNomenclatureDeleteClick(object sender, EventArgs e)
		{
			if(listViewNomenclature.SelectedItems.Count > 0) listViewNomenclature.Items[listViewNomenclature.SelectedItems[0].Index].Remove();
			selectTableLine = -1;
			groupBox1.Text = "...";
			unitsTextBox.Clear();
			amountTextBox.Text = "0,00";
			priceTextBox.Text = "0,00";
			calculate();
		}
		void ButtonNomenclaturesDeleteClick(object sender, EventArgs e)
		{
			listViewClear();
			selectTableLine = -1;
			groupBox1.Text = "...";
			unitsTextBox.Clear();
			amountTextBox.Text = "0,00";
			priceTextBox.Text = "0,00";
			calculate();
		}
		void ComboBox1KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyData == Keys.Enter){
				if(comboBox1.Text != "") comboBox1.Items.Add(comboBox1.Text);
				search();
			}
		}
		void FindButtonClick(object sender, EventArgs e)
		{
			if(comboBox1.Text != "") comboBox1.Items.Add(comboBox1.Text);
			search();
		}
		void ListViewNomenclatureSelectedIndexChanged(object sender, EventArgs e)
		{
			if(listViewNomenclature.SelectedItems.Count > 0){
				selectTableLine = listViewNomenclature.SelectedItems[0].Index;
				groupBox1.Text = listViewNomenclature.Items[selectTableLine].SubItems[1].Text;
				if(groupBox1.Text.Length > 40) groupBox1.Text = groupBox1.Text.Remove(40) + "...";
				unitsTextBox.Text = listViewNomenclature.Items[selectTableLine].SubItems[2].Text;
				amountTextBox.Text = listViewNomenclature.Items[selectTableLine].SubItems[3].Text;
				priceTextBox.Text = listViewNomenclature.Items[selectTableLine].SubItems[4].Text;
			}
		}
		void UnitsTextBoxTextChanged(object sender, EventArgs e)
		{
			if(listViewNomenclature.Items.Count > 0 && selectTableLine > -1){
				listViewNomenclature.Items[selectTableLine].SubItems[2].Text = unitsTextBox.Text;
			}
		}
		void Button8Click(object sender, EventArgs e)
		{
			if(DataForms.FUnits != null) DataForms.FUnits.Close();
			if(DataForms.FUnits == null) {
				DataForms.FUnits = new FormUnits();
				DataForms.FUnits.MdiParent = DataForms.FClient;
				DataForms.FUnits.TextBoxReturnValue = unitsTextBox;
				DataForms.FUnits.ShowMenuReturnValue();
				DataForms.FUnits.Show();
			}
		}
		void Button9Click(object sender, EventArgs e)
		{
			unitsTextBox.Clear();
		}
		void AmountTextBoxKeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab){
				String Value = amountTextBox.Text;
				amountTextBox.Clear();
				amountTextBox.Text = Conversion.StringToMoney(Conversion.StringToDouble(Value).ToString());
				if(amountTextBox.Text == "" || Conversion.checkString(amountTextBox.Text) == false) amountTextBox.Text = "0,00";
				calculate();
			}
		}
		void AmountTextBoxTextChanged(object sender, EventArgs e)
		{
			if(amountTextBox.Text == "" || Conversion.checkString(amountTextBox.Text) == false) amountTextBox.Text = "0,00";
			if(listViewNomenclature.Items.Count > 0 && selectTableLine > -1){
				listViewNomenclature.Items[selectTableLine].SubItems[3].Text = amountTextBox.Text;
				calculate();
			}
		}
		void AmountTextBoxLostFocus(object sender, EventArgs e)
		{
			String Value = amountTextBox.Text;
			amountTextBox.Clear();
			amountTextBox.Text = Conversion.StringToMoney(Conversion.StringToDouble(Value).ToString());
			if(amountTextBox.Text == "" || Conversion.checkString(amountTextBox.Text) == false) amountTextBox.Text = "0,00";
			calculate();
		}
		void Button7Click(object sender, EventArgs e)
		{
			Calculator Calc = new Calculator();
			Calc.TextBoxReturnValue = amountTextBox;
			Calc.MdiParent = DataForms.FClient;
			Calc.Show();
		}
		void Button6Click(object sender, EventArgs e)
		{
			amountTextBox.Text = "0,00";
			calculate();
		}
		void PriceTextBoxKeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab){
				String Value = priceTextBox.Text;
				priceTextBox.Clear();
				priceTextBox.Text = Conversion.StringToMoney(Conversion.StringToDouble(Value).ToString());
				if(priceTextBox.Text == "" || Conversion.checkString(priceTextBox.Text) == false) priceTextBox.Text = "0,00";
				calculate();
			}
		}
		void PriceTextBoxTextChanged(object sender, EventArgs e)
		{
			if(priceTextBox.Text == "" || Conversion.checkString(priceTextBox.Text) == false) priceTextBox.Text = "0,00";
			if(listViewNomenclature.Items.Count > 0 && selectTableLine > -1){
				listViewNomenclature.Items[selectTableLine].SubItems[4].Text = priceTextBox.Text;
				calculate();
			}
		}
		void PriceTextBoxLostFocus(object sender, EventArgs e)
		{
			String Value = priceTextBox.Text;
			priceTextBox.Clear();
			priceTextBox.Text = Conversion.StringToMoney(Conversion.StringToDouble(Value).ToString());
			if(priceTextBox.Text == "" || Conversion.checkString(priceTextBox.Text) == false) priceTextBox.Text = "0,00";
			calculate();
		}
		
	}
}
