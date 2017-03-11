/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 04.03.2017
 * Time: 18:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Aggregator.Database.Local;
using Excel;

namespace Aggregator.Client.Directories
{
	/// <summary>
	/// Description of FormCounteragentsEdit.
	/// </summary>
	public partial class FormCounteragentsFile : Form
	{
		public FormCounteragentsFile()
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
				initEditor();
			}
		}
		
		void reopenExcel()
		{
			if(File.Exists(fileTextBox.Text)){
				if(fileTextBox.Text.Substring(fileTextBox.Text.Length - 3) == "xls"){
					readExcelFormat972003();
				}else{
					readExcelFormat2007();
				}
			}else{
				
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
		
		void initEditor()
		{
			if(dataSet == null) return;
			if(dataSet.Tables.Count > 0){
				dataGrid1.Enabled = true;
				applySettingsButton.Enabled = true;
				resetSettingsButton.Enabled = true;
				
				numericUpDown1.Minimum = 1;
				numericUpDown1.Maximum = dataSet.Tables[0].Rows.Count;
				numericUpDown1.Value = 1;
				numericUpDown1.Enabled = true;
				
				numericUpDown2.Minimum = 1;
				numericUpDown2.Maximum = dataSet.Tables[0].Rows.Count;
				numericUpDown2.Value = dataSet.Tables[0].Rows.Count;
				numericUpDown2.Enabled = true;
				
				numericUpDown3.Minimum = 1;
				numericUpDown3.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown3.Value = 1;
				numericUpDown3.Enabled = true;
				
				numericUpDown4.Minimum = 1;
				numericUpDown4.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown4.Value = dataSet.Tables[0].Columns.Count;
				numericUpDown4.Enabled = true;
				
				numericUpDown5.Minimum = 0;
				numericUpDown5.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown6.Minimum = 0;
				numericUpDown6.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown7.Minimum = 0;
				numericUpDown7.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown8.Minimum = 0;
				numericUpDown8.Maximum = dataSet.Tables[0].Columns.Count;
			}
		}
		
		void initColunms()
		{
			if(dataSet == null) return;
			for(int i = 0; i < dataSet.Tables[0].Columns.Count; i++) {
				dataSet.Tables[0].Columns[i].ColumnName = (i+1).ToString();
			}
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
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */	
		void FormCounteragentsEditLoad(object sender, EventArgs e)
		{
			
		}
		void FormCounteragentsEditFormClosed(object sender, FormClosedEventArgs e)
		{
			Dispose();
		}
		void DataGrid1MouseClick(object sender, MouseEventArgs e)
		{
			stringNumberLabel.Text = "Строка №: " + (dataGrid1.CurrentRowIndex + 1).ToString();
		}
		void OpenExcelButtonClick(object sender, EventArgs e)
		{
			openFileExcel();
		}
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			if(dataSet == null){
				checkBox1.Checked = false;
				return;
			}
			if(checkBox1.Checked){
				numericUpDown5.Enabled = true;
				numericUpDown5.Minimum = 1;
				numericUpDown5.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown5.Value = 1;
			}else{
				numericUpDown5.Enabled = false;
				numericUpDown5.Minimum = 0;
				numericUpDown5.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown5.Value = 0;
			}
			initColunms();
		}
		void CheckBox2CheckedChanged(object sender, EventArgs e)
		{
			if(dataSet == null){
				checkBox2.Checked = false;
				return;
			}
			if(checkBox2.Checked){
				numericUpDown6.Enabled = true;
				numericUpDown6.Minimum = 1;
				numericUpDown6.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown6.Value = 1;
			}else{
				numericUpDown6.Enabled = false;
				numericUpDown6.Minimum = 0;
				numericUpDown6.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown6.Value = 0;
			}
			initColunms();
		}
		void CheckBox3CheckedChanged(object sender, EventArgs e)
		{
			if(dataSet == null){
				checkBox3.Checked = false;
				return;
			}
			if(checkBox3.Checked){
				numericUpDown7.Enabled = true;
				numericUpDown7.Minimum = 1;
				numericUpDown7.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown7.Value = 1;
			}else{
				numericUpDown7.Enabled = false;
				numericUpDown7.Minimum = 0;
				numericUpDown7.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown7.Value = 0;
			}
			initColunms();
		}
		void CheckBox4CheckedChanged(object sender, EventArgs e)
		{
			if(dataSet == null){
				checkBox4.Checked = false;
				return;
			}
			if(checkBox4.Checked){
				numericUpDown8.Enabled = true;
				numericUpDown8.Minimum = 1;
				numericUpDown8.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown8.Value = 1;
			}else{
				numericUpDown8.Enabled = false;
				numericUpDown8.Minimum = 0;
				numericUpDown8.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown8.Value = 0;
			}
			initColunms();
		}
		void CheckBox5CheckedChanged(object sender, EventArgs e)
		{
			if(dataSet == null){
				checkBox5.Checked = false;
				return;
			}
			if(checkBox5.Checked){
				numericUpDown9.Enabled = true;
				numericUpDown9.Minimum = 1;
				numericUpDown9.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown9.Value = 1;
			}else{
				numericUpDown9.Enabled = false;
				numericUpDown9.Minimum = 0;
				numericUpDown9.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown9.Value = 0;
			}
			initColunms();
		}
		void CheckBox6CheckedChanged(object sender, EventArgs e)
		{
			if(dataSet == null){
				checkBox6.Checked = false;
				return;
			}
			if(checkBox6.Checked){
				numericUpDown10.Enabled = true;
				numericUpDown10.Minimum = 1;
				numericUpDown10.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown10.Value = 1;
			}else{
				numericUpDown10.Enabled = false;
				numericUpDown10.Minimum = 0;
				numericUpDown10.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown10.Value = 0;
			}
			initColunms();
		}
		void CheckBox7CheckedChanged(object sender, EventArgs e)
		{
			if(dataSet == null){
				checkBox7.Checked = false;
				return;
			}
			if(checkBox7.Checked){
				numericUpDown11.Enabled = true;
				numericUpDown11.Minimum = 1;
				numericUpDown11.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown11.Value = 1;
			}else{
				numericUpDown11.Enabled = false;
				numericUpDown11.Minimum = 0;
				numericUpDown11.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown11.Value = 0;
			}
			initColunms();
		}
		void CheckBox8CheckedChanged(object sender, EventArgs e)
		{
			if(dataSet == null){
				checkBox8.Checked = false;
				return;
			}
			if(checkBox8.Checked){
				numericUpDown12.Enabled = true;
				numericUpDown12.Minimum = 1;
				numericUpDown12.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown12.Value = 1;
			}else{
				numericUpDown12.Enabled = false;
				numericUpDown12.Minimum = 0;
				numericUpDown12.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown12.Value = 0;
			}
			initColunms();
		}
		void CheckBox9CheckedChanged(object sender, EventArgs e)
		{
			if(dataSet == null){
				checkBox9.Checked = false;
				return;
			}
			if(checkBox9.Checked){
				numericUpDown13.Enabled = true;
				numericUpDown13.Minimum = 1;
				numericUpDown13.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown13.Value = 1;
			}else{
				numericUpDown13.Enabled = false;
				numericUpDown13.Minimum = 0;
				numericUpDown13.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown13.Value = 0;
			}
			initColunms();
		}
		void CheckBox10CheckedChanged(object sender, EventArgs e)
		{
			if(dataSet == null){
				checkBox10.Checked = false;
				return;
			}
			if(checkBox10.Checked){
				numericUpDown14.Enabled = true;
				numericUpDown14.Minimum = 1;
				numericUpDown14.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown14.Value = 1;
			}else{
				numericUpDown14.Enabled = false;
				numericUpDown14.Minimum = 0;
				numericUpDown14.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown14.Value = 0;
			}
			initColunms();
		}
		void CheckBox11CheckedChanged(object sender, EventArgs e)
		{
			if(dataSet == null){
				checkBox11.Checked = false;
				return;
			}
			if(checkBox11.Checked){
				numericUpDown15.Enabled = true;
				numericUpDown15.Minimum = 1;
				numericUpDown15.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown15.Value = 1;
			}else{
				numericUpDown15.Enabled = false;
				numericUpDown15.Minimum = 0;
				numericUpDown15.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown15.Value = 0;
			}
			initColunms();
		}
		void CheckBox12CheckedChanged(object sender, EventArgs e)
		{
			if(dataSet == null){
				checkBox12.Checked = false;
				return;
			}
			if(checkBox12.Checked){
				numericUpDown16.Enabled = true;
				numericUpDown16.Minimum = 1;
				numericUpDown16.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown16.Value = 1;
			}else{
				numericUpDown16.Enabled = false;
				numericUpDown16.Minimum = 0;
				numericUpDown16.Maximum = dataSet.Tables[0].Columns.Count;
				numericUpDown16.Value = 0;
			}
			initColunms();
		}
		void ApplySettingsButtonClick(object sender, EventArgs e)
		{
			reopenExcel();
			if(dataSet == null) return;
			
			int i;
			int count;
			count = dataSet.Tables[0].Rows.Count;
			for(i = Convert.ToInt32(numericUpDown2.Value); i < count; i++){
				dataSet.Tables[0].Rows[i].Delete();
			}
			for(i = 0; i < numericUpDown1.Value - 1; i++){
				dataSet.Tables[0].Rows[i].Delete();
			}
			count = dataSet.Tables[0].Columns.Count;
			for(i = Convert.ToInt32(numericUpDown4.Value); i < count; i++){
				dataSet.Tables[0].Columns.RemoveAt(dataSet.Tables[0].Columns.Count-1);
			}
			for(i = 0; i < numericUpDown3.Value - 1; i++){
				dataSet.Tables[0].Columns.RemoveAt(i);
			}
		}
		void ResetSettingsButtonClick(object sender, EventArgs e)
		{
			reopenExcel();
			initEditor();
		}
		void NumericUpDownAllValueChanged(object sender, EventArgs e)
		{
			initColunms();
		}
	}
}
