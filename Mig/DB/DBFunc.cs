using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mig.Func
{
    static class DBFunc
    {
        static string connStr = "server=localhost;user=root;database=cmo;port=3306;password=123456;";
        public class MyResultRow
        {           
            public DataRow ResultRow;           
            public string ErrorText;           
            public bool HasError;
        }
        static public MyResultRow GetRow(string sql, List<object> param)
        {
            MyResultRow rw = new MyResultRow();
            MySqlConnection connection = new MySqlConnection(connStr);
            MySqlCommand sqlCom = new MySqlCommand(sql, connection);
            try {
                connection.Open();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sqlCom);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                rw.ResultRow = dt.Rows[0];
                connection.Close();
            }
            catch(Exception ex)
            {
                rw.HasError = true;
                rw.ErrorText = ex.ToString();
            }
            finally
            {
                connection.Close();
            }
            return rw;
        }
    }
}
