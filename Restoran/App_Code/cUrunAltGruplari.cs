using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restoran.App_Code
{
    class cUrunAltGruplari
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
        private string _urunAltGrupAdi;
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

        public string UrunAltGrupAdi
        {
            get
            {
                return _urunAltGrupAdi;
            }

            set
            {
                _urunAltGrupAdi = value;
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

        public DataTable UrunAltGruplariniGetir()
        {
            dt.Clear();
            SqlDataAdapter da = new SqlDataAdapter("Select ID,UrunAltGrupAdi,Aktif from UrunAltGruplari", conn);
            da.Fill(dt);
            return dt;
        }
        //public void UrunAltGruplariniDoldur(ComboBox cb)
        //{
        //    cb.Items.Clear();
        //    SqlCommand comm = new SqlCommand("Select ID,UrunAltGrupAdi,Aktif from UrunAltGruplari", conn);
        //    if (conn.State == ConnectionState.Closed)
        //    {
        //        try
        //        {
        //            conn.Open();
        //            SqlDataReader dr = comm.ExecuteReader();
        //            if (dr.HasRows)
        //            {
        //                while (dr.Read())
        //                {
        //                    cUrunAltGruplari cUAG = new cUrunAltGruplari();
        //                    cUAG._id = Convert.ToInt32(dr[0]);
        //                    cUAG._urunAltGrupAdi = dr[1].ToString();                            
        //                    cb.Items.Add(cUAG);
        //                }
        //            }
        //        }
        //        catch (SqlException ex)
        //        {

        //            string gata = ex.Message;
        //        }
        //        finally
        //        {

        //            conn.Close();
        //        }

        //    }
        //}
        public void UrunAltGruplariniDoldur(ComboBox cb)
        {
            cb.Items.Clear();
            SqlCommand comm = new SqlCommand("Select ID,UrunAltGrupAdi,Aktif from UrunAltGruplari", conn);
            if (conn.State == ConnectionState.Closed) conn.Open();

            try
            {

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //cUrunAltGruplari cUAG = new cUrunAltGruplari();
                        //cUAG._id = Convert.ToInt32(dr[0]);
                        //cUAG._urunAltGrupAdi = dr[1].ToString();
                        //cb.Items.Add(cUAG);
                        cb.Items.Add(dr[1].ToString());
                    }
                }
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


        internal int UrunAltGrupIDGetir(string UrunAltGrup)
        {
            SqlCommand comm = new SqlCommand("select ID from UrunAltGruplari where UrunAltGrupAdi=@UrunAltGrupAdi", conn);
            comm.Parameters.Add("@UrunAltGrupAdi", SqlDbType.VarChar).Value = UrunAltGrup;
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlDataReader dr;
            int urunaltgrup = 0;
            try
            {
                dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    urunaltgrup = Convert.ToInt32(dr[0]);
                }
            }
            catch (SqlException ex)
            {

                string hata = ex.Message;
            }
            finally { conn.Close(); }
            return urunaltgrup;
        }
        public override string ToString()
        {
            return _urunAltGrupAdi;
        }

        #endregion


    }
}
