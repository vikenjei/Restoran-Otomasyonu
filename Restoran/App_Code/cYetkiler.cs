using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.App_Code
{
    class cYetkiler
    {
        #region Objects
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        #endregion
        #region Members

        private int _id;
        private string _yetkiAdi;
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

        public string YetkiAdi
        {
            get
            {
                return _yetkiAdi;
            }

            set
            {
                _yetkiAdi = value;
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
        #region Sql
        SqlConnection conn = new SqlConnection(cMain.connStr);
        #endregion
        #region Metods
        public DataTable YetkileriGetir()
        {
            dt.Clear();
            SqlDataAdapter da = new SqlDataAdapter("Select ID,YetkiAdi,Aktif from Yetkiler", conn);
            da.Fill(dt);
            return dt;
        }
        public string YetkiGetirByYetkiID(int YetkiID)
        {
            string YetkiAdi = "";
            SqlCommand comm = new SqlCommand("Select YetkiAdi from Yetkiler where ID = @YetkiID", conn);
            comm.Parameters.Add("@YetkiID", SqlDbType.Int).Value = YetkiID;
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                    YetkiAdi = comm.ExecuteScalar().ToString();
                }
                catch (SqlException ex)
                {
                    string s = ex.Message;

                }
                finally
                {
                    conn.Close();
                }
            }
            return YetkiAdi; ;
        }

        public bool YetkiKaydet(string YetkiAdi, string AktifMi)
        {
            bool Active = true;
            bool Basarili = false;
            if (AktifMi != "Evet")
            {
                Active = false;
            }
            SqlCommand comm = new SqlCommand("Insert into Yetkiler (YetkiAdi, Aktif) values (@YetkiAdi, @Aktif)", conn);
            comm.Parameters.Add("@YetkiAdi", SqlDbType.VarChar).Value = YetkiAdi;
            comm.Parameters.Add("@Aktif", SqlDbType.Bit).Value = Active;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                Basarili = Convert.ToBoolean(comm.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {

                string hata = ex.Message;
            }
            finally { conn.Close(); }
            return Basarili;
        }
        internal bool YetkiKontrol(string YetkiAdi, string ID)
        {
            bool Varmi = false;
            SqlCommand comm = new SqlCommand("select count(*) from Yetkiler where YetkiAdi=@YetkiAdi and ID != @ID", conn);
            comm.Parameters.Add("@YetkiAdi", SqlDbType.VarChar).Value = YetkiAdi;
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(ID);
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                int Sayisi = Convert.ToInt32(comm.ExecuteScalar());
                if (Sayisi > 0)
                    Varmi = true;
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally { conn.Close(); }
            return Varmi;
        }
        internal bool YetkiKontrol(string YetkiAdi)
        {
            bool Varmi = false;
            SqlCommand comm = new SqlCommand("select count(*) from Yetkiler where YetkiAdi=@YetkiAdi", conn);
            comm.Parameters.Add("@YetkiAdi", SqlDbType.VarChar).Value = YetkiAdi;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                int Sayisi = Convert.ToInt32(comm.ExecuteScalar());
                if (Sayisi > 0)
                    Varmi = true;
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally { conn.Close(); }
            return Varmi;
        }
        internal bool YetkiDegistir(cYetkiler y)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Update Yetkiler Set YetkiAdi=@YetkiAdi,Aktif = @Aktif where ID=@ID", conn);
            comm.Parameters.Add("@YetkiAdi", SqlDbType.VarChar).Value = y._yetkiAdi;
            comm.Parameters.Add("@Aktif", SqlDbType.Bit).Value = y._aktif;
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = y._id;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                Sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally { conn.Close(); }
            return Sonuc;
        }
        internal bool YetkiSil(int ID)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Delete From Yetkiler where ID=@ID", conn);
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                Sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally { conn.Close(); }
            return Sonuc;
        }
        internal int YetkiIDGetir(string YetkiAdi)
        {
            SqlCommand comm = new SqlCommand("Select ID from Yetkiler where YetkiAdi = @YetkiAdi", conn);
            comm.Parameters.Add("@YetkiAdi", SqlDbType.VarChar).Value = YetkiAdi;
            if (conn.State == ConnectionState.Closed) conn.Open();
            int ID = 0;
            try
            {
                ID = Convert.ToInt32(comm.ExecuteScalar());
            }
            catch (SqlException ex)
            {

                string hata = ex.Message;
            }
            finally { conn.Close(); }
            return ID;

        }
        #endregion  
    }
}
