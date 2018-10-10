namespace Mig
{
    partial class fFilterColumnEdit
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
            this.cmbAllFilter = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgAllColumn = new System.Windows.Forms.DataGridView();
            this.dgMyColumn = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgAllColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMyColumn)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbAllFilter
            // 
            this.cmbAllFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAllFilter.FormattingEnabled = true;
            this.cmbAllFilter.Location = new System.Drawing.Point(68, 39);
            this.cmbAllFilter.Margin = new System.Windows.Forms.Padding(2);
            this.cmbAllFilter.Name = "cmbAllFilter";
            this.cmbAllFilter.Size = new System.Drawing.Size(273, 21);
            this.cmbAllFilter.TabIndex = 0;
            this.cmbAllFilter.SelectedValueChanged += new System.EventHandler(this.cmbAllFilter_SelectedValueChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(577, 537);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(72, 22);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(666, 537);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(65, 22);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgAllColumn
            // 
            this.dgAllColumn.AllowUserToAddRows = false;
            this.dgAllColumn.AllowUserToDeleteRows = false;
            this.dgAllColumn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgAllColumn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAllColumn.Location = new System.Drawing.Point(16, 76);
            this.dgAllColumn.MultiSelect = false;
            this.dgAllColumn.Name = "dgAllColumn";
            this.dgAllColumn.ReadOnly = true;
            this.dgAllColumn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgAllColumn.ShowCellErrors = false;
            this.dgAllColumn.ShowCellToolTips = false;
            this.dgAllColumn.ShowEditingIcon = false;
            this.dgAllColumn.ShowRowErrors = false;
            this.dgAllColumn.Size = new System.Drawing.Size(325, 439);
            this.dgAllColumn.TabIndex = 5;
            this.dgAllColumn.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAllColumn_CellDoubleClick);
            this.dgAllColumn.DoubleClick += new System.EventHandler(this.dgAllColumn_DoubleClick);
            // 
            // dgMyColumn
            // 
            this.dgMyColumn.AllowUserToAddRows = false;
            this.dgMyColumn.AllowUserToDeleteRows = false;
            this.dgMyColumn.AllowUserToResizeRows = false;
            this.dgMyColumn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgMyColumn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMyColumn.Location = new System.Drawing.Point(388, 76);
            this.dgMyColumn.MultiSelect = false;
            this.dgMyColumn.Name = "dgMyColumn";
            this.dgMyColumn.ReadOnly = true;
            this.dgMyColumn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMyColumn.ShowCellErrors = false;
            this.dgMyColumn.ShowCellToolTips = false;
            this.dgMyColumn.ShowEditingIcon = false;
            this.dgMyColumn.ShowRowErrors = false;
            this.dgMyColumn.Size = new System.Drawing.Size(345, 439);
            this.dgMyColumn.TabIndex = 6;
            this.dgMyColumn.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMyColumn_CellDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Фильтр:";
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Image = global::Mig.Properties.Resources.arrow_upp;
            this.button1.Location = new System.Drawing.Point(347, 76);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 30);
            this.button1.TabIndex = 7;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Image = global::Mig.Properties.Resources.arrow_down;
            this.button2.Location = new System.Drawing.Point(347, 122);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(35, 30);
            this.button2.TabIndex = 8;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // fFilterColumnEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 573);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgMyColumn);
            this.Controls.Add(this.dgAllColumn);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbAllFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fFilterColumnEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование";
            this.Load += new System.EventHandler(this.fFilterColumnEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgAllColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMyColumn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbAllFilter;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgAllColumn;
        private System.Windows.Forms.DataGridView dgMyColumn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
    }
}