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
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		
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
			this.button3 = new System.Windows.Forms.Button();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.button4 = new System.Windows.Forms.Button();
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
			this.panel1.Controls.Add(this.button3);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(620, 49);
			this.panel1.TabIndex = 0;
			// 
			// button3
			// 
			this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button3.ImageKey = "group_delete.png";
			this.button3.ImageList = this.imageList1;
			this.button3.Location = new System.Drawing.Point(196, 12);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(86, 23);
			this.button3.TabIndex = 2;
			this.button3.Text = "Удалить";
			this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.button3.UseVisualStyleBackColor = true;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "report_user.png");
			this.imageList1.Images.SetKeyName(1, "folder_user.png");
			this.imageList1.Images.SetKeyName(2, "group.png");
			this.imageList1.Images.SetKeyName(3, "group_add.png");
			this.imageList1.Images.SetKeyName(4, "group_delete.png");
			this.imageList1.Images.SetKeyName(5, "group_edit.png");
			this.imageList1.Images.SetKeyName(6, "group_error.png");
			this.imageList1.Images.SetKeyName(7, "group_gear.png");
			this.imageList1.Images.SetKeyName(8, "group_go.png");
			// 
			// button2
			// 
			this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button2.ImageKey = "group_edit.png";
			this.button2.ImageList = this.imageList1;
			this.button2.Location = new System.Drawing.Point(104, 12);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(86, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "Изменить";
			this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.ImageKey = "group_add.png";
			this.button1.ImageList = this.imageList1;
			this.button1.Location = new System.Drawing.Point(12, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(86, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Добавить";
			this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.button1.UseVisualStyleBackColor = true;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.button4);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 293);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(620, 51);
			this.panel2.TabIndex = 1;
			// 
			// button4
			// 
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button4.Location = new System.Drawing.Point(533, 16);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 0;
			this.button4.Text = "Закрыть";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.listView1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 49);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(620, 244);
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
			this.listView1.Size = new System.Drawing.Size(620, 244);
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
			this.Load += new System.EventHandler(this.FormUsersLoad);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
