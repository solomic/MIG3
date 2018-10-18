using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Pref;

namespace Mig.Entity
{
    class Address
    {
        int         _Id;
        string      _FullAddress;
        string      _Code;
        string      _KladrCode;
        DateTime    _Created;
        string      _CreatedBy;
        DateTime    _Updated;
        string      _UpdatedBy;
        string      _Obl;
        string      _Rayon;
        string      _Town;
        string      _Street;
        string      _House;
        string      _Corp;
        string      _Stroenie;
        string      _Flat;
        string      _SocrObl;
        string      _SocrRayon;
        string      _SocrTown;
        string      _SocrStreet;
        string      _Status;
        string      _pin;
        bool        _Deleted;
        bool        _Valid;
        bool        _Change;
        /*----------------------------------------------------*/
        DataRow rw;
        /*----------------------------------------------------*/

        DataColumn cl;

        public int Id  {
            get
            {
                return _Id;
            }
        }
        public string FullAddress
        {
            get
            {
                return _FullAddress;
            }
            set
            {
                _FullAddress = value;
            }
        }
        /*----------------------------------------------------*/
        public Address()
        {
            
            rw = new DataTable().NewRow();
            
            //cl.DataType = System.Type.GetType("System.Int32");
            //cl.ColumnName = "id";
        }
        /*----------------------------------------------------*/
        public string Read()
        {
            string res = "";
            string sql = "select ai.id, CASE WHEN ai.status='Y' THEN 'Да' ELSE '' END as \"Основной\",full_address as \"Адрес регистрации\" from cmodb.address ad " +
             " left join cmodb.addr_inter ai on ai.address_code = ad.code " +
             " where ai.contact_id =:param1 AND ai.deleted='N' " +
              " order by ai.status desc nulls last, ai.created DESC";
            DataTable tmp = DB.QueryTableMultipleParams(sql, new List<object> { pref.CONTACTID });
            _Id = Convert.ToInt32( tmp.Rows[0]["id"] );

            return res;
        }
        public void Save()
        {

        }
        public string Validate()
        {
            string res = "";

            return res;
        }
    }
}
