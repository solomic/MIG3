using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mig.Tables
{
    static public class AllTables
    {
        static public DataTable t_Contact;

        static public void Init()
        {
            t_Contact = new DataTable();
            t_Contact.Columns.Add("id", typeof(int));
            t_Contact.Columns.Add("last_name", typeof(string));
            t_Contact.Columns.Add("first_name", typeof(string));
        }
    }
}
