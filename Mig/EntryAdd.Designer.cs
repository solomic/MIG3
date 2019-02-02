namespace Mig
{
    partial class fEntryAdd
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
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTarget = new System.Windows.Forms.ComboBox();
            this.tText = new System.Windows.Forms.TextBox();
            this.tOutput = new Mig.datetimepicker.myDatePicker();
            this.tInput = new Mig.datetimepicker.myDatePicker();
            this.label65 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox14.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.label1);
            this.groupBox14.Controls.Add(this.cmbTarget);
            this.groupBox14.Controls.Add(this.tText);
            this.groupBox14.Controls.Add(this.tOutput);
            this.groupBox14.Controls.Add(this.tInput);
            this.groupBox14.Controls.Add(this.label65);
            this.groupBox14.Controls.Add(this.label64);
            this.groupBox14.Controls.Add(this.label63);
            this.groupBox14.Location = new System.Drawing.Point(11, 11);
            this.groupBox14.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox14.Size = new System.Drawing.Size(355, 124);
            this.groupBox14.TabIndex = 46;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Въезд/Выезд";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Куда:";
            // 
            // cmbTarget
            // 
            this.cmbTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTarget.FormattingEnabled = true;
            this.cmbTarget.Items.AddRange(new object[] {
            "по России",
            "зарубеж"});
            this.cmbTarget.Location = new System.Drawing.Point(89, 18);
            this.cmbTarget.Name = "cmbTarget";
            this.cmbTarget.Size = new System.Drawing.Size(121, 21);
            this.cmbTarget.TabIndex = 6;
            // 
            // tText
            // 
            this.tText.Location = new System.Drawing.Point(89, 84);
            this.tText.Margin = new System.Windows.Forms.Padding(2);
            this.tText.Name = "tText";
            this.tText.Size = new System.Drawing.Size(198, 20);
            this.tText.TabIndex = 5;
            // 
            // tOutput
            // 
            this.tOutput.AutoSize = true;
            this.tOutput.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tOutput.BackColor = System.Drawing.Color.Transparent;
            this.tOutput.Location = new System.Drawing.Point(88, 40);
            this.tOutput.Margin = new System.Windows.Forms.Padding(0);
            this.tOutput.Name = "tOutput";
            this.tOutput.SelectedDate = "";
            this.tOutput.Size = new System.Drawing.Size(93, 20);
            this.tOutput.TabIndex = 4;
            // 
            // tInput
            // 
            this.tInput.AutoSize = true;
            this.tInput.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tInput.BackColor = System.Drawing.Color.Transparent;
            this.tInput.Location = new System.Drawing.Point(88, 62);
            this.tInput.Margin = new System.Windows.Forms.Padding(0);
            this.tInput.Name = "tInput";
            this.tInput.SelectedDate = "";
            this.tInput.Size = new System.Drawing.Size(93, 20);
            this.tInput.TabIndex = 3;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(49, 87);
            this.label65.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(34, 13);
            this.label65.TabIndex = 2;
            this.label65.Text = "Куда:";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(12, 43);
            this.label64.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(77, 13);
            this.label64.TabIndex = 1;
            this.label64.Text = "Дата выезда:";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(12, 65);
            this.label63.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(76, 13);
            this.label63.TabIndex = 0;
            this.label63.Text = "Дата въезда:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(291, 140);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(210, 140);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 47;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // fEntryAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 172);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox14);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fEntryAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Въезд/Выезд";
            this.Load += new System.EventHandler(this.fEntryAdd_Load);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.TextBox tText;
        private datetimepicker.myDatePicker tOutput;
        private datetimepicker.myDatePicker tInput;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTarget;
    }
}