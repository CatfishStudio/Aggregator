/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 07.03.2017
 * Время: 10:21
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Data.OleDb;
using Aggregator.Data;
using Aggregator.Database.Server;
using Excel;
using Aggregator.Database.Local;

namespace Aggregator.Client.Directories
{
	/// <summary>
	/// Description of FormContragentFile.
	/// </summary>
	public partial class FormCounteragentFile : Form
	{
		public FormCounteragentFile()
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
		String excelTableID;
		
		OleDb oleDb;
		SqlServer sqlServer;
		DataSet dataSet;
		
		void openFileExcel()
		{
			if(openFileDialog1.ShowDialog() == DialogResult.OK){
				fileTextBox.Text = openFileDialog1.FileName;
				dateLabel.Text = DateTime.Now.ToString();
				if(fileTextBox.Text.Substring(fileTextBox.Text.Length - 3) == "xls"){
					readExcelFormat972003();
				}else{
					readExcelFormat2007();
				}
				initTable();
			}
		}
		
		void readExcelFormat972003()
		{
			try{
				FileStream stream = File.Open(fileTextBox.Text, FileMode.Open, FileAccess.Read);
				IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
				dataSet = excelReader.AsDataSet();
				initColunms();
				dataGrid1.DataSource = dataSet;
				dataGrid1.DataMember = dataSet.Tables[0].TableName;
				excelReader.Close();
			}catch(Exception ex){
				MessageBox.Show(ex.ToString(), "ERROR");
			}
		}
		
		void readExcelFormat2007()
		{
			try{
				FileStream stream = File.Open(fileTextBox.Text, FileMode.Open, FileAccess.Read);
				IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
				dataSet = excelReader.AsDataSet();
				initColunms();
				dataGrid1.DataSource = dataSet;
				dataGrid1.DataMember = dataSet.Tables[0].TableName;
				excelReader.Close();				
			}catch(Exception ex){
				MessageBox.Show(ex.ToString(), "ERROR");
			}
		}
		
		void initTable()
		{
			if(dataSet == null) return;
			if(dataSet.Tables.Count > 0){
				dataGrid1.Enabled = true;
				bool empty;
				// check columns
				for(int col = 0; col < dataSet.Tables[0].Columns.Count; col++){
					empty = true;
					for(int row = 0; row < dataSet.Tables[0].Rows.Count; row++){
						if(dataSet.Tables[0].Rows[row][col].ToString() != ""){
							empty = false;
							break;
						}
					}
					if(empty){
						dataSet.Tables[0].Columns.RemoveAt(col);
						col--;
					}
				}
				// check rows
				for(int row = 0; row < dataSet.Tables[0].Rows.Count; row++){
					empty = true;
					for(int col = 0; col < dataSet.Tables[0].Columns.Count; col++){
						if(dataSet.Tables[0].Rows[row][col].ToString() != ""){
							empty = false;
							break;
						}
					}
					if(empty){
						dataSet.Tables[0].Rows.RemoveAt(row);
						row--;
					}
				}
			}
			dataGrid1.Update();
			initColunms();
		}
		
		void initColunms()
		{
			if(dataSet == null) return;
			for(int i = 0; i < dataSet.Tables[0].Columns.Count; i++) {
				dataSet.Tables[0].Columns[i].ColumnName = (i+1).ToString();
			}
			
			numericUpDown5.Maximum = dataSet.Tables[0].Columns.Count;
			numericUpDown6.Maximum = dataSet.Tables[0].Columns.Count;
			numericUpDown7.Maximum = dataSet.Tables[0].Columns.Count;
			numericUpDown8.Maximum = dataSet.Tables[0].Columns.Count;
			numericUpDown9.Maximum = dataSet.Tables[0].Columns.Count;
			numericUpDown10.Maximum = dataSet.Tables[0].Columns.Count;
			numericUpDown11.Maximum = dataSet.Tables[0].Columns.Count;
			numericUpDown12.Maximum = dataSet.Tables[0].Columns.Count;
			numericUpDown13.Maximum = dataSet.Tables[0].Columns.Count;
			numericUpDown14.Maximum = dataSet.Tables[0].Columns.Count;
			numericUpDown15.Maximum = dataSet.Tables[0].Columns.Count;
			numericUpDown16.Maximum = dataSet.Tables[0].Columns.Count;
			
			if(numericUpDown5.Value > 0) dataSet.Tables[0].Columns[Convert.ToInt32(numericUpDown5.Value - 1)].ColumnName = "Наименование";
			if(numericUpDown6.Value > 0) dataSet.Tables[0].Columns[Convert.ToInt32(numericUpDown6.Value - 1)].ColumnName = "Код";
			if(numericUpDown7.Value > 0) dataSet.Tables[0].Columns[Convert.ToInt32(numericUpDown7.Value - 1)].ColumnName = "Серия";
			if(numericUpDown8.Value > 0) dataSet.Tables[0].Columns[Convert.ToInt32(numericUpDown8.Value - 1)].ColumnName = "Артикул";
			if(numericUpDown9.Value > 0) dataSet.Tables[0].Columns[Convert.ToInt32(numericUpDown9.Value - 1)].ColumnName = "Цена отпускная";
			if(numericUpDown10.Value > 0) dataSet.Tables[0].Columns[Convert.ToInt32(numericUpDown10.Value - 1)].ColumnName = "Цена со скидкой 1";
			if(numericUpDown11.Value > 0) dataSet.Tables[0].Columns[Convert.ToInt32(numericUpDown11.Value - 1)].ColumnName = "Цена со скидкой 2";
			if(numericUpDown12.Value > 0) dataSet.Tables[0].Columns[Convert.ToInt32(numericUpDown12.Value - 1)].ColumnName = "Цена со скидкой 3";
			if(numericUpDown13.Value > 0) dataSet.Tables[0].Columns[Convert.ToInt32(numericUpDown13.Value - 1)].ColumnName = "Цена со скидкой 4";
			if(numericUpDown14.Value > 0) dataSet.Tables[0].Columns[Convert.ToInt32(numericUpDown14.Value - 1)].ColumnName = "Остаток";
			if(numericUpDown15.Value > 0) dataSet.Tables[0].Columns[Convert.ToInt32(numericUpDown15.Value - 1)].ColumnName = "Производитель";
			if(numericUpDown16.Value > 0) dataSet.Tables[0].Columns[Convert.ToInt32(numericUpDown16.Value - 1)].ColumnName = "Срок годности";
		}
		
		void enableNumeric(CheckBox cb, bool enable)
		{
			NumericUpDown numUpDown = null;
			if(cb.Name == "checkBox1") numUpDown = numericUpDown5;
			if(cb.Name == "checkBox2") numUpDown = numericUpDown6;
			if(cb.Name == "checkBox3") numUpDown = numericUpDown7;
			if(cb.Name == "checkBox4") numUpDown = numericUpDown8;
			if(cb.Name == "checkBox5") numUpDown = numericUpDown9;
			if(cb.Name == "checkBox6") numUpDown = numericUpDown10;
			if(cb.Name == "checkBox7") numUpDown = numericUpDown11;
			if(cb.Name == "checkBox8") numUpDown = numericUpDown12;
			if(cb.Name == "checkBox9") numUpDown = numericUpDown13;
			if(cb.Name == "checkBox10") numUpDown = numericUpDown14;
			if(cb.Name == "checkBox11") numUpDown = numericUpDown15;
			if(cb.Name == "checkBox12") numUpDown = numericUpDown16;

			if(enable == true && numUpDown != null){
				numUpDown.Enabled = true;
				numUpDown.Minimum = 1;
				numUpDown.Value = 1;
			}else{
				numUpDown.Enabled = false;
				numUpDown.Minimum = 0;
				numUpDown.Value = 0;
			}
		}
		
		void getFolders()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				oleDb.oleDbCommandSelect.CommandText = "SELECT name FROM Counteragents WHERE (type = 'folder')";
				oleDb.ExecuteFill("Counteragents");
				foreach(DataRow row in oleDb.dataSet.Tables["Counteragents"].Rows){
					foldersComboBox.Items.Add(row["name"].ToString());
				}
				oleDb.dataSet.Clear();
				foldersComboBox.Text = ParentFolder;
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				
			}
		}
		
		void open()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				oleDb.oleDbCommandSelect.CommandText = "SELECT id, name, type, " +
					"organization_address, organization_phone, organization_site, organization_email, " +
					"contact_fullname, contact_post, contact_phone, contact_skype, contact_email, information, " +
					"excel_filename, excel_date, excel_column_name, excel_column_code, excel_column_series, " +
					"excel_column_article, excel_column_remainder, excel_column_manufacturer, excel_column_price, " +
					"excel_column_discount_1, excel_column_discount_2, excel_column_discount_3, excel_column_discount_4, " +
					"excel_column_term, excel_table_id, parent" +
					" FROM Counteragents WHERE (id = " + ID + ")";
				oleDb.ExecuteFill("Counteragents");
				idTextBox.Text = oleDb.dataSet.Tables["Counteragents"].Rows[0]["id"].ToString();
				textBox1.Text = oleDb.dataSet.Tables["Counteragents"].Rows[0]["name"].ToString();
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER && DataConfig.typeDatabase == DataConstants.TYPE_MSSQL){
				// MSSQL SERVER
				
			}
		}
		
		void saveNew()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				oleDb.oleDbCommandSelect.CommandText = "SELECT id, name, type, " +
					"organization_address, organization_phone, organization_site, organization_email, " +
					"contact_fullname, contact_post, contact_phone, contact_skype, contact_email, information, " +
					"excel_filename, excel_date, excel_column_name, excel_column_code, excel_column_series, " +
					"excel_column_article, excel_column_remainder, excel_column_manufacturer, excel_column_price, " +
					"excel_column_discount_1, excel_column_discount_2, excel_column_discount_3, excel_column_discount_4, " +
					"excel_column_term, excel_table_id, parent" +
					" FROM Counteragents WHERE (id = 0)";
				oleDb.ExecuteFill("Counteragents");				
				
				DataRow newRow = oleDb.dataSet.Tables["Counteragents"].NewRow();
				newRow["name"] = textBox1.Text;
				newRow["type"] = "file";
				newRow["organization_address"] = textBox2.Text;
				newRow["organization_phone"] = textBox3.Text;
				newRow["organization_site"] = textBox4.Text;
				newRow["organization_email"] = textBox5.Text;
				newRow["contact_fullname"] = textBox6.Text;
				newRow["contact_post"] = textBox7.Text;
				newRow["contact_phone"] = textBox8.Text;
				newRow["contact_skype"] = textBox9.Text;
				newRow["contact_email"] = textBox10.Text;
				newRow["information"] = textBox11.Text;
				newRow["excel_filename"] = fileTextBox.Text;
				newRow["excel_date"] = dateLabel.Text;
				newRow["excel_column_name"] = numericUpDown5.Value;
				newRow["excel_column_code"] = numericUpDown6.Value;
				newRow["excel_column_series"] = numericUpDown7.Value;
				newRow["excel_column_article"] = numericUpDown8.Value;
				newRow["excel_column_remainder"] = numericUpDown14.Value;
				newRow["excel_column_manufacturer"] = numericUpDown15.Value;
				newRow["excel_column_price"] = numericUpDown9.Value;
				newRow["excel_column_discount_1"] = numericUpDown10.Value;
				newRow["excel_column_discount_2"] = numericUpDown11.Value;
				newRow["excel_column_discount_3"] = numericUpDown12.Value;
				newRow["excel_column_discount_4"] = numericUpDown13.Value;
				newRow["excel_column_term"] = numericUpDown16.Value;
				excelTableID = "Price" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now); 
				newRow["excel_table_id"] = excelTableID;
				newRow["parent"] = foldersComboBox.Text; // (!) Проверить чтобы папка существовала
				
				oleDb.dataSet.Tables["Counteragents"].Rows.Add(newRow);
				
				oleDb.oleDbCommandInsert.CommandText = "INSERT INTO Counteragents " +
					"(name, type, " +
					"organization_address, organization_phone, organization_site, organization_email, " +
					"contact_fullname, contact_post, contact_phone, contact_skype, contact_email, information, " +
					"excel_filename, excel_date, excel_column_name, excel_column_code, excel_column_series, " +
					"excel_column_article, excel_column_remainder, excel_column_manufacturer, excel_column_price, " +
					"excel_column_discount_1, excel_column_discount_2, excel_column_discount_3, excel_column_discount_4, " +
					"excel_column_term, excel_table_id, parent)" +
					" VALUES (@name, @type, " +
					"@organization_address, @organization_phone, @organization_site, @organization_email, " +
					"@contact_fullname, @contact_post, @contact_phone, @contact_skype, @contact_email, @information, " +
					"@excel_filename, @excel_date, @excel_column_name, @excel_column_code, @excel_column_series, " +
					"@excel_column_article, @excel_column_remainder, @excel_column_manufacturer, @excel_column_price, " +
					"@excel_column_discount_1, @excel_column_discount_2, @excel_column_discount_3, @excel_column_discount_4, " +
					"@excel_column_term, @excel_table_id, @parent)";
				oleDb.oleDbCommandInsert.Parameters.Add("@name", OleDbType.VarChar, 255, "name");
				oleDb.oleDbCommandInsert.Parameters.Add("@type", OleDbType.VarChar, 255, "type");
				oleDb.oleDbCommandInsert.Parameters.Add("@organization_address", OleDbType.VarChar, 255, "organization_address");
				oleDb.oleDbCommandInsert.Parameters.Add("@organization_phone", OleDbType.VarChar, 255, "organization_phone");
				oleDb.oleDbCommandInsert.Parameters.Add("@organization_site", OleDbType.VarChar, 255, "organization_site");
				oleDb.oleDbCommandInsert.Parameters.Add("@organization_email", OleDbType.VarChar, 255, "organization_email");
				oleDb.oleDbCommandInsert.Parameters.Add("@contact_fullname", OleDbType.VarChar, 255, "contact_fullname");
				oleDb.oleDbCommandInsert.Parameters.Add("@contact_post", OleDbType.VarChar, 255, "contact_post");
				oleDb.oleDbCommandInsert.Parameters.Add("@contact_phone", OleDbType.VarChar, 255, "contact_phone");
				oleDb.oleDbCommandInsert.Parameters.Add("@contact_skype", OleDbType.VarChar, 255, "contact_skype");
				oleDb.oleDbCommandInsert.Parameters.Add("@contact_email", OleDbType.VarChar, 255, "contact_email");
				oleDb.oleDbCommandInsert.Parameters.Add("@information", OleDbType.LongVarChar, 0, "information");
				oleDb.oleDbCommandInsert.Parameters.Add("@excel_filename", OleDbType.LongVarChar, 0, "excel_filename");
				oleDb.oleDbCommandInsert.Parameters.Add("@excel_date", OleDbType.VarChar, 255, "excel_date");
				oleDb.oleDbCommandInsert.Parameters.Add("@excel_column_name", OleDbType.Integer, 0, "excel_column_name");
				oleDb.oleDbCommandInsert.Parameters.Add("@excel_column_code", OleDbType.Integer, 0, "excel_column_code");
				oleDb.oleDbCommandInsert.Parameters.Add("@excel_column_series", OleDbType.Integer, 0, "excel_column_series");
				oleDb.oleDbCommandInsert.Parameters.Add("@excel_column_article", OleDbType.Integer, 0, "excel_column_article");
				oleDb.oleDbCommandInsert.Parameters.Add("@excel_column_remainder", OleDbType.Integer, 0, "excel_column_remainder");
				oleDb.oleDbCommandInsert.Parameters.Add("@excel_column_manufacturer", OleDbType.Integer, 0, "excel_column_manufacturer");
				oleDb.oleDbCommandInsert.Parameters.Add("@excel_column_price", OleDbType.Integer, 0, "excel_column_price");
				oleDb.oleDbCommandInsert.Parameters.Add("@excel_column_discount_1", OleDbType.Integer, 0, "excel_column_discount_1");
				oleDb.oleDbCommandInsert.Parameters.Add("@excel_column_discount_2", OleDbType.Integer, 0, "excel_column_discount_2");
				oleDb.oleDbCommandInsert.Parameters.Add("@excel_column_discount_3", OleDbType.Integer, 0, "excel_column_discount_3");
				oleDb.oleDbCommandInsert.Parameters.Add("@excel_column_discount_4", OleDbType.Integer, 0, "excel_column_discount_4");
				oleDb.oleDbCommandInsert.Parameters.Add("@excel_column_term", OleDbType.Integer, 0, "excel_column_term");
				oleDb.oleDbCommandInsert.Parameters.Add("@excel_table_id", OleDbType.VarChar, 255, "excel_table_id");
				oleDb.oleDbCommandInsert.Parameters.Add("@parent", OleDbType.VarChar, 255, "parent");
				
				if(oleDb.ExecuteUpdate("Counteragents")){
					DataForms.FClient.updateHistory("Counteragents");
					Utilits.Console.Log("Создан новый контрагент.");
					Close();
				}
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				
			}
			
		}
		
		void saveEdit()
		{
			
		}
		
		bool check()
		{
			if(textBox1.Text == "") return false;
			return true;
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */	
		void FormCounteragentFileLoad(object sender, EventArgs e)
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL) oleDb = new OleDb(DataConfig.localDatabase);
			if(DataConfig.typeConnection == DataConstants.CONNETION_SERVER) sqlServer = new SqlServer();
			getFolders();
			if(ID == null){
				Text = "Создать";
			}else{
				Text = "Изменить";
				open();
			}
		}
		void FormCounteragentFileFormClosed(object sender, FormClosedEventArgs e)
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL && oleDb != null) oleDb.Dispose();
			if(DataConfig.typeConnection == DataConstants.CONNETION_SERVER && sqlServer != null) sqlServer.Dispose();
			Dispose();
		}
		void OpenExcelButtonClick(object sender, EventArgs e)
		{
			openFileExcel();
		}
		void CheckBoxAllCheckedChanged(object sender, EventArgs e)
		{
			if(dataSet == null){
				(sender as CheckBox).Checked = false;
				return;
			}
			enableNumeric((sender as CheckBox), (sender as CheckBox).Checked);
			initColunms();
		}
		void NumericUpDownAllValueChanged(object sender, EventArgs e)
		{
			initColunms();
		}
		void ButtonSaveClick(object sender, EventArgs e)
		{
			if(check()){
				if(ID == null) saveNew();
				else saveEdit();
			}else{
				MessageBox.Show("Некорректно заполнены поля формы.", "Сообщение:");
			}
		}
		void ButtonCancelClick(object sender, EventArgs e)
		{
			Close();
		}
		void DataGrid1PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Delete)
			{
				foreach (DataRow row in dataSet.Tables[0].Rows){
					if(row.RowState == DataRowState.Deleted) row.Delete();
				}
			}
		}

	}
}
