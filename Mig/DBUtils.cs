using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUS
{
    class DBUtils
    {
        /// <summary>
        /// Методы реализующие выполнение запросов с возвращением одного параметра либо без параметров вовсе.
        /// </summary>
        public class MySqlExecute
        {
            public class MyResult
        {
            /// <summary>
            /// Возвращает результат запроса.
            /// </summary>
            public string ResultText;
            /// <summary>
            /// Возвращает True - если произошла ошибка.
            /// </summary>
            public string ErrorText;
            /// <summary>
            /// Возвращает текст ошибки.
            /// </summary>
            public bool HasError;
        }

        /// <summary>
        /// Для выполнения запросов к MySQL с возвращением 1 параметра.
        /// </summary>
        /// <param name="sql">Текст запроса к базе данных</param>
        /// <param name="connection">Строка подключения к базе данных</param>
        /// <returns>Возвращает значение при успешном выполнении запроса, текст ошибки - при ошибке.</returns>
        public static MyResult SqlScalar(string sql, List<object> param, string connection)
        {
            MyResult result = new MyResult();
            NpgsqlConnection conn = new NpgsqlConnection(connection);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            try
            {
                
                cmd.Parameters.Clear();               
                int i = 1;
                foreach (object prm in param)
                {
                    cmd.Parameters.AddWithValue("param" + i.ToString(), prm);
                    i++;
                }
                
                conn.Open();                
                try
                {
                    result.ResultText = cmd.ExecuteScalar().ToString();
                    result.HasError = false;
                }
                catch (Exception ex)
                {
                    result.ErrorText = ex.Message;
                    result.HasError = true;
                }
               
            }
            catch (Exception ex)//Этот эксепшн на случай отсутствия соединения с сервером.
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
            /// <summary>
            /// Для выполнения запросов к MySQL без возвращения параметров.
            /// </summary>
            /// <param name="sql">Текст запроса к базе данных</param>
            /// <param name="connection">Строка подключения к базе данных</param>
            /// <returns>Возвращает True - ошибка или False - выполнено успешно.</returns>
            public static MyResult SqlNoneQuery(string sql, List<object> param, string connection)
            {
                MyResult result = new MyResult();
                NpgsqlConnection conn = new NpgsqlConnection(connection);
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                try
                {                    
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
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        result.HasError = false;
                    }
                    catch (Exception ex)
                    {
                        result.ErrorText = ex.Message;
                        result.HasError = true;
                    }
                   
                }
                catch (Exception ex)//Этот эксепшн на случай отсутствия соединения с сервером.
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
        /// <summary>
        /// Методы реализующие выполнение запросов с возвращением набора данных.
        /// </summary>
        public class MySqlExecuteData
        {
            /// <summary>
            /// Возвращаемый набор данных.
            /// </summary>
            public class MyResultData
            {
                /// <summary>
                /// Возвращает результат запроса.
                /// </summary>
                public DataTable ResultData;
                /// <summary>
                /// Возвращает True - если произошла ошибка.
                /// </summary>
                public string ErrorText;
                /// <summary>
                /// Возвращает текст ошибки.
                /// </summary>
                public bool HasError;
            }


            /// <summary>
            /// Выполняет запрос выборки набора строк.
            /// </summary>
            /// <param name="sql">Текст запроса к базе данных</param>
            /// <param name="connection">Строка подключения к базе данных</param>
            /// <returns>Возвращает набор строк в DataSet.</returns>
            public static MyResultData SqlReturnDataset(string sql,List<object> param, string connection)
            {
                MyResultData result = new MyResultData();
                NpgsqlConnection conn = new NpgsqlConnection(connection);
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                try
                {
                    
                    cmd.Parameters.Clear();
                   
                    int i = 1;

                    foreach (object prm in param)
                    {
                        cmd.Parameters.AddWithValue("param" + i.ToString(), prm);
                        i++;
                    }
                   
                    conn.Open();

                    try
                    {
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter();
                        da.SelectCommand = cmd;
                        DataSet ds1 = new DataSet();
                        da.Fill(ds1);
                        result.ResultData = ds1.Tables[0];
                    }
                    catch (Exception ex)
                    {
                        result.HasError = true;
                        result.ErrorText = ex.Message;
                    }
                   
                }
                catch (Exception ex)//Этот эксепшн на случай отсутствия соединения с сервером.
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
}
