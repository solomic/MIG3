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
        public static string ClassName = "Class: DB.cs\n";
        static public NpgsqlConnection conn;
       
        static public DataTable QueryTableMultipleParams(string comm, List<object> param)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlCommand cmd;
            try
            {
                cmd = new NpgsqlCommand(comm, DB.conn);
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
                ds.Reset();
                da.Fill(ds);
                dt= ds.Tables[0];
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ClassName + "Function:QueryTableMultipleParams\n Error:" + ex.Message);
                throw new Exception(ex.Message);
            }
            
            return dt;

        }
        
        static public string GetTableValue(string comm, List<object> param)
        {
            string StrRes="";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlCommand cmd;
            try
            {
                cmd = new NpgsqlCommand(comm, DB.conn);
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

                StrRes = Convert.ToString(cmd.ExecuteScalar());
            }
            
            catch(Exception ex)
            {
                Logger.Log.Error(ClassName + "Function:GetTableValue\n Error:" + ex.Message);
                throw new Exception(ex.Message);
            }
            
            return StrRes;

        }
        static public int GetTableValueInt(string comm, List<object> param)
        {
            int StrRes ;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlCommand cmd;
            try
            {

                cmd = new NpgsqlCommand(comm, DB.conn);
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

                StrRes = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ClassName + "Function:GetTableValueInt\n Error:" + ex.Message);
                throw new Exception(ex.Message);
            }

            return StrRes;

        }
        static public DateTime? GetTableValueDt(string comm, List<object> param)
        {
            DateTime? StrRes=null;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlCommand cmd;
            try
            {
                cmd = new NpgsqlCommand(comm, DB.conn);
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

                string str = cmd.ExecuteScalar().ToString();
                if (str != "")
                    StrRes = Convert.ToDateTime(str);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ClassName + "Function:GetTableValueDt\n Error:" + ex.Message);
                throw new Exception(ex.Message);
            }

            return StrRes;

        }

        static public void Open(string pUser,string pPassword,string pHost, string pPort,string pDatabase)
        {
            try
            {
                string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", pHost, pPort, pUser, pPassword, pDatabase);
                conn = new NpgsqlConnection(connstring);
                conn.Open();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ClassName + "Function:Open\n Error:" + ex.Message);
                throw new Exception(ex.Message);
            }

        }
        static public void Close()
        {
            try
            {
                conn.Close();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ClassName + "Function:Close\n Error:" + ex.Message);
                throw new Exception(ex.Message);
              
            }
        }
    }
}
