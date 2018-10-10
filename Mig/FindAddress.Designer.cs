namespace Mig
{
    partial class fFindAddress
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
            this.btnSaveNewAddr = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbHouse = new System.Windows.Forms.ComboBox();
            this.label66 = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label65 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCorp = new System.Windows.Forms.ComboBox();
            this.cmbFlat = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.cmbStroenie = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSaveNewAddr
            // 
            this.btnSaveNewAddr.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSaveNewAddr.Location = new System.Drawing.Point(210, 259);
            this.btnSaveNewAddr.Name = "btnSaveNewAddr";
            this.btnSaveNewAddr.Size = new System.Drawing.Size(75, 23);
            this.btnSaveNewAddr.TabIndex = 0;
            this.btnSaveNewAddr.Text = "Ок";
            this.btnSaveNewAddr.UseVisualStyleBackColor = true;
            this.btnSaveNewAddr.Click += new System.EventHandler(this.btnSaveNewAddr_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(300, 259);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cmbHouse
            // 
            this.cmbHouse.FormattingEnabled = true;
            this.cmbHouse.Location = new System.Drawing.Point(113, 108);
            this.cmbHouse.Name = "cmbHouse";
            this.cmbHouse.Size = new System.Drawing.Size(94, 21);
            this.cmbHouse.TabIndex = 27;
            this.cmbHouse.TextChanged += new System.EventHandler(this.comboBox5_TextChanged);
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(74, 110);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(35, 15);
            this.label66.TabIndex = 26;
            this.label66.Text = "Дом:";
            // 
            // comboBox4
            // 
            this.comboBox4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(113, 80);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(263, 21);
            this.comboBox4.TabIndex = 25;
            this.comboBox4.SelectedIndexChanged += new System.EventHandler(this.comboBox4_SelectedIndexChanged);
            this.comboBox4.TextChanged += new System.EventHandler(this.comboBox4_TextChanged);
            // 
            // comboBox3
            // 
            this.comboBox3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.comboBox3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(113, 52);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(263, 21);
            this.comboBox3.TabIndex = 24;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(113, 25);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(263, 21);
            this.comboBox1.TabIndex = 22;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.TextUpdate += new System.EventHandler(this.comboBox1_TextUpdate);
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(65, 82);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(46, 15);
            this.label65.TabIndex = 21;
            this.label65.Text = "Улица:";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(44, 55);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(68, 15);
            this.label64.TabIndex = 20;
            this.label64.Text = "Город/н.п.:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(61, 28);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(51, 15);
            this.label19.TabIndex = 18;
            this.label19.Text = "Регион:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 28;
            // 
            // cmbCorp
            // 
            this.cmbCorp.FormattingEnabled = true;
            this.cmbCorp.Location = new System.Drawing.Point(113, 136);
            this.cmbCorp.Name = "cmbCorp";
            this.cmbCorp.Size = new System.Drawing.Size(94, 21);
            this.cmbCorp.TabIndex = 29;
            this.cmbCorp.TextChanged += new System.EventHandler(this.cmbCorp_TextChanged);
            // 
            // cmbFlat
            // 
            this.cmbFlat.FormattingEnabled = true;
            this.cmbFlat.Location = new System.Drawing.Point(113, 188);
            this.cmbFlat.Name = "cmbFlat";
            this.cmbFlat.Size = new System.Drawing.Size(94, 21);
            this.cmbFlat.TabIndex = 30;
            this.cmbFlat.TextChanged += new System.EventHandler(this.cmbFlat_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 31;
            this.label2.Text = "Корпус:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 32;
            this.label3.Text = "Квартира:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(113, 214);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(263, 40);
            this.textBox1.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 217);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 15);
            this.label4.TabIndex = 34;
            this.label4.Text = "Полный:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(232, 190);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(144, 20);
            this.textBox2.TabIndex = 35;
            // 
            // cmbStroenie
            // 
            this.cmbStroenie.FormattingEnabled = true;
            this.cmbStroenie.Location = new System.Drawing.Point(113, 161);
            this.cmbStroenie.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbStroenie.Name = "cmbStroenie";
            this.cmbStroenie.Size = new System.Drawing.Size(94, 21);
            this.cmbStroenie.TabIndex = 36;
            this.cmbStroenie.TextChanged += new System.EventHandler(this.cmbStroenie_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 163);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 37;
            this.label5.Text = "Строение:";
            // 
            // fFindAddress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 291);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbStroenie);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbFlat);
            this.Controls.Add(this.cmbCorp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbHouse);
            this.Controls.Add(this.label66);
            this.Controls.Add(this.comboBox4);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label65);
            this.Controls.Add(this.label64);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveNewAddr);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fFindAddress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавить новый адрес";
            this.Load += new System.EventHandler(this.fFindAddress_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveNewAddr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbHouse;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCorp;
        private System.Windows.Forms.ComboBox cmbFlat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox cmbStroenie;
        private System.Windows.Forms.Label label5;
    }
}