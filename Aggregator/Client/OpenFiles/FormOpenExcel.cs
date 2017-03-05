﻿/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 03.03.2017
 * Time: 18:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Excel;
using System.Data;
using System.Data.OleDb;

namespace Aggregator.Client.OpenFiles
{
	/// <summary>
	/// Description of FormOpenExcel.
	/// </summary>
	public partial class FormOpenExcel : Form
	{
		public FormOpenExcel()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public String FileName;
		public String FileFormat;
		
		void excelDataRead()
		{
			try{
				FileStream stream = File.Open(FileName, FileMode.Open, FileAccess.Read);

				//1. Reading from a binary Excel file ('97-2003 format; *.xls)
				IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
				
				//2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
				//IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
				
				//3. DataSet - The result of each spreadsheet will be created in the result.Tables
				//DataSet result = excelReader.AsDataSet();
				
				//4. DataSet - Create column names from first row
				excelReader.IsFirstRowAsColumnNames = true;
				DataSet result = excelReader.AsDataSet();
				
				//5. Data Reader methods
				
				while (excelReader.Read())
				{
					//excelReader.GetInt32(0);
				}

				MessageBox.Show(result.Tables.Count.ToString());
				MessageBox.Show(result.Tables[0].Rows.Count.ToString());
				MessageBox.Show(result.Tables[0].Rows[0][0].ToString());
				
				excelReader.Close();
			}catch(Exception ex){
				MessageBox.Show(ex.ToString(), "ERROR");
			}
		}
		
		void readExcelFormat972003()
		{
			dataGrid1.CaptionText = FileName;
			try{
				FileStream stream = File.Open(FileName, FileMode.Open, FileAccess.Read);
				IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
				DataSet dataSet = excelReader.AsDataSet();
				dataGrid1.DataSource = dataSet;
				excelReader.Close();
			}catch(Exception ex){
				MessageBox.Show(ex.ToString(), "ERROR");
			}
		}
		
		void readExcelFormat2007()
		{
			dataGrid1.CaptionText = FileName;
			try{
				FileStream stream = File.Open(FileName, FileMode.Open, FileAccess.Read);
				IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
				DataSet dataSet = excelReader.AsDataSet();
				dataGrid1.DataSource = dataSet;
				excelReader.Close();
			}catch(Exception ex){
				MessageBox.Show(ex.ToString(), "ERROR");
			}
		}
		
		/*
		void readExcelOleDb()
		{
			string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; data source={" +	FileName + "}; Extended Properties=Excel 12.0;";
			string sqlCommand = string.Format("select * from [{0}$]", sheetName);
			OleDbConnection conn = null;
			OleDbDataAdapter adapter = null;
			DataTable dt = null;
			
			try{
				conn = new OleDbConnection(connectionString);
				conn.Open();
				dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
				adapter = new OleDbDataAdapter("select *", conn.ConnectionString);
				adapter.Fill(dataSet1);
				
				
			}catch(Exception ex){
				MessageBox.Show(ex.ToString(), "ERROR");
			}
		}
		*/
		
		void FormOpenXLSLoad(object sender, EventArgs e)
		{
			if(FileFormat == "xls"){
				readExcelFormat972003();
			}else if(FileFormat == "xlsx"){
				readExcelFormat2007();
			}
			
		}
		
	}
}