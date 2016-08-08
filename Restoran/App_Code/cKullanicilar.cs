using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.App_Code
{
    class cKullanicilar
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
        private int _yetkiID;
        private string _adi;
        private string _soyadi;
        private string _sifre;
        private DateTime _kayitTarihi;
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

        public int YetkiID
        {
            get
            {
                return _yetkiID;
            }

            set
            {
                _yetkiID = value;
            }
        }

        public string Adi
        {
            get
            {
                return _adi;
            }

            set
            {
                _adi = value;
            }
        }

        public string Soyadi
        {
            get
            {
                return _soyadi;
            }

            set
            {
                _soyadi = value;
            }
        }

        public string Sifre
        {
            get
            {
                return _sifre;
            }

            set
            {
                _sifre = value;
            }
        }

        public DateTime KayitTarihi
        {
            get
            {
                return _kayitTarihi;
            }

            set
            {
                _kayitTarihi = value;
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


        public DataTable KullanicilariGetir()
        {
            dt.Clear();
            SqlDataAdapter da = new SqlDataAdapter("Select Kullanicilar.ID,Kullanicilar.YetkiID,YetkiAdi,Adi,Soyadi,Sifre,KayitTarihi,Kullanicilar.Aktif from Kullanicilar inner join Yetkiler on Kullanicilar.YetkiID=Yetkiler.ID", conn);
            da.Fill(dt);
            return dt;
        }
        public bool KullaniciBulBySifre(cKullanicilar cK)
        {
            bool sonuc = false;
            SqlCommand comm = new SqlCommand("Select Kullanicilar.ID,Kullanicilar.YetkiID,YetkiAdi,Adi,Soyadi,Sifre,KayitTarihi from Kullanicilar inner join Yetkiler on Kullanicilar.YetkiID = Yetkiler.ID where Kullanicilar.Aktif= 1 and Sifre =@Sifre", conn);
            comm.Parameters.Add("@Sifre", SqlDbType.VarChar).Value = cK._sifre;
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                    //kullanici adını yakala
                    //kullanici no yu yakala.
                    SqlDataReader dr = comm.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            cK._id = Convert.ToInt32(dr[0]);
                            cK._yetkiID = Convert.ToInt32(dr[1]);
                            cK._adi = dr[3].ToString();
                            cK._soyadi = dr[4].ToString();
                            sonuc = true;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    string a = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
            return sonuc;
        }

        internal bool KullaniciKontrol(string Adi, string Soyadi, string Yetki, string ID)
        {
            bool Varmi = false;
            SqlCommand comm = new SqlCommand("select count(*) from Kullanicilar where Adi=@Adi and Soyadi=@Soyadi and YetkiID=@YetkiID and ID != @ID", conn);
            comm.Parameters.Add("@Adi", SqlDbType.VarChar).Value = Adi;
            comm.Parameters.Add("@Soyadi", SqlDbType.VarChar).Value = Soyadi;
            comm.Parameters.Add("@YetkiID", SqlDbType.Int).Value = Convert.ToInt32(Yetki);
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

        internal bool KullaniciKaydet(cKullanicilar k)
        {
            bool Basarili = false;
            SqlCommand comm = new SqlCommand("Insert into Kullanicilar (YetkiID,Adi,Soyadi,Sifre,KayitTarihi,Aktif) values (@YetkiID,@Adi,@Soyadi,@Sifre,@KayitTarihi,@Aktif)", conn);
            comm.Parameters.Add("@YetkiID", SqlDbType.Int).Value = _yetkiID;
            comm.Parameters.Add("@Adi", SqlDbType.VarChar).Value = _adi;
            comm.Parameters.Add("@Soyadi", SqlDbType.VarChar).Value = _soyadi;
            comm.Parameters.Add("@Sifre", SqlDbType.VarChar).Value = _sifre;
            comm.Parameters.Add("@KayitTarihi", SqlDbType.DateTime).Value = _kayitTarihi;
            comm.Parameters.Add("@Aktif", SqlDbType.Bit).Value = _aktif;
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

        internal bool KullaniciDegistir(cKullanicilar k)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Update Kullanicilar Set YetkiID=@YetkiID,Adi=@Adi,Soyadi=@Soyadi,Sifre=@Sifre,KayitTarihi=@KayitTarihi,Aktif=@Aktif where ID=@ID", conn);
            comm.Parameters.Add("@YetkiID", SqlDbType.Int).Value = _yetkiID;
            comm.Parameters.Add("@Adi", SqlDbType.VarChar).Value = _adi;
            comm.Parameters.Add("@Soyadi", SqlDbType.VarChar).Value = _soyadi;
            comm.Parameters.Add("@Sifre", SqlDbType.VarChar).Value = _sifre;
            comm.Parameters.Add("@KayitTarihi", SqlDbType.DateTime).Value = _kayitTarihi;
            comm.Parameters.Add("@Aktif", SqlDbType.Bit).Value = _aktif;
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = _id;
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

        internal bool KullaniciSil(int KullaniciID)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Delete From Kullanicilar where ID=@ID", conn);
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = KullaniciID;
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

        internal bool KullaniciKontrol(string Ad, string Soyad, string Yetki)
        {
            bool Varmi = false;
            SqlCommand comm = new SqlCommand("select count(*) from Kullanicilar inner join Yetkiler on Yetkiler.ID = Kullanicilar.YetkiID where Adi=@Adi and Soyadi=@Soyadi and YetkiAdi=@YetkiAdi", conn);
            comm.Parameters.Add("@Adi", SqlDbType.VarChar).Value = Adi;
            comm.Parameters.Add("@Soyadi", SqlDbType.VarChar).Value = Soyadi;
            comm.Parameters.Add("@YetkiAdi", SqlDbType.VarChar).Value = Yetki;
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





        #endregion
    }
}
