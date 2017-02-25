/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 25.02.2017
 * Time: 16:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Data;
using Aggregator.Database;

namespace Aggregator.Admin
{
	/// <summary>
	/// Description of FormQueryDesigner.
	/// </summary>
	public partial class FormQueryDesigner : Form
	{
		public FormQueryDesigner()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		private OleDb2 oleDb;
		
		void FormQueryDesignerLoad(object sender, EventArgs e)
		{
			comboBox1.Items.Add(DataConfig.configFile);
			comboBox1.Items.Add(DataConfig.localDatabase);
		}
		void FormQueryDesignerFormClosed(object sender, FormClosedEventArgs e)
		{
			Dispose();
			DataForms.FQueryDesigner = null;
		}
		void Button1Click(object sender, EventArgs e)
		{
			oleDb.dataSet.Clear();
			dataGrid1.DataSource = oleDb.dataSet;
		}
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			oleDb = new OleDb2(comboBox1.Text);
		}
		void Button2Click(object sender, EventArgs e)
		{
			// Запрос Select
			if(checkBox1.Checked){
				oleDb.SelectCommand = textBox1.Text;
				oleDb.ExecuteFill(oleDb.dataSet, textBox2.Text);
				dataGrid1.DataSource = oleDb.dataSet;
			}
			// Запрос Insert
			if(checkBox2.Checked){
				oleDb.InsertCommand = textBox1.Text;
				oleDb.ExecuteUpdate(oleDb.dataSet, textBox2.Text);
				dataGrid1.DataSource = oleDb.dataSet;
			}
			// Запрос Update
			if(checkBox3.Checked){
				oleDb.UpdateCommand = textBox1.Text;
				oleDb.ExecuteUpdate(oleDb.dataSet, textBox2.Text);
				dataGrid1.DataSource = oleDb.dataSet;
			}
			// Запрос Delete
			if(checkBox4.Checked){
				oleDb.DeleteCommand = textBox1.Text;
				oleDb.ExecuteUpdate(oleDb.dataSet, textBox2.Text);
				dataGrid1.DataSource = oleDb.dataSet;
			}
		}
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			checkBox2.Checked = false;
			checkBox3.Checked = false;
			checkBox4.Checked = false;
		}
		void CheckBox2CheckedChanged(object sender, EventArgs e)
		{
			checkBox1.Checked = false;
			checkBox3.Checked = false;
			checkBox4.Checked = false;
		}
		void CheckBox3CheckedChanged(object sender, EventArgs e)
		{
			checkBox1.Checked = false;
			checkBox2.Checked = false;
			checkBox4.Checked = false;
		}
		void CheckBox4CheckedChanged(object sender, EventArgs e)
		{
			checkBox1.Checked = false;
			checkBox2.Checked = false;
			checkBox3.Checked = false;
		}
	}
}
