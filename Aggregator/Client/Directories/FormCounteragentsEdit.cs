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
	public partial class FormCounteragentsEdit : Form
	{
		public FormCounteragentsEdit()
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
				if(fileTextBox.Text.Substring(fileTextBox.Text.Length - 3) == "xls"){
					readExcelFormat972003();
				}else{
					readExcelFormat2007();
				}
			}
			numericUpDown1.Minimum = 1;
			numericUpDown1.Maximum = dataSet.Tables[0].Rows.Count;
			numericUpDown1.Value = 1;
			numericUpDown2.Minimum = 1;
			numericUpDown2.Maximum = dataSet.Tables[0].Rows.Count;
			numericUpDown2.Value = dataSet.Tables[0].Rows.Count;
			numericUpDown3.Minimum = 1;
			numericUpDown3.Maximum = dataSet.Tables[0].Columns.Count;
			numericUpDown3.Value = 1;
			numericUpDown4.Minimum = 1;
			numericUpDown4.Maximum = dataSet.Tables[0].Columns.Count;
			numericUpDown4.Value = dataSet.Tables[0].Columns.Count;
		}
		
		void readExcelFormat972003()
		{
			try{
				FileStream stream = File.Open(fileTextBox.Text, FileMode.Open, FileAccess.Read);
				IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
				dataSet = excelReader.AsDataSet();
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
				dataGrid1.DataSource = dataSet;
				dataGrid1.DataMember = dataSet.Tables[0].TableName;
				excelReader.Close();
			}catch(Exception ex){
				MessageBox.Show(ex.ToString(), "ERROR");
			}
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
		
	}
}
