using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.App_Code
{
    class cSalon
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
        private string _salonAdi;
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

        public string SalonAdi
        {
            get
            {
                return _salonAdi;
            }

            set
            {
                _salonAdi = value;
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
        public DataTable SalonlariGetir()
        {
            dt.Clear();
            SqlDataAdapter da = new SqlDataAdapter("Select ID,SalonAdi,Aktif from Salonlar", conn);
            da.Fill(dt);
            return dt;
        }
        public int salonIDGetir(string salonAdi)
        {
            int id = 0;
            SqlCommand comm = new SqlCommand("Select ID from Salonlar Where SalonAdi = @SalonAdi", conn);
            comm.Parameters.Add("@SalonAdi", SqlDbType.VarChar).Value = salonAdi;
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                    id = Convert.ToInt32(comm.ExecuteScalar());

                }
                catch (SqlException ex)
                {
                    string gata = ex.Message;

                }
                finally
                {
                    conn.Close();
                }
            }


            return id;
        }

        #endregion
    }
}
