using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pref;
using Npgsql.Logging;

namespace Mig
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Logger.InitLogger();//инициализация - требуется один раз в начале
           // log4net.Util.LogLog.InternalDebugging = true;
            Logger.Log.Info("Запуск приложения");           
           
            Application.Run(new fFilter());

           // DB.Close();

            Logger.Log.Info("Выход");
        }
        
    }
}
