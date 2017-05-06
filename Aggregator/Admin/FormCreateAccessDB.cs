/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 06.05.2017
 * Время: 12:06
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Database;

namespace Aggregator.Admin
{
	/// <summary>
	/// Description of FormCreateAccessDB.
	/// </summary>
	public partial class FormCreateAccessDB : Form
	{
		public FormCreateAccessDB()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void FormCreateAccessDBLoad(object sender, EventArgs e)
		{
	
		}
		void FormCreateAccessDBFormClosed(object sender, FormClosedEventArgs e)
		{
	
		}
		void FormCreateAccessDBActivated(object sender, EventArgs e)
		{
	
		}
		void ButtonCloseClick(object sender, EventArgs e)
		{
			Close();
		}
		void Button1Click(object sender, EventArgs e)
		{
			if(folderBrowserDialog1.ShowDialog() == DialogResult.OK){
				pathTextBox.Text = folderBrowserDialog1.SelectedPath;
			}
		}
		void ButtonSaveClick(object sender, EventArgs e)
		{
			CreateDatabaseMSAccess createDatabaseMSAccess = new CreateDatabaseMSAccess(pathTextBox.Text + "\\" + baseNameTextBox.Text + ".mdb");
			createDatabaseMSAccess.CreateDB();
		}
	}
}
