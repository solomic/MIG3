using Mig.Tables;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mig.Func;

namespace Mig.Entity
{
    class Contact
    {
        /*Поля таблицы*/
        int _id;
        string _lastname;
        string _firstname;

        /*данные в виде строки*/
        DataRow rw;
        /*доп. калк. поля*/
        string _ErrorMessage;

        public int id
        {
            get
            {
                return _id;
            }
            
        }
        public string lastname
        {
            get
            {
                return _lastname;
            }
            set
            {
                _lastname = value;
            }
        }
        public string firstname
        {
            get
            {
                return _firstname;
            }
            set
            {
                _firstname = value;
            }
        }

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        public Contact()
        {
            rw = AllTables.t_Contact.NewRow();
        }
        public string ReadFromDB(int ContactId)
        {
            
            string sql = "SELECT id,last_name FROM cmo.contact"; 
            DBFunc.MyResultRow rw = new DBFunc.MyResultRow();
            rw = DBFunc.GetRow(sql,new List<object> { ContactId });
            return "";
        }
        public DataRow GetContactDataRow()
        {
            RefreshDS();
            return rw;
        }
        public string RefreshDS()
        {
            rw["id"] = _id;
            rw["last_name"] = _lastname;
            rw["first_name"] = _firstname;
            /*... все поля*/
            return "0";
        }
        public string Save()
        {
            /*собрать Update*/

            return "";
        }
    }
}
