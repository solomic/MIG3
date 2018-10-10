namespace Mig
{
    partial class fAddressAll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fAddressAll));
            this.dgAddrAll = new ADGV.AdvancedDataGridView();
            this.dgAddrStud = new ADGV.AdvancedDataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.закрепитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открепитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgAddrAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAddrStud)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgAddrAll
            // 
            this.dgAddrAll.AllowUserToAddRows = false;
            this.dgAddrAll.AllowUserToDeleteRows = false;
            this.dgAddrAll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgAddrAll.AutoGenerateContextFilters = true;
            this.dgAddrAll.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgAddrAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAddrAll.DateWithTime = false;
            this.dgAddrAll.EnableHeadersVisualStyles = false;
            this.dgAddrAll.Location = new System.Drawing.Point(12, 54);
            this.dgAddrAll.Name = "dgAddrAll";
            this.dgAddrAll.ReadOnly = true;
            this.dgAddrAll.Size = new System.Drawing.Size(1036, 356);
            this.dgAddrAll.TabIndex = 0;
            this.dgAddrAll.TimeFilter = false;
            this.dgAddrAll.VirtualMode = true;
            this.dgAddrAll.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgAddrAll_CellMouseClick);
            // 
            // dgAddrStud
            // 
            this.dgAddrStud.AllowUserToAddRows = false;
            this.dgAddrStud.AllowUserToDeleteRows = false;
            this.dgAddrStud.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgAddrStud.AutoGenerateContextFilters = true;
            this.dgAddrStud.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgAddrStud.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAddrStud.DateWithTime = false;
            this.dgAddrStud.EnableHeadersVisualStyles = false;
            this.dgAddrStud.Location = new System.Drawing.Point(12, 416);
            this.dgAddrStud.Name = "dgAddrStud";
            this.dgAddrStud.ReadOnly = true;
            this.dgAddrStud.Size = new System.Drawing.Size(1036, 322);
            this.dgAddrStud.TabIndex = 1;
            this.dgAddrStud.TimeFilter = false;
            this.dgAddrStud.VirtualMode = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1060, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.закрепитьToolStripMenuItem,
            this.открепитьToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(96, 22);
            this.toolStripDropDownButton1.Text = "Действие...";
            // 
            // закрепитьToolStripMenuItem
            // 
            this.закрепитьToolStripMenuItem.Name = "закрепитьToolStripMenuItem";
            this.закрепитьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.закрепитьToolStripMenuItem.Text = "Закрепить";
            this.закрепитьToolStripMenuItem.Click += new System.EventHandler(this.закрепитьToolStripMenuItem_Click);
            // 
            // открепитьToolStripMenuItem
            // 
            this.открепитьToolStripMenuItem.Name = "открепитьToolStripMenuItem";
            this.открепитьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.открепитьToolStripMenuItem.Text = "Открепить";
            this.открепитьToolStripMenuItem.Click += new System.EventHandler(this.открепитьToolStripMenuItem_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(111, 22);
            this.toolStripButton1.Text = "Переназначить";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::Mig.Properties.Resources.Delete;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(71, 22);
            this.toolStripButton2.Text = "Удалить";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(186, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // fAddressAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 750);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgAddrStud);
            this.Controls.Add(this.dgAddrAll);
            this.MinimizeBox = false;
            this.Name = "fAddressAll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактор адресов";
            this.Load += new System.EventHandler(this.fAddressAll_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgAddrAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAddrStud)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ADGV.AdvancedDataGridView dgAddrAll;
        private ADGV.AdvancedDataGridView dgAddrStud;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem закрепитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открепитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}