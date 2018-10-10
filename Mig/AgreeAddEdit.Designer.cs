namespace Mig
{
    partial class fAgreeAddEdit
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
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.tpAgreeToDt = new Mig.datetimepicker.myDatePicker();
            this.tpAgreeFromDt = new Mig.datetimepicker.myDatePicker();
            this.tpAgreeDt = new Mig.datetimepicker.myDatePicker();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.tAgree = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.tpAgreeToDt);
            this.groupBox10.Controls.Add(this.tpAgreeFromDt);
            this.groupBox10.Controls.Add(this.tpAgreeDt);
            this.groupBox10.Controls.Add(this.label44);
            this.groupBox10.Controls.Add(this.label43);
            this.groupBox10.Controls.Add(this.label42);
            this.groupBox10.Controls.Add(this.label41);
            this.groupBox10.Controls.Add(this.tAgree);
            this.groupBox10.Location = new System.Drawing.Point(12, 12);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(410, 132);
            this.groupBox10.TabIndex = 2;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Договор";
            // 
            // tpAgreeToDt
            // 
            this.tpAgreeToDt.AutoSize = true;
            this.tpAgreeToDt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tpAgreeToDt.BackColor = System.Drawing.Color.Transparent;
            this.tpAgreeToDt.Location = new System.Drawing.Point(303, 80);
            this.tpAgreeToDt.Margin = new System.Windows.Forms.Padding(0);
            this.tpAgreeToDt.Name = "tpAgreeToDt";
            this.tpAgreeToDt.SelectedDate = "";
            this.tpAgreeToDt.Size = new System.Drawing.Size(93, 20);
            this.tpAgreeToDt.TabIndex = 17;
            // 
            // tpAgreeFromDt
            // 
            this.tpAgreeFromDt.AutoSize = true;
            this.tpAgreeFromDt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tpAgreeFromDt.BackColor = System.Drawing.Color.Transparent;
            this.tpAgreeFromDt.Location = new System.Drawing.Point(155, 81);
            this.tpAgreeFromDt.Margin = new System.Windows.Forms.Padding(0);
            this.tpAgreeFromDt.Name = "tpAgreeFromDt";
            this.tpAgreeFromDt.SelectedDate = "";
            this.tpAgreeFromDt.Size = new System.Drawing.Size(93, 20);
            this.tpAgreeFromDt.TabIndex = 16;
            // 
            // tpAgreeDt
            // 
            this.tpAgreeDt.AutoSize = true;
            this.tpAgreeDt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tpAgreeDt.BackColor = System.Drawing.Color.Transparent;
            this.tpAgreeDt.Location = new System.Drawing.Point(155, 54);
            this.tpAgreeDt.Margin = new System.Windows.Forms.Padding(0);
            this.tpAgreeDt.Name = "tpAgreeDt";
            this.tpAgreeDt.SelectedDate = "";
            this.tpAgreeDt.Size = new System.Drawing.Size(93, 20);
            this.tpAgreeDt.TabIndex = 15;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(267, 83);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(24, 13);
            this.label44.TabIndex = 7;
            this.label44.Text = "По:";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(122, 83);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(17, 13);
            this.label43.TabIndex = 6;
            this.label43.Text = "С:";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(116, 57);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(23, 13);
            this.label42.TabIndex = 5;
            this.label42.Text = "От:";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(12, 31);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(127, 13);
            this.label41.TabIndex = 4;
            this.label41.Text = "Договор/Направление:";
            // 
            // tAgree
            // 
            this.tAgree.BackColor = System.Drawing.SystemColors.Window;
            this.tAgree.Location = new System.Drawing.Point(155, 28);
            this.tAgree.Name = "tAgree";
            this.tAgree.Size = new System.Drawing.Size(242, 20);
            this.tAgree.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(344, 151);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(254, 151);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // fAgreeAddEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 186);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox10);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fAgreeAddEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавить/редактировать договор";
            this.Load += new System.EventHandler(this.fAgreeAddEdit_Load);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox10;
        private datetimepicker.myDatePicker tpAgreeToDt;
        private datetimepicker.myDatePicker tpAgreeFromDt;
        private datetimepicker.myDatePicker tpAgreeDt;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox tAgree;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}