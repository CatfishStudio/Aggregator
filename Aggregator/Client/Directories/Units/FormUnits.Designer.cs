/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 18.03.2017
 * Время: 16:03
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
namespace Aggregator.Client.Directories
{
	partial class FormUnits
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.Button findButton;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Button refreshButton;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.Button editButton;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem создатьЗаписьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem изменитьЗаписьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem удалитьЗаписьToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem выбратьЗаписьToolStripMenuItem;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUnits));
			this.panel1 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel7 = new System.Windows.Forms.Panel();
			this.findButton = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.refreshButton = new System.Windows.Forms.Button();
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
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.создатьЗаписьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.изменитьЗаписьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.удалитьЗаписьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.выбратьЗаписьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.buttonClose);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 325);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(611, 45);
			this.panel1.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
			this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.Location = new System.Drawing.Point(443, 10);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Выбрать";
			this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Visible = false;
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.Location = new System.Drawing.Point(524, 10);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 0;
			this.buttonClose.Text = "Закрыть";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.panel7);
			this.panel2.Controls.Add(this.findButton);
			this.panel2.Controls.Add(this.comboBox1);
			this.panel2.Controls.Add(this.refreshButton);
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Controls.Add(this.deleteButton);
			this.panel2.Controls.Add(this.editButton);
			this.panel2.Controls.Add(this.addButton);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(611, 45);
			this.panel2.TabIndex = 3;
			// 
			// panel7
			// 
			this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel7.Location = new System.Drawing.Point(434, 12);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(4, 23);
			this.panel7.TabIndex = 13;
			// 
			// findButton
			// 
			this.findButton.Image = ((System.Drawing.Image)(resources.GetObject("findButton.Image")));
			this.findButton.Location = new System.Drawing.Point(403, 12);
			this.findButton.Name = "findButton";
			this.findButton.Size = new System.Drawing.Size(25, 23);
			this.findButton.TabIndex = 12;
			this.toolTip1.SetToolTip(this.findButton, "Поиск");
			this.findButton.UseVisualStyleBackColor = true;
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(115, 12);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(282, 21);
			this.comboBox1.TabIndex = 11;
			// 
			// refreshButton
			// 
			this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
			this.refreshButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.refreshButton.Location = new System.Drawing.Point(444, 12);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(85, 23);
			this.refreshButton.TabIndex = 7;
			this.refreshButton.Text = "Обновить";
			this.refreshButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip1.SetToolTip(this.refreshButton, "Обновить данные");
			this.refreshButton.UseVisualStyleBackColor = true;
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
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.listView1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 45);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(611, 280);
			this.panel3.TabIndex = 4;
			// 
			// listView1
			// 
			this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader1,
			this.columnHeader2,
			this.columnHeader3,
			this.columnHeader4});
			this.listView1.ContextMenuStrip = this.contextMenuStrip1;
			this.listView1.Cursor = System.Windows.Forms.Cursors.Default;
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.LargeImageList = this.imageList1;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(611, 280);
			this.listView1.SmallImageList = this.imageList1;
			this.listView1.StateImageList = this.imageList1;
			this.listView1.TabIndex = 6;
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
			this.columnHeader2.Width = 400;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "";
			this.columnHeader3.Width = 50;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "№";
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.создатьЗаписьToolStripMenuItem,
			this.изменитьЗаписьToolStripMenuItem,
			this.удалитьЗаписьToolStripMenuItem,
			this.toolStripMenuItem2,
			this.выбратьЗаписьToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(172, 98);
			// 
			// создатьЗаписьToolStripMenuItem
			// 
			this.создатьЗаписьToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("создатьЗаписьToolStripMenuItem.Image")));
			this.создатьЗаписьToolStripMenuItem.Name = "создатьЗаписьToolStripMenuItem";
			this.создатьЗаписьToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.создатьЗаписьToolStripMenuItem.Text = "Создать запись.";
			// 
			// изменитьЗаписьToolStripMenuItem
			// 
			this.изменитьЗаписьToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("изменитьЗаписьToolStripMenuItem.Image")));
			this.изменитьЗаписьToolStripMenuItem.Name = "изменитьЗаписьToolStripMenuItem";
			this.изменитьЗаписьToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.изменитьЗаписьToolStripMenuItem.Text = "Изменить запись.";
			// 
			// удалитьЗаписьToolStripMenuItem
			// 
			this.удалитьЗаписьToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("удалитьЗаписьToolStripMenuItem.Image")));
			this.удалитьЗаписьToolStripMenuItem.Name = "удалитьЗаписьToolStripMenuItem";
			this.удалитьЗаписьToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.удалитьЗаписьToolStripMenuItem.Text = "Удалить запись.";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(168, 6);
			this.toolStripMenuItem2.Visible = false;
			// 
			// выбратьЗаписьToolStripMenuItem
			// 
			this.выбратьЗаписьToolStripMenuItem.Name = "выбратьЗаписьToolStripMenuItem";
			this.выбратьЗаписьToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.выбратьЗаписьToolStripMenuItem.Text = "Выбрать запись.";
			this.выбратьЗаписьToolStripMenuItem.Visible = false;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "folder.png");
			this.imageList1.Images.SetKeyName(1, "page.png");
			// 
			// FormUnits
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(611, 370);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormUnits";
			this.Text = "Единицы измерения";
			this.Activated += new System.EventHandler(this.FormUnitsActivated);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormUnitsFormClosed);
			this.Load += new System.EventHandler(this.FormUnitsLoad);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
