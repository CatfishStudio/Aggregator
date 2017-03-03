/*
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

namespace Aggregator.Client.OpenFiles
{
	/// <summary>
	/// Description of FormOpenXLS.
	/// </summary>
	public partial class FormOpenXLS : Form
	{
		public FormOpenXLS()
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
		
		void FormOpenXLSLoad(object sender, EventArgs e)
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
		
	}
}
