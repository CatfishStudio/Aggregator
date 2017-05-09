/*
 * Создано в SharpDevelop.
 * Пользователь: Somov Studio
 * Дата: 09.05.2017
 * Время: 6:58
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Aggregator.Trial
{
	/// <summary>
	/// Description of FormTrial.
	/// </summary>
	public partial class FormTrial : Form
	{
		public FormTrial()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void Button2Click(object sender, EventArgs e)
		{
			Close();
		}
		void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(linkLabel1.Text);
		}
	}
}
