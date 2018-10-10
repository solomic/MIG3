namespace Mig
{
    partial class fExpelledEdit
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
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tExpelledNum = new System.Windows.Forms.TextBox();
            this.tExpelled = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tExpelledDt = new Mig.datetimepicker.myDatePicker();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.tExpelledDt);
            this.groupBox8.Controls.Add(this.label20);
            this.groupBox8.Controls.Add(this.label19);
            this.groupBox8.Controls.Add(this.tExpelledNum);
            this.groupBox8.Controls.Add(this.tExpelled);
            this.groupBox8.Location = new System.Drawing.Point(11, 22);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox8.Size = new System.Drawing.Size(505, 107);
            this.groupBox8.TabIndex = 40;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Основание прекращения или завершения обучения (в связи с ...):";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(254, 68);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(36, 13);
            this.label20.TabIndex = 38;
            this.label20.Text = "Дата:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(13, 67);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(89, 13);
            this.label19.TabIndex = 37;
            this.label19.Text = "Номер приказа:";
            // 
            // tExpelledNum
            // 
            this.tExpelledNum.BackColor = System.Drawing.SystemColors.Window;
            this.tExpelledNum.Location = new System.Drawing.Point(102, 66);
            this.tExpelledNum.Margin = new System.Windows.Forms.Padding(2);
            this.tExpelledNum.Name = "tExpelledNum";
            this.tExpelledNum.Size = new System.Drawing.Size(120, 20);
            this.tExpelledNum.TabIndex = 35;
            // 
            // tExpelled
            // 
            this.tExpelled.BackColor = System.Drawing.SystemColors.Window;
            this.tExpelled.Location = new System.Drawing.Point(14, 28);
            this.tExpelled.Margin = new System.Windows.Forms.Padding(2);
            this.tExpelled.Name = "tExpelled";
            this.tExpelled.Size = new System.Drawing.Size(484, 20);
            this.tExpelled.TabIndex = 34;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(441, 140);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 41;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(348, 140);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 42;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tExpelledDt
            // 
            this.tExpelledDt.AutoSize = true;
            this.tExpelledDt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tExpelledDt.BackColor = System.Drawing.Color.Transparent;
            this.tExpelledDt.Location = new System.Drawing.Point(292, 66);
            this.tExpelledDt.Margin = new System.Windows.Forms.Padding(0);
            this.tExpelledDt.Name = "tExpelledDt";
            this.tExpelledDt.SelectedDate = "";
            this.tExpelledDt.Size = new System.Drawing.Size(93, 20);
            this.tExpelledDt.TabIndex = 39;
            // 
            // fExpelledEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 175);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fExpelledEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование";
            this.Load += new System.EventHandler(this.fExpelledEdit_Load);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tExpelledNum;
        private System.Windows.Forms.TextBox tExpelled;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private datetimepicker.myDatePicker tExpelledDt;
    }
}