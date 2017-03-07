/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 04.03.2017
 * Time: 18:53
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
	/// Description of FormCounteragents.
	/// </summary>
	public partial class FormCounteragents : Form
	{
		public FormCounteragents()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void addUser()
		{
			FormCounteragentsFile FCounteragentsEdit = new FormCounteragentsFile();
			FCounteragentsEdit.MdiParent = DataForms.FClient;
			FCounteragentsEdit.ID = null;
			FCounteragentsEdit.Show();
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */	
		void FormCounteragentsLoad(object sender, EventArgs e)
		{
	
		}
		void FormCounteragentsFormClosed(object sender, FormClosedEventArgs e)
		{
			Dispose();
			DataForms.FCounteragents = null;
		}
		void AddButtonClick(object sender, EventArgs e)
		{
			addUser();
		}
	}
}
