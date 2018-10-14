using Pref;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mig.Entity
{
    class ListAddress
    {
        List<Address> AddrList;
        
        public string Read(int ContactId)
        {
            string res = "";
            string sql = "select ai.id, CASE WHEN ai.status='Y' THEN 'Да' ELSE '' END as \"Основной\",full_address as \"Адрес регистрации\" from cmodb.address ad " +
             " left join cmodb.addr_inter ai on ai.address_code = ad.code " +
             " where ai.contact_id =:param1 AND ai.deleted='N' " +
              " order by ai.status desc nulls last, ai.created DESC";
            DataTable tmp = DB.QueryTableMultipleParams(sql, new List<object> { pref.CONTACTID });
           // _Id = Convert.ToInt32(tmp.Rows[0]["id"]);
            return res;
        }
        public ListAddress()
        {
            
        }
    }
}
