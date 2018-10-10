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
    public partial class fFilterColumnEdit : Form
    {
        public fFilterColumnEdit()
        {
            InitializeComponent();
        }

        private void fFilterColumnEdit_Load(object sender, EventArgs e)
        {
            /*заполняем названия фильтров*/
            if (DB.conn.State == ConnectionState.Open)
            {
                try
                {
                    foreach (DataRow row in DB.QueryTableMultipleParams("SELECT filtername FROM cmodb.filters  order by filter_order ASC;",null).Rows)
                        cmbAllFilter.Items.Add(row.ItemArray[0].ToString());

                    /*Все колонки*/
                    dgAllColumn.DataSource = DB.QueryTableMultipleParams("SELECT name,id FROM cmodb.mflt ORDER BY column_order;", null);
                    dgAllColumn.Columns["id"].Visible = false;
                    dgAllColumn.Columns["name"].HeaderText = "Наименование";


                }
                catch (Exception msg)
                {
                    Console.WriteLine(msg.ToString());
                }
            }
        }

        private void cmbAllFilter_SelectedValueChanged(object sender, EventArgs e)
        {

            /*выбрали фильтр*/
            dgMyColumn.Rows.Clear();
            dgMyColumn.Columns.Clear();
            dgMyColumn.Refresh();  

            if (DB.conn.State == ConnectionState.Open)
            {
                try
                {
                    string sql;
                    NpgsqlCommand cmd;

                    sql = "SELECT code FROM cmodb.filters where filtername=:_filtername;";
                    cmd = new NpgsqlCommand(sql, DB.conn);
                    cmd.Parameters.AddWithValue("_filtername", cmbAllFilter.Text);
                    int filter_code = Convert.ToInt32(cmd.ExecuteScalar());

                    /*колонки пользователя*/
                    sql = "SELECT name,column_id FROM cmodb.user_filter_column fil "+
                    " LEFT JOIN cmodb.mflt col on col.id = fil.column_id"+
                    " where fil.filter_id =:param1 AND fil.user_name =:param2 ORDER BY fil.column_ord; ";

                     dgMyColumn.Columns.Add("name", "Наименование");
                    dgMyColumn.Columns.Add("column_id", "column_id");                    
                    foreach (DataRow row in DB.QueryTableMultipleParams(sql, new List<object> { filter_code, pref.USER }).Rows)
                        dgMyColumn.Rows.Add(row.ItemArray[0].ToString(), row.ItemArray[1].ToString());
                    dgMyColumn.Columns["column_id"].Visible = false;
  
                }
                catch (Exception msg)
                {
                    MessageBox.Show(msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(msg.ToString());
                }
            }
         
        }

        private void dgAllColumn_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void dgAllColumn_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgAllColumn.SelectedRows.Count != 0 && cmbAllFilter.Text!="")
            {

                foreach (DataGridViewRow row in dgMyColumn.Rows)
                {
                    if (row.Cells["name"].Value.ToString().Equals(dgAllColumn[0,e.RowIndex].Value))
                    {
                        
                        return;
                    }

                }
                dgMyColumn.Rows.Add(dgAllColumn[0, e.RowIndex].Value, dgAllColumn[1, e.RowIndex].Value);


            }
        }

        private void dgMyColumn_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgMyColumn.SelectedRows.Count != 0)
            {
                dgMyColumn.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            /*Удаляем тек колонки пользователя*/
            if (DB.conn.State == ConnectionState.Open)
            {

                NpgsqlTransaction transaction = null;
                NpgsqlCommand cmd;
                try
                {
                    
                    transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);

                    string sql;

                    sql = "SELECT code FROM cmodb.filters where filtername=:_filtername;";
                    cmd = new NpgsqlCommand(sql, DB.conn);
                    cmd.Parameters.AddWithValue("_filtername", cmbAllFilter.Text);
                    int filter_code = Convert.ToInt32(cmd.ExecuteScalar());

                    sql = "DELETE FROM cmodb.user_filter_column where user_name=:user_name AND filter_id=:filter_id;";
                    cmd = new NpgsqlCommand(sql, DB.conn);
                    cmd.Transaction = transaction;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("user_name", pref.USER);
                    cmd.Parameters.AddWithValue("filter_id", filter_code);
                    cmd.ExecuteNonQuery();


                    string expr = "";
                    int i = 1;
                    foreach (DataGridViewRow row in dgMyColumn.Rows)
                    {
                        if (i == 1)
                            expr += "SELECT :filter_id,:user_name," + row.Cells["column_id"].Value.ToString()+","+i.ToString();
                        else
                            expr += " UNION SELECT :filter_id,:user_name," + row.Cells["column_id"].Value.ToString() + "," + i.ToString();
                        i++;
                    }
                    sql = "INSERT INTO cmodb.user_filter_column(filter_id,user_name,column_id,column_ord) "+ expr+";";
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("filter_id", filter_code);
                    cmd.Parameters.AddWithValue("user_name", pref.USER);
                    cmd.ExecuteNonQuery();


                    sql = "SELECT cmodb.\"GenerateUserFilter\"(:user_name, :filter_id);";
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("filter_id", filter_code);
                    cmd.Parameters.AddWithValue("user_name", pref.USER);
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Изменения успешно сохранены", "Ура", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception msg)
                {
                    if (transaction != null) transaction.Rollback();
                    Console.WriteLine("Ошибка добавления колонок: \n" + msg.Message);
                    MessageBox.Show("Ошибка добавления колонок: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            /*добавляем новый список*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int offset = -1;
            DataGridViewRow row = dgMyColumn.SelectedRows[0];
            if (row.Index == 0 && offset == -1 || ((row.Index == dgMyColumn.NewRowIndex - 1) && offset == 1 || row.Index == dgMyColumn.NewRowIndex))
                 return; // Ничего делать не надо => выходим
            // Получаем текущий индекс строки
            int currentIndex = row.Index;
            // Удаляем ее из коллекции
            dgMyColumn.Rows.Remove(row);
            // А теперь добавляем со смещением
            dgMyColumn.Rows.Insert(currentIndex + offset, row);
            dgMyColumn.Rows[currentIndex - 1].Selected = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            int offset = 1;
            DataGridViewRow row = dgMyColumn.SelectedRows[0];
            if (row.Index == 0 && offset == -1 || ((row.Index == dgMyColumn.NewRowIndex - 1) && offset == 1 || row.Index == dgMyColumn.Rows.Count-1))
                 return; // Ничего делать не надо => выходим
            // Получаем текущий индекс строки
            int currentIndex = row.Index;
            // Удаляем ее из коллекции
            dgMyColumn.Rows.Remove(row);
            // А теперь добавляем со смещением
            dgMyColumn.Rows.Insert(currentIndex + offset, row);
            dgMyColumn.Rows[currentIndex + 1].Selected = true;
            
        }
    }
}
