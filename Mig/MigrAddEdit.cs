using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pref;
using Npgsql;

namespace Mig
{
    public partial class fMigrAddEdit : Form
    {
        public string ClassName = "Class: fMigrAddEdit.cs\n";
        string Action = "";
        public fMigrAddEdit()
        {
            InitializeComponent();
        }
        public fMigrAddEdit(string method)
        {
            Action = method;
            InitializeComponent();
        }
        public void set90day()
        {
            if (tMigEntryDt.SelectedDate!="")
            {

                l90d.Text = "+ 90 д. = "+Convert.ToDateTime(tMigEntryDt.SelectedDate).AddDays(90).ToString("dd.MM.yyyy");
            }
            else
            {
                l90d.Text = "+ 90 д. = ";
            }
        }
        public string EntryClear()
        {
            string res = "";
            NpgsqlTransaction transaction = null;
            NpgsqlCommand cmd;
            string sql = "";
            try
            {
                int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new NpgsqlCommand(sql, DB.conn);               
                sql = "UPDATE cmodb.entry SET " +
                   " status='N',updated=now(),updated_by=CURRENT_USER " +
                   "  WHERE contact_id=:contact_id and status=:status; ";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("contact_id", Contact_id);                
                cmd.Parameters.AddWithValue("status", "Y");
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                res = msg.Message;
            }
            return res;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!tMigEntryDt.ValidateDate() || !tMigTenureFrom.ValidateDate() || !tMigTenureTo.ValidateDate())
                return;

            NpgsqlTransaction transaction = null;
            NpgsqlCommand cmd;
            string sql="";
            try
            {
                int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new NpgsqlCommand(sql, DB.conn);
                if (Action == "Add" || Action == "Extend")
                {
                    sql = "UPDATE cmodb.migr_card SET status='N', updated=now(),updated_by=CURRENT_USER where contact_id=:contact_id and status='Y';";
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("contact_id", Contact_id);
                    cmd.ExecuteNonQuery();

                    /*чистим въезд выезд*/
                    sql = "UPDATE cmodb.entry SET status='N', updated=now(),updated_by=CURRENT_USER where contact_id=:contact_id and status='Y';";
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("contact_id", Contact_id);
                    cmd.ExecuteNonQuery();
                }
                if (Action == "Add" || Action == "Extend")
                {
                    sql = "INSERT INTO cmodb.migr_card( " +
                    " contact_id, ser, num, kpp_code, entry_dt, tenure_from_dt,  " +
                    " tenure_to_dt, status, purpose_entry,updated,updated_by) " +
                     " VALUES(:contact_id, :ser, :num, :kpp_code, :entry_dt, :tenure_from_dt, " +
                    " :tenure_to_dt, :status, :purpose_entry,now(),CURRENT_USER); ";
                }
                else
                {
                    sql = "UPDATE cmodb.migr_card SET " +
                   " ser=:ser, num=:num, kpp_code=:kpp_code, entry_dt=:entry_dt, tenure_from_dt=:tenure_from_dt,  " +
                   " tenure_to_dt=:tenure_to_dt, purpose_entry=:purpose_entry,updated=now(),updated_by=CURRENT_USER " +
                   "  WHERE contact_id=:contact_id and status=:status; ";

                }
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("contact_id", Contact_id);
                cmd.Parameters.AddWithValue("ser", tMigrSer.Text);
                cmd.Parameters.AddWithValue("num", tMigrNum.Text);
                cmd.Parameters.AddWithValue("kpp_code", cmbKPP.Text/* SelectedValue.ToString()*/);               
                cmd.Parameters.AddWithValue("purpose_entry", tMigrPurpose.Text);               
                if (tMigEntryDt.SelectedDate == "")
                    cmd.Parameters.AddWithValue("entry_dt", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("entry_dt", Convert.ToDateTime(tMigEntryDt.SelectedDate));
                if (tMigTenureFrom.SelectedDate == "")
                    cmd.Parameters.AddWithValue("tenure_from_dt", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("tenure_from_dt", Convert.ToDateTime(tMigTenureFrom.SelectedDate));
                if (tMigTenureTo.SelectedDate == "")
                    cmd.Parameters.AddWithValue("tenure_to_dt", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("tenure_to_dt", Convert.ToDateTime(tMigTenureTo.SelectedDate));
                cmd.Parameters.AddWithValue("status", "Y");

                cmd.ExecuteNonQuery();


                string err = "";
                if (tMigTenureTo.SelectedDate != "")
                {
                   
                    DateTime Tenure = Convert.ToDateTime(tMigTenureTo.SelectedDate);
                    DateTime? DulDt = DB.GetTableValueDt("select validity from cmodb.dul where contact_id=:param1 and status='Y'", new List<object> { pref.CONTACTID });
                    DateTime? AgreeDt = DB.GetTableValueDt("select to_dt from cmodb.agree where contact_id=:param1 and status='Y'", new List<object> { pref.CONTACTID });

                    if(DulDt!=null)
                    {
                        if(Tenure> DulDt)
                        {
                            err = "'Срок пребывания до' больше срока действия паспорта!\n";
                        }
                    }
                    if(AgreeDt != null)
                    {
                        if (Tenure > AgreeDt)
                        {
                            err += "'Срок пребывания до' больше срока действия договора!\n";
                        }
                    }

                   

                }
                if (err != "")
                {
                    err += "Продолжить?";
                    if (MessageBox.Show(err, "Информация", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        transaction.Rollback();
                    }
                    else
                    {
                        transaction.Commit();
                        MessageBox.Show("Добавлено успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    transaction.Commit();
                    MessageBox.Show("Добавлено успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }



            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Logger.Log.Error(ClassName + "Function:btnSave_Click\n Error:" + msg.Message);
                MessageBox.Show("Ошибка при добавлении: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        

    private void fMigrAddEdit_Load(object sender, EventArgs e)
        {
            try {
                /*миграционка*/
                cmbKPP.DataSource = DB.QueryTableMultipleParams("SELECT  distinct kpp_code FROM cmodb.migr_card where kpp_code is not null and kpp_code <>'' order by kpp_code", null);
                cmbKPP.SelectedIndex = -1;
                tMigrPurpose.Text = "УЧЕБА";


                if (Action == "Edit" || Action == "Extend")
                {

                    DataTable MigrCardActiveTable = DB.QueryTableMultipleParams(pref.GetMigrCardActiveSql, new List<object> { pref.CONTACTID });
                    /*миграционка*/
                    if (MigrCardActiveTable.Rows.Count != 0)
                    {
                        if (Action == "Extend")
                        {
                            tMigTenureTo.SelectedDate = null;
                        }
                        else
                        {
                            tMigTenureTo.SelectedDate = MigrCardActiveTable.Rows[0]["tenure_to_dt"] == DBNull.Value ? null : Convert.ToDateTime(MigrCardActiveTable.Rows[0]["tenure_to_dt"]).ToString("dd.MM.yyyy");
                        }
                        tMigEntryDt.SelectedDate = MigrCardActiveTable.Rows[0]["entry_dt"] == DBNull.Value ? null : Convert.ToDateTime(MigrCardActiveTable.Rows[0]["entry_dt"]).ToString("dd.MM.yyyy");
                        tMigTenureFrom.SelectedDate = MigrCardActiveTable.Rows[0]["tenure_from_dt"] == DBNull.Value ? null : Convert.ToDateTime(MigrCardActiveTable.Rows[0]["tenure_from_dt"]).ToString("dd.MM.yyyy");
                        tMigrSer.Text = MigrCardActiveTable.Rows[0]["ser"].ToString();
                        tMigrNum.Text = MigrCardActiveTable.Rows[0]["num"].ToString();
                        cmbKPP.Text = MigrCardActiveTable.Rows[0]["kpp_code"].ToString();
                        tMigrPurpose.Text = MigrCardActiveTable.Rows[0]["purpose_entry"].ToString();

                    }
                }
                //+90 дней
                set90day();

            }
            catch (Exception msg)
            {
                Logger.Log.Error(ClassName + "Function:fMigrAddEdit_Load\n Error:" + msg.Message);
                MessageBox.Show(msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tMigEntryDt_Enter(object sender, EventArgs e)
        {
            
        }

      

        private void tMigEntryDt_Validated(object sender, EventArgs e)
        {
            set90day();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
        }

        private void tMigEntryDt_EnabledChanged(object sender, EventArgs e)
        {
            
        }
    }
}
