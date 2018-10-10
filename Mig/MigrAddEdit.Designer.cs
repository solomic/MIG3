namespace Mig
{
    partial class fMigrAddEdit
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.l90d = new System.Windows.Forms.Label();
            this.tMigTenureTo = new Mig.datetimepicker.myDatePicker();
            this.tMigTenureFrom = new Mig.datetimepicker.myDatePicker();
            this.tMigEntryDt = new Mig.datetimepicker.myDatePicker();
            this.cmbKPP = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.tMigrPurpose = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.tMigrNum = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.tMigrSer = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.l90d);
            this.groupBox5.Controls.Add(this.tMigTenureTo);
            this.groupBox5.Controls.Add(this.tMigTenureFrom);
            this.groupBox5.Controls.Add(this.tMigEntryDt);
            this.groupBox5.Controls.Add(this.cmbKPP);
            this.groupBox5.Controls.Add(this.label34);
            this.groupBox5.Controls.Add(this.label28);
            this.groupBox5.Controls.Add(this.tMigrPurpose);
            this.groupBox5.Controls.Add(this.label33);
            this.groupBox5.Controls.Add(this.label29);
            this.groupBox5.Controls.Add(this.tMigrNum);
            this.groupBox5.Controls.Add(this.label32);
            this.groupBox5.Controls.Add(this.label30);
            this.groupBox5.Controls.Add(this.tMigrSer);
            this.groupBox5.Controls.Add(this.label31);
            this.groupBox5.Location = new System.Drawing.Point(11, 11);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(444, 176);
            this.groupBox5.TabIndex = 37;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Миграционная карта";
            // 
            // l90d
            // 
            this.l90d.AutoSize = true;
            this.l90d.Location = new System.Drawing.Point(227, 40);
            this.l90d.Name = "l90d";
            this.l90d.Size = new System.Drawing.Size(49, 13);
            this.l90d.TabIndex = 49;
            this.l90d.Text = "+ 90 д. =";
            // 
            // tMigTenureTo
            // 
            this.tMigTenureTo.AutoSize = true;
            this.tMigTenureTo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tMigTenureTo.BackColor = System.Drawing.Color.Transparent;
            this.tMigTenureTo.Location = new System.Drawing.Point(338, 60);
            this.tMigTenureTo.Margin = new System.Windows.Forms.Padding(0);
            this.tMigTenureTo.Name = "tMigTenureTo";
            this.tMigTenureTo.SelectedDate = "";
            this.tMigTenureTo.Size = new System.Drawing.Size(93, 20);
            this.tMigTenureTo.TabIndex = 48;
            // 
            // tMigTenureFrom
            // 
            this.tMigTenureFrom.AutoSize = true;
            this.tMigTenureFrom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tMigTenureFrom.BackColor = System.Drawing.Color.Transparent;
            this.tMigTenureFrom.Location = new System.Drawing.Point(122, 60);
            this.tMigTenureFrom.Margin = new System.Windows.Forms.Padding(0);
            this.tMigTenureFrom.Name = "tMigTenureFrom";
            this.tMigTenureFrom.SelectedDate = "";
            this.tMigTenureFrom.Size = new System.Drawing.Size(93, 20);
            this.tMigTenureFrom.TabIndex = 47;
            // 
            // tMigEntryDt
            // 
            this.tMigEntryDt.AutoSize = true;
            this.tMigEntryDt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tMigEntryDt.BackColor = System.Drawing.Color.Transparent;
            this.tMigEntryDt.Location = new System.Drawing.Point(122, 37);
            this.tMigEntryDt.Margin = new System.Windows.Forms.Padding(0);
            this.tMigEntryDt.Name = "tMigEntryDt";
            this.tMigEntryDt.SelectedDate = "";
            this.tMigEntryDt.Size = new System.Drawing.Size(93, 20);
            this.tMigEntryDt.TabIndex = 46;
            this.tMigEntryDt.EnabledChanged += new System.EventHandler(this.tMigEntryDt_EnabledChanged);
            this.tMigEntryDt.Enter += new System.EventHandler(this.tMigEntryDt_Enter);
            this.tMigEntryDt.Validated += new System.EventHandler(this.tMigEntryDt_Validated);
            // 
            // cmbKPP
            // 
            this.cmbKPP.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbKPP.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbKPP.DisplayMember = "kpp_code";
            this.cmbKPP.FormattingEnabled = true;
            this.cmbKPP.Location = new System.Drawing.Point(122, 107);
            this.cmbKPP.Name = "cmbKPP";
            this.cmbKPP.Size = new System.Drawing.Size(144, 21);
            this.cmbKPP.TabIndex = 45;
            this.cmbKPP.ValueMember = "kpp_code";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(39, 132);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(76, 13);
            this.label34.TabIndex = 31;
            this.label34.Text = "Цель въезда:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(39, 41);
            this.label28.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(76, 13);
            this.label28.TabIndex = 19;
            this.label28.Text = "Дата въезда:";
            // 
            // tMigrPurpose
            // 
            this.tMigrPurpose.BackColor = System.Drawing.SystemColors.Window;
            this.tMigrPurpose.Location = new System.Drawing.Point(122, 130);
            this.tMigrPurpose.Margin = new System.Windows.Forms.Padding(2);
            this.tMigrPurpose.Name = "tMigrPurpose";
            this.tMigrPurpose.Size = new System.Drawing.Size(76, 20);
            this.tMigrPurpose.TabIndex = 30;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(81, 110);
            this.label33.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(33, 13);
            this.label33.TabIndex = 28;
            this.label33.Text = "КПП:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 64);
            this.label29.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(109, 13);
            this.label29.TabIndex = 22;
            this.label29.Text = "Срок пребывания с:";
            // 
            // tMigrNum
            // 
            this.tMigrNum.BackColor = System.Drawing.SystemColors.Window;
            this.tMigrNum.Location = new System.Drawing.Point(298, 84);
            this.tMigrNum.Margin = new System.Windows.Forms.Padding(2);
            this.tMigrNum.Name = "tMigrNum";
            this.tMigrNum.Size = new System.Drawing.Size(129, 20);
            this.tMigrNum.TabIndex = 27;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(253, 87);
            this.label32.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(44, 13);
            this.label32.TabIndex = 25;
            this.label32.Text = "Номер:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(228, 64);
            this.label30.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(115, 13);
            this.label30.TabIndex = 23;
            this.label30.Text = "Срок пребывания до:";
            // 
            // tMigrSer
            // 
            this.tMigrSer.BackColor = System.Drawing.SystemColors.Window;
            this.tMigrSer.Location = new System.Drawing.Point(122, 84);
            this.tMigrSer.Margin = new System.Windows.Forms.Padding(2);
            this.tMigrSer.Name = "tMigrSer";
            this.tMigrSer.Size = new System.Drawing.Size(76, 20);
            this.tMigrSer.TabIndex = 26;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(72, 87);
            this.label31.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(41, 13);
            this.label31.TabIndex = 24;
            this.label31.Text = "Серия:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(380, 192);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 38;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(299, 192);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 39;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // fMigrAddEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 223);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fMigrAddEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавить/редактировать миграционную карту";
            this.Load += new System.EventHandler(this.fMigrAddEdit_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private datetimepicker.myDatePicker tMigTenureTo;
        private datetimepicker.myDatePicker tMigTenureFrom;
        private datetimepicker.myDatePicker tMigEntryDt;
        private System.Windows.Forms.ComboBox cmbKPP;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox tMigrPurpose;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox tMigrNum;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox tMigrSer;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label l90d;
    }
}