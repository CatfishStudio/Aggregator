/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 04.03.2017
 * Time: 18:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Aggregator.Client.Directories
{
	partial class FormCounteragents
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.Button editButton;
		private System.Windows.Forms.Button priceButton;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button refreshButton;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button addFolderButton;
		private System.Windows.Forms.Button editFolderButton;
		private System.Windows.Forms.Button deleteFolderButton;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.Button findButton;
		private System.Windows.Forms.ImageList imageList1;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCounteragents));
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonClose = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.priceButton = new System.Windows.Forms.Button();
			this.panel4 = new System.Windows.Forms.Panel();
			this.deleteButton = new System.Windows.Forms.Button();
			this.editButton = new System.Windows.Forms.Button();
			this.addButton = new System.Windows.Forms.Button();
			this.panel3 = new System.Windows.Forms.Panel();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.refreshButton = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.addFolderButton = new System.Windows.Forms.Button();
			this.editFolderButton = new System.Windows.Forms.Button();
			this.deleteFolderButton = new System.Windows.Forms.Button();
			this.panel6 = new System.Windows.Forms.Panel();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.findButton = new System.Windows.Forms.Button();
			this.panel7 = new System.Windows.Forms.Panel();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.buttonClose);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 345);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(713, 45);
			this.panel1.TabIndex = 0;
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.Location = new System.Drawing.Point(626, 10);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 0;
			this.buttonClose.Text = "Закрыть";
			this.buttonClose.UseVisualStyleBackColor = true;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.panel7);
			this.panel2.Controls.Add(this.findButton);
			this.panel2.Controls.Add(this.comboBox1);
			this.panel2.Controls.Add(this.panel6);
			this.panel2.Controls.Add(this.deleteFolderButton);
			this.panel2.Controls.Add(this.editFolderButton);
			this.panel2.Controls.Add(this.addFolderButton);
			this.panel2.Controls.Add(this.refreshButton);
			this.panel2.Controls.Add(this.panel5);
			this.panel2.Controls.Add(this.priceButton);
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Controls.Add(this.deleteButton);
			this.panel2.Controls.Add(this.editButton);
			this.panel2.Controls.Add(this.addButton);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(713, 45);
			this.panel2.TabIndex = 1;
			// 
			// panel5
			// 
			this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel5.Location = new System.Drawing.Point(299, 12);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(4, 23);
			this.panel5.TabIndex = 6;
			// 
			// priceButton
			// 
			this.priceButton.Image = ((System.Drawing.Image)(resources.GetObject("priceButton.Image")));
			this.priceButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.priceButton.Location = new System.Drawing.Point(218, 12);
			this.priceButton.Name = "priceButton";
			this.priceButton.Size = new System.Drawing.Size(75, 23);
			this.priceButton.TabIndex = 5;
			this.priceButton.Text = "Прайс";
			this.priceButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip1.SetToolTip(this.priceButton, "Просмотреть прайс");
			this.priceButton.UseVisualStyleBackColor = true;
			// 
			// panel4
			// 
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel4.Location = new System.Drawing.Point(105, 12);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(4, 23);
			this.panel4.TabIndex = 4;
			// 
			// deleteButton
			// 
			this.deleteButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteButton.Image")));
			this.deleteButton.Location = new System.Drawing.Point(74, 12);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(25, 23);
			this.deleteButton.TabIndex = 2;
			this.toolTip1.SetToolTip(this.deleteButton, "Удалить");
			this.deleteButton.UseVisualStyleBackColor = true;
			// 
			// editButton
			// 
			this.editButton.Image = ((System.Drawing.Image)(resources.GetObject("editButton.Image")));
			this.editButton.Location = new System.Drawing.Point(43, 12);
			this.editButton.Name = "editButton";
			this.editButton.Size = new System.Drawing.Size(25, 23);
			this.editButton.TabIndex = 1;
			this.toolTip1.SetToolTip(this.editButton, "Изменить");
			this.editButton.UseVisualStyleBackColor = true;
			// 
			// addButton
			// 
			this.addButton.Image = ((System.Drawing.Image)(resources.GetObject("addButton.Image")));
			this.addButton.Location = new System.Drawing.Point(12, 12);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(25, 23);
			this.addButton.TabIndex = 0;
			this.toolTip1.SetToolTip(this.addButton, "Добавить");
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.AddButtonClick);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.listView1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 45);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(713, 300);
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
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(713, 300);
			this.listView1.TabIndex = 1;
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
			this.columnHeader2.Text = "Наименование";
			this.columnHeader2.Width = 300;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Телефон";
			this.columnHeader3.Width = 150;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "№";
			// 
			// refreshButton
			// 
			this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
			this.refreshButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.refreshButton.Location = new System.Drawing.Point(532, 12);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(85, 23);
			this.refreshButton.TabIndex = 7;
			this.refreshButton.Text = "Обновить";
			this.refreshButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip1.SetToolTip(this.refreshButton, "Обновить данные");
			this.refreshButton.UseVisualStyleBackColor = true;
			// 
			// addFolderButton
			// 
			this.addFolderButton.Image = ((System.Drawing.Image)(resources.GetObject("addFolderButton.Image")));
			this.addFolderButton.Location = new System.Drawing.Point(115, 12);
			this.addFolderButton.Name = "addFolderButton";
			this.addFolderButton.Size = new System.Drawing.Size(25, 23);
			this.addFolderButton.TabIndex = 8;
			this.toolTip1.SetToolTip(this.addFolderButton, "Создать папку");
			this.addFolderButton.UseVisualStyleBackColor = true;
			// 
			// editFolderButton
			// 
			this.editFolderButton.Image = ((System.Drawing.Image)(resources.GetObject("editFolderButton.Image")));
			this.editFolderButton.Location = new System.Drawing.Point(146, 12);
			this.editFolderButton.Name = "editFolderButton";
			this.editFolderButton.Size = new System.Drawing.Size(25, 23);
			this.editFolderButton.TabIndex = 9;
			this.toolTip1.SetToolTip(this.editFolderButton, "Редактировать папку");
			this.editFolderButton.UseVisualStyleBackColor = true;
			// 
			// deleteFolderButton
			// 
			this.deleteFolderButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteFolderButton.Image")));
			this.deleteFolderButton.Location = new System.Drawing.Point(177, 12);
			this.deleteFolderButton.Name = "deleteFolderButton";
			this.deleteFolderButton.Size = new System.Drawing.Size(25, 23);
			this.deleteFolderButton.TabIndex = 10;
			this.toolTip1.SetToolTip(this.deleteFolderButton, "Удалить папку");
			this.deleteFolderButton.UseVisualStyleBackColor = true;
			// 
			// panel6
			// 
			this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel6.Location = new System.Drawing.Point(208, 12);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(4, 23);
			this.panel6.TabIndex = 7;
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(309, 12);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(176, 21);
			this.comboBox1.TabIndex = 11;
			// 
			// findButton
			// 
			this.findButton.Image = ((System.Drawing.Image)(resources.GetObject("findButton.Image")));
			this.findButton.Location = new System.Drawing.Point(491, 12);
			this.findButton.Name = "findButton";
			this.findButton.Size = new System.Drawing.Size(25, 23);
			this.findButton.TabIndex = 12;
			this.toolTip1.SetToolTip(this.findButton, "Поиск");
			this.findButton.UseVisualStyleBackColor = true;
			// 
			// panel7
			// 
			this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel7.Location = new System.Drawing.Point(522, 12);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(4, 23);
			this.panel7.TabIndex = 13;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "application.png");
			this.imageList1.Images.SetKeyName(1, "folder.png");
			// 
			// FormCounteragents
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(713, 390);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "FormCounteragents";
			this.Text = "Контрагенты";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormCounteragentsFormClosed);
			this.Load += new System.EventHandler(this.FormCounteragentsLoad);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
