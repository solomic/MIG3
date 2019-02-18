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
using System.Data.SqlClient;

namespace Mig
{
    public partial class fStage : Form
    {
        string Action = "";
        int StageId = 0;
        public fStage()
        {
            InitializeComponent();
        }
        public fStage(string method,int id)
        {
            Action = method;
            StageId = id;
            InitializeComponent();
        }

        private void fStage_Load(object sender, EventArgs e)
        {
            if (Action == "Edit")
            {
                DataTable StageTable = DB.QueryTableMultipleParams(pref.GetStageEditSql, new List<object> { pref.CONTACTID, StageId });
                if (StageTable.Rows.Count != 0)
                {
                    tStage.Text = StageTable.Rows[0]["stage"].ToString();
                    tDueDt.SelectedDate = StageTable.Rows[0]["due_dt"] == DBNull.Value ? null : Convert.ToDateTime(StageTable.Rows[0]["due_dt"]).ToString("dd.MM.yyyy");
                    tPayDt.SelectedDate = StageTable.Rows[0]["pay_dt"] == DBNull.Value ? null : Convert.ToDateTime(StageTable.Rows[0]["pay_dt"]).ToString("dd.MM.yyyy");
                    tReceipt.CheckState = (StageTable.Rows[0]["receipt"].ToString() == "да" ? CheckState.Checked : CheckState.Unchecked);
                    tAmount.Text = StageTable.Rows[0]["amount"].ToString();
                }
            }
          
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            try
            {
                int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn);
               
                if (Action == "Add")
                {
                    sql = "INSERT INTO cmodb.stage(contact_id, stage, due_dt, amount, receipt, pay_dt, status) "+
                        " VALUES(:contact_id, :stage, :due_dt, :amount, :receipt, :pay_dt, :status); ";
                }
                else
                {
                    sql = "UPDATE cmodb.stage SET " +
                       " stage=:stage, due_dt=:due_dt, amount=:amount, receipt=:receipt, pay_dt=:pay_dt, status=:status  " +
                       "  WHERE contact_id=:contact_id and status=:status and id=:id; ";

                }
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("contact_id", Contact_id);
                cmd.Parameters.AddWithValue("stage", tStage.Text);
                if (Action=="Edit")
                    cmd.Parameters.AddWithValue("id", StageId);
                cmd.Parameters.AddWithValue("amount", Convert.ToDouble(tAmount.Text.Replace('.','.')));
                cmd.Parameters.AddWithValue("receipt", tReceipt.Checked==true? "Y":"N");
                if (tDueDt.SelectedDate == "")
                    cmd.Parameters.AddWithValue("due_dt", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("due_dt", Convert.ToDateTime(tDueDt.SelectedDate));
                if (tPayDt.SelectedDate == "")
                    cmd.Parameters.AddWithValue("pay_dt", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("pay_dt", Convert.ToDateTime(tPayDt.SelectedDate));
               
                cmd.Parameters.AddWithValue("status", "Y");

                cmd.ExecuteNonQuery();
                transaction.Commit();

                MessageBox.Show("Успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Ошибка: \n" + msg.Message);
                MessageBox.Show("Ошибка: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
