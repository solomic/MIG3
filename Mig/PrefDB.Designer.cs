namespace Mig
{
    partial class fPrefDB
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
            this.tHost = new System.Windows.Forms.TextBox();
            this.tDB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tRep = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tInvRep = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tHost
            // 
            this.tHost.Location = new System.Drawing.Point(128, 17);
            this.tHost.Margin = new System.Windows.Forms.Padding(4);
            this.tHost.Name = "tHost";
            this.tHost.Size = new System.Drawing.Size(312, 22);
            this.tHost.TabIndex = 0;
            // 
            // tDB
            // 
            this.tDB.Location = new System.Drawing.Point(128, 50);
            this.tDB.Margin = new System.Windows.Forms.Padding(4);
            this.tDB.Name = "tDB";
            this.tDB.Size = new System.Drawing.Size(132, 22);
            this.tDB.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Хост:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(88, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "БД:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(340, 155);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(232, 155);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tRep
            // 
            this.tRep.Location = new System.Drawing.Point(128, 80);
            this.tRep.Margin = new System.Windows.Forms.Padding(4);
            this.tRep.Name = "tRep";
            this.tRep.Size = new System.Drawing.Size(311, 22);
            this.tRep.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(57, 83);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Отчеты:";
            // 
            // tInvRep
            // 
            this.tInvRep.Location = new System.Drawing.Point(128, 110);
            this.tInvRep.Margin = new System.Windows.Forms.Padding(4);
            this.tInvRep.Name = "tInvRep";
            this.tInvRep.Size = new System.Drawing.Size(311, 22);
            this.tInvRep.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(17, 110);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 36);
            this.label6.TabIndex = 14;
            this.label6.Text = "Отчеты, приглашения:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // fPrefDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 196);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tInvRep);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tRep);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tDB);
            this.Controls.Add(this.tHost);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fPrefDB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.fPrefDB_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tHost;
        private System.Windows.Forms.TextBox tDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tRep;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tInvRep;
        private System.Windows.Forms.Label label6;
    }
}