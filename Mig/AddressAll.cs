using Npgsql;
using NpgsqlTypes;
using Pref;
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

namespace Mig
{
    public partial class fAddressAll : Form
    {
        public fAddressAll()
        {
            InitializeComponent();
        }

        private void fAddressAll_Load(object sender, EventArgs e)
        {
            LoadAllAddr();
        }

        private void LoadAllAddr()
        {
            string sql = "";
            
           
            sql = "select code as \"Код\",full_address as \"Адрес\",kladr_code as \"Код КЛАДР\",case when ad.pin='1' then 'x' else '' end as pin from cmodb.address ad ORDER BY ad.pin desc, full_address asc";
            bindingSource1.DataSource = DB.QueryTableMultipleParams(sql, null);
            dgAddrAll.DataSource = bindingSource1;
        }

        private void dgAddrAll_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (dgAddrAll.SelectedCells.Count == 0)
            {
                return;
            }           
            string sql = "select c.contact_id,c.contact_id as \"Код\",fio as \"ФИО\",nationality as \"Гражданство\",birthday as \"Дата рождения\",cmodb.lookupvalue('CON.TYPE',c.type) as \"Статус студента\", CASE WHEN ai.status='Y' then 'Текущий адрес' else 'Архив' end as \"Статус адреса\" from cmodb.contact c " +
            "left join cmodb.addr_inter ai on c.contact_id = ai.contact_id "+
            "left join cmodb.address addr on addr.code = ai.address_code "+
            "where  addr.code =@param1 "+
             "  order by c.last_name ASC";
            dgAddrStud.DataSource = DB.QueryTableMultipleParams(sql, new List<object> { dgAddrAll.CurrentRow.Cells["Код"].Value });
            dgAddrStud.Columns["contact_id"].Visible = false;

        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try {
                string msg = "";
                fSelectAddress fSelectAddressForm = new fSelectAddress();
                if (fSelectAddressForm.ShowDialog(this) == DialogResult.OK)
                {
                    int AddrCodeCur = Convert.ToInt32(dgAddrAll.CurrentRow.Cells["Код"].Value);
                    string AddrCur =dgAddrAll.CurrentRow.Cells["Адрес"].Value.ToString();
                    int AddrCodeNew = fSelectAddressForm.SelectedAddressCode;
                    string AddrNew = fSelectAddressForm.SelectedFullAddress;

                    
                    int[] AllRows = getAllRowsId(dgAddrStud);

                    if (AllRows.Count() != 0)
                    {
                        string conf = "Адрес\n" + AddrCur + "\n будет заменен у всех студентов на\n" + AddrNew + "\nПродолжить?";

                        if (MessageBox.Show(conf,"Предупреждение",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                        { 
                            msg = DeSelect(AddrCodeCur, AddrCodeNew, AllRows);
                            if (msg != "")
                                MessageBox.Show("Ошибка: \n" + msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                            {
                                MessageBox.Show("Адрес назначен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            fAddressAll_Load(this, null);
                         }
                    }
                }
            }
            catch(Exception err)
            {

            }
        }
        private string DeSelect(int oldcode,int newcode, int[] ids)
        {
            string mes = "";
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            try
            {

                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn);
                sql = "UPDATE cmodb.addr_inter "+
                    "SET  address_code =:newcode "+
                  " WHERE contact_id = ANY(:contact_id) AND address_code =:oldcode; ";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                NpgsqlParameter arpar = new NpgsqlParameter();
                arpar.ParameterName = "contact_id";
                arpar.NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Integer;
                arpar.Value = ids;
                cmd.Parameters.Add(arpar);                           
                cmd.Parameters.AddWithValue("oldcode", oldcode);
                cmd.Parameters.AddWithValue("newcode", newcode);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                mes = msg.Message;
                //Logger.Log.Error(ClassName + "Function:SetContactType\n Error:" + msg);
            }
            return mes;
        }
        public string SetAddrPin(int addrcode,char pin)
        {
            string mes = "";
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            try
            {

                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn);
                sql = "UPDATE cmodb.address " +
                    "SET  pin =:pin " +
                  " WHERE code = :code; ";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
               
                cmd.Parameters.AddWithValue("pin", pin);
                cmd.Parameters.AddWithValue("code", addrcode);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                mes = msg.Message;
                //Logger.Log.Error(ClassName + "Function:SetContactType\n Error:" + msg);
            }
            return mes;
        }

        public string DeleteAddr(int addrcode)
        {
            string mes = "";
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            try
            {
                sql = " select count(1) from cmodb.contact c " +
                       " left join cmodb.addr_inter ai on c.contact_id = ai.contact_id " +
                       " left join cmodb.address addr on addr.code = ai.address_code " +
                       " where  addr.code =@param1 ";
                int cnt = DB.GetTableValueInt(sql, new List<object> { dgAddrAll.CurrentRow.Cells["Код"].Value });
                if (cnt == 0)
                {
                    transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                    cmd = new SqlCommand(sql, DB.conn);
                    sql = "DELETE FROM cmodb.address " +
                         " WHERE code = :code; ";
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("code", addrcode);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                else
                {
                    mes = "Для удаления адреса необходимо перепривязать всех студентов!";
                }
            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                mes = msg.Message;
                //Logger.Log.Error(ClassName + "Function:SetContactType\n Error:" + msg);
            }
            return mes;
        }
        public int[] getAllRowsId(DataGridView gv)
        {
            int[] AllCells = new int[gv.Rows.Count];

            for (int i = 0; i < gv.Rows.Count; i++)
            {
                AllCells[i] = gv.Rows[i].Index;
            }
            int[] DistRows = new int[AllCells.Distinct<int>().Count()];
            int a = 0;
            foreach (var m in AllCells.Distinct<int>())
            {
                DistRows[a++] = m;
            }
            int[] iId = new int[DistRows.Count()];
            if (DistRows.Count() > 0)
            {

                for (int i = 0; i < DistRows.Count(); i++)
                {
                    iId[i] = Convert.ToInt32(gv.Rows[DistRows[i]].Cells["contact_id"].Value);
                }
            }


            return iId;
        }

        private void закрепитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgAddrAll.SelectedCells.Count == 0)
            {
                return;
            }
            string msg = SetAddrPin(Convert.ToInt32( dgAddrAll.CurrentRow.Cells["Код"].Value), '1');
            if (msg != "")
                MessageBox.Show("Ошибка: \n" + msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                LoadAllAddr();
                MessageBox.Show("Адрес в топчике!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void открепитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgAddrAll.SelectedCells.Count == 0)
            {
                return;
            }
            string msg = SetAddrPin(Convert.ToInt32(dgAddrAll.CurrentRow.Cells["Код"].Value), '0');
            if (msg != "")
                MessageBox.Show("Ошибка: \n" + msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                LoadAllAddr();
                MessageBox.Show("Адрес откреплен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dgAddrAll.SelectedCells.Count == 0)
            {
                return;
            }
            string msg = DeleteAddr(Convert.ToInt32(dgAddrAll.CurrentRow.Cells["Код"].Value));
            if (msg != "")
                MessageBox.Show("Ошибка: \n" + msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                LoadAllAddr();
                MessageBox.Show("Адрес успешно удален!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bindingSource1.Filter = "Адрес LIKE '%" + textBox1.Text + "%'";
            }
            catch (Exception err)
            {
               // Logger.Log.Error(ClassName + "Function:textBox1_TextChanged\n Error:" + err);
            }
        }
    }
}
