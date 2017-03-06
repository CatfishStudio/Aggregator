﻿/*
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
				initEditor();
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
				initEditor();
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
		}
		
	}
}
