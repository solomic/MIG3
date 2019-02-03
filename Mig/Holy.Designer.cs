namespace Mig
{
    partial class fHolyForm
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
            this.components = new System.ComponentModel.Container();
            this.adgHoly = new ADGV.AdvancedDataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.bsHoly = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.adgHoly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsHoly)).BeginInit();
            this.SuspendLayout();
            // 
            // adgHoly
            // 
            this.adgHoly.AutoGenerateContextFilters = true;
            this.adgHoly.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.adgHoly.DateWithTime = false;
            this.adgHoly.Location = new System.Drawing.Point(12, 12);
            this.adgHoly.MultiSelect = false;
            this.adgHoly.Name = "adgHoly";
            this.adgHoly.Size = new System.Drawing.Size(477, 497);
            this.adgHoly.TabIndex = 0;
            this.adgHoly.TimeFilter = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(414, 515);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // fHolyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 542);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.adgHoly);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fHolyForm";
            this.Text = "Производственный календарь";
            this.Load += new System.EventHandler(this.fHolyForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.adgHoly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsHoly)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ADGV.AdvancedDataGridView adgHoly;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.BindingSource bsHoly;
    }
}