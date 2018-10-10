namespace Mig
{
    partial class fDocAddEdit
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tDocValidTo = new Mig.datetimepicker.myDatePicker();
            this.tDocValidFrom = new Mig.datetimepicker.myDatePicker();
            this.tDocIssue = new Mig.datetimepicker.myDatePicker();
            this.label19 = new System.Windows.Forms.Label();
            this.cmbDocType = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.tIdent = new System.Windows.Forms.TextBox();
            this.tInvite = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.tDocSer = new System.Windows.Forms.TextBox();
            this.tDocNum = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.tDocValidTo);
            this.groupBox6.Controls.Add(this.tDocValidFrom);
            this.groupBox6.Controls.Add(this.tDocIssue);
            this.groupBox6.Controls.Add(this.label19);
            this.groupBox6.Controls.Add(this.cmbDocType);
            this.groupBox6.Controls.Add(this.label21);
            this.groupBox6.Controls.Add(this.tIdent);
            this.groupBox6.Controls.Add(this.tInvite);
            this.groupBox6.Controls.Add(this.label27);
            this.groupBox6.Controls.Add(this.label22);
            this.groupBox6.Controls.Add(this.label26);
            this.groupBox6.Controls.Add(this.tDocSer);
            this.groupBox6.Controls.Add(this.tDocNum);
            this.groupBox6.Controls.Add(this.label23);
            this.groupBox6.Controls.Add(this.label25);
            this.groupBox6.Controls.Add(this.label24);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox6.Location = new System.Drawing.Point(11, 11);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(444, 190);
            this.groupBox6.TabIndex = 37;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Информация";
            // 
            // tDocValidTo
            // 
            this.tDocValidTo.AutoSize = true;
            this.tDocValidTo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tDocValidTo.BackColor = System.Drawing.Color.Transparent;
            this.tDocValidTo.Location = new System.Drawing.Point(332, 137);
            this.tDocValidTo.Margin = new System.Windows.Forms.Padding(0);
            this.tDocValidTo.Name = "tDocValidTo";
            this.tDocValidTo.SelectedDate = "";
            this.tDocValidTo.Size = new System.Drawing.Size(93, 20);
            this.tDocValidTo.TabIndex = 25;
            // 
            // tDocValidFrom
            // 
            this.tDocValidFrom.AutoSize = true;
            this.tDocValidFrom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tDocValidFrom.BackColor = System.Drawing.Color.Transparent;
            this.tDocValidFrom.Location = new System.Drawing.Point(126, 138);
            this.tDocValidFrom.Margin = new System.Windows.Forms.Padding(0);
            this.tDocValidFrom.Name = "tDocValidFrom";
            this.tDocValidFrom.SelectedDate = "";
            this.tDocValidFrom.Size = new System.Drawing.Size(93, 20);
            this.tDocValidFrom.TabIndex = 24;
            // 
            // tDocIssue
            // 
            this.tDocIssue.AutoSize = true;
            this.tDocIssue.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tDocIssue.BackColor = System.Drawing.Color.Transparent;
            this.tDocIssue.Location = new System.Drawing.Point(126, 115);
            this.tDocIssue.Margin = new System.Windows.Forms.Padding(0);
            this.tDocIssue.Name = "tDocIssue";
            this.tDocIssue.SelectedDate = "";
            this.tDocIssue.Size = new System.Drawing.Size(93, 20);
            this.tDocIssue.TabIndex = 23;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(35, 28);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(86, 13);
            this.label19.TabIndex = 19;
            this.label19.Text = "Тип документа:";
            // 
            // cmbDocType
            // 
            this.cmbDocType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbDocType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbDocType.DisplayMember = "value";
            this.cmbDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDocType.FormattingEnabled = true;
            this.cmbDocType.Location = new System.Drawing.Point(126, 25);
            this.cmbDocType.Name = "cmbDocType";
            this.cmbDocType.Size = new System.Drawing.Size(119, 21);
            this.cmbDocType.TabIndex = 18;
            this.cmbDocType.ValueMember = "code";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(31, 49);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(90, 13);
            this.label21.TabIndex = 6;
            this.label21.Text = "Идентификатор:";
            // 
            // tIdent
            // 
            this.tIdent.BackColor = System.Drawing.SystemColors.Window;
            this.tIdent.Location = new System.Drawing.Point(126, 47);
            this.tIdent.Margin = new System.Windows.Forms.Padding(2);
            this.tIdent.Name = "tIdent";
            this.tIdent.Size = new System.Drawing.Size(119, 20);
            this.tIdent.TabIndex = 4;
            // 
            // tInvite
            // 
            this.tInvite.BackColor = System.Drawing.SystemColors.Window;
            this.tInvite.Location = new System.Drawing.Point(126, 69);
            this.tInvite.Margin = new System.Windows.Forms.Padding(2);
            this.tInvite.Name = "tInvite";
            this.tInvite.Size = new System.Drawing.Size(119, 20);
            this.tInvite.TabIndex = 5;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(232, 140);
            this.label27.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(100, 13);
            this.label27.TabIndex = 17;
            this.label27.Text = "Срок действия по:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(-3, 72);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(114, 13);
            this.label22.TabIndex = 7;
            this.label22.Text = "Номер приглашения:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(29, 140);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(94, 13);
            this.label26.TabIndex = 16;
            this.label26.Text = "Срок действия с:";
            // 
            // tDocSer
            // 
            this.tDocSer.BackColor = System.Drawing.SystemColors.Window;
            this.tDocSer.Location = new System.Drawing.Point(126, 92);
            this.tDocSer.Margin = new System.Windows.Forms.Padding(2);
            this.tDocSer.Name = "tDocSer";
            this.tDocSer.Size = new System.Drawing.Size(76, 20);
            this.tDocSer.TabIndex = 8;
            // 
            // tDocNum
            // 
            this.tDocNum.BackColor = System.Drawing.SystemColors.Window;
            this.tDocNum.Location = new System.Drawing.Point(279, 92);
            this.tDocNum.Margin = new System.Windows.Forms.Padding(2);
            this.tDocNum.Name = "tDocNum";
            this.tDocNum.Size = new System.Drawing.Size(119, 20);
            this.tDocNum.TabIndex = 9;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(85, 95);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(41, 13);
            this.label23.TabIndex = 10;
            this.label23.Text = "Серия:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(46, 117);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(76, 13);
            this.label25.TabIndex = 13;
            this.label25.Text = "Дата выдачи:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(232, 95);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(44, 13);
            this.label24.TabIndex = 11;
            this.label24.Text = "Номер:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(279, 206);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 38;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(380, 206);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 39;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // fDocAddEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 238);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fDocAddEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавить";
            this.Load += new System.EventHandler(this.fDocMigrAdd_Load);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox6;
        private datetimepicker.myDatePicker tDocValidTo;
        private datetimepicker.myDatePicker tDocValidFrom;
        private datetimepicker.myDatePicker tDocIssue;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cmbDocType;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox tIdent;
        private System.Windows.Forms.TextBox tInvite;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox tDocSer;
        private System.Windows.Forms.TextBox tDocNum;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}