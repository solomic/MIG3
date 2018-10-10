namespace Mig
{
    partial class fDocFms
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
            this.myDatePicker1 = new Mig.datetimepicker.myDatePicker();
            this.btnSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // myDatePicker1
            // 
            this.myDatePicker1.AutoSize = true;
            this.myDatePicker1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.myDatePicker1.BackColor = System.Drawing.Color.Transparent;
            this.myDatePicker1.Location = new System.Drawing.Point(13, 13);
            this.myDatePicker1.Margin = new System.Windows.Forms.Padding(0);
            this.myDatePicker1.Name = "myDatePicker1";
            this.myDatePicker1.SelectedDate = "";
            this.myDatePicker1.Size = new System.Drawing.Size(93, 20);
            this.myDatePicker1.TabIndex = 0;
            // 
            // btnSelect
            // 
            this.btnSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSelect.Location = new System.Drawing.Point(120, 13);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "Выбрать";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // fDocFms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(211, 48);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.myDatePicker1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fDocFms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Дата сдачи документов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private datetimepicker.myDatePicker myDatePicker1;
        private System.Windows.Forms.Button btnSelect;
    }
}