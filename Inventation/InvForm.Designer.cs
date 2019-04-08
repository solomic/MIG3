namespace Inventation
{
    partial class fMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.авторизацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.общиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сортировкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedDataGridView1 = new ADGV.AdvancedDataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.приглашениеОткрытьВWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.приглашениеСформироватьИПоказатьВПапкеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.гарантийноеПисьмоОткрытьВWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.гарантийноеПисьмоСформироватьИПоказатьВПапкеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.подтверждениеОткрытьВWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.подтверждениеСформироватьИПоказатьВПапкеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.открытьОбщуюПапкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView1)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.авторизацияToolStripMenuItem,
            this.настройкиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1292, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // авторизацияToolStripMenuItem
            // 
            this.авторизацияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConnect,
            this.btnDisconnect});
            this.авторизацияToolStripMenuItem.Name = "авторизацияToolStripMenuItem";
            this.авторизацияToolStripMenuItem.Size = new System.Drawing.Size(113, 24);
            this.авторизацияToolStripMenuItem.Text = "Авторизация";
            // 
            // btnConnect
            // 
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(194, 26);
            this.btnConnect.Text = "Подключиться...";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(194, 26);
            this.btnDisconnect.Text = "Отключиться";
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.общиеToolStripMenuItem,
            this.сортировкаToolStripMenuItem});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // общиеToolStripMenuItem
            // 
            this.общиеToolStripMenuItem.Name = "общиеToolStripMenuItem";
            this.общиеToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.общиеToolStripMenuItem.Text = "Общие...";
            // 
            // сортировкаToolStripMenuItem
            // 
            this.сортировкаToolStripMenuItem.Name = "сортировкаToolStripMenuItem";
            this.сортировкаToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.сортировкаToolStripMenuItem.Text = "Сортировка...";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem,
            this.копироватьToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(163, 52);
            // 
            // добавитьToolStripMenuItem
            // 
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
            this.добавитьToolStripMenuItem.Text = "Добавить";
            // 
            // копироватьToolStripMenuItem
            // 
            this.копироватьToolStripMenuItem.Name = "копироватьToolStripMenuItem";
            this.копироватьToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
            this.копироватьToolStripMenuItem.Text = "Копировать";
            // 
            // advancedDataGridView1
            // 
            this.advancedDataGridView1.AutoGenerateContextFilters = true;
            this.advancedDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.advancedDataGridView1.DateWithTime = false;
            this.advancedDataGridView1.Location = new System.Drawing.Point(12, 117);
            this.advancedDataGridView1.Name = "advancedDataGridView1";
            this.advancedDataGridView1.RowTemplate.Height = 24;
            this.advancedDataGridView1.Size = new System.Drawing.Size(1259, 498);
            this.advancedDataGridView1.TabIndex = 1;
            this.advancedDataGridView1.TimeFilter = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 46);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 28);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // cmbFilter
            // 
            this.cmbFilter.FormattingEnabled = true;
            this.cmbFilter.Location = new System.Drawing.Point(106, 46);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(234, 24);
            this.cmbFilter.TabIndex = 3;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(154, 87);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(186, 24);
            this.comboBox1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Поиск по фамилии:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(355, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 28);
            this.button1.TabIndex = 6;
            this.button1.Text = "Оформление в ФМС";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(531, 46);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(151, 28);
            this.button2.TabIndex = 7;
            this.button2.Text = "Получено из ФМС";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(688, 46);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(161, 28);
            this.button3.TabIndex = 8;
            this.button3.Text = "Выдано/Отправлено";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(855, 46);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(93, 28);
            this.button4.TabIndex = 9;
            this.button4.Text = "Приехал";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(954, 47);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(145, 28);
            this.button5.TabIndex = 10;
            this.button5.Text = "Не приехал(отказ)";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1121, 48);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(92, 27);
            this.button6.TabIndex = 11;
            this.button6.Text = "Удалить...";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.приглашениеОткрытьВWordToolStripMenuItem,
            this.приглашениеСформироватьИПоказатьВПапкеToolStripMenuItem,
            this.гарантийноеПисьмоОткрытьВWordToolStripMenuItem,
            this.гарантийноеПисьмоСформироватьИПоказатьВПапкеToolStripMenuItem,
            this.подтверждениеОткрытьВWordToolStripMenuItem,
            this.подтверждениеСформироватьИПоказатьВПапкеToolStripMenuItem,
            this.toolStripSeparator1,
            this.открытьОбщуюПапкуToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(482, 178);
            this.contextMenuStrip2.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip2_Opening);
            // 
            // приглашениеОткрытьВWordToolStripMenuItem
            // 
            this.приглашениеОткрытьВWordToolStripMenuItem.Name = "приглашениеОткрытьВWordToolStripMenuItem";
            this.приглашениеОткрытьВWordToolStripMenuItem.Size = new System.Drawing.Size(481, 24);
            this.приглашениеОткрытьВWordToolStripMenuItem.Text = "Приглашение: открыть в Word...";
            // 
            // приглашениеСформироватьИПоказатьВПапкеToolStripMenuItem
            // 
            this.приглашениеСформироватьИПоказатьВПапкеToolStripMenuItem.Name = "приглашениеСформироватьИПоказатьВПапкеToolStripMenuItem";
            this.приглашениеСформироватьИПоказатьВПапкеToolStripMenuItem.Size = new System.Drawing.Size(481, 24);
            this.приглашениеСформироватьИПоказатьВПапкеToolStripMenuItem.Text = "Приглашение: сформировать и показать в папке...";
            // 
            // гарантийноеПисьмоОткрытьВWordToolStripMenuItem
            // 
            this.гарантийноеПисьмоОткрытьВWordToolStripMenuItem.Name = "гарантийноеПисьмоОткрытьВWordToolStripMenuItem";
            this.гарантийноеПисьмоОткрытьВWordToolStripMenuItem.Size = new System.Drawing.Size(481, 24);
            this.гарантийноеПисьмоОткрытьВWordToolStripMenuItem.Text = "Гарантийное письмо: открыть в word...";
            // 
            // гарантийноеПисьмоСформироватьИПоказатьВПапкеToolStripMenuItem
            // 
            this.гарантийноеПисьмоСформироватьИПоказатьВПапкеToolStripMenuItem.Name = "гарантийноеПисьмоСформироватьИПоказатьВПапкеToolStripMenuItem";
            this.гарантийноеПисьмоСформироватьИПоказатьВПапкеToolStripMenuItem.Size = new System.Drawing.Size(481, 24);
            this.гарантийноеПисьмоСформироватьИПоказатьВПапкеToolStripMenuItem.Text = "Гарантийное письмо: сформировать и показать в папке...";
            // 
            // подтверждениеОткрытьВWordToolStripMenuItem
            // 
            this.подтверждениеОткрытьВWordToolStripMenuItem.Name = "подтверждениеОткрытьВWordToolStripMenuItem";
            this.подтверждениеОткрытьВWordToolStripMenuItem.Size = new System.Drawing.Size(481, 24);
            this.подтверждениеОткрытьВWordToolStripMenuItem.Text = "Подтверждение: открыть в word...";
            // 
            // подтверждениеСформироватьИПоказатьВПапкеToolStripMenuItem
            // 
            this.подтверждениеСформироватьИПоказатьВПапкеToolStripMenuItem.Name = "подтверждениеСформироватьИПоказатьВПапкеToolStripMenuItem";
            this.подтверждениеСформироватьИПоказатьВПапкеToolStripMenuItem.Size = new System.Drawing.Size(481, 24);
            this.подтверждениеСформироватьИПоказатьВПапкеToolStripMenuItem.Text = "Подтверждение: сформировать и показать в папке...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(478, 6);
            // 
            // открытьОбщуюПапкуToolStripMenuItem
            // 
            this.открытьОбщуюПапкуToolStripMenuItem.Name = "открытьОбщуюПапкуToolStripMenuItem";
            this.открытьОбщуюПапкуToolStripMenuItem.Size = new System.Drawing.Size(481, 24);
            this.открытьОбщуюПапкуToolStripMenuItem.Text = "Открыть общую папку...";
            // 
            // fMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1292, 627);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.cmbFilter);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.advancedDataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "fMainForm";
            this.Text = "ЦМО: приглашения";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView1)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem авторизацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnConnect;
        private System.Windows.Forms.ToolStripMenuItem btnDisconnect;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem общиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сортировкаToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private ADGV.AdvancedDataGridView advancedDataGridView1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cmbFilter;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem приглашениеОткрытьВWordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem приглашениеСформироватьИПоказатьВПапкеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem гарантийноеПисьмоОткрытьВWordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem гарантийноеПисьмоСформироватьИПоказатьВПапкеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem подтверждениеОткрытьВWordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem подтверждениеСформироватьИПоказатьВПапкеToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem открытьОбщуюПапкуToolStripMenuItem;
    }
}

