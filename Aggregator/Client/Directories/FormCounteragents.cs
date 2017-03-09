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
		
		void addFile()
		{
			FormCounteragentFile FCounteragentEdit = new FormCounteragentFile();
			FCounteragentEdit.MdiParent = DataForms.FClient;
			FCounteragentEdit.ID = null;
			FCounteragentEdit.Show();
		}
		
		void addFolder()
		{
			FormCounteragentFolder FCounteragentFolder = new FormCounteragentFolder();
			FCounteragentFolder.MdiParent = DataForms.FClient;
			FCounteragentFolder.ID = null;
			FCounteragentFolder.Show();
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
			addFile();
		}
		void AddFolderButtonClick(object sender, EventArgs e)
		{
			addFolder();
		}
	}
}
