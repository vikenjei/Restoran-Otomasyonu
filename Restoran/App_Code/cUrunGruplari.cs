using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restoran.App_Code;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Restoran.App_Code
{
    class cUrunGruplari
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
        private string _urunGrupAdi;
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

        public string UrunGrupAdi
        {
            get
            {
                return _urunGrupAdi;
            }

            set
            {
                _urunGrupAdi = value;
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
        public DataTable UrunGruplariniGetir()
        {
            dt.Clear();
            SqlDataAdapter da = new SqlDataAdapter("Select ID,UrunGrupAdi,Aktif from UrunGruplari", conn);
            da.Fill(dt);
            return dt;
        }
        //public void UrunGruplariniDoldur(ComboBox cb)
        //{
        //    cb.Items.Clear();
        //    SqlCommand comm = new SqlCommand("Select ID,UrunGrupAdi,Aktif from UrunGruplari", conn);
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
        //                    cUrunGruplari cUG = new cUrunGruplari();
        //                    cUG._id = Convert.ToInt32(dr[0]);
        //                    cUG._urunGrupAdi = dr[1].ToString();
        //                    cb.Items.Add(cUG);
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
        public void UrunGruplariniDoldur(ComboBox cb)
        {
            cb.Items.Clear();
            SqlCommand comm = new SqlCommand("Select ID,UrunGrupAdi,Aktif from UrunGruplari", conn);
            if (conn.State == ConnectionState.Closed) conn.Open();
            {
                try
                {

                    SqlDataReader dr = comm.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //cUrunGruplari cUG = new cUrunGruplari();
                            //cUG._id = Convert.ToInt32(dr[0]);
                            //cUG._urunGrupAdi = dr[1].ToString();
                            //cb.Items.Add(cUG);
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
        }
        internal int UrunGrupIDGetir(string UrunGrup)
        {
            SqlCommand comm = new SqlCommand("select ID from UrunGruplari where UrunGrupAdi=@UrunGrupAdi", conn);
            comm.Parameters.Add("@UrunGrupAdi", SqlDbType.VarChar).Value = UrunGrup;
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlDataReader dr;
            int urungrup = 0;
            try
            {
                dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    urungrup = Convert.ToInt32(dr[0]);
                }
            }
            catch (SqlException ex)
            {

                string hata = ex.Message;
            }
            finally { conn.Close(); }
            return urungrup;
        }


        public override string ToString()
        {
            return _urunGrupAdi;
        }
        #endregion

    }
}
