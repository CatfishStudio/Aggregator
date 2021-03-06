﻿/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 25.02.2017
 * Time: 11:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Aggregator.Admin
{
	partial class FormSettingsDatabase
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.TextBox localDatabaseTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox typeConnectionСomboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox serverTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.TextBox typeDatabaseTextBox;
		private System.Windows.Forms.Button testConnectButton;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettingsDatabase));
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.typeDatabaseTextBox = new System.Windows.Forms.TextBox();
			this.typeConnectionСomboBox = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.testConnectButton = new System.Windows.Forms.Button();
			this.serverTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.localDatabaseTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.buttonSave);
			this.panel1.Controls.Add(this.buttonClose);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 266);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(418, 51);
			this.panel1.TabIndex = 0;
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
			this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonSave.Location = new System.Drawing.Point(230, 16);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(85, 23);
			this.buttonSave.TabIndex = 1;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.ButtonSaveClick);
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.Image = ((System.Drawing.Image)(resources.GetObject("buttonClose.Image")));
			this.buttonClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonClose.Location = new System.Drawing.Point(321, 16);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(85, 23);
			this.buttonClose.TabIndex = 0;
			this.buttonClose.Text = "Отменить";
			this.buttonClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.typeDatabaseTextBox);
			this.panel2.Controls.Add(this.typeConnectionСomboBox);
			this.panel2.Controls.Add(this.label6);
			this.panel2.Controls.Add(this.groupBox2);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.groupBox1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(418, 266);
			this.panel2.TabIndex = 1;
			// 
			// typeDatabaseTextBox
			// 
			this.typeDatabaseTextBox.Location = new System.Drawing.Point(12, 68);
			this.typeDatabaseTextBox.Name = "typeDatabaseTextBox";
			this.typeDatabaseTextBox.ReadOnly = true;
			this.typeDatabaseTextBox.Size = new System.Drawing.Size(233, 20);
			this.typeDatabaseTextBox.TabIndex = 8;
			// 
			// typeConnectionСomboBox
			// 
			this.typeConnectionСomboBox.FormattingEnabled = true;
			this.typeConnectionСomboBox.Location = new System.Drawing.Point(12, 24);
			this.typeConnectionСomboBox.Name = "typeConnectionСomboBox";
			this.typeConnectionСomboBox.Size = new System.Drawing.Size(233, 21);
			this.typeConnectionСomboBox.TabIndex = 3;
			this.typeConnectionСomboBox.SelectedIndexChanged += new System.EventHandler(this.TypeConnectionСomboBoxSelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(12, 48);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(233, 23);
			this.label6.TabIndex = 6;
			this.label6.Text = "Система управления базами данных:";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.testConnectButton);
			this.groupBox2.Controls.Add(this.serverTextBox);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox2.Location = new System.Drawing.Point(9, 164);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(397, 96);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "MS SQL Server";
			// 
			// testConnectButton
			// 
			this.testConnectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.testConnectButton.Location = new System.Drawing.Point(221, 65);
			this.testConnectButton.Name = "testConnectButton";
			this.testConnectButton.Size = new System.Drawing.Size(165, 23);
			this.testConnectButton.TabIndex = 2;
			this.testConnectButton.Text = "Проверить соединение";
			this.testConnectButton.UseVisualStyleBackColor = true;
			this.testConnectButton.Click += new System.EventHandler(this.TestConnectButtonClick);
			// 
			// serverTextBox
			// 
			this.serverTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.serverTextBox.Location = new System.Drawing.Point(8, 39);
			this.serverTextBox.Name = "serverTextBox";
			this.serverTextBox.Size = new System.Drawing.Size(378, 20);
			this.serverTextBox.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(8, 22);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(172, 23);
			this.label3.TabIndex = 0;
			this.label3.Text = "Сервер (строка подключения):";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(9, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(260, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Выбранный тип подключения к базе данных:";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.localDatabaseTextBox);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox1.Location = new System.Drawing.Point(9, 94);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(397, 64);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "OleDb";
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.button1.Location = new System.Drawing.Point(361, 36);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(28, 20);
			this.button1.TabIndex = 2;
			this.button1.Text = "...";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// localDatabaseTextBox
			// 
			this.localDatabaseTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.localDatabaseTextBox.Location = new System.Drawing.Point(8, 36);
			this.localDatabaseTextBox.Name = "localDatabaseTextBox";
			this.localDatabaseTextBox.ReadOnly = true;
			this.localDatabaseTextBox.Size = new System.Drawing.Size(347, 20);
			this.localDatabaseTextBox.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(6, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(207, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Путь к базе данных (OleDb):";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "database.mdb";
			this.openFileDialog1.Filter = "Файл базы данных *.mdb|*.mdb";
			// 
			// FormSettingsDatabase
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(418, 317);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormSettingsDatabase";
			this.Text = "Настройка базы данных";
			this.Activated += new System.EventHandler(this.FormSettingsDatabaseActivated);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSettingsFormClosed);
			this.Load += new System.EventHandler(this.FormSettingsLoad);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}
	}
}
