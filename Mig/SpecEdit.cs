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
    public partial class fSpecEdit : Form
    {
        public fSpecEdit()
        {
            InitializeComponent();
        }

        public int GetStudFacCnt(int code)
        {
            string sql;
            SqlCommand cmd;
            sql = "SELECT COUNT(*) FROM cmodb.teach_info where  status='Y' and deleted='N' and facult_code=@facult_code;";
            cmd = new SqlCommand(sql, DB.conn);
            cmd.Parameters.AddWithValue("facult_code", code);
            return Convert.ToInt32(cmd.ExecuteScalar());
            
        }
        public int GetStudSpecCnt(string code)
        {
            string sql;
            SqlCommand cmd;

            sql = "SELECT COUNT(*) FROM cmodb.teach_info where  status='Y' and deleted='N' and spec_code=@spec_code;";
            cmd = new SqlCommand(sql, DB.conn);
            cmd.Parameters.AddWithValue("spec_code", code);
            return Convert.ToInt32(cmd.ExecuteScalar());

        }

        private void fSpecEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void fSpecEdit_Load(object sender, EventArgs e)
        {
            /*загружаем факультеты*/
           
                try
                {                
                    dataGridView1.DataSource = DB.QueryTableMultipleParams("SELECT  id, code, short_name, name FROM cmodb.facultet where status='Y' and deleted='N';", null);
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[2].HeaderText = "Краткое наименование";
                    dataGridView1.Columns[3].HeaderText = "Наименование";
                    dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

               // dataGridView1_MouseClick(sender, null);

                }
                catch (Exception msg)
                {
                // Console.WriteLine();
                Logger.Log.Error(msg.ToString());
                }
            
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            /*загрузка специальностей*/
            if (DB.conn.State == ConnectionState.Open)
            {
                try
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    SqlCommand cmd;
                    string sql = "SELECT code, name, cmodb.LookupValue('PTEACH',prog_teach_code) prog_teach_code, id FROM cmodb.speciality where par_code=@code and status='Y' and deleted='N';";                    
                    cmd = new SqlCommand(sql, DB.conn);
                    cmd.Parameters.Clear();                    
                    cmd.Parameters.AddWithValue("code", dataGridView1.CurrentRow.Cells["code"].Value);
                    ds.Reset();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    ds.Reset();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    dataGridView2.DataSource = dt;
                    dataGridView2.Columns[3].Visible = false;
                    dataGridView2.Columns[0].HeaderText = "Шифр специальности";
                    dataGridView2.Columns[1].HeaderText = "Наименование";
                    dataGridView2.Columns[2].HeaderText = "Программа обучения";
                    dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    ds.Dispose();
                    dt.Dispose();

                }
                catch (Exception msg)
                {
                    // Console.WriteLine("Ошибка во время загрузки специальностей: \n"+msg.Message);
                    Logger.Log.Error(msg.ToString());
                    MessageBox.Show("Ошибка во время загрузки специальностей" , "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            fFacAddEdit fFacAddEditForm = new fFacAddEdit();
            fFacAddEditForm.FacShortName = "";
            fFacAddEditForm.FacName = "";
            if (fFacAddEditForm.ShowDialog(this) == DialogResult.OK)
            {
                string facshortname = fFacAddEditForm.FacShortName;
                string facname = fFacAddEditForm.FacName;
                /*добавляем факультет*/
                if (DB.conn.State == ConnectionState.Open)
                {

                    SqlTransaction transaction = null;
                    SqlCommand cmd;
                    try
                    {
                        transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                        string sql = "INSERT INTO cmodb.facultet(code, name, short_name, status) VALUES (NEXT VALUE FOR [cmodb].[FacCode], @name, @shortname, 'Y');";
                        cmd = new SqlCommand(sql, DB.conn, transaction);
                       // cmd.Transaction = transaction;//???????
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("name", facname);
                        cmd.Parameters.AddWithValue("shortname", facshortname);
                        cmd.ExecuteNonQuery();
                        
                        transaction.Commit();

                        fSpecEdit_Load(this,e);

                        

                    }
                    catch (Exception msg)
                    {
                        if (transaction != null) transaction.Rollback();
                        //Console.WriteLine("Ошибка добавления факультета: \n" + msg.Message);
                        Logger.Log.Error(msg.ToString());
                        MessageBox.Show("Ошибка добавления факультета" , "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }
            fFacAddEditForm.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*удаляем факультет*/
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Запись не выбрана!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Действительно хотите удалить выбранный факультет и все дочерние специальности?", "Удаление", MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {

                if (DB.conn.State == ConnectionState.Open)
                {

                    SqlTransaction transaction = null;
                    SqlCommand cmd;
                    try
                    {
                        transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);

                        string sql;
                        sql = "UPDATE cmodb.facultet SET status='N',deleted='N' where id=@id;";
                        cmd = new SqlCommand(sql, DB.conn, transaction);
                       // cmd.Transaction = transaction;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("id", dataGridView1.CurrentRow.Cells["id"].Value); //берем id
                        cmd.ExecuteNonQuery();
                                                

                        sql = "UPDATE cmodb.speciality SET status='N',deleted='N' where par_code=@code;";
                        cmd.CommandText = sql;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("code", dataGridView1.CurrentRow.Cells["code"].Value); //берем code
                        cmd.ExecuteNonQuery();
                        
                        transaction.Commit();

                        dataGridView1.Rows.Remove(dataGridView1.CurrentRow);

                        dataGridView1_MouseClick(sender,null);
                    }
                    catch (Exception msg)
                    {
                        if (transaction != null) transaction.Rollback();
                        //Console.WriteLine("Ошибка при удалении факультета: \n" + msg.Message);
                        Logger.Log.Error(msg.ToString());
                        MessageBox.Show("Ошибка при удалении факультета", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*редактировать факультет*/
            if (dataGridView1.SelectedRows.Count == 0)
                return;

          //  int cnt = GetStudFacCnt(dataGridView1.CurrentRow.Cells["code"].Value.ToString());

            fFacAddEdit fFacAddEditForm = new fFacAddEdit();
            fFacAddEditForm.FacShortName = dataGridView1.CurrentRow.Cells["short_name"].Value.ToString();
            fFacAddEditForm.FacName = dataGridView1.CurrentRow.Cells["name"].Value.ToString();
            if (fFacAddEditForm.ShowDialog(this) == DialogResult.OK)
            {
                string facshortname = fFacAddEditForm.FacShortName;
                string facname = fFacAddEditForm.FacName;

                SqlTransaction transaction = null;
                SqlCommand cmd;
                try
                {
                    transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                    string sql = "UPDATE cmodb.facultet SET name=@name,short_name = @short_name where id=@id;";
                    cmd = new SqlCommand(sql, DB.conn, transaction);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("name", facname);
                    cmd.Parameters.AddWithValue("short_name", facshortname);
                    cmd.Parameters.AddWithValue("id", dataGridView1.CurrentRow.Cells["id"].Value);
                    cmd.ExecuteNonQuery();
                    //cmd.Transaction = transaction;
                    transaction.Commit();

                    dataGridView1.CurrentRow.Cells[2].Value = facshortname;
                    dataGridView1.CurrentRow.Cells[3].Value = facname;
                }
                catch (Exception msg)
                {
                    if (transaction != null) transaction.Rollback();
                    Logger.Log.Error(msg.ToString());
                    MessageBox.Show("Ошибка при редактировании факультета" , "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            fFacAddEditForm.Dispose();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            button2_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            fSpecAddEdit fSpecAddEditForm = new fSpecAddEdit();
            fSpecAddEditForm.SpecCode = "";
            fSpecAddEditForm.SpecName = "";
            fSpecAddEditForm.FacName = dataGridView1.CurrentRow.Cells["name"].Value.ToString();
            if (fSpecAddEditForm.ShowDialog(this) == DialogResult.OK)
            {
                string SpecCode = fSpecAddEditForm.SpecCode;
                string SpecName = fSpecAddEditForm.SpecName;
                string POCode = fSpecAddEditForm.POCode;
                /*добавляем специальность*/
                if (DB.conn.State == ConnectionState.Open)
                {

                    SqlTransaction transaction = null;
                    SqlCommand cmd;
                    try
                    {
                        transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                        string sql = "INSERT INTO cmodb.speciality(code, name, status, par_code,prog_teach_code,spec_code) VALUES (@code, @name,'Y', @par_code,@prog_teach_code,NEXT VALUE FOR [cmodb].[SpecCode]);";
                        cmd = new SqlCommand(sql, DB.conn, transaction);
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("code", SpecCode);
                        cmd.Parameters.AddWithValue("name", SpecName);
                        cmd.Parameters.AddWithValue("par_code", dataGridView1.CurrentRow.Cells["code"].Value);
                        cmd.Parameters.AddWithValue("prog_teach_code", POCode);
                        cmd.ExecuteNonQuery();
                       // cmd.Transaction = transaction;
                        transaction.Commit();

                        dataGridView1_MouseClick(this, null);

                    }
                    catch (Exception msg)
                    {
                        if (transaction != null) transaction.Rollback();
                        Logger.Log.Error(msg.ToString());
                        MessageBox.Show("Ошибка при добавлении специальностиn" , "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }
            fSpecAddEditForm.Dispose();

            dataGridView1_MouseClick(this, null);
        }

        private void btnSpecEdit_Click(object sender, EventArgs e)
        {
            /*редактировать специальность*/
            if (dataGridView2.SelectedRows.Count == 0)
                return;

           // int cnt = GetStudSpecCnt(dataGridView2.CurrentRow.Cells["code"].Value.ToString());

            fSpecAddEdit fSpecAddEditForm = new fSpecAddEdit();
            fSpecAddEditForm.SpecCode = dataGridView2.CurrentRow.Cells["code"].Value.ToString();
            fSpecAddEditForm.SpecName = dataGridView2.CurrentRow.Cells["name"].Value.ToString();
            fSpecAddEditForm.FacName = dataGridView1.CurrentRow.Cells["name"].Value.ToString();
          
            fSpecAddEditForm.POCodeStr = dataGridView2.CurrentRow.Cells["prog_teach_code"].Value.ToString();
           

            
            if (fSpecAddEditForm.ShowDialog(this) == DialogResult.OK)
            {
                string SpecCode = fSpecAddEditForm.SpecCode;
                string SpecName = fSpecAddEditForm.SpecName;
                string POCode = fSpecAddEditForm.POCode;

                SqlTransaction transaction = null;
                SqlCommand cmd;
                try
                {
                    transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                    string sql = "UPDATE cmodb.speciality SET code=@code,name = @name,prog_teach_code=@prog_teach_code where id=@id;";
                    cmd = new SqlCommand(sql, DB.conn, transaction);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("code", SpecCode);
                    cmd.Parameters.AddWithValue("name", SpecName);
                    cmd.Parameters.AddWithValue("prog_teach_code", POCode);
                    cmd.Parameters.AddWithValue("id", dataGridView2.CurrentRow.Cells["id"].Value);
                    cmd.ExecuteNonQuery();
                   // cmd.Transaction = transaction;
                    transaction.Commit();

                    dataGridView2.CurrentRow.Cells["code"].Value = SpecCode;
                    dataGridView2.CurrentRow.Cells["name"].Value = SpecName;
                }
                catch (Exception msg)
                {
                    if (transaction != null) transaction.Rollback();
                    Logger.Log.Error(msg.ToString());
                    MessageBox.Show("Ошибка при редактировании специальности" , "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            fSpecAddEditForm.Dispose();

            dataGridView1_MouseClick(this, null);
        }

        private void btnSpecDel_Click(object sender, EventArgs e)
        {
            /*удаляем специальность*/
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Запись не выбрана!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);               
                return;
            }

            if (MessageBox.Show("Действительно хотите удалить выбранную специальность?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                if (DB.conn.State == ConnectionState.Open)
                {
                    SqlTransaction transaction = null;
                    SqlCommand cmd;
                    try
                    {                        
                        transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                        string sql;
                        sql = "UPDATE cmodb.speciality SET status='N' where id=@id;";                        
                        cmd = new SqlCommand(sql, DB.conn, transaction);
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("id", dataGridView2.CurrentRow.Cells["id"].Value); //берем id
                        cmd.ExecuteNonQuery();
                       // cmd.Transaction = transaction;
                        transaction.Commit();

                        dataGridView2.Rows.Remove(dataGridView2.CurrentRow);

                        dataGridView1_MouseClick(this, null);
                    }
                    catch (Exception msg)
                    {
                        if (transaction != null) transaction.Rollback();
                        Logger.Log.Error(msg.ToString());
                        MessageBox.Show("Ошибка при удалении специальности" , "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            btnSpecEdit_Click(this, null);
        }
    }
}
