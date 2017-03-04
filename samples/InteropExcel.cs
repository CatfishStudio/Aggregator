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
using Microsoft.Office.Interop.Excel;

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
				ApplicationClass app = new ApplicationClass();
				Workbook book = null;
				Worksheet sheet = null;
				Range range = null;
								
				app.Visible = false;
				app.ScreenUpdating = false;
				app.DisplayAlerts = false;
				
				book = app.Workbooks.Open(FileName,
				       Missing.Value, Missing.Value, Missing.Value,
				       Missing.Value, Missing.Value, Missing.Value, Missing.Value,
				       Missing.Value, Missing.Value, Missing.Value, Missing.Value,
				       Missing.Value, Missing.Value, Missing.Value);
				sheet = (Worksheet)book.Worksheets[1];
				//MessageBox.Show(sheet.Range[0, 0].Value.ToString());
			}catch(Exception ex){
				MessageBox.Show(ex.ToString(), "ERROR");
			}
		}
		
	}
}