namespace Mig
{
    partial class fContactEdit
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tDulValidity = new Mig.datetimepicker.myDatePicker();
            this.tDulIssue = new Mig.datetimepicker.myDatePicker();
            this.tDulType = new System.Windows.Forms.ComboBox();
            this.tDulNum = new System.Windows.Forms.TextBox();
            this.tDulSer = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(232, 127);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 58;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(323, 127);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 59;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tDulValidity
            // 
            this.tDulValidity.AutoSize = true;
            this.tDulValidity.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tDulValidity.BackColor = System.Drawing.Color.Transparent;
            this.tDulValidity.Location = new System.Drawing.Point(305, 53);
            this.tDulValidity.Margin = new System.Windows.Forms.Padding(0);
            this.tDulValidity.Name = "tDulValidity";
            this.tDulValidity.SelectedDate = "";
            this.tDulValidity.Size = new System.Drawing.Size(93, 20);
            this.tDulValidity.TabIndex = 41;
            // 
            // tDulIssue
            // 
            this.tDulIssue.AutoSize = true;
            this.tDulIssue.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tDulIssue.BackColor = System.Drawing.Color.Transparent;
            this.tDulIssue.Location = new System.Drawing.Point(305, 26);
            this.tDulIssue.Margin = new System.Windows.Forms.Padding(0);
            this.tDulIssue.Name = "tDulIssue";
            this.tDulIssue.SelectedDate = "";
            this.tDulIssue.Size = new System.Drawing.Size(93, 20);
            this.tDulIssue.TabIndex = 41;
            // 
            // tDulType
            // 
            this.tDulType.DisplayMember = "value";
            this.tDulType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tDulType.FormattingEnabled = true;
            this.tDulType.Location = new System.Drawing.Point(75, 25);
            this.tDulType.Name = "tDulType";
            this.tDulType.Size = new System.Drawing.Size(116, 21);
            this.tDulType.TabIndex = 41;
            this.tDulType.ValueMember = "code";
            this.tDulType.SelectedIndexChanged += new System.EventHandler(this.tDulType_SelectedIndexChanged);
            // 
            // tDulNum
            // 
            this.tDulNum.BackColor = System.Drawing.SystemColors.Window;
            this.tDulNum.Location = new System.Drawing.Point(75, 80);
            this.tDulNum.Name = "tDulNum";
            this.tDulNum.Size = new System.Drawing.Size(116, 20);
            this.tDulNum.TabIndex = 7;
            this.tDulNum.TextChanged += new System.EventHandler(this.tDulNum_TextChanged);
            // 
            // tDulSer
            // 
            this.tDulSer.BackColor = System.Drawing.SystemColors.Window;
            this.tDulSer.Location = new System.Drawing.Point(75, 53);
            this.tDulSer.Name = "tDulSer";
            this.tDulSer.Size = new System.Drawing.Size(116, 20);
            this.tDulSer.TabIndex = 6;
            this.tDulSer.TextChanged += new System.EventHandler(this.tDulSer_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(214, 56);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(85, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Срок действия:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(223, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Дата выдачи:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 83);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Номер:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(31, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Серия:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(40, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Вид:";
            // 
            // fContactEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 162);
            this.Controls.Add(this.tDulValidity);
            this.Controls.Add(this.tDulIssue);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tDulType);
            this.Controls.Add(this.tDulNum);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tDulSer);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fContactEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование";
            this.Load += new System.EventHandler(this.fContactEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private datetimepicker.myDatePicker tDulValidity;
        private datetimepicker.myDatePicker tDulIssue;
        private System.Windows.Forms.ComboBox tDulType;
        private System.Windows.Forms.TextBox tDulNum;
        private System.Windows.Forms.TextBox tDulSer;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
    }
}