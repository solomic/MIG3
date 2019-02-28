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
    public partial class fEntryAdd : Form
    {
        public string EntryState = "";
        string Action = "";
        int Id = 0;
        public fEntryAdd()
        {
            InitializeComponent();
        }
        public fEntryAdd(string Act,int id)
        {
            Action = Act;
            Id = id;
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbTarget.Text =="")
            {
                MessageBox.Show("И куда он поехал?!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (tText.Text.Trim() == "")
            {
                MessageBox.Show("Введите комментарий!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            try
            {
                int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn);

                if (Action == "ADD" && cmbTarget.Text == "зарубеж")
                {
                    sql = "UPDATE cmodb.entry SET status='N' where contact_id=@contact_id and status='Y';";
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("contact_id", Contact_id);
                    cmd.ExecuteNonQuery();

                   
                }
                cmd.Parameters.Clear();
                if (Action == "ADD")
                {
                    sql = "INSERT INTO cmodb.entry( " +
                    " contact_id, entry_dt, leave_dt, txt,status,type  )" +
                    " VALUES(@contact_id, @entry_dt, @leave_dt, @txt,@status,@type) ;";
                    cmd.Parameters.AddWithValue("contact_id", Contact_id);
                    if (cmbTarget.Text == "зарубеж")
                        cmd.Parameters.AddWithValue("status", "Y");
                    else
                        cmd.Parameters.AddWithValue("status", "N");
                }
                if (Action == "EDIT")
                {
                    sql = "UPDATE cmodb.entry SET " +
                        " entry_dt=@entry_dt, leave_dt=@leave_dt, txt=@txt,type=@type " +
                        " where id=@id ;";
                    cmd.Parameters.AddWithValue("id", Id);
                }
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("txt", tText.Text);  
                if (tInput.SelectedDate == "")
                    cmd.Parameters.AddWithValue("entry_dt", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("entry_dt", Convert.ToDateTime(tInput.SelectedDate));
                if (tOutput.SelectedDate == "")
                    cmd.Parameters.AddWithValue("leave_dt", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("leave_dt", Convert.ToDateTime(tOutput.SelectedDate));   
                
                cmd.Parameters.AddWithValue("type", cmbTarget.Text);
                cmd.ExecuteNonQuery();

                transaction.Commit();

                MessageBox.Show(Action == "ADD"?"Добавлено успешно":"Обновлено успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EntryState = cmbTarget.Text;
                this.DialogResult = DialogResult.OK;

            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();               
                MessageBox.Show("Ошибка: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fEntryAdd_Load(object sender, EventArgs e)
        {
            if(Action=="EDIT")
            {
                DataTable d = DB.QueryTableMultipleParams("SELECT leave_dt,entry_dt, txt, type FROM cmodb.entry where id=@param1;", new List<object> { Id });
                cmbTarget.Text = d.Rows[0]["type"].ToString();
                tOutput.SelectedDate = d.Rows[0]["leave_dt"].ToString();
                tInput.SelectedDate = d.Rows[0]["entry_dt"].ToString();
                tText.Text = d.Rows[0]["txt"].ToString();

                //отключаем возможность изменить тип
                cmbTarget.Enabled = false;
                
            }
            else
            {
                cmbTarget.Text ="";
                tOutput.SelectedDate = "";
                tInput.SelectedDate = "";
                tText.Text = "";
            }
        }
    }
}
