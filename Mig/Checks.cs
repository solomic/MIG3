using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mig
{
    static class Checks
    {
       
        public static string CheckDULFields(string method,string cmbDulType, string tDulSer, string tDulNum,string tDulIssue,string tDulValidity)
        {
            string Res = "";
            string msg = "";
            //проверяем что заполнено хотя бы одно поле
            if (method == "add")
            {
                if (cmbDulType != "" || tDulSer != "" || tDulNum != "" || tDulIssue != "" || tDulValidity != "")
                {
                    method = "update";
                }
                else
                    msg = "not add";
            }
            if (method == "update")
            {
                /*обязательные поля*/
                if (cmbDulType == "")
                    Res += "Тип документа\n";
                if (tDulNum.Trim() == "")
                    Res += "Номер\n";
                if (tDulIssue == "")
                    Res += "Дата выдачи\n";
                //if (tDulValidity == "")
                //    Res += "Срок действия\n";

                if (Res != "")
                    msg = "Паспорт - заполните обязательные поля:\n" + Res;
                /*end обязательные поля*/

                Res = "";
                if (cmbDulType == "")
                {
                    Res += "Укажите <Тип документа>!\n";
                }

                /*   дата выдачи не может быть больше текущей даты*/               
                if (tDulIssue != "")
                {
                    DateTime d = Convert.ToDateTime(tDulIssue);
                    if (d > DateTime.Now)
                    {
                        Res += "<Дата выдачи> не может быть больше текущей даты!\n";
                    }
                }
                else
                {
                    Res += "<Дата выдачи> обязательно для заполнения!\n";
                }


                if (Res != "")
                    msg += "\n" + Res;
            }

            return msg;
        }

       
    }
}
