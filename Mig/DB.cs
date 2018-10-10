using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Pref;
using System.Data;
using System.Windows.Forms;
using Npgsql.Logging;


namespace Mig
{
    static class DB
    {      
        
        public class SqlResultDS
        {
            public DataTable ResultData;
            public string ErrorText="";               
            public bool HasError=false;
        }
        public class SqlResult
        {
            public dynamic ResultData;
            public string ErrorText = "";
            public bool HasError = false;
        }

        static public SqlResultDS QueryTableMultipleParams(string constr, string comm, List<object> param)
        {
            NpgsqlConnection conn = new NpgsqlConnection(constr);
            SqlResultDS result = new SqlResultDS();
            DataSet ds = new DataSet();
            NpgsqlCommand cmd = new NpgsqlCommand(comm, conn);
            try {
                    conn.Open();
                    cmd.Parameters.Clear();
                    if (param != null)
                    {
                        int i = 1;

                        foreach (object prm in param)
                        {
                            cmd.Parameters.AddWithValue("param" + i.ToString(), prm);
                            i++;
                        }
                    }
                    ds.Reset();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    result.ResultData = ds.Tables[0];
            }
            catch(Exception ex)
            {
                result.ErrorText = ex.Message;
                result.HasError = true;
            }
            finally
            {
                conn.Close();
            }

            return result;   

        }
        
        static public SqlResult GetTableValue(string constr,string comm, List<object> param,string type)
        {
            SqlResult result = new SqlResult();
            NpgsqlConnection conn = new NpgsqlConnection(constr);
            NpgsqlCommand cmd = new NpgsqlCommand(comm, conn);
            try {
                conn.Open();
                cmd.Parameters.Clear();
                if (param != null)
                {
                    int i = 1;

                    foreach (object prm in param)
                    {
                        cmd.Parameters.AddWithValue("param" + i.ToString(), prm);
                        i++;
                    }
                }

                switch (type)
                {
                    case "string":
                        result.ResultData = Convert.ToString(cmd.ExecuteScalar());
                        break;
                    case "int":
                        result.ResultData = Convert.ToInt32(cmd.ExecuteScalar());
                        break;
                    case "DateTime?":
                        string str = cmd.ExecuteScalar().ToString();
                        if (str != "")
                            result.ResultData = Convert.ToDateTime(str);                        
                        break;
                       
                }

            }
            catch (Exception ex)
            {
                result.ErrorText = ex.Message;
                result.HasError = true;
            }
            finally
            {
                conn.Close();
            }
            return result;

        }
        static public SqlResult SqlNoneQuery(string constr, string comm, List<object> param)
        {
            SqlResult result = new SqlResult();
            NpgsqlConnection conn = new NpgsqlConnection(constr);
            NpgsqlCommand cmd = new NpgsqlCommand(comm, conn);
            try
            {
                conn.Open();
                cmd.Parameters.Clear();
                if (param != null)
                {
                    int i = 1;

                    foreach (object prm in param)
                    {
                        cmd.Parameters.AddWithValue("param" + i.ToString(), prm);
                        i++;
                    }
                }

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                result.ErrorText = ex.Message;
                result.HasError = true;
            }
            finally
            {
                conn.Close();
            }
            return result;

        }




    }
}
