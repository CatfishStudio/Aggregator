/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 23.02.2017
 * Time: 11:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Aggregator.Client
{
	partial class FormClient
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem создатьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem текстовыйФайлToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem видToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem панельИнструментовToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem консольСообщенийToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem документыToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem журналыToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem сервисToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem администраторToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClient));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.текстовыйФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.видToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.панельИнструментовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.консольСообщенийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.документыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.журналыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.сервисToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.администраторToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.menuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripMenuItem1,
			this.видToolStripMenuItem,
			this.справочникиToolStripMenuItem,
			this.документыToolStripMenuItem,
			this.журналыToolStripMenuItem,
			this.отчетыToolStripMenuItem,
			this.сервисToolStripMenuItem,
			this.администраторToolStripMenuItem,
			this.справкаToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(784, 40);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.создатьToolStripMenuItem,
			this.открытьToolStripMenuItem,
			this.toolStripSeparator1,
			this.выходToolStripMenuItem});
			this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(80, 36);
			this.toolStripMenuItem1.Text = "&Файл";
			// 
			// создатьToolStripMenuItem
			// 
			this.создатьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.текстовыйФайлToolStripMenuItem});
			this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
			this.создатьToolStripMenuItem.Size = new System.Drawing.Size(168, 38);
			this.создатьToolStripMenuItem.Text = "Создать";
			// 
			// текстовыйФайлToolStripMenuItem
			// 
			this.текстовыйФайлToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("текстовыйФайлToolStripMenuItem.Image")));
			this.текстовыйФайлToolStripMenuItem.Name = "текстовыйФайлToolStripMenuItem";
			this.текстовыйФайлToolStripMenuItem.Size = new System.Drawing.Size(181, 38);
			this.текстовыйФайлToolStripMenuItem.Text = "Текстовый файл";
			// 
			// открытьToolStripMenuItem
			// 
			this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
			this.открытьToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			this.открытьToolStripMenuItem.Text = "Открыть";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(118, 6);
			// 
			// выходToolStripMenuItem
			// 
			this.выходToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("выходToolStripMenuItem.Image")));
			this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
			this.выходToolStripMenuItem.Size = new System.Drawing.Size(168, 38);
			this.выходToolStripMenuItem.Text = "Выход";
			// 
			// видToolStripMenuItem
			// 
			this.видToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.панельИнструментовToolStripMenuItem,
			this.консольСообщенийToolStripMenuItem});
			this.видToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("видToolStripMenuItem.Image")));
			this.видToolStripMenuItem.Name = "видToolStripMenuItem";
			this.видToolStripMenuItem.Size = new System.Drawing.Size(71, 36);
			this.видToolStripMenuItem.Text = "&Вид";
			// 
			// панельИнструментовToolStripMenuItem
			// 
			this.панельИнструментовToolStripMenuItem.Checked = true;
			this.панельИнструментовToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.панельИнструментовToolStripMenuItem.Name = "панельИнструментовToolStripMenuItem";
			this.панельИнструментовToolStripMenuItem.Size = new System.Drawing.Size(212, 38);
			this.панельИнструментовToolStripMenuItem.Text = "Панель инструментов";
			this.панельИнструментовToolStripMenuItem.Click += new System.EventHandler(this.ПанельИнструментовToolStripMenuItemClick);
			// 
			// консольСообщенийToolStripMenuItem
			// 
			this.консольСообщенийToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("консольСообщенийToolStripMenuItem.Image")));
			this.консольСообщенийToolStripMenuItem.Name = "консольСообщенийToolStripMenuItem";
			this.консольСообщенийToolStripMenuItem.Size = new System.Drawing.Size(212, 38);
			this.консольСообщенийToolStripMenuItem.Text = "Консоль сообщений";
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripButton1,
			this.toolStripSeparator2});
			this.toolStrip1.Location = new System.Drawing.Point(0, 40);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(784, 39);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 540);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(784, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// справочникиToolStripMenuItem
			// 
			this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
			this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 36);
			this.справочникиToolStripMenuItem.Text = "Справочники";
			// 
			// документыToolStripMenuItem
			// 
			this.документыToolStripMenuItem.Name = "документыToolStripMenuItem";
			this.документыToolStripMenuItem.Size = new System.Drawing.Size(82, 36);
			this.документыToolStripMenuItem.Text = "Документы";
			// 
			// журналыToolStripMenuItem
			// 
			this.журналыToolStripMenuItem.Name = "журналыToolStripMenuItem";
			this.журналыToolStripMenuItem.Size = new System.Drawing.Size(72, 36);
			this.журналыToolStripMenuItem.Text = "Журналы";
			// 
			// отчетыToolStripMenuItem
			// 
			this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
			this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(60, 36);
			this.отчетыToolStripMenuItem.Text = "Отчеты";
			// 
			// сервисToolStripMenuItem
			// 
			this.сервисToolStripMenuItem.Name = "сервисToolStripMenuItem";
			this.сервисToolStripMenuItem.Size = new System.Drawing.Size(59, 36);
			this.сервисToolStripMenuItem.Text = "Сервис";
			// 
			// администраторToolStripMenuItem
			// 
			this.администраторToolStripMenuItem.Name = "администраторToolStripMenuItem";
			this.администраторToolStripMenuItem.Size = new System.Drawing.Size(106, 36);
			this.администраторToolStripMenuItem.Text = "Администратор";
			// 
			// справкаToolStripMenuItem
			// 
			this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
			this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 36);
			this.справкаToolStripMenuItem.Text = "Справка";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(36, 36);
			this.toolStripButton1.Text = "toolStripButton1";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
			// 
			// FormClient
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.menuStrip1);
			this.IsMdiContainer = true;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "FormClient";
			this.Text = "Агрегатор.";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClientFormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormClientFormClosed);
			this.Load += new System.EventHandler(this.FormClientLoad);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
