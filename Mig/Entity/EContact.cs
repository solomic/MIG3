using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mig.Entity
{
    class EContact
    {
        int pId;
        string sId = "[id]";

        int pContact_Id;
        string sContact_Id = "[contact_id]";
        //===================================================================



        //===================================================================
        public EContact()
        {

        }
        //===================================================================
        public byte getContactInfo()
        {
            byte res = 1;

            return res;
        }

        string getContactSQLSelect()
        {
            string res="";

            return res;
        }

        string getContactSQLUpdate()
        {
            string res = "";

            return res;
        }

        string getContactSQLInsert()
        {
            string res = "";

            return res;
        }

        public int Id { get => pId;/* set => pId = value;*/ }
        public int Contact_Id { get => pContact_Id; /*set => pContact_Id = value;*/ }
        
    }
}
