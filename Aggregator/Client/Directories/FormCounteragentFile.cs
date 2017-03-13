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
		public String ExcelTableID;
		bool firstColumnNumber;
		
		OleDb oleDb;
		SqlServer sqlServer;
		DataSet dataSet;
		
		void openFileExcel()
		{
			if(openFileDialog1.ShowDialog() == DialogResult.OK){
				fileTextBox.Text = openFileDialog1.FileName;
				dateLabel.Text = DateTime.Now.ToString();
				firstColumnNumber = false;
				resetEditor();
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
		
		void resetEditor()
		{
			numericUpDown5.Minimum = 0; numericUpDown5.Maximum = 0;
			numericUpDown5.Value = 0; checkBox1.Checked = false;
			numericUpDown6.Minimum = 0; numericUpDown6.Maximum = 0;
			numericUpDown6.Value = 0; checkBox2.Checked = false;
			numericUpDown7.Minimum = 0; numericUpDown7.Maximum = 0;
			numericUpDown7.Value = 0; checkBox3.Checked = false;
			numericUpDown8.Minimum = 0; numericUpDown8.Maximum = 0;
			numericUpDown8.Value = 0; checkBox4.Checked = false;
			numericUpDown9.Minimum = 0; numericUpDown9.Maximum = 0;
			numericUpDown9.Value = 0; checkBox5.Checked = false;
			numericUpDown10.Minimum = 0; numericUpDown10.Maximum = 0;
			numericUpDown10.Value = 0; checkBox6.Checked = false;
			numericUpDown11.Minimum = 0; numericUpDown11.Maximum = 0;
			numericUpDown11.Value = 0; checkBox7.Checked = false;
			numericUpDown12.Minimum = 0; numericUpDown12.Maximum = 0;
			numericUpDown12.Value = 0; checkBox8.Checked = false;
			numericUpDown13.Minimum = 0; numericUpDown13.Maximum = 0;
			numericUpDown13.Value = 0; checkBox9.Checked = false;
			numericUpDown14.Minimum = 0; numericUpDown14.Maximum = 0;
			numericUpDown14.Value = 0; checkBox10.Checked = false;
			numericUpDown15.Minimum = 0; numericUpDown15.Maximum = 0;
			numericUpDown15.Value = 0; checkBox11.Checked = false;
			numericUpDown16.Minimum = 0; numericUpDown16.Maximum = 0;
			numericUpDown16.Value = 0; checkBox12.Checked = false;
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
			if(firstColumnNumber) dataSet.Tables[0].Columns[0].ColumnName = "№ п/п";
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
				oleDb = new OleDb(DataConfig.localDatabase);
				oleDb.oleDbCommandSelect.CommandText = "SELECT name FROM Counteragents WHERE (type = 'folder')";
				oleDb.ExecuteFill("Counteragents");
				foreach(DataRow row in oleDb.dataSet.Tables["Counteragents"].Rows){
					foldersComboBox.Items.Add(row["name"].ToString());
				}
				foldersComboBox.Text = ParentFolder;
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				
			}
		}
		
		void getPrice()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				oleDb = new OleDb(DataConfig.localDatabase);
				oleDb.oleDbCommandSelect.CommandText = "SELECT * FROM " + ExcelTableID;
				if(oleDb.ExecuteFill(ExcelTableID)){
					dataSet = oleDb.dataSet.Copy();
					dataSet.DataSetName = ExcelTableID;
					oleDb.dataSet.Clear();
					dataGrid1.DataSource = dataSet;
					dataGrid1.DataMember = dataSet.Tables[0].TableName;
					dataGrid1.Enabled = true;
					dataGrid1.Update();
					//initColunms();
					Utilits.Console.Log("[Контрагенты] Прайс " + ExcelTableID + " успешно загружен.");
				}else{
					Utilits.Console.Log("[ОШИБКА] Таблицы " + ExcelTableID + " нет в базе данных", false, true);
				}
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				
			}
		}
		
		bool setPrice()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				String sqlCommand;
				QueryOleDb query;
				query = new QueryOleDb(DataConfig.localDatabase);
				sqlCommand = "CREATE TABLE " + ExcelTableID + " (" +
					"[id] COUNTER PRIMARY KEY, " +
					"[name] VARCHAR DEFAULT '', " +
					"[code] VARCHAR DEFAULT '', " +
					"[series] VARCHAR DEFAULT '', " +
					"[article] VARCHAR DEFAULT '', " +
					"[remainder] FLOAT DEFAULT 0, " +
					"[manufacturer] VARCHAR DEFAULT '', " +
					"[price] FLOAT DEFAULT 0, " +
					"[discount1] FLOAT DEFAULT 0, " +
					"[discount2] FLOAT DEFAULT 0, " +
					"[discount3] FLOAT DEFAULT 0, " +
					"[discount4] FLOAT DEFAULT 0, " +
					"[term] DATETIME" +
					")";
				query.SetCommand(sqlCommand);
				if(!query.Execute()){
					query.Dispose();
					Utilits.Console.Log("[ОШИБКА] Прайс таблица " + ExcelTableID + " не создана!", false, true);
					return false;
				}
				
				oleDb = new OleDb(DataConfig.localDatabase);
				oleDb.oleDbCommandSelect.CommandText = "SELECT id, name, code, " +
					"series, article, remainder, manufacturer, "+
					"price, discount1, discount2, discount3, discount4, term "+
					"FROM "+ExcelTableID+" WHERE ([id] = 0)";
				oleDb.ExecuteFill(ExcelTableID);
				double value;
				try{
					foreach (DataRow row in dataSet.Tables[0].Rows){
						if(row.RowState == DataRowState.Deleted) continue;
						DataRow newRow = oleDb.dataSet.Tables[ExcelTableID].NewRow();
						if(numericUpDown5.Value > 0) newRow["name"] = row[(int)numericUpDown5.Value-1];
						else newRow["name"] = "";
						if(numericUpDown6.Value > 0) newRow["code"] = row[(int)numericUpDown6.Value-1];
						else newRow["code"] = "";
						if(numericUpDown7.Value > 0) newRow["series"] = row[(int)numericUpDown7.Value-1];
						else newRow["series"] = "";
						if(numericUpDown8.Value > 0) newRow["article"] = row[(int)numericUpDown8.Value-1];
						else newRow["article"] = "";
						if(numericUpDown14.Value > 0) newRow["remainder"] = row[(int)numericUpDown14.Value-1];
						else newRow["remainder"] = 0;
						if(numericUpDown15.Value > 0) newRow["manufacturer"] = row[(int)numericUpDown15.Value-1];
						else newRow["manufacturer"] = "";
						if(numericUpDown9.Value > 0){
							value = (double)row[(int)numericUpDown9.Value-1];
							newRow["price"] = Math.Round(value, 2);
						} else newRow["price"] = 0;
						if(numericUpDown10.Value > 0){
							value = (double)row[(int)numericUpDown10.Value-1];
							newRow["discount1"] = Math.Round(value, 2);
						} else newRow["discount1"] = 0;
						if(numericUpDown11.Value > 0){
							value = (double)row[(int)numericUpDown11.Value-1];
							newRow["discount2"] = Math.Round(value, 2);
						} else newRow["discount2"] = 0;
						if(numericUpDown12.Value > 0){
							value = (double)row[(int)numericUpDown12.Value-1];
							newRow["discount3"] = Math.Round(value, 2);
						} else newRow["discount3"] = 0;
						if(numericUpDown13.Value > 0) {
							value = (double)row[(int)numericUpDown13.Value-1];
							newRow["discount4"] = Math.Round(value, 2);
						} else newRow["discount4"] = 0;
						if(numericUpDown16.Value > 0) newRow["term"] = row[(int)numericUpDown16.Value-1];
						else newRow["term"] = "01.01.0001";
						oleDb.dataSet.Tables[ExcelTableID].Rows.Add(newRow);
					}
				}catch(Exception ex){
					oleDb.Error();
					Utilits.Console.Log("[ОШИБКА] Ошибка совпадения колонок, или у одной из выбранных колонок не верный тим данных. Описание ошибки: " + ex.ToString(), false, true);
					removePrice(ExcelTableID);
					return false;
				}
				oleDb.oleDbCommandInsert.CommandText = "INSERT INTO " + ExcelTableID + " (" +
					"name, code, series, article, remainder, manufacturer, price, " +
					"discount1, discount2, discount3, discount4, term) " +
					"VALUES (@name, @code, @series, @article, @remainder, @manufacturer, @price, " +
					"@discount1, @discount2, @discount3, @discount4, @term)";
				oleDb.oleDbCommandInsert.Parameters.Add("@name", OleDbType.VarChar, 255, "name");
				oleDb.oleDbCommandInsert.Parameters.Add("@code", OleDbType.VarChar, 255, "code");
				oleDb.oleDbCommandInsert.Parameters.Add("@series", OleDbType.VarChar, 255, "series");
				oleDb.oleDbCommandInsert.Parameters.Add("@article", OleDbType.VarChar, 255, "article");
				oleDb.oleDbCommandInsert.Parameters.Add("@remainder", OleDbType.Double, 15, "remainder");
				oleDb.oleDbCommandInsert.Parameters.Add("@manufacturer", OleDbType.VarChar, 255, "manufacturer");
				oleDb.oleDbCommandInsert.Parameters.Add("@price", OleDbType.Double, 15, "price");
				oleDb.oleDbCommandInsert.Parameters.Add("@discount1", OleDbType.Double, 15, "discount1");
				oleDb.oleDbCommandInsert.Parameters.Add("@discount2", OleDbType.Double, 15, "discount2");
				oleDb.oleDbCommandInsert.Parameters.Add("@discount3", OleDbType.Double, 15, "discount3");
				oleDb.oleDbCommandInsert.Parameters.Add("@discount4", OleDbType.Double, 15, "discount4");
				oleDb.oleDbCommandInsert.Parameters.Add("@term", OleDbType.Date, 15, "term");
				if(!oleDb.ExecuteUpdate(ExcelTableID)){
					oleDb.Error();
					Utilits.Console.Log("[ОШИБКА] В прайс таблицу " + ExcelTableID + " неполучилось записать данные!", false, true);
					removePrice(ExcelTableID);
					return false;
				}
				
				Utilits.Console.Log("[Контрагенты] Прайс таблица " + ExcelTableID + " успешно создана.");
				return true;
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				
			}
			return false;
		}
		
		void removePrice(String excelTableID)
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				String sqlCommand;
				QueryOleDb query;
				query = new QueryOleDb(DataConfig.localDatabase);
				sqlCommand = "DROP TABLE " + excelTableID;
				query.SetCommand(sqlCommand);
				if(!query.Execute()){
					query.Dispose();
					Utilits.Console.Log("[ОШИБКА] Прайс таблицу " + excelTableID + " не получилось удалить!", false, true);
				}else{
					query.Dispose();
					Utilits.Console.Log("Созданная прайс таблица " + excelTableID + " была удалена.");
				}
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				
			}
		}
		
		void open()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				getPrice();
				oleDb = new OleDb(DataConfig.localDatabase);
				oleDb.oleDbCommandSelect.CommandText = "SELECT id, name, type, " +
					"organization_address, organization_phone, organization_site, organization_email, " +
					"contact_fullname, contact_post, contact_phone, contact_skype, contact_email, information, " +
					"excel_filename, excel_date, excel_column_name, excel_column_code, excel_column_series, " +
					"excel_column_article, excel_column_remainder, excel_column_manufacturer, excel_column_price, " +
					"excel_column_discount_1, excel_column_discount_2, excel_column_discount_3, excel_column_discount_4, " +
					"excel_column_term, excel_table_id, parent " +
					"FROM Counteragents WHERE (id = " + ID + ")";
				oleDb.ExecuteFill("Counteragents");
				idTextBox.Text = oleDb.dataSet.Tables["Counteragents"].Rows[0]["id"].ToString();
				textBox1.Text = oleDb.dataSet.Tables["Counteragents"].Rows[0]["name"].ToString();
				textBox2.Text = oleDb.dataSet.Tables["Counteragents"].Rows[0]["organization_address"].ToString();
				textBox3.Text = oleDb.dataSet.Tables["Counteragents"].Rows[0]["organization_phone"].ToString();
				textBox4.Text = oleDb.dataSet.Tables["Counteragents"].Rows[0]["organization_site"].ToString();
				textBox5.Text = oleDb.dataSet.Tables["Counteragents"].Rows[0]["organization_email"].ToString();
				textBox6.Text = oleDb.dataSet.Tables["Counteragents"].Rows[0]["contact_fullname"].ToString();
				textBox7.Text = oleDb.dataSet.Tables["Counteragents"].Rows[0]["contact_post"].ToString();
				textBox8.Text = oleDb.dataSet.Tables["Counteragents"].Rows[0]["contact_phone"].ToString();
				textBox9.Text = oleDb.dataSet.Tables["Counteragents"].Rows[0]["contact_skype"].ToString();
				textBox10.Text = oleDb.dataSet.Tables["Counteragents"].Rows[0]["contact_email"].ToString();
				textBox11.Text = oleDb.dataSet.Tables["Counteragents"].Rows[0]["information"].ToString();
				fileTextBox.Text = oleDb.dataSet.Tables["Counteragents"].Rows[0]["excel_filename"].ToString();
				dateLabel.Text = oleDb.dataSet.Tables["Counteragents"].Rows[0]["excel_date"].ToString();
				checkBox1.Checked = true;	numericUpDown5.Value = 2;
				checkBox2.Checked = true;	numericUpDown6.Value = 3;
				checkBox3.Checked = true;	numericUpDown7.Value = 4;
				checkBox4.Checked = true;	numericUpDown8.Value = 5;
				checkBox10.Checked = true;	numericUpDown14.Value = 6;
				checkBox11.Checked = true;	numericUpDown15.Value = 7;
				checkBox5.Checked = true;	numericUpDown9.Value = 8;
				checkBox6.Checked = true;	numericUpDown10.Value = 9;
				checkBox7.Checked = true;	numericUpDown11.Value = 10;
				checkBox8.Checked = true;	numericUpDown12.Value = 11;
				checkBox9.Checked = true;	numericUpDown13.Value = 12;
				checkBox12.Checked = true;	numericUpDown16.Value = 13;
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER && DataConfig.typeDatabase == DataConstants.TYPE_MSSQL){
				// MSSQL SERVER
				
			}
		}
		
		void saveNew()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				oleDb = new OleDb(DataConfig.localDatabase);
				oleDb.oleDbCommandSelect.CommandText = "SELECT id, name, type, " +
					"organization_address, organization_phone, organization_site, organization_email, " +
					"contact_fullname, contact_post, contact_phone, contact_skype, contact_email, information, " +
					"excel_filename, excel_date, excel_column_name, excel_column_code, excel_column_series, " +
					"excel_column_article, excel_column_remainder, excel_column_manufacturer, excel_column_price, " +
					"excel_column_discount_1, excel_column_discount_2, excel_column_discount_3, excel_column_discount_4, " +
					"excel_column_term, excel_table_id, parent " +
					"FROM Counteragents WHERE (id = 0)";
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
				newRow["excel_table_id"] = ExcelTableID;
				newRow["parent"] = foldersComboBox.Text;
				
				oleDb.dataSet.Tables["Counteragents"].Rows.Add(newRow);
				
				oleDb.oleDbCommandInsert.CommandText = "INSERT INTO Counteragents " +
					"(name, type, " +
					"organization_address, organization_phone, organization_site, organization_email, " +
					"contact_fullname, contact_post, contact_phone, contact_skype, contact_email, information, " +
					"excel_filename, excel_date, excel_column_name, excel_column_code, excel_column_series, " +
					"excel_column_article, excel_column_remainder, excel_column_manufacturer, excel_column_price, " +
					"excel_column_discount_1, excel_column_discount_2, excel_column_discount_3, excel_column_discount_4, " +
					"excel_column_term, excel_table_id, parent) " +
					"VALUES (@name, @type, " +
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
				}else{
					oleDb.Error();
					removePrice(ExcelTableID);
					Utilits.Console.Log("[ОШИБКА] Ошибка создания нового контрагента.");
				}
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				
			}
			
		}
		
		void saveEdit()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				oleDb = new OleDb(DataConfig.localDatabase);
				oleDb.oleDbCommandSelect.CommandText = "SELECT id, name, type, " +
					"organization_address, organization_phone, organization_site, organization_email, " +
					"contact_fullname, contact_post, contact_phone, contact_skype, contact_email, information, " +
					"excel_filename, excel_date, excel_column_name, excel_column_code, excel_column_series, " +
					"excel_column_article, excel_column_remainder, excel_column_manufacturer, excel_column_price, " +
					"excel_column_discount_1, excel_column_discount_2, excel_column_discount_3, excel_column_discount_4, " +
					"excel_column_term, excel_table_id, parent " +
					"FROM Counteragents WHERE (id = " + ID +")";
				oleDb.ExecuteFill("Counteragents");	
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["name"] = textBox1.Text;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["type"] = "file";
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["organization_address"] = textBox2.Text;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["organization_phone"] = textBox3.Text;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["organization_site"] = textBox4.Text;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["organization_email"] = textBox5.Text;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["contact_fullname"] = textBox6.Text;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["contact_post"] = textBox7.Text;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["contact_phone"] = textBox8.Text;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["contact_skype"] = textBox9.Text;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["contact_email"] = textBox10.Text;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["information"] = textBox11.Text;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["excel_filename"] = fileTextBox.Text;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["excel_date"] = dateLabel.Text;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["excel_column_name"] = numericUpDown5.Value;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["excel_column_code"] = numericUpDown6.Value;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["excel_column_series"] = numericUpDown7.Value;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["excel_column_article"] = numericUpDown8.Value;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["excel_column_remainder"] = numericUpDown14.Value;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["excel_column_manufacturer"] = numericUpDown15.Value;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["excel_column_price"] = numericUpDown9.Value;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["excel_column_discount_1"] = numericUpDown10.Value;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["excel_column_discount_2"] = numericUpDown11.Value;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["excel_column_discount_3"] = numericUpDown12.Value;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["excel_column_discount_4"] = numericUpDown13.Value;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["excel_column_term"] = numericUpDown16.Value;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["parent"] = foldersComboBox.Text;
				oleDb.dataSet.Tables["Counteragents"].Rows[0]["excel_table_id"] = ExcelTableID;
				oleDb.oleDbCommandUpdate.CommandText = "UPDATE Counteragents SET " +
					"[name] = @name , [type] = @type, " +
					"[organization_address] = @organization_address, " +
					"[organization_phone] = @organization_phone, " +
					"[organization_site] = @organization_site, " +
					"[organization_email] = @organization_email, " +
					"[contact_fullname] = @contact_fullname, " +
					"[contact_post] = @contact_post, " +
					"[contact_phone] = @contact_phone, " +
					"[contact_skype] = @contact_skype, " +
					"[contact_email] = @contact_email, " +
					"[information] = @information, " +
					"[excel_filename] = @excel_filename, " +
					"[excel_date] = @excel_date, " +
					"[excel_column_name] = @excel_column_name, " +
					"[excel_column_code] = @excel_column_code, " +
					"[excel_column_series] = @excel_column_series, " +
					"[excel_column_article] = @excel_column_article, " +
					"[excel_column_remainder] = @excel_column_remainder, " +
					"[excel_column_manufacturer] = @excel_column_manufacturer, " +
					"[excel_column_price] = @excel_column_price, " +
					"[excel_column_discount_1] = @excel_column_discount_1, " +
					"[excel_column_discount_2] = @excel_column_discount_2, " +
					"[excel_column_discount_3] = @excel_column_discount_3, " +
					"[excel_column_discount_4] = @excel_column_discount_4, " +
					"[excel_column_term] = @excel_column_term, " +
					"[parent] = @parent, " +
					"[excel_table_id] = @excel_table_id " +
					"WHERE ([id] = @id)";
				oleDb.oleDbCommandUpdate.Parameters.Add("@name", OleDbType.VarChar, 255, "name");
				oleDb.oleDbCommandUpdate.Parameters.Add("@type", OleDbType.VarChar, 255, "type");
				oleDb.oleDbCommandUpdate.Parameters.Add("@organization_address", OleDbType.VarChar, 255, "organization_address");
				oleDb.oleDbCommandUpdate.Parameters.Add("@organization_phone", OleDbType.VarChar, 255, "organization_phone");
				oleDb.oleDbCommandUpdate.Parameters.Add("@organization_site", OleDbType.VarChar, 255, "organization_site");
				oleDb.oleDbCommandUpdate.Parameters.Add("@organization_email", OleDbType.VarChar, 255, "organization_email");
				oleDb.oleDbCommandUpdate.Parameters.Add("@contact_fullname", OleDbType.VarChar, 255, "contact_fullname");
				oleDb.oleDbCommandUpdate.Parameters.Add("@contact_post", OleDbType.VarChar, 255, "contact_post");
				oleDb.oleDbCommandUpdate.Parameters.Add("@contact_phone", OleDbType.VarChar, 255, "contact_phone");
				oleDb.oleDbCommandUpdate.Parameters.Add("@contact_skype", OleDbType.VarChar, 255, "contact_skype");
				oleDb.oleDbCommandUpdate.Parameters.Add("@contact_email", OleDbType.VarChar, 255, "contact_email");
				oleDb.oleDbCommandUpdate.Parameters.Add("@information", OleDbType.LongVarChar, 0, "information");
				oleDb.oleDbCommandUpdate.Parameters.Add("@excel_filename", OleDbType.LongVarChar, 0, "excel_filename");
				oleDb.oleDbCommandUpdate.Parameters.Add("@excel_date", OleDbType.VarChar, 255, "excel_date");
				oleDb.oleDbCommandUpdate.Parameters.Add("@excel_column_name", OleDbType.Integer, 0, "excel_column_name");
				oleDb.oleDbCommandUpdate.Parameters.Add("@excel_column_code", OleDbType.Integer, 0, "excel_column_code");
				oleDb.oleDbCommandUpdate.Parameters.Add("@excel_column_series", OleDbType.Integer, 0, "excel_column_series");
				oleDb.oleDbCommandUpdate.Parameters.Add("@excel_column_article", OleDbType.Integer, 0, "excel_column_article");
				oleDb.oleDbCommandUpdate.Parameters.Add("@excel_column_remainder", OleDbType.Integer, 0, "excel_column_remainder");
				oleDb.oleDbCommandUpdate.Parameters.Add("@excel_column_manufacturer", OleDbType.Integer, 0, "excel_column_manufacturer");
				oleDb.oleDbCommandUpdate.Parameters.Add("@excel_column_price", OleDbType.Integer, 0, "excel_column_price");
				oleDb.oleDbCommandUpdate.Parameters.Add("@excel_column_discount_1", OleDbType.Integer, 0, "excel_column_discount_1");
				oleDb.oleDbCommandUpdate.Parameters.Add("@excel_column_discount_2", OleDbType.Integer, 0, "excel_column_discount_2");
				oleDb.oleDbCommandUpdate.Parameters.Add("@excel_column_discount_3", OleDbType.Integer, 0, "excel_column_discount_3");
				oleDb.oleDbCommandUpdate.Parameters.Add("@excel_column_discount_4", OleDbType.Integer, 0, "excel_column_discount_4");
				oleDb.oleDbCommandUpdate.Parameters.Add("@excel_column_term", OleDbType.Integer, 0, "excel_column_term");
				oleDb.oleDbCommandUpdate.Parameters.Add("@parent", OleDbType.VarChar, 255, "parent");
				oleDb.oleDbCommandUpdate.Parameters.Add("@excel_table_id", OleDbType.VarChar, 255, "excel_table_id");
				oleDb.oleDbCommandUpdate.Parameters.Add("@id", OleDbType.Integer, 10, "id");
				if(oleDb.ExecuteUpdate("Counteragents")){
					DataForms.FClient.updateHistory("Counteragents");
					Utilits.Console.Log("Контрагент успешно изменён.");
					Close();
				}else{
					oleDb.Error();
					Utilits.Console.Log("[ОШИБКА] Ошибка изменения контрагента.");
				}
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				
			}
		}
		
		bool check()
		{
			if(textBox1.Text == "") return false;
			if(foldersComboBox.Text != ""){
				bool checkParent = false;
				for(int i = 0; i < foldersComboBox.Items.Count; i++){
					if(foldersComboBox.Text == foldersComboBox.Items[i].ToString()){
						checkParent = true;
						break;
					}
				}
				if(checkParent) return true;
				else return false;
			}else{
				return true;
			}
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
				firstColumnNumber = true;
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
				if(ID == null){
					ExcelTableID = "Price" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
					if(setPrice()) saveNew();
				}else{
					String oldExcelTableID = ExcelTableID;
					ExcelTableID = "Price" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
					if(setPrice()){
						removePrice(oldExcelTableID);
						saveEdit();
					}
				}
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
		void ButtonClearClick(object sender, EventArgs e)
		{
			Button target = (sender as Button);
			if(target.Name == "button1") textBox1.Clear();
			if(target.Name == "button2") textBox2.Clear();
			if(target.Name == "button3") textBox3.Clear();
			if(target.Name == "button4") textBox4.Clear();
			if(target.Name == "button5") textBox5.Clear();
			if(target.Name == "button6") textBox6.Clear();
			if(target.Name == "button7") textBox7.Clear();
			if(target.Name == "button8") textBox8.Clear();
			if(target.Name == "button9") textBox9.Clear();
			if(target.Name == "button10") textBox10.Clear();
			if(target.Name == "button11") foldersComboBox.Text = "";
		}

	}
}
