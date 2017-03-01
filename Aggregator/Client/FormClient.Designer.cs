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
		private System.Windows.Forms.ToolStripMenuItem константыToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem контрагентыToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem номенклатураToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem прайсToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem журналПрайсовToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem калькуляторToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem календарьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem пользователиToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem базаДанныхToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
		public System.Windows.Forms.Panel consolePanel;
		private System.Windows.Forms.ToolTip toolTip1;
		public System.Windows.Forms.TextBox consoleText;
		private System.Windows.Forms.ToolStripMenuItem консольЗапросовToolStripMenuItem;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
		
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
			this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.константыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.контрагентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.номенклатураToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.документыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.прайсToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.журналыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.журналПрайсовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.сервисToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.калькуляторToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.календарьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.администраторToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.пользователиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.базаДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.консольЗапросовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.consolePanel = new System.Windows.Forms.Panel();
			this.consoleText = new System.Windows.Forms.TextBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.menuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.consolePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripMenuItem1,
			this.видToolStripMenuItem,
			this.справочникиToolStripMenuItem,
			this.документыToolStripMenuItem,
			this.журналыToolStripMenuItem,
			this.отчетыToolStripMenuItem,
			this.сервисToolStripMenuItem,
			this.администраторToolStripMenuItem,
			this.настройкиToolStripMenuItem,
			this.справкаToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(784, 24);
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
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(48, 20);
			this.toolStripMenuItem1.Text = "&Файл";
			// 
			// создатьToolStripMenuItem
			// 
			this.создатьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.текстовыйФайлToolStripMenuItem});
			this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
			this.создатьToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			this.создатьToolStripMenuItem.Text = "Создать";
			// 
			// текстовыйФайлToolStripMenuItem
			// 
			this.текстовыйФайлToolStripMenuItem.Name = "текстовыйФайлToolStripMenuItem";
			this.текстовыйФайлToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
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
			this.выходToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			this.выходToolStripMenuItem.Text = "Выход";
			// 
			// видToolStripMenuItem
			// 
			this.видToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.панельИнструментовToolStripMenuItem,
			this.консольСообщенийToolStripMenuItem});
			this.видToolStripMenuItem.Name = "видToolStripMenuItem";
			this.видToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.видToolStripMenuItem.Text = "&Вид";
			// 
			// панельИнструментовToolStripMenuItem
			// 
			this.панельИнструментовToolStripMenuItem.Checked = true;
			this.панельИнструментовToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.панельИнструментовToolStripMenuItem.Name = "панельИнструментовToolStripMenuItem";
			this.панельИнструментовToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.панельИнструментовToolStripMenuItem.Text = "Панель инструментов";
			// 
			// консольСообщенийToolStripMenuItem
			// 
			this.консольСообщенийToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("консольСообщенийToolStripMenuItem.Image")));
			this.консольСообщенийToolStripMenuItem.Name = "консольСообщенийToolStripMenuItem";
			this.консольСообщенийToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.консольСообщенийToolStripMenuItem.Text = "Консоль сообщений";
			this.консольСообщенийToolStripMenuItem.Click += new System.EventHandler(this.КонсольСообщенийToolStripMenuItemClick);
			// 
			// справочникиToolStripMenuItem
			// 
			this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.константыToolStripMenuItem,
			this.toolStripSeparator3,
			this.контрагентыToolStripMenuItem,
			this.номенклатураToolStripMenuItem});
			this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
			this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
			this.справочникиToolStripMenuItem.Text = "Справочники";
			// 
			// константыToolStripMenuItem
			// 
			this.константыToolStripMenuItem.Name = "константыToolStripMenuItem";
			this.константыToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.константыToolStripMenuItem.Text = "Константы";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(152, 6);
			// 
			// контрагентыToolStripMenuItem
			// 
			this.контрагентыToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("контрагентыToolStripMenuItem.Image")));
			this.контрагентыToolStripMenuItem.Name = "контрагентыToolStripMenuItem";
			this.контрагентыToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.контрагентыToolStripMenuItem.Text = "Контрагенты";
			// 
			// номенклатураToolStripMenuItem
			// 
			this.номенклатураToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("номенклатураToolStripMenuItem.Image")));
			this.номенклатураToolStripMenuItem.Name = "номенклатураToolStripMenuItem";
			this.номенклатураToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.номенклатураToolStripMenuItem.Text = "Номенклатура";
			// 
			// документыToolStripMenuItem
			// 
			this.документыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.прайсToolStripMenuItem});
			this.документыToolStripMenuItem.Name = "документыToolStripMenuItem";
			this.документыToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
			this.документыToolStripMenuItem.Text = "Документы";
			// 
			// прайсToolStripMenuItem
			// 
			this.прайсToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("прайсToolStripMenuItem.Image")));
			this.прайсToolStripMenuItem.Name = "прайсToolStripMenuItem";
			this.прайсToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
			this.прайсToolStripMenuItem.Text = "Прайс";
			// 
			// журналыToolStripMenuItem
			// 
			this.журналыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.журналПрайсовToolStripMenuItem});
			this.журналыToolStripMenuItem.Name = "журналыToolStripMenuItem";
			this.журналыToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
			this.журналыToolStripMenuItem.Text = "Журналы";
			// 
			// журналПрайсовToolStripMenuItem
			// 
			this.журналПрайсовToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("журналПрайсовToolStripMenuItem.Image")));
			this.журналПрайсовToolStripMenuItem.Name = "журналПрайсовToolStripMenuItem";
			this.журналПрайсовToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.журналПрайсовToolStripMenuItem.Text = "Журнал прайсов";
			// 
			// отчетыToolStripMenuItem
			// 
			this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
			this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
			this.отчетыToolStripMenuItem.Text = "Отчеты";
			// 
			// сервисToolStripMenuItem
			// 
			this.сервисToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.калькуляторToolStripMenuItem,
			this.календарьToolStripMenuItem});
			this.сервисToolStripMenuItem.Name = "сервисToolStripMenuItem";
			this.сервисToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.сервисToolStripMenuItem.Text = "Сервис";
			// 
			// калькуляторToolStripMenuItem
			// 
			this.калькуляторToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("калькуляторToolStripMenuItem.Image")));
			this.калькуляторToolStripMenuItem.Name = "калькуляторToolStripMenuItem";
			this.калькуляторToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.калькуляторToolStripMenuItem.Text = "Калькулятор";
			// 
			// календарьToolStripMenuItem
			// 
			this.календарьToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("календарьToolStripMenuItem.Image")));
			this.календарьToolStripMenuItem.Name = "календарьToolStripMenuItem";
			this.календарьToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.календарьToolStripMenuItem.Text = "Календарь";
			// 
			// администраторToolStripMenuItem
			// 
			this.администраторToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.пользователиToolStripMenuItem,
			this.базаДанныхToolStripMenuItem,
			this.консольЗапросовToolStripMenuItem});
			this.администраторToolStripMenuItem.Name = "администраторToolStripMenuItem";
			this.администраторToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
			this.администраторToolStripMenuItem.Text = "Администратор";
			// 
			// пользователиToolStripMenuItem
			// 
			this.пользователиToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("пользователиToolStripMenuItem.Image")));
			this.пользователиToolStripMenuItem.Name = "пользователиToolStripMenuItem";
			this.пользователиToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
			this.пользователиToolStripMenuItem.Text = "Пользователи";
			this.пользователиToolStripMenuItem.Click += new System.EventHandler(this.ПользователиToolStripMenuItemClick);
			// 
			// базаДанныхToolStripMenuItem
			// 
			this.базаДанныхToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("базаДанныхToolStripMenuItem.Image")));
			this.базаДанныхToolStripMenuItem.Name = "базаДанныхToolStripMenuItem";
			this.базаДанныхToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
			this.базаДанныхToolStripMenuItem.Text = "База данных";
			this.базаДанныхToolStripMenuItem.Click += new System.EventHandler(this.БазаДанныхToolStripMenuItemClick);
			// 
			// консольЗапросовToolStripMenuItem
			// 
			this.консольЗапросовToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("консольЗапросовToolStripMenuItem.Image")));
			this.консольЗапросовToolStripMenuItem.Name = "консольЗапросовToolStripMenuItem";
			this.консольЗапросовToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
			this.консольЗапросовToolStripMenuItem.Text = "Консоль запросов";
			this.консольЗапросовToolStripMenuItem.Click += new System.EventHandler(this.КонсольЗапросовToolStripMenuItemClick);
			// 
			// настройкиToolStripMenuItem
			// 
			this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
			this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
			this.настройкиToolStripMenuItem.Text = "Настройки";
			// 
			// справкаToolStripMenuItem
			// 
			this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.помощьToolStripMenuItem,
			this.оПрограммеToolStripMenuItem});
			this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
			this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
			this.справкаToolStripMenuItem.Text = "Справка";
			// 
			// помощьToolStripMenuItem
			// 
			this.помощьToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("помощьToolStripMenuItem.Image")));
			this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
			this.помощьToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.помощьToolStripMenuItem.Text = "Помощь";
			// 
			// оПрограммеToolStripMenuItem
			// 
			this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
			this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.оПрограммеToolStripMenuItem.Text = "О программе";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripButton1,
			this.toolStripSeparator2});
			this.toolStrip1.Location = new System.Drawing.Point(0, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(784, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton1.Text = "toolStripButton1";
			this.toolStripButton1.ToolTipText = "Консоль";
			this.toolStripButton1.Click += new System.EventHandler(this.ToolStripButton1Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripStatusLabel1,
			this.toolStripStatusLabel2});
			this.statusStrip1.Location = new System.Drawing.Point(0, 540);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(784, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripStatusLabel1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabel1.Image")));
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(16, 17);
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(16, 17);
			this.toolStripStatusLabel2.Text = "...";
			// 
			// consolePanel
			// 
			this.consolePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.consolePanel.Controls.Add(this.consoleText);
			this.consolePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.consolePanel.Location = new System.Drawing.Point(0, 406);
			this.consolePanel.Name = "consolePanel";
			this.consolePanel.Size = new System.Drawing.Size(784, 134);
			this.consolePanel.TabIndex = 4;
			this.consolePanel.Visible = false;
			// 
			// consoleText
			// 
			this.consoleText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.consoleText.Location = new System.Drawing.Point(0, 0);
			this.consoleText.Multiline = true;
			this.consoleText.Name = "consoleText";
			this.consoleText.Size = new System.Drawing.Size(780, 130);
			this.consoleText.TabIndex = 0;
			// 
			// timer1
			// 
			this.timer1.Interval = 2000;
			this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "database.png");
			this.imageList1.Images.SetKeyName(1, "database_go.png");
			this.imageList1.Images.SetKeyName(2, "database_delete.png");
			// 
			// FormClient
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.consolePanel);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "FormClient";
			this.Text = "Агрегатор v 1.0";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClientFormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormClientFormClosed);
			this.Load += new System.EventHandler(this.FormClientLoad);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.consolePanel.ResumeLayout(false);
			this.consolePanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
