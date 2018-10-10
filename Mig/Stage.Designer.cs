namespace Mig
{
    partial class fStage
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
            this.tReceipt = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tPayDt = new Mig.datetimepicker.myDatePicker();
            this.tDueDt = new Mig.datetimepicker.myDatePicker();
            this.tStage = new System.Windows.Forms.ComboBox();
            this.tAmount = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // tReceipt
            // 
            this.tReceipt.AutoSize = true;
            this.tReceipt.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tReceipt.Location = new System.Drawing.Point(60, 128);
            this.tReceipt.Name = "tReceipt";
            this.tReceipt.Size = new System.Drawing.Size(80, 17);
            this.tReceipt.TabIndex = 4;
            this.tReceipt.Text = "Квитанция";
            this.tReceipt.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Этап:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Сумма:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Срок оплаты:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Факт. дата оплаты:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(167, 154);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(79, 154);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tPayDt
            // 
            this.tPayDt.AutoSize = true;
            this.tPayDt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tPayDt.BackColor = System.Drawing.Color.Transparent;
            this.tPayDt.Location = new System.Drawing.Point(126, 100);
            this.tPayDt.Margin = new System.Windows.Forms.Padding(0);
            this.tPayDt.Name = "tPayDt";
            this.tPayDt.SelectedDate = "";
            this.tPayDt.Size = new System.Drawing.Size(93, 20);
            this.tPayDt.TabIndex = 2;
            // 
            // tDueDt
            // 
            this.tDueDt.AutoSize = true;
            this.tDueDt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tDueDt.BackColor = System.Drawing.Color.Transparent;
            this.tDueDt.Location = new System.Drawing.Point(126, 75);
            this.tDueDt.Margin = new System.Windows.Forms.Padding(0);
            this.tDueDt.Name = "tDueDt";
            this.tDueDt.SelectedDate = "";
            this.tDueDt.Size = new System.Drawing.Size(93, 20);
            this.tDueDt.TabIndex = 1;
            // 
            // tStage
            // 
            this.tStage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tStage.FormattingEnabled = true;
            this.tStage.Items.AddRange(new object[] {
            "1 этап",
            "2 этап",
            "3 этап",
            "4 этап",
            "5 этап",
            "6 этап",
            "7 этап"});
            this.tStage.Location = new System.Drawing.Point(126, 24);
            this.tStage.Name = "tStage";
            this.tStage.Size = new System.Drawing.Size(74, 21);
            this.tStage.TabIndex = 11;
            // 
            // tAmount
            // 
            this.tAmount.Location = new System.Drawing.Point(126, 50);
            this.tAmount.Name = "tAmount";
            this.tAmount.Size = new System.Drawing.Size(100, 20);
            this.tAmount.TabIndex = 12;
            // 
            // fStage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 192);
            this.Controls.Add(this.tAmount);
            this.Controls.Add(this.tStage);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tReceipt);
            this.Controls.Add(this.tPayDt);
            this.Controls.Add(this.tDueDt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fStage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавить/редактировать этап";
            this.Load += new System.EventHandler(this.fStage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private datetimepicker.myDatePicker tDueDt;
        private datetimepicker.myDatePicker tPayDt;
        private System.Windows.Forms.CheckBox tReceipt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox tStage;
        private System.Windows.Forms.MaskedTextBox tAmount;
    }
}