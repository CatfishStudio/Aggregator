/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 26.02.2017
 * Time: 16:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Aggregator.User
{
	partial class FormUsers
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.Button editButton;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Button buttonRefresh;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUsers));
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonRefresh = new System.Windows.Forms.Button();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panel4 = new System.Windows.Forms.Panel();
			this.deleteButton = new System.Windows.Forms.Button();
			this.editButton = new System.Windows.Forms.Button();
			this.addButton = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.buttonClose = new System.Windows.Forms.Button();
			this.panel3 = new System.Windows.Forms.Panel();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.buttonRefresh);
			this.panel1.Controls.Add(this.panel4);
			this.panel1.Controls.Add(this.deleteButton);
			this.panel1.Controls.Add(this.editButton);
			this.panel1.Controls.Add(this.addButton);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(620, 45);
			this.panel1.TabIndex = 0;
			// 
			// buttonRefresh
			// 
			this.buttonRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonRefresh.ImageKey = "arrow_refresh_small.png";
			this.buttonRefresh.ImageList = this.imageList1;
			this.buttonRefresh.Location = new System.Drawing.Point(298, 12);
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new System.Drawing.Size(86, 23);
			this.buttonRefresh.TabIndex = 4;
			this.buttonRefresh.Text = "Обновить";
			this.buttonRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonRefresh.UseVisualStyleBackColor = true;
			this.buttonRefresh.Click += new System.EventHandler(this.ButtonRefreshClick);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "drive_user.png");
			this.imageList1.Images.SetKeyName(1, "report_user.png");
			this.imageList1.Images.SetKeyName(2, "folder_user.png");
			this.imageList1.Images.SetKeyName(3, "group.png");
			this.imageList1.Images.SetKeyName(4, "group_add.png");
			this.imageList1.Images.SetKeyName(5, "group_delete.png");
			this.imageList1.Images.SetKeyName(6, "group_edit.png");
			this.imageList1.Images.SetKeyName(7, "group_error.png");
			this.imageList1.Images.SetKeyName(8, "group_gear.png");
			this.imageList1.Images.SetKeyName(9, "group_go.png");
			this.imageList1.Images.SetKeyName(10, "arrow_refresh_small.png");
			// 
			// panel4
			// 
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel4.Location = new System.Drawing.Point(288, 12);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(4, 23);
			this.panel4.TabIndex = 3;
			// 
			// deleteButton
			// 
			this.deleteButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.deleteButton.ImageKey = "group_delete.png";
			this.deleteButton.ImageList = this.imageList1;
			this.deleteButton.Location = new System.Drawing.Point(196, 12);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(86, 23);
			this.deleteButton.TabIndex = 2;
			this.deleteButton.Text = "Удалить";
			this.deleteButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.DeleteButtonClick);
			// 
			// editButton
			// 
			this.editButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.editButton.ImageKey = "group_edit.png";
			this.editButton.ImageList = this.imageList1;
			this.editButton.Location = new System.Drawing.Point(104, 12);
			this.editButton.Name = "editButton";
			this.editButton.Size = new System.Drawing.Size(86, 23);
			this.editButton.TabIndex = 1;
			this.editButton.Text = "Изменить";
			this.editButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.editButton.UseVisualStyleBackColor = true;
			this.editButton.Click += new System.EventHandler(this.EditButtonClick);
			// 
			// addButton
			// 
			this.addButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.addButton.ImageKey = "group_add.png";
			this.addButton.ImageList = this.imageList1;
			this.addButton.Location = new System.Drawing.Point(12, 12);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(86, 23);
			this.addButton.TabIndex = 0;
			this.addButton.Text = "Добавить";
			this.addButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.AddButtonClick);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.buttonClose);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 299);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(620, 45);
			this.panel2.TabIndex = 1;
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.Location = new System.Drawing.Point(533, 10);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 0;
			this.buttonClose.Text = "Закрыть";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.listView1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 45);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(620, 254);
			this.panel3.TabIndex = 2;
			// 
			// listView1
			// 
			this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader1,
			this.columnHeader2,
			this.columnHeader3,
			this.columnHeader4});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.LargeImageList = this.imageList1;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(620, 254);
			this.listView1.SmallImageList = this.imageList1;
			this.listView1.StateImageList = this.imageList1;
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "...";
			this.columnHeader1.Width = 40;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Имя";
			this.columnHeader2.Width = 350;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Права";
			this.columnHeader3.Width = 150;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "№";
			// 
			// FormUsers
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(620, 344);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormUsers";
			this.Text = "Пользователи";
			this.Activated += new System.EventHandler(this.FormUsersActivated);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormUsersFormClosed);
			this.Load += new System.EventHandler(this.FormUsersLoad);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
