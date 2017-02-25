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
			oleDb.SelectCommand = textBox1.Text;
			oleDb.ExecuteFill(textBox2.Text);
			dataGrid1.DataSource = oleDb.dataSet;
			Utilits.Console.Log("ЗАПРОС: " + textBox1.Text);
			// Запрос Select
			/*
			if(checkBox1.Checked){
				oleDb.SelectCommand = textBox1.Text;
				oleDb.ExecuteFill(textBox2.Text);
				dataGrid1.DataSource = oleDb.dataSet;
				Utilits.Console.Log("SELECT");
			}			
			// Запрос Insert
			if(checkBox2.Checked){
				oleDb.InsertCommand = textBox1.Text;
				oleDb.ExecuteUpdate(textBox2.Text);
				dataGrid1.DataSource = oleDb.dataSet;
				Utilits.Console.Log("INSERT");
			}
			// Запрос Update
			if(checkBox3.Checked){
				oleDb.UpdateCommand = textBox1.Text;
				oleDb.ExecuteUpdate(textBox2.Text);
				dataGrid1.DataSource = oleDb.dataSet;
				Utilits.Console.Log("UPDATE");
			}
			// Запрос Delete
			if(checkBox4.Checked){
				oleDb.DeleteCommand = textBox1.Text;
				oleDb.ExecuteUpdate(textBox2.Text);
				dataGrid1.DataSource = oleDb.dataSet;
				Utilits.Console.Log("DELETE");
			}
			*/
		}
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			textBox1.Text = "SELECT * FROM Table";
		}
		void CheckBox2CheckedChanged(object sender, EventArgs e)
		{
			textBox1.Text = "INSERT INTO Table ([Name]) VALUES ('name')";
		}
		void CheckBox3CheckedChanged(object sender, EventArgs e)
		{
			textBox1.Text = "UPDATE Table SET [Name] = 'name' WHERE (ID = 0)";
		}
		void CheckBox4CheckedChanged(object sender, EventArgs e)
		{
			textBox1.Text = "DELETE FROM Table WHERE (ID = 0)";
		}
	}
}
