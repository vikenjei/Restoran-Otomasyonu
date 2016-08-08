using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.App_Code
{
    class cSatislar
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
        private int _salonID;
        private int _urunID;
        private int _kullaniciID;
        private int _odemeTipiID;
        private DateTime _tarih;
        private double _fiyat;
        private int _adet;
        private double _tutar;
        private int _kuver;
        private string _masaNo;
        private int _adisyonID;
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

        public int SalonID
        {
            get
            {
                return _salonID;
            }

            set
            {
                _salonID = value;
            }
        }

        public int UrunID
        {
            get
            {
                return _urunID;
            }

            set
            {
                _urunID = value;
            }
        }

        public int KullaniciID
        {
            get
            {
                return _kullaniciID;
            }

            set
            {
                _kullaniciID = value;
            }
        }

        public int OdemeTipiID
        {
            get
            {
                return _odemeTipiID;
            }

            set
            {
                _odemeTipiID = value;
            }
        }

        public DateTime Tarih
        {
            get
            {
                return _tarih;
            }

            set
            {
                _tarih = value;
            }
        }

        public double Fiyat
        {
            get
            {
                return _fiyat;
            }

            set
            {
                _fiyat = value;
            }
        }

        public int Adet
        {
            get
            {
                return _adet;
            }

            set
            {
                _adet = value;
            }
        }

        public double Tutar
        {
            get
            {
                return _tutar;
            }

            set
            {
                _tutar = value;
            }
        }

        public int Kuver
        {
            get
            {
                return _kuver;
            }

            set
            {
                _kuver = value;
            }
        }

        public string MasaNo
        {
            get
            {
                return _masaNo;
            }

            set
            {
                _masaNo = value;
            }
        }

        public int AdisyonID
        {
            get
            {
                return _adisyonID;
            }

            set
            {
                _adisyonID = value;
            }
        }
        #endregion

        #endregion

        #region Metods
        public bool SatisEkle(cSatislar cS)
        {
            bool sonuc = false;
            SqlCommand comm = new SqlCommand("Insert into Satislar (SalonID,UrunID,KullaniciID,OdemeTipiID,Tarih,Fiyat,Adet,Tutar,Kuver,MasaNo,AdisyonID) values (@SalonID,@UrunID,@KullaniciID,@OdemeTipiID,@Tarih,@Fiyat,@Adet,@Tutar,@Kuver,@MasaNo,@AdisyonID)", conn);
            comm.Parameters.Add("@SalonID", SqlDbType.Int).Value = cS._salonID;
            comm.Parameters.Add("@UrunID", SqlDbType.Int).Value = cS._urunID;
            comm.Parameters.Add("@KullaniciID", SqlDbType.Int).Value = cS._kullaniciID;
            comm.Parameters.Add("@OdemeTipiID", SqlDbType.Int).Value = cS._odemeTipiID;
            comm.Parameters.Add("@Tarih", SqlDbType.DateTime).Value = cS._tarih;
            comm.Parameters.Add("@Fiyat", SqlDbType.Money).Value = cS._fiyat;
            comm.Parameters.Add("@Adet", SqlDbType.Int).Value = cS._adet;
            comm.Parameters.Add("@Tutar", SqlDbType.Money).Value = cS._tutar;
            comm.Parameters.Add("@Kuver", SqlDbType.Int).Value = cS._kuver;
            comm.Parameters.Add("@MasaNo", SqlDbType.VarChar).Value = cS._masaNo;
            comm.Parameters.Add("@AdisyonID", SqlDbType.Int).Value = cS._adisyonID;
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                    sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());

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

            return sonuc;
        }

        public string SatisYapilacakMasayiCek(int AdisyonID)
        {
            string masaNo = "";
            SqlCommand comm = new SqlCommand("Select MasaNo from AdisyonDetay where AdisyonID=@AdisyonID", conn);
            comm.Parameters.Add("@AdisyonID", SqlDbType.Int).Value = AdisyonID;
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                    masaNo = comm.ExecuteScalar().ToString();
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

            return masaNo;
        }


        public DataTable SatislariGetir()
        { 
            SqlDataAdapter da = new SqlDataAdapter("Select  * from Satislar", conn);
            da.Fill(dt);
            return dt;
        }
        public DataTable SonGununSatislariniGetir( string tarih)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select AdisyonID,Sum(Tutar) as Tutar,SalonAdi,Adi + Soyadi, MasaNo,OdemeTipi from Satislar inner join Salonlar on Satislar.SalonID = Salonlar.ID inner join Kullanicilar on Kullanicilar.ID = Satislar.KullaniciID inner join OdemeTipleri on Satislar.OdemeTipiID = OdemeTipleri.ID where Convert(varchar(20), Tarih, 104) Like Convert(varchar(20), @Tarih, 104) + '%' group by AdisyonID,SalonAdi, Adi + Soyadi , MasaNo, OdemeTipi", conn);
            da.SelectCommand.Parameters.Add("@Tarih", SqlDbType.DateTime).Value = tarih;
            try
            {
                da.Fill(dt);
            }
            catch (SqlException ex)
            {

                string s = ex.Message;
            }
            
            return dt;
        }
        #endregion
    }
}
