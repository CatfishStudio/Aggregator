/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 04.03.2017
 * Time: 10:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Data;

namespace Aggregator.Client.Directories
{
	/// <summary>
	/// Description of FormConstants.
	/// </summary>
	public partial class FormConstants : Form
	{
		public FormConstants()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */	
		
		void FormConstantsLoad(object sender, EventArgs e)
		{
	
		}
		void FormConstantsFormClosed(object sender, FormClosedEventArgs e)
		{
			Dispose();
			DataForms.FConstants = null;
		}
		void ButtonSaveClick(object sender, EventArgs e)
		{
			if(DataConfig.userPermissions == "user" || DataConfig.userPermissions == "guest") {
				MessageBox.Show("У вас недостаточно прав чтобы выполнить эту операцию.", "Сообщение");
				return;
			}
		}
		void ButtonCancelClick(object sender, EventArgs e)
		{
			Close();
		}
	}
}
