namespace Mig
{
    partial class fDocMigrHist
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
            this.dgDoc = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgMigrCard = new System.Windows.Forms.DataGridView();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDoc)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMigrCard)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dgDoc);
            this.groupBox6.Location = new System.Drawing.Point(13, 26);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox6.Size = new System.Drawing.Size(1311, 363);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Документы";
            // 
            // dgDoc
            // 
            this.dgDoc.AllowUserToAddRows = false;
            this.dgDoc.AllowUserToDeleteRows = false;
            this.dgDoc.AllowUserToResizeRows = false;
            this.dgDoc.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgDoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDoc.Location = new System.Drawing.Point(27, 39);
            this.dgDoc.Margin = new System.Windows.Forms.Padding(4);
            this.dgDoc.MultiSelect = false;
            this.dgDoc.Name = "dgDoc";
            this.dgDoc.ReadOnly = true;
            this.dgDoc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDoc.Size = new System.Drawing.Size(1261, 317);
            this.dgDoc.TabIndex = 0;
            this.dgDoc.VirtualMode = true;
            this.dgDoc.Click += new System.EventHandler(this.dgDoc_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgMigrCard);
            this.groupBox5.Location = new System.Drawing.Point(13, 441);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(1311, 371);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Миграционные карты";
            // 
            // dgMigrCard
            // 
            this.dgMigrCard.AllowUserToAddRows = false;
            this.dgMigrCard.AllowUserToDeleteRows = false;
            this.dgMigrCard.AllowUserToOrderColumns = true;
            this.dgMigrCard.AllowUserToResizeRows = false;
            this.dgMigrCard.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgMigrCard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMigrCard.GridColor = System.Drawing.SystemColors.ScrollBar;
            this.dgMigrCard.Location = new System.Drawing.Point(27, 40);
            this.dgMigrCard.Margin = new System.Windows.Forms.Padding(4);
            this.dgMigrCard.Name = "dgMigrCard";
            this.dgMigrCard.ReadOnly = true;
            this.dgMigrCard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMigrCard.ShowEditingIcon = false;
            this.dgMigrCard.Size = new System.Drawing.Size(1261, 304);
            this.dgMigrCard.TabIndex = 0;
            this.dgMigrCard.VirtualMode = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(40, 413);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(137, 21);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Отображать все";
            this.checkBox1.UseVisualStyleBackColor = true;
         
            // 
            // fDocMigrHist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1337, 839);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fDocMigrHist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "История";
            this.Load += new System.EventHandler(this.fDocMigrHist_Load);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDoc)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgMigrCard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dgDoc;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgMigrCard;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}