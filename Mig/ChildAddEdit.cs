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
    public partial class fChildAddEdit : Form
    {
        string Action = "";
        int ChildId = 0;
        public fChildAddEdit()
        {
            InitializeComponent();
        }
        public fChildAddEdit(string method, int id)
        {
            Action = method;
            ChildId = id;
            InitializeComponent();
        }
        
        private void fChildAddEdit_Load(object sender, EventArgs e)
        {
            if (Action == "Edit")
            {
                DataTable ChildTable = DB.QueryTableMultipleParams(pref.GetChildEditSql, new List<object> { ChildId });
                if (ChildTable.Rows.Count != 0)
                {
                    tFIO.Text = ChildTable.Rows[0]["fio"].ToString();
                    tBirthday.SelectedDate = ChildTable.Rows[0]["birthday"] == DBNull.Value ? null : Convert.ToDateTime(ChildTable.Rows[0]["birthday"]).ToString("dd.MM.yyyy");                
                    tAddress.Text = ChildTable.Rows[0]["address"].ToString();
                    tNat.Text = ChildTable.Rows[0]["nationality"].ToString();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            NpgsqlTransaction transaction = null;
            NpgsqlCommand cmd;
            string sql = "";
            try
            {
                int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new NpgsqlCommand(sql, DB.conn);

                if (Action == "Add")
                {
                    sql = "INSERT INTO cmodb.children(contact_id, fio, birthday, address, nationality) VALUES (:contact_id, :fio, :birthday, :address, :nationality);";
                }
                else
                {
                    sql = "UPDATE cmodb.children SET " +
                       " contact_id=:contact_id, fio=:fio, birthday=:birthday, address=:address, nationality=:nationality " +
                       "  WHERE contact_id=:contact_id and id=:id; ";

                }
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("contact_id", Contact_id);
                cmd.Parameters.AddWithValue("fio", tFIO.Text);
                if (Action == "Edit")
                    cmd.Parameters.AddWithValue("id", ChildId);               
                cmd.Parameters.AddWithValue("address", tAddress.Text);
                cmd.Parameters.AddWithValue("nationality", tNat.Text);
                if (tBirthday.SelectedDate == "")
                    cmd.Parameters.AddWithValue("birthday", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("birthday", Convert.ToDateTime(tBirthday.SelectedDate));

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
