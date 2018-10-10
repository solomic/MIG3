namespace Mig
{
    partial class fSpecEdit
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFacDel = new System.Windows.Forms.Button();
            this.btnFacEdit = new System.Windows.Forms.Button();
            this.btnFacAdd = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSpecDel = new System.Windows.Forms.Button();
            this.btnSpecEdit = new System.Windows.Forms.Button();
            this.btnSpecAdd = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFacDel);
            this.groupBox1.Controls.Add(this.btnFacEdit);
            this.groupBox1.Controls.Add(this.btnFacAdd);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(660, 240);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Факультет";
            // 
            // btnFacDel
            // 
            this.btnFacDel.Location = new System.Drawing.Point(192, 20);
            this.btnFacDel.Name = "btnFacDel";
            this.btnFacDel.Size = new System.Drawing.Size(75, 23);
            this.btnFacDel.TabIndex = 3;
            this.btnFacDel.Text = "Удалить";
            this.btnFacDel.UseVisualStyleBackColor = true;
            this.btnFacDel.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnFacEdit
            // 
            this.btnFacEdit.Location = new System.Drawing.Point(88, 20);
            this.btnFacEdit.Name = "btnFacEdit";
            this.btnFacEdit.Size = new System.Drawing.Size(75, 23);
            this.btnFacEdit.TabIndex = 2;
            this.btnFacEdit.Text = "Изменить";
            this.btnFacEdit.UseVisualStyleBackColor = true;
            this.btnFacEdit.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnFacAdd
            // 
            this.btnFacAdd.Location = new System.Drawing.Point(7, 20);
            this.btnFacAdd.Name = "btnFacAdd";
            this.btnFacAdd.Size = new System.Drawing.Size(75, 23);
            this.btnFacAdd.TabIndex = 1;
            this.btnFacAdd.Text = "Добавить";
            this.btnFacAdd.UseVisualStyleBackColor = true;
            this.btnFacAdd.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 51);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(648, 183);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSpecDel);
            this.groupBox2.Controls.Add(this.btnSpecEdit);
            this.groupBox2.Controls.Add(this.btnSpecAdd);
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Location = new System.Drawing.Point(12, 258);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(660, 300);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Специальность";
            // 
            // btnSpecDel
            // 
            this.btnSpecDel.Location = new System.Drawing.Point(192, 28);
            this.btnSpecDel.Name = "btnSpecDel";
            this.btnSpecDel.Size = new System.Drawing.Size(75, 23);
            this.btnSpecDel.TabIndex = 3;
            this.btnSpecDel.Text = "Удалить";
            this.btnSpecDel.UseVisualStyleBackColor = true;
            this.btnSpecDel.Click += new System.EventHandler(this.btnSpecDel_Click);
            // 
            // btnSpecEdit
            // 
            this.btnSpecEdit.Location = new System.Drawing.Point(88, 28);
            this.btnSpecEdit.Name = "btnSpecEdit";
            this.btnSpecEdit.Size = new System.Drawing.Size(75, 23);
            this.btnSpecEdit.TabIndex = 2;
            this.btnSpecEdit.Text = "Изменить";
            this.btnSpecEdit.UseVisualStyleBackColor = true;
            this.btnSpecEdit.Click += new System.EventHandler(this.btnSpecEdit_Click);
            // 
            // btnSpecAdd
            // 
            this.btnSpecAdd.Location = new System.Drawing.Point(7, 29);
            this.btnSpecAdd.Name = "btnSpecAdd";
            this.btnSpecAdd.Size = new System.Drawing.Size(75, 23);
            this.btnSpecAdd.TabIndex = 1;
            this.btnSpecAdd.Text = "Добавить";
            this.btnSpecAdd.UseVisualStyleBackColor = true;
            this.btnSpecAdd.Click += new System.EventHandler(this.button4_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(6, 58);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(648, 236);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.DoubleClick += new System.EventHandler(this.dataGridView2_DoubleClick);
            // 
            // fSpecEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 568);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fSpecEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование специальностей";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fSpecEdit_FormClosed);
            this.Load += new System.EventHandler(this.fSpecEdit_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btnFacDel;
        private System.Windows.Forms.Button btnFacEdit;
        private System.Windows.Forms.Button btnFacAdd;
        private System.Windows.Forms.Button btnSpecDel;
        private System.Windows.Forms.Button btnSpecEdit;
        private System.Windows.Forms.Button btnSpecAdd;
    }
}