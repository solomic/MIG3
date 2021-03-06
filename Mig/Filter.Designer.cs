﻿namespace Mig
{
    partial class fFilter
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lFilter = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miGraduate = new System.Windows.Forms.ToolStripMenuItem();
            this.miContinueTeach = new System.Windows.Forms.ToolStripMenuItem();
            this.miStudent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tpDeductSet = new System.Windows.Forms.ToolStripMenuItem();
            this.tpDeductClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tpDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tpRecover = new System.Windows.Forms.ToolStripMenuItem();
            this.tpRF = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tpDocDone = new System.Windows.Forms.ToolStripMenuItem();
            this.tpDocClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.tpRegExendSet = new System.Windows.Forms.ToolStripMenuItem();
            this.tpRegExendClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stCnt = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsFilterLoad = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridView1 = new ADGV.AdvancedDataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.бДToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConnectDB = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSpr = new System.Windows.Forms.ToolStripMenuItem();
            this.tpSpec = new System.Windows.Forms.ToolStripMenuItem();
            this.tpAddress = new System.Windows.Forms.ToolStripMenuItem();
            this.tpInvite = new System.Windows.Forms.ToolStripMenuItem();
            this.mHoly = new System.Windows.Forms.ToolStripMenuItem();
            this.mSpr = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnReset = new System.Windows.Forms.ToolStripMenuItem();
            this.tpData = new System.Windows.Forms.ToolStripMenuItem();
            this.tpBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.tpSync = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConnectPref = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.lFilter);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbFilter);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(8, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1057, 40);
            this.panel1.TabIndex = 2;
            // 
            // lFilter
            // 
            this.lFilter.AutoSize = true;
            this.lFilter.Location = new System.Drawing.Point(837, 13);
            this.lFilter.Name = "lFilter";
            this.lFilter.Size = new System.Drawing.Size(0, 13);
            this.lFilter.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(379, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "ФИЛЬТР:";
            // 
            // cmbFilter
            // 
            this.cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter.FormattingEnabled = true;
            this.cmbFilter.Location = new System.Drawing.Point(454, 10);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(354, 21);
            this.cmbFilter.TabIndex = 2;
            this.cmbFilter.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(137, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(210, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ПОИСК:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miGraduate,
            this.miContinueTeach,
            this.miStudent,
            this.toolStripMenuItem1,
            this.tpDeductSet,
            this.tpDeductClear,
            this.toolStripMenuItem2,
            this.tpDelete,
            this.tpRecover,
            this.tpRF,
            this.toolStripMenuItem3,
            this.tpDocDone,
            this.tpDocClear,
            this.toolStripMenuItem4,
            this.tpRegExendSet,
            this.tpRegExendClear,
            this.toolStripMenuItem5});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(250, 298);
            this.contextMenuStrip1.Text = "Выпускник";
            // 
            // miGraduate
            // 
            this.miGraduate.Name = "miGraduate";
            this.miGraduate.Size = new System.Drawing.Size(249, 22);
            this.miGraduate.Text = "Выпускник";
            this.miGraduate.Click += new System.EventHandler(this.miGraduate_Click);
            // 
            // miContinueTeach
            // 
            this.miContinueTeach.Name = "miContinueTeach";
            this.miContinueTeach.Size = new System.Drawing.Size(249, 22);
            this.miContinueTeach.Text = "Продолжение обучения";
            this.miContinueTeach.Click += new System.EventHandler(this.miContinueTeach_Click);
            // 
            // miStudent
            // 
            this.miStudent.Name = "miStudent";
            this.miStudent.Size = new System.Drawing.Size(249, 22);
            this.miStudent.Text = "Отмена";
            this.miStudent.Click += new System.EventHandler(this.miStudent_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(246, 6);
            // 
            // tpDeductSet
            // 
            this.tpDeductSet.Name = "tpDeductSet";
            this.tpDeductSet.Size = new System.Drawing.Size(249, 22);
            this.tpDeductSet.Text = "На отчисление";
            this.tpDeductSet.Click += new System.EventHandler(this.tpDeductSet_Click);
            // 
            // tpDeductClear
            // 
            this.tpDeductClear.Name = "tpDeductClear";
            this.tpDeductClear.Size = new System.Drawing.Size(249, 22);
            this.tpDeductClear.Text = "Снять \"на отчисление\"";
            this.tpDeductClear.Click += new System.EventHandler(this.tpDeductClear_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(246, 6);
            // 
            // tpDelete
            // 
            this.tpDelete.Name = "tpDelete";
            this.tpDelete.Size = new System.Drawing.Size(249, 22);
            this.tpDelete.Text = "Закончено обучение/Отчислен";
            this.tpDelete.Click += new System.EventHandler(this.tpDelete_Click);
            // 
            // tpRecover
            // 
            this.tpRecover.Name = "tpRecover";
            this.tpRecover.Size = new System.Drawing.Size(249, 22);
            this.tpRecover.Text = "Восстановить";
            this.tpRecover.Click += new System.EventHandler(this.tpRecover_Click);
            // 
            // tpRF
            // 
            this.tpRF.Name = "tpRF";
            this.tpRF.Size = new System.Drawing.Size(249, 22);
            this.tpRF.Text = "Получен паспорт РФ";
            this.tpRF.Click += new System.EventHandler(this.tpRF_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(246, 6);
            // 
            // tpDocDone
            // 
            this.tpDocDone.Name = "tpDocDone";
            this.tpDocDone.Size = new System.Drawing.Size(249, 22);
            this.tpDocDone.Text = "Виза: документы сданы";
            this.tpDocDone.Click += new System.EventHandler(this.tpDocDone_Click);
            // 
            // tpDocClear
            // 
            this.tpDocClear.Name = "tpDocClear";
            this.tpDocClear.Size = new System.Drawing.Size(249, 22);
            this.tpDocClear.Text = "Виза: снять выделение";
            this.tpDocClear.Click += new System.EventHandler(this.tpDocClear_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(246, 6);
            // 
            // tpRegExendSet
            // 
            this.tpRegExendSet.Name = "tpRegExendSet";
            this.tpRegExendSet.Size = new System.Drawing.Size(249, 22);
            this.tpRegExendSet.Text = "Регистрация: продлить";
            this.tpRegExendSet.Click += new System.EventHandler(this.tpRegExendSet_Click);
            // 
            // tpRegExendClear
            // 
            this.tpRegExendClear.Name = "tpRegExendClear";
            this.tpRegExendClear.Size = new System.Drawing.Size(249, 22);
            this.tpRegExendClear.Text = "Регистрация: снять выделение";
            this.tpRegExendClear.Click += new System.EventHandler(this.tpRegExendClear_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(246, 6);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stCnt,
            this.tsFilterLoad});
            this.statusStrip1.Location = new System.Drawing.Point(0, 750);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1077, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stCnt
            // 
            this.stCnt.Name = "stCnt";
            this.stCnt.Size = new System.Drawing.Size(0, 17);
            // 
            // tsFilterLoad
            // 
            this.tsFilterLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsFilterLoad.Name = "tsFilterLoad";
            this.tsFilterLoad.Size = new System.Drawing.Size(0, 17);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateContextFilters = true;
            this.dataGridView1.ColumnHeadersHeight = 35;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.DateWithTime = false;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(8, 83);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1057, 664);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.TimeFilter = false;
            this.dataGridView1.VirtualMode = true;
            this.dataGridView1.SortStringChanged += new System.EventHandler(this.dataGridView1_SortStringChanged);
            this.dataGridView1.FilterStringChanged += new System.EventHandler(this.dataGridView1_FilterStringChanged);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.advancedDataGridView1_CellDoubleClick);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.advancedDataGridView1_CellPainting);
            this.dataGridView1.ColumnDisplayIndexChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnDisplayIndexChanged);
            this.dataGridView1.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnWidthChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.бДToolStripMenuItem,
            this.btnSpr,
            this.tpData});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1077, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // бДToolStripMenuItem
            // 
            this.бДToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConnectDB,
            this.btnExit});
            this.бДToolStripMenuItem.Name = "бДToolStripMenuItem";
            this.бДToolStripMenuItem.Size = new System.Drawing.Size(34, 20);
            this.бДToolStripMenuItem.Text = "БД";
            // 
            // btnConnectDB
            // 
            this.btnConnectDB.Name = "btnConnectDB";
            this.btnConnectDB.Size = new System.Drawing.Size(161, 22);
            this.btnConnectDB.Text = "Подключение...";
            this.btnConnectDB.Click += new System.EventHandler(this.btnConnectDB_Click);
            // 
            // btnExit
            // 
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(161, 22);
            this.btnExit.Text = "Выход";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSpr
            // 
            this.btnSpr.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tpSpec,
            this.tpAddress,
            this.tpInvite,
            this.mHoly,
            this.mSpr,
            this.miColumn});
            this.btnSpr.Name = "btnSpr";
            this.btnSpr.Size = new System.Drawing.Size(99, 20);
            this.btnSpr.Text = "Редактировать";
            // 
            // tpSpec
            // 
            this.tpSpec.Name = "tpSpec";
            this.tpSpec.Size = new System.Drawing.Size(280, 22);
            this.tpSpec.Text = "Специальности...";
            this.tpSpec.Click += new System.EventHandler(this.tpSpec_Click);
            // 
            // tpAddress
            // 
            this.tpAddress.Name = "tpAddress";
            this.tpAddress.Size = new System.Drawing.Size(280, 22);
            this.tpAddress.Text = "Адреса...";
            this.tpAddress.Click += new System.EventHandler(this.tpAddress_Click);
            // 
            // tpInvite
            // 
            this.tpInvite.Name = "tpInvite";
            this.tpInvite.Size = new System.Drawing.Size(280, 22);
            this.tpInvite.Text = "Сведения о принимающей стороне...";
            this.tpInvite.Click += new System.EventHandler(this.tpInvite_Click);
            // 
            // mHoly
            // 
            this.mHoly.Name = "mHoly";
            this.mHoly.Size = new System.Drawing.Size(280, 22);
            this.mHoly.Text = "Праздники...";
            this.mHoly.Click += new System.EventHandler(this.mHoly_Click);
            // 
            // mSpr
            // 
            this.mSpr.Name = "mSpr";
            this.mSpr.Size = new System.Drawing.Size(280, 22);
            this.mSpr.Text = "Справочники...";
            // 
            // miColumn
            // 
            this.miColumn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miColumnEdit,
            this.miColumnReset});
            this.miColumn.Name = "miColumn";
            this.miColumn.Size = new System.Drawing.Size(280, 22);
            this.miColumn.Text = "Колонки";
            // 
            // miColumnEdit
            // 
            this.miColumnEdit.Name = "miColumnEdit";
            this.miColumnEdit.Size = new System.Drawing.Size(213, 22);
            this.miColumnEdit.Text = "Редактировать...";
            this.miColumnEdit.Click += new System.EventHandler(this.miColumnEdit_Click);
            // 
            // miColumnReset
            // 
            this.miColumnReset.Name = "miColumnReset";
            this.miColumnReset.Size = new System.Drawing.Size(213, 22);
            this.miColumnReset.Text = "Сбросить по умолчанию";
            this.miColumnReset.Click += new System.EventHandler(this.miColumnReset_Click);
            // 
            // tpData
            // 
            this.tpData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tpBackup,
            this.tpSync,
            this.btnConnectPref});
            this.tpData.Name = "tpData";
            this.tpData.Size = new System.Drawing.Size(62, 20);
            this.tpData.Text = "Данные";
            // 
            // tpBackup
            // 
            this.tpBackup.Name = "tpBackup";
            this.tpBackup.Size = new System.Drawing.Size(184, 22);
            this.tpBackup.Text = "Резервная копия БД";
            this.tpBackup.Click += new System.EventHandler(this.tpBackup_Click);
            // 
            // tpSync
            // 
            this.tpSync.Name = "tpSync";
            this.tpSync.Size = new System.Drawing.Size(184, 22);
            this.tpSync.Text = "Синхронизация";
            this.tpSync.Visible = false;
            this.tpSync.Click += new System.EventHandler(this.tpSync_Click);
            // 
            // btnConnectPref
            // 
            this.btnConnectPref.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnConnectPref.Name = "btnConnectPref";
            this.btnConnectPref.Size = new System.Drawing.Size(184, 22);
            this.btnConnectPref.Text = "Подключение";
            this.btnConnectPref.Click += new System.EventHandler(this.allViewToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Image = global::Mig.Properties.Resources.add_icon_icons_com_52393;
            this.button1.Location = new System.Drawing.Point(0, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 32);
            this.button1.TabIndex = 6;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bindingSource1
            // 
            this.bindingSource1.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.bindingSource1_ListChanged);
            // 
            // fFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 772);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "fFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Миграционный учет";
            this.Load += new System.EventHandler(this.fMigr_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFilter;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stCnt;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miGraduate;
        private System.Windows.Forms.ToolStripMenuItem miContinueTeach;
        private System.Windows.Forms.ToolStripMenuItem miStudent;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tpDeductSet;
        private System.Windows.Forms.ToolStripMenuItem tpDeductClear;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tpDelete;
        private System.Windows.Forms.ToolStripMenuItem tpRecover;
        private System.Windows.Forms.ToolStripMenuItem tpRF;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tpDocDone;
        private System.Windows.Forms.ToolStripMenuItem tpDocClear;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem tpRegExendSet;
        private System.Windows.Forms.ToolStripMenuItem tpRegExendClear;
        private System.Windows.Forms.BindingSource bindingSource1;
        private ADGV.AdvancedDataGridView dataGridView1;
        private System.Windows.Forms.Label lFilter;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnSpr;
        private System.Windows.Forms.ToolStripMenuItem tpSpec;
        private System.Windows.Forms.ToolStripMenuItem tpAddress;
        private System.Windows.Forms.ToolStripMenuItem tpInvite;
        private System.Windows.Forms.ToolStripMenuItem tpData;
        private System.Windows.Forms.ToolStripMenuItem tpBackup;
        private System.Windows.Forms.ToolStripMenuItem tpSync;
        private System.Windows.Forms.ToolStripMenuItem btnConnectPref;
        private System.Windows.Forms.ToolStripMenuItem бДToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnConnectDB;
        private System.Windows.Forms.ToolStripMenuItem btnExit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripStatusLabel tsFilterLoad;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem mHoly;
        private System.Windows.Forms.ToolStripMenuItem mSpr;
        private System.Windows.Forms.ToolStripMenuItem miColumn;
        private System.Windows.Forms.ToolStripMenuItem miColumnEdit;
        private System.Windows.Forms.ToolStripMenuItem miColumnReset;
    }
}