using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Mig
{
    public partial class fFindAddress : Form
    {

        public fFindAddress()
        {
            InitializeComponent();
        }
        int _Address_code;
        public string FullAddress { get { return textBox1.Text; } }

        public string KladrCode { get { return comboBox4.SelectedValue == null ? "" : comboBox4.SelectedValue.ToString(); } }

        public string House { get { return cmbHouse.Text.Trim(); } }
        public string Stroenie { get { return cmbStroenie.Text.Trim(); } }
        public string Corp { get { return cmbCorp.Text.Trim(); } }
        public string Flat { get { return cmbFlat.Text.Trim(); } }

        public int Address_code { get { return _Address_code; }
            set { _Address_code = value; } }

        String GetFullAddress()
        {
            String res = "";
            if (comboBox4.SelectedValue != null)
            {
                try
                {
                    string sql;
                    SqlCommand cmd;
                    sql = "SELECT cmodb.GetFullAddress(@kladr,@house,@corp,@str,@flat);";
                    cmd = new SqlCommand(sql, DB.conn);
                    cmd.Parameters.AddWithValue("kladr", comboBox4.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("house", cmbHouse.Text);
                    cmd.Parameters.AddWithValue("corp", cmbCorp.Text);
                    cmd.Parameters.AddWithValue("str", cmbStroenie.Text);
                    cmd.Parameters.AddWithValue("flat", cmbFlat.Text);
                    res = cmd.ExecuteScalar().ToString();
                    /*
                    if (cmbHouse.Text != "")
                        res += ", д. " + cmbHouse.Text;
                    if (cmbCorp.Text != "")
                        res += ", корп. " + cmbCorp.Text;
                    if (cmbStroenie.Text != "")
                        res += ", стр. " + cmbStroenie.Text;
                    if (cmbFlat.Text != "")
                        res += ", кв. " + cmbFlat.Text;*/
                    return res;
                }
                catch { }
            }

            return res;
        }

        private void fFindAddress_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndexChanged -= this.comboBox1_SelectedIndexChanged;
            this.comboBox3.SelectedIndexChanged -= this.comboBox3_SelectedIndexChanged;
            this.comboBox4.SelectedIndexChanged -= this.comboBox4_SelectedIndexChanged;


            comboBox1.DataSource = DB.QueryTableMultipleParams("SELECT name+' '+ socr AS name, code, status  FROM kladr.kladr where code like '__00000000000' order by name ;", null);
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "code";
            comboBox1.Text = "";

            // LoadComplete = true;
            textBox1.Text = "";

            this.comboBox1.SelectedIndexChanged += this.comboBox1_SelectedIndexChanged;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*загрузим города и н п */
            try
            {
                string region_code = comboBox1.SelectedValue.ToString().Substring(0, 2);
                if (region_code != "77")
                {
                    this.comboBox3.SelectedIndexChanged -= this.comboBox3_SelectedIndexChanged;
                    this.comboBox4.SelectedIndexChanged -= this.comboBox4_SelectedIndexChanged;

                    string sql = "SELECT kl.name + ' ' + kl.socr + '., '||klrn.name as name, kl.code  , klrn.name as rn" +
                    " FROM kladr.kladr kl " +
                    " LEFT JOIN (SELECT name + ' ' + socr as name, code FROM kladr.kladr where code like (@param1+'___00000000')) klrn on rpad(substring(kl.code, 1, 5), 13, '0') = klrn.code " +
                    " where kl.code like (@param1+'_________00') and kl.code <> (@param1+'00000000000') and not(kl.code LIKE (@param1+'___00000000'))";


                    comboBox3.DataSource = DB.QueryTableMultipleParams(sql, new List<object> { region_code });
                    comboBox3.DisplayMember = "name";
                    comboBox3.ValueMember = "code";

                    /*чистим поля*/
                    comboBox3.SelectedIndex = -1;
                    comboBox4.SelectedIndex = -1;
                    cmbHouse.Text = "";
                    cmbCorp.Text = "";
                    cmbStroenie.Text = "";
                    cmbFlat.Text = "";
                    textBox1.Clear();
                    textBox2.Clear();


                    this.comboBox3.SelectedIndexChanged += this.comboBox3_SelectedIndexChanged;
                    this.comboBox4.SelectedIndexChanged += this.comboBox4_SelectedIndexChanged;
                }
                else
                {
                    this.comboBox4.SelectedIndexChanged -= this.comboBox4_SelectedIndexChanged;
                    comboBox4.DataSource = DB.QueryTableMultipleParams("SELECT name+' '+ socr AS name, code  FROM kladr.street where code like (@param1+'%')  order by name ;", new List<object> { comboBox1.SelectedValue.ToString().Substring(0, 11) });
                    comboBox4.DisplayMember = "name";
                    comboBox4.ValueMember = "code";

                    /*чистим поля*/
                    comboBox3.DataSource = null;
                    comboBox4.SelectedIndex = -1;
                    cmbHouse.Text = "";
                    cmbCorp.Text = "";
                    cmbStroenie.Text = "";
                    cmbFlat.Text = "";
                    textBox1.Clear();
                    textBox2.Clear();

                    this.comboBox4.SelectedIndexChanged += this.comboBox4_SelectedIndexChanged;
                }

            }
            catch { }
        }



        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                this.comboBox4.SelectedIndexChanged -= this.comboBox4_SelectedIndexChanged;
                comboBox4.DataSource = DB.QueryTableMultipleParams("SELECT name+' '+ socr AS name, code  FROM kladr.street where code like (@param1+'%')  order by name ;", new List<object> { comboBox3.SelectedValue.ToString().Substring(0, 11) });
                comboBox4.DisplayMember = "name";
                comboBox4.ValueMember = "code";

                /*чистим поля*/
                comboBox4.Text = "";
                cmbHouse.Text = "";
                cmbCorp.Text = "";
                cmbStroenie.Text = "";
                cmbFlat.Text = "";
                textBox1.Clear();
                textBox2.Clear();

                this.comboBox4.SelectedIndexChanged += this.comboBox4_SelectedIndexChanged;

            }
            catch { }
        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*код КЛАДР до улицы*/
            /*чистим поля*/
            cmbHouse.Text = "";
            cmbCorp.Text = "";
            cmbStroenie.Text = "";
            cmbFlat.Text = "";
            textBox1.Clear();
            textBox2.Clear();


            textBox1.Text = GetFullAddress();
            textBox2.Text = comboBox4.SelectedValue.ToString();

        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {

        }

        private void comboBox5_TextChanged(object sender, EventArgs e)
        {
            if (cmbHouse.Text != "")
                textBox1.Text = GetFullAddress();
        }

        private void cmbCorp_TextChanged(object sender, EventArgs e)
        {
            if (cmbCorp.Text != "")
                textBox1.Text = GetFullAddress();
        }

        private void cmbStroenie_TextChanged(object sender, EventArgs e)
        {
            if (cmbStroenie.Text != "")
                textBox1.Text = GetFullAddress();
        }

        private void cmbFlat_TextChanged(object sender, EventArgs e)
        {
            if (cmbFlat.Text != "")
                textBox1.Text = GetFullAddress();
        }

        private void btnSaveNewAddr_Click(object sender, EventArgs e)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            try
            {
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                string sql = "SELECT [cmodb].[AddressCalc](@kladr_code,@house,@corp,@stroenie,@flat,@fulladdress) ; ";
                cmd = new SqlCommand(sql, DB.conn, transaction);
                //cmd.Transaction = transaction;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("kladr_code", textBox2.Text);
                cmd.Parameters.AddWithValue("house", cmbHouse.Text);
                cmd.Parameters.AddWithValue("corp", cmbCorp.Text);
                cmd.Parameters.AddWithValue("stroenie", cmbStroenie.Text);
                cmd.Parameters.AddWithValue("flat", cmbFlat.Text);
                cmd.Parameters.AddWithValue("fulladdress", textBox1.Text);
                _Address_code = Convert.ToInt32(cmd.ExecuteScalar());

                transaction.Commit();
                MessageBox.Show("Адрес успешно добавлен", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Ошибка добавления нового адреса: \n" + msg.Message);
                MessageBox.Show("Ошибка добавления нового адреса: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
