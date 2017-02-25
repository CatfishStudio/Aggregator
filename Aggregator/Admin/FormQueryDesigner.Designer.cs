/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 25.02.2017
 * Time: 16:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Aggregator.Admin
{
	partial class FormQueryDesigner
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label2;
		
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBox2);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.comboBox1);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(466, 78);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Основные настройки:";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(91, 46);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(362, 20);
			this.textBox2.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(10, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Имя таблицы:";
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(91, 19);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(362, 21);
			this.comboBox1.TabIndex = 1;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Источник:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkBox4);
			this.groupBox2.Controls.Add(this.checkBox3);
			this.groupBox2.Controls.Add(this.checkBox2);
			this.groupBox2.Controls.Add(this.checkBox1);
			this.groupBox2.Controls.Add(this.button2);
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Controls.Add(this.textBox1);
			this.groupBox2.Location = new System.Drawing.Point(12, 96);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(466, 160);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Запрос:";
			// 
			// checkBox4
			// 
			this.checkBox4.Location = new System.Drawing.Point(188, 127);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(65, 24);
			this.checkBox4.TabIndex = 13;
			this.checkBox4.Text = "Delete";
			this.checkBox4.UseVisualStyleBackColor = true;
			this.checkBox4.CheckedChanged += new System.EventHandler(this.CheckBox4CheckedChanged);
			// 
			// checkBox3
			// 
			this.checkBox3.Location = new System.Drawing.Point(126, 127);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(65, 24);
			this.checkBox3.TabIndex = 12;
			this.checkBox3.Text = "Update";
			this.checkBox3.UseVisualStyleBackColor = true;
			this.checkBox3.CheckedChanged += new System.EventHandler(this.CheckBox3CheckedChanged);
			// 
			// checkBox2
			// 
			this.checkBox2.Location = new System.Drawing.Point(70, 127);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(65, 24);
			this.checkBox2.TabIndex = 11;
			this.checkBox2.Text = "Insert";
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox2.CheckedChanged += new System.EventHandler(this.CheckBox2CheckedChanged);
			// 
			// checkBox1
			// 
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(10, 127);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(65, 24);
			this.checkBox1.TabIndex = 10;
			this.checkBox1.Text = "Select";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1CheckedChanged);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(297, 127);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 9;
			this.button2.Text = "Выполнить";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(378, 127);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 8;
			this.button1.Text = "Очистить.";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(10, 19);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(443, 102);
			this.textBox1.TabIndex = 7;
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.dataGrid1);
			this.groupBox3.Location = new System.Drawing.Point(12, 262);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(466, 157);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Результат:";
			// 
			// dataGrid1
			// 
			this.dataGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGrid1.BackgroundColor = System.Drawing.Color.White;
			this.dataGrid1.CaptionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.dataGrid1.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.dataGrid1.CaptionText = "Результат запроса:";
			this.dataGrid1.DataMember = "";
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(6, 16);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.ReadOnly = true;
			this.dataGrid1.Size = new System.Drawing.Size(454, 135);
			this.dataGrid1.TabIndex = 1;
			// 
			// FormQueryDesigner
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 431);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "FormQueryDesigner";
			this.Text = "Консоль запросов";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormQueryDesignerFormClosed);
			this.Load += new System.EventHandler(this.FormQueryDesignerLoad);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
