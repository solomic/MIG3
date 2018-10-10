namespace Mig
{
    partial class fHost
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
            this.dgHost = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnActivate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgHost)).BeginInit();
            this.SuspendLayout();
            // 
            // dgHost
            // 
            this.dgHost.AllowUserToAddRows = false;
            this.dgHost.AllowUserToDeleteRows = false;
            this.dgHost.AllowUserToOrderColumns = true;
            this.dgHost.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgHost.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgHost.Location = new System.Drawing.Point(15, 14);
            this.dgHost.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgHost.MultiSelect = false;
            this.dgHost.Name = "dgHost";
            this.dgHost.ReadOnly = true;
            this.dgHost.RowTemplate.Height = 24;
            this.dgHost.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgHost.Size = new System.Drawing.Size(1060, 288);
            this.dgHost.TabIndex = 1;
            this.dgHost.DoubleClick += new System.EventHandler(this.dgPf_DoubleClick);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(973, 308);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Закрыть";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnActivate
            // 
            this.btnActivate.Location = new System.Drawing.Point(693, 308);
            this.btnActivate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(121, 28);
            this.btnActivate.TabIndex = 3;
            this.btnActivate.Text = "Активировать";
            this.btnActivate.UseVisualStyleBackColor = true;
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(823, 308);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 28);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // fHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 348);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnActivate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgHost);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "fHost";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Принимающая сторона";
            this.Load += new System.EventHandler(this.fHost_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgHost)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgHost;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnActivate;
        private System.Windows.Forms.Button btnAdd;
    }
}