/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 26.02.2017
 * Time: 16:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Data;
using Aggregator.Database.Local;

namespace Aggregator.User
{
	/// <summary>
	/// Description of FormUsers.
	/// </summary>
	public partial class FormUsers : Form
	{
		public FormUsers()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		private OleDb oleDb;
		
		public void DataTableUpdate()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				oleDb.oleDbCommandSelect.CommandText = "SELECT id, name, pass, permissions FROM Users";
				oleDb.ExecuteFill("Users");
				listView1.Items.Clear();
				foreach(DataRow row in oleDb.dataSet.Tables["Users"].Rows)
	        	{
					ListViewItem listViewItem = new ListViewItem();
					listViewItem.SubItems.Add(row["name"].ToString());
					listViewItem.SubItems.Add(row["permissions"].ToString());
					listViewItem.SubItems.Add(row["id"].ToString());
					listViewItem.StateImageIndex = 0;
					listView1.Items.Add(listViewItem);
				}
				Utilits.Console.Log("OK!");
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				
			}
		}
		
				
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */	
		void FormUsersLoad(object sender, EventArgs e)
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				oleDb = new OleDb(DataConfig.localDatabase);
				DataTableUpdate();
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				
			}
		}
	}
}
