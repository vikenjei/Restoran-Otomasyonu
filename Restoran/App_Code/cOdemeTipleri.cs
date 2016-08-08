using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.App_Code
{
    class cOdemeTipleri
    {
        #region Objects
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        #endregion
        #region Sql
        SqlConnection conn = new SqlConnection(cMain.connStr);
        #endregion
        #region Members
        private int _id;
        private string _odemeTipi;
        private bool _aktif;


        #endregion
        #region Properties
        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public string OdemeTipi
        {
            get
            {
                return _odemeTipi;
            }

            set
            {
                _odemeTipi = value;
            }
        }

        public bool Aktif
        {
            get
            {
                return _aktif;
            }

            set
            {
                _aktif = value;
            }
        }
        #endregion
        #region Metods
        public DataTable OdemeTipleriniGetir()
        {
            dt.Clear();
            SqlDataAdapter da = new SqlDataAdapter("Select ID,OdemeTipi,Aktif from OdemeTipleri", conn);
            da.Fill(dt);
            return dt;
        }


        #endregion 
    }
}
