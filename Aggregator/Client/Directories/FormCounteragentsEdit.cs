/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 04.03.2017
 * Time: 18:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Database.Local;

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
	}
}
