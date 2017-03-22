/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 19.03.2017
 * Время: 10:27
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
namespace Aggregator.Client.Documents.PurchasePlan
{
	partial class FormPurchasePlanDoc
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label autorLabel;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Button priceButton;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.TextBox docNumberTextBox;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPurchasePlanDoc));
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.autorLabel = new System.Windows.Forms.Label();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.docNumberTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panel3 = new System.Windows.Forms.Panel();
			this.priceButton = new System.Windows.Forms.Button();
			this.panel4 = new System.Windows.Forms.Panel();
			this.deleteButton = new System.Windows.Forms.Button();
			this.addButton = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.buttonSave);
			this.panel1.Controls.Add(this.buttonCancel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 403);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(717, 45);
			this.panel1.TabIndex = 3;
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
			this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonSave.Location = new System.Drawing.Point(533, 10);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(83, 23);
			this.buttonSave.TabIndex = 1;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.ButtonSaveClick);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
			this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCancel.Location = new System.Drawing.Point(622, 10);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(83, 23);
			this.buttonCancel.TabIndex = 0;
			this.buttonCancel.Text = "Отменить";
			this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.autorLabel);
			this.panel2.Controls.Add(this.dateTimePicker1);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.docNumberTextBox);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(717, 40);
			this.panel2.TabIndex = 4;
			// 
			// autorLabel
			// 
			this.autorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.autorLabel.Location = new System.Drawing.Point(385, 9);
			this.autorLabel.Name = "autorLabel";
			this.autorLabel.Size = new System.Drawing.Size(320, 23);
			this.autorLabel.TabIndex = 18;
			this.autorLabel.Text = "Автор:";
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.CustomFormat = "dd.MM.yyyy";
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker1.Location = new System.Drawing.Point(270, 6);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(96, 20);
			this.dateTimePicker1.TabIndex = 17;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(230, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Дата:";
			// 
			// docNumberTextBox
			// 
			this.docNumberTextBox.Location = new System.Drawing.Point(53, 6);
			this.docNumberTextBox.Name = "docNumberTextBox";
			this.docNumberTextBox.ReadOnly = true;
			this.docNumberTextBox.Size = new System.Drawing.Size(171, 20);
			this.docNumberTextBox.TabIndex = 1;
			this.docNumberTextBox.Text = "ПЗ-";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(4, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Номер:";
			// 
			// tabControl1
			// 
			this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 40);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(717, 363);
			this.tabControl1.TabIndex = 5;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.listView1);
			this.tabPage1.Controls.Add(this.panel3);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(709, 334);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Прайс-листы";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// listView1
			// 
			this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader1,
			this.columnHeader2,
			this.columnHeader3,
			this.columnHeader4});
			this.listView1.Cursor = System.Windows.Forms.Cursors.Default;
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.LargeImageList = this.imageList1;
			this.listView1.Location = new System.Drawing.Point(3, 32);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(703, 299);
			this.listView1.SmallImageList = this.imageList1;
			this.listView1.StateImageList = this.imageList1;
			this.listView1.TabIndex = 7;
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
			this.columnHeader2.Text = "Контрагент";
			this.columnHeader2.Width = 400;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Прайс-лист";
			this.columnHeader3.Width = 200;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "№";
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "folder.png");
			this.imageList1.Images.SetKeyName(1, "page.png");
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.priceButton);
			this.panel3.Controls.Add(this.panel4);
			this.panel3.Controls.Add(this.deleteButton);
			this.panel3.Controls.Add(this.addButton);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(3, 3);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(703, 29);
			this.panel3.TabIndex = 0;
			// 
			// priceButton
			// 
			this.priceButton.Image = ((System.Drawing.Image)(resources.GetObject("priceButton.Image")));
			this.priceButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.priceButton.Location = new System.Drawing.Point(77, 3);
			this.priceButton.Name = "priceButton";
			this.priceButton.Size = new System.Drawing.Size(90, 23);
			this.priceButton.TabIndex = 6;
			this.priceButton.Text = "Прайс-лист";
			this.priceButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.priceButton.UseVisualStyleBackColor = true;
			this.priceButton.Click += new System.EventHandler(this.PriceButtonClick);
			// 
			// panel4
			// 
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel4.Location = new System.Drawing.Point(67, 3);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(4, 23);
			this.panel4.TabIndex = 5;
			// 
			// deleteButton
			// 
			this.deleteButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteButton.Image")));
			this.deleteButton.Location = new System.Drawing.Point(36, 3);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(25, 23);
			this.deleteButton.TabIndex = 4;
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.DeleteButtonClick);
			// 
			// addButton
			// 
			this.addButton.Image = ((System.Drawing.Image)(resources.GetObject("addButton.Image")));
			this.addButton.Location = new System.Drawing.Point(5, 3);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(25, 23);
			this.addButton.TabIndex = 3;
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.AddButtonClick);
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(709, 334);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Номенклатура";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// FormPurchasePlanDoc
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(717, 448);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormPurchasePlanDoc";
			this.Text = "План закупок";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormPurchasePlanDocFormClosed);
			this.Load += new System.EventHandler(this.FormPurchasePlanDocLoad);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
