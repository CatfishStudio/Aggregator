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
		OleDb oleDb;
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
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */	
		void FormCounteragentFileLoad(object sender, EventArgs e)
		{
	
		}
		void FormCounteragentFileFormClosed(object sender, FormClosedEventArgs e)
		{
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
	
		}
		void ButtonCancelClick(object sender, EventArgs e)
		{
			
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
