namespace Mig
{
    partial class fTeachAddEdit
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.tSpecCode = new System.Windows.Forms.TextBox();
            this.cmbIndSrok = new System.Windows.Forms.ComboBox();
            this.cmbTotalSrok = new System.Windows.Forms.ComboBox();
            this.cmbPO = new System.Windows.Forms.ComboBox();
            this.cmbFin = new System.Windows.Forms.ComboBox();
            this.cmbFO = new System.Windows.Forms.ComboBox();
            this.cmbSpec = new System.Windows.Forms.ComboBox();
            this.cmbFac = new System.Windows.Forms.ComboBox();
            this.label53 = new System.Windows.Forms.Label();
            this.tYearAmount = new System.Windows.Forms.TextBox();
            this.tTeachInd = new System.Windows.Forms.TextBox();
            this.tTeachTotal = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.tKurs = new System.Windows.Forms.TextBox();
            this.tYear = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(518, 365);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(427, 365);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.tSpecCode);
            this.groupBox9.Controls.Add(this.cmbIndSrok);
            this.groupBox9.Controls.Add(this.cmbTotalSrok);
            this.groupBox9.Controls.Add(this.cmbPO);
            this.groupBox9.Controls.Add(this.cmbFin);
            this.groupBox9.Controls.Add(this.cmbFO);
            this.groupBox9.Controls.Add(this.cmbSpec);
            this.groupBox9.Controls.Add(this.cmbFac);
            this.groupBox9.Controls.Add(this.label53);
            this.groupBox9.Controls.Add(this.tYearAmount);
            this.groupBox9.Controls.Add(this.tTeachInd);
            this.groupBox9.Controls.Add(this.tTeachTotal);
            this.groupBox9.Controls.Add(this.label52);
            this.groupBox9.Controls.Add(this.label51);
            this.groupBox9.Controls.Add(this.label50);
            this.groupBox9.Controls.Add(this.tKurs);
            this.groupBox9.Controls.Add(this.tYear);
            this.groupBox9.Controls.Add(this.label45);
            this.groupBox9.Controls.Add(this.label40);
            this.groupBox9.Controls.Add(this.label39);
            this.groupBox9.Controls.Add(this.label38);
            this.groupBox9.Controls.Add(this.label37);
            this.groupBox9.Controls.Add(this.label36);
            this.groupBox9.Controls.Add(this.label35);
            this.groupBox9.Location = new System.Drawing.Point(12, 12);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(581, 347);
            this.groupBox9.TabIndex = 4;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Образование";
            // 
            // tSpecCode
            // 
            this.tSpecCode.Location = new System.Drawing.Point(146, 190);
            this.tSpecCode.Margin = new System.Windows.Forms.Padding(2);
            this.tSpecCode.Name = "tSpecCode";
            this.tSpecCode.ReadOnly = true;
            this.tSpecCode.Size = new System.Drawing.Size(183, 20);
            this.tSpecCode.TabIndex = 32;
            this.tSpecCode.Visible = false;
            // 
            // cmbIndSrok
            // 
            this.cmbIndSrok.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIndSrok.FormattingEnabled = true;
            this.cmbIndSrok.Items.AddRange(new object[] {
            "мес.",
            "год"});
            this.cmbIndSrok.Location = new System.Drawing.Point(224, 266);
            this.cmbIndSrok.Name = "cmbIndSrok";
            this.cmbIndSrok.Size = new System.Drawing.Size(78, 21);
            this.cmbIndSrok.TabIndex = 30;
            this.cmbIndSrok.Visible = false;
            // 
            // cmbTotalSrok
            // 
            this.cmbTotalSrok.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTotalSrok.FormattingEnabled = true;
            this.cmbTotalSrok.Items.AddRange(new object[] {
            "мес.",
            "год"});
            this.cmbTotalSrok.Location = new System.Drawing.Point(224, 240);
            this.cmbTotalSrok.Name = "cmbTotalSrok";
            this.cmbTotalSrok.Size = new System.Drawing.Size(78, 21);
            this.cmbTotalSrok.TabIndex = 29;
            this.cmbTotalSrok.Visible = false;
            // 
            // cmbPO
            // 
            this.cmbPO.DisplayMember = "value";
            this.cmbPO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPO.FormattingEnabled = true;
            this.cmbPO.Location = new System.Drawing.Point(146, 84);
            this.cmbPO.Name = "cmbPO";
            this.cmbPO.Size = new System.Drawing.Size(183, 21);
            this.cmbPO.TabIndex = 28;
            this.cmbPO.ValueMember = "code";
            this.cmbPO.SelectedValueChanged += new System.EventHandler(this.cmbPO_SelectedValueChanged);
            // 
            // cmbFin
            // 
            this.cmbFin.DisplayMember = "value";
            this.cmbFin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFin.FormattingEnabled = true;
            this.cmbFin.Location = new System.Drawing.Point(146, 57);
            this.cmbFin.Name = "cmbFin";
            this.cmbFin.Size = new System.Drawing.Size(183, 21);
            this.cmbFin.TabIndex = 27;
            this.cmbFin.ValueMember = "code";
            // 
            // cmbFO
            // 
            this.cmbFO.DisplayMember = "value";
            this.cmbFO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFO.FormattingEnabled = true;
            this.cmbFO.Location = new System.Drawing.Point(146, 29);
            this.cmbFO.Name = "cmbFO";
            this.cmbFO.Size = new System.Drawing.Size(183, 21);
            this.cmbFO.TabIndex = 26;
            this.cmbFO.ValueMember = "code";
            // 
            // cmbSpec
            // 
            this.cmbSpec.DisplayMember = "name";
            this.cmbSpec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpec.FormattingEnabled = true;
            this.cmbSpec.Location = new System.Drawing.Point(146, 138);
            this.cmbSpec.Name = "cmbSpec";
            this.cmbSpec.Size = new System.Drawing.Size(415, 21);
            this.cmbSpec.TabIndex = 25;
            this.cmbSpec.ValueMember = "spec_code";
            this.cmbSpec.SelectedValueChanged += new System.EventHandler(this.cmbSpec_SelectedValueChanged);
            // 
            // cmbFac
            // 
            this.cmbFac.DisplayMember = "name";
            this.cmbFac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFac.FormattingEnabled = true;
            this.cmbFac.Location = new System.Drawing.Point(146, 111);
            this.cmbFac.Name = "cmbFac";
            this.cmbFac.Size = new System.Drawing.Size(415, 21);
            this.cmbFac.TabIndex = 24;
            this.cmbFac.ValueMember = "code";
            this.cmbFac.SelectedValueChanged += new System.EventHandler(this.cmbFac_SelectedValueChanged);
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(38, 294);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(100, 13);
            this.label53.TabIndex = 21;
            this.label53.Text = "Стоимость за год:";
            this.label53.Visible = false;
            // 
            // tYearAmount
            // 
            this.tYearAmount.BackColor = System.Drawing.SystemColors.Window;
            this.tYearAmount.Location = new System.Drawing.Point(146, 292);
            this.tYearAmount.Name = "tYearAmount";
            this.tYearAmount.Size = new System.Drawing.Size(100, 20);
            this.tYearAmount.TabIndex = 20;
            this.tYearAmount.Visible = false;
            // 
            // tTeachInd
            // 
            this.tTeachInd.BackColor = System.Drawing.SystemColors.Window;
            this.tTeachInd.Location = new System.Drawing.Point(146, 266);
            this.tTeachInd.Name = "tTeachInd";
            this.tTeachInd.Size = new System.Drawing.Size(71, 20);
            this.tTeachInd.TabIndex = 19;
            this.tTeachInd.Visible = false;
            // 
            // tTeachTotal
            // 
            this.tTeachTotal.BackColor = System.Drawing.SystemColors.Window;
            this.tTeachTotal.Location = new System.Drawing.Point(146, 240);
            this.tTeachTotal.Name = "tTeachTotal";
            this.tTeachTotal.Size = new System.Drawing.Size(71, 20);
            this.tTeachTotal.TabIndex = 18;
            this.tTeachTotal.Visible = false;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(18, 267);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(120, 13);
            this.label52.TabIndex = 17;
            this.label52.Text = "Срок обучения индив.:";
            this.label52.Visible = false;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(20, 241);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(120, 13);
            this.label51.TabIndex = 16;
            this.label51.Text = "Срок обучения общий:";
            this.label51.Visible = false;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(104, 215);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(34, 13);
            this.label50.TabIndex = 15;
            this.label50.Text = "Курс:";
            this.label50.Visible = false;
            // 
            // tKurs
            // 
            this.tKurs.BackColor = System.Drawing.SystemColors.Control;
            this.tKurs.Location = new System.Drawing.Point(146, 214);
            this.tKurs.Name = "tKurs";
            this.tKurs.Size = new System.Drawing.Size(100, 20);
            this.tKurs.TabIndex = 14;
            this.tKurs.Visible = false;
            // 
            // tYear
            // 
            this.tYear.BackColor = System.Drawing.SystemColors.Window;
            this.tYear.Location = new System.Drawing.Point(146, 165);
            this.tYear.Name = "tYear";
            this.tYear.Size = new System.Drawing.Size(100, 20);
            this.tYear.TabIndex = 13;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(43, 166);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(95, 13);
            this.label45.TabIndex = 12;
            this.label45.Text = "Год поступления:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(39, 59);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(99, 13);
            this.label40.TabIndex = 11;
            this.label40.Text = "Финансирование:";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(42, 32);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(96, 13);
            this.label39.TabIndex = 10;
            this.label39.Text = "Форма обучения:";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(20, 87);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(118, 13);
            this.label38.TabIndex = 9;
            this.label38.Text = "Программа обучения:";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(50, 140);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(88, 13);
            this.label37.TabIndex = 8;
            this.label37.Text = "Специальность:";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(18, 192);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(119, 13);
            this.label36.TabIndex = 7;
            this.label36.Text = "Шифр специальности:";
            this.label36.Visible = false;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(75, 112);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(66, 13);
            this.label35.TabIndex = 6;
            this.label35.Text = "Факультет:";
            // 
            // fTeachAddEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 396);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fTeachAddEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавить/редактировать";
            this.Load += new System.EventHandler(this.fTeachAddEdit_Load);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TextBox tSpecCode;
        private System.Windows.Forms.ComboBox cmbIndSrok;
        private System.Windows.Forms.ComboBox cmbTotalSrok;
        private System.Windows.Forms.ComboBox cmbPO;
        private System.Windows.Forms.ComboBox cmbFin;
        private System.Windows.Forms.ComboBox cmbFO;
        private System.Windows.Forms.ComboBox cmbSpec;
        private System.Windows.Forms.ComboBox cmbFac;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.TextBox tYearAmount;
        private System.Windows.Forms.TextBox tTeachInd;
        private System.Windows.Forms.TextBox tTeachTotal;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.TextBox tKurs;
        private System.Windows.Forms.TextBox tYear;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
    }
}