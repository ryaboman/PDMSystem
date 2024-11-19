namespace PDMS
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Search = new System.Windows.Forms.Button();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchMarkName = new System.Windows.Forms.ToolStripMenuItem();
            this.PDF_Viewer = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenKompas = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseItem = new System.Windows.Forms.ToolStripMenuItem();
            this.предварительныйСоставИзделияToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.сформироватьСоставToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отобразитьСоставToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.архивToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поместитьИзделияВАрхивToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поискПоОбозначениюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обновитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.About = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutApp = new System.Windows.Forms.ToolStripMenuItem();
            this.руководствоПользователяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.axAcroPDF2 = new AxAcroPDFLib.AxAcroPDF();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddDeviceContextNemu = new System.Windows.Forms.ToolStripMenuItem();
            this.Correction = new System.Windows.Forms.ToolStripMenuItem();
            this.коррекцияСИзвещениемToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OtherCorrection = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF2)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView2
            // 
            this.treeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.treeView2.Location = new System.Drawing.Point(0, 0);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(335, 526);
            this.treeView2.TabIndex = 0;
            this.treeView2.DoubleClick += new System.EventHandler(this.treeView2_DoubleClick);
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.textBox2.Location = new System.Drawing.Point(3, 18);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(248, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // Search
            // 
            this.Search.Location = new System.Drawing.Point(257, 18);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(75, 22);
            this.Search.TabIndex = 2;
            this.Search.Text = "Поиск";
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.предварительныйСоставИзделияToolStripMenuItem1,
            this.архивToolStripMenuItem,
            this.About});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1135, 24);
            this.menuStrip2.TabIndex = 4;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SearchMarkName,
            this.PDF_Viewer,
            this.OpenKompas,
            this.CloseItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(48, 20);
            this.toolStripMenuItem1.Text = "Файл";
            // 
            // SearchMarkName
            // 
            this.SearchMarkName.CheckOnClick = true;
            this.SearchMarkName.Name = "SearchMarkName";
            this.SearchMarkName.Size = new System.Drawing.Size(278, 22);
            this.SearchMarkName.Text = "Поиск по обозначению";
            // 
            // PDF_Viewer
            // 
            this.PDF_Viewer.CheckOnClick = true;
            this.PDF_Viewer.Name = "PDF_Viewer";
            this.PDF_Viewer.Size = new System.Drawing.Size(278, 22);
            this.PDF_Viewer.Text = "Внешняя программа просмотра PDF";
            // 
            // OpenKompas
            // 
            this.OpenKompas.CheckOnClick = true;
            this.OpenKompas.Name = "OpenKompas";
            this.OpenKompas.Size = new System.Drawing.Size(278, 22);
            this.OpenKompas.Text = "Открывать в Компасе";
            // 
            // CloseItem
            // 
            this.CloseItem.Name = "CloseItem";
            this.CloseItem.Size = new System.Drawing.Size(278, 22);
            this.CloseItem.Text = "Закрыть программу";
            this.CloseItem.Click += new System.EventHandler(this.CloseItem_Click);
            // 
            // предварительныйСоставИзделияToolStripMenuItem1
            // 
            this.предварительныйСоставИзделияToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сформироватьСоставToolStripMenuItem,
            this.отобразитьСоставToolStripMenuItem});
            this.предварительныйСоставИзделияToolStripMenuItem1.Name = "предварительныйСоставИзделияToolStripMenuItem1";
            this.предварительныйСоставИзделияToolStripMenuItem1.Size = new System.Drawing.Size(81, 20);
            this.предварительныйСоставИзделияToolStripMenuItem1.Text = "Разработка";
            // 
            // сформироватьСоставToolStripMenuItem
            // 
            this.сформироватьСоставToolStripMenuItem.Name = "сформироватьСоставToolStripMenuItem";
            this.сформироватьСоставToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.сформироватьСоставToolStripMenuItem.Text = "Сформировать состав";
            this.сформироватьСоставToolStripMenuItem.Click += new System.EventHandler(this.AdderItem_Click);
            // 
            // отобразитьСоставToolStripMenuItem
            // 
            this.отобразитьСоставToolStripMenuItem.Name = "отобразитьСоставToolStripMenuItem";
            this.отобразитьСоставToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.отобразитьСоставToolStripMenuItem.Text = "Отобразить состав";
            // 
            // архивToolStripMenuItem
            // 
            this.архивToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.поместитьИзделияВАрхивToolStripMenuItem,
            this.поискПоОбозначениюToolStripMenuItem,
            this.обновитьToolStripMenuItem});
            this.архивToolStripMenuItem.Name = "архивToolStripMenuItem";
            this.архивToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.архивToolStripMenuItem.Text = "Архив";
            // 
            // поместитьИзделияВАрхивToolStripMenuItem
            // 
            this.поместитьИзделияВАрхивToolStripMenuItem.Name = "поместитьИзделияВАрхивToolStripMenuItem";
            this.поместитьИзделияВАрхивToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.поместитьИзделияВАрхивToolStripMenuItem.Text = "Поместить изделия в архив";
            this.поместитьИзделияВАрхивToolStripMenuItem.Click += new System.EventHandler(this.MoveDeviceItem_Click);
            // 
            // поискПоОбозначениюToolStripMenuItem
            // 
            this.поискПоОбозначениюToolStripMenuItem.Name = "поискПоОбозначениюToolStripMenuItem";
            this.поискПоОбозначениюToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.поискПоОбозначениюToolStripMenuItem.Text = "Поиск по обозначению";
            // 
            // обновитьToolStripMenuItem
            // 
            this.обновитьToolStripMenuItem.Name = "обновитьToolStripMenuItem";
            this.обновитьToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.обновитьToolStripMenuItem.Text = "Обновить архив";
            this.обновитьToolStripMenuItem.Click += new System.EventHandler(this.UpDateLink);
            // 
            // About
            // 
            this.About.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutApp,
            this.руководствоПользователяToolStripMenuItem});
            this.About.Name = "About";
            this.About.Size = new System.Drawing.Size(65, 20);
            this.About.Text = "Справка";
            // 
            // AboutApp
            // 
            this.AboutApp.Name = "AboutApp";
            this.AboutApp.Size = new System.Drawing.Size(221, 22);
            this.AboutApp.Text = "О программе";
            this.AboutApp.Click += new System.EventHandler(this.AboutApp_Click);
            // 
            // руководствоПользователяToolStripMenuItem
            // 
            this.руководствоПользователяToolStripMenuItem.Name = "руководствоПользователяToolStripMenuItem";
            this.руководствоПользователяToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.руководствоПользователяToolStripMenuItem.Text = "Руководство пользователя";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.AutoScrollMargin = new System.Drawing.Size(300, 0);
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1MinSize = 335;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2MinSize = 796;
            this.splitContainer1.Size = new System.Drawing.Size(1135, 585);
            this.splitContainer1.SplitterDistance = 335;
            this.splitContainer1.TabIndex = 5;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.textBox2);
            this.splitContainer2.Panel1.Controls.Add(this.Search);
            this.splitContainer2.Panel1MinSize = 55;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.treeView2);
            this.splitContainer2.Panel2MinSize = 526;
            this.splitContainer2.Size = new System.Drawing.Size(335, 585);
            this.splitContainer2.SplitterDistance = 55;
            this.splitContainer2.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(796, 585);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.axAcroPDF2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(788, 559);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Вторичное представление";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // axAcroPDF2
            // 
            this.axAcroPDF2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axAcroPDF2.Enabled = true;
            this.axAcroPDF2.Location = new System.Drawing.Point(3, 3);
            this.axAcroPDF2.Name = "axAcroPDF2";
            this.axAcroPDF2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDF2.OcxState")));
            this.axAcroPDF2.Size = new System.Drawing.Size(782, 553);
            this.axAcroPDF2.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(788, 559);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Информация";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(788, 559);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Изменения";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddDeviceContextNemu,
            this.Correction});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(174, 48);
            // 
            // AddDeviceContextNemu
            // 
            this.AddDeviceContextNemu.Name = "AddDeviceContextNemu";
            this.AddDeviceContextNemu.Size = new System.Drawing.Size(173, 22);
            this.AddDeviceContextNemu.Text = "Добавить изделие";
            this.AddDeviceContextNemu.Click += new System.EventHandler(this.AdderDraw);
            // 
            // Correction
            // 
            this.Correction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.коррекцияСИзвещениемToolStripMenuItem,
            this.OtherCorrection});
            this.Correction.Name = "Correction";
            this.Correction.Size = new System.Drawing.Size(173, 22);
            this.Correction.Text = "Коррекция";
            // 
            // коррекцияСИзвещениемToolStripMenuItem
            // 
            this.коррекцияСИзвещениемToolStripMenuItem.Name = "коррекцияСИзвещениемToolStripMenuItem";
            this.коррекцияСИзвещениемToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.коррекцияСИзвещениемToolStripMenuItem.Text = "Коррекция с извещением";
            // 
            // OtherCorrection
            // 
            this.OtherCorrection.Name = "OtherCorrection";
            this.OtherCorrection.Size = new System.Drawing.Size(216, 22);
            this.OtherCorrection.Text = "Простая коррекция";
            this.OtherCorrection.Click += new System.EventHandler(this.CorrectionDevice_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1135, 609);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip2);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MainMenuStrip = this.menuStrip2;
            this.MinimumSize = new System.Drawing.Size(1151, 648);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF2)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьИзделиеВАрхивToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        CommonClass g_object = new CommonClass();
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button Search;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem CloseItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripMenuItem PDF_Viewer;
        public AxAcroPDFLib.AxAcroPDF axAcroPDF2;
        private System.Windows.Forms.ToolStripMenuItem OpenKompas;
        private System.Windows.Forms.ToolStripMenuItem SearchMarkName;
        private System.Windows.Forms.ToolStripMenuItem AddDeviceContextNemu;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem AboutApp;
        private System.Windows.Forms.ToolStripMenuItem About;
        private System.Windows.Forms.ToolStripMenuItem Correction;
        private System.Windows.Forms.ToolStripMenuItem коррекцияСИзвещениемToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OtherCorrection;
        private System.Windows.Forms.ToolStripMenuItem архивToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поместитьИзделияВАрхивToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem предварительныйСоставИзделияToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem сформироватьСоставToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отобразитьСоставToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поискПоОбозначениюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem руководствоПользователяToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolStripMenuItem обновитьToolStripMenuItem;
    }
}

