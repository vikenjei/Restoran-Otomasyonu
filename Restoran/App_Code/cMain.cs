using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.App_Code
{
    class cMain
    {
        #region Objects

        public static bool isLogin = false;
        public static string fromWhere = string.Empty;        
        #endregion
        public static string connStr = "Data Source=MONSTER\\MSSQL2012; Initial Catalog=Reston; uid=sa; pwd=12345";
        public static string masaNo = "";
        public static int girenKullaniciID;
        public static int salonID;
        public static int tiklananAdisyon;
    }
}
