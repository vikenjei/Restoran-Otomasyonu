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
    class cAdisyon
    {

        #region Members
        private int _id;
        private int _salonId;
        private int _masaId;
        private int _urunId;
        private int _kullaniciId;
        private DateTime _tarih;
        private int _kuver;
        private int _adet;
        private double _fiyat;
        private string _masaNo;
        private int _adisyonID;
        private bool _adisyonAcikMi;
        private bool _odemedeMi;
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

        public int SalonId
        {
            get
            {
                return _salonId;
            }

            set
            {
                _salonId = value;
            }
        }

        public int MasaId
        {
            get
            {
                return _masaId;
            }

            set
            {
                _masaId = value;
            }
        }

        public int UrunId
        {
            get
            {
                return _urunId;
            }

            set
            {
                _urunId = value;
            }
        }

        public int KullaniciId
        {
            get
            {
                return _kullaniciId;
            }

            set
            {
                _kullaniciId = value;
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

        public bool AdisyonAcikMi
        {
            get
            {
                return _adisyonAcikMi;
            }

            set
            {
                _adisyonAcikMi = value;
            }
        }

        public bool OdemedeMi
        {
            get
            {
                return _odemedeMi;
            }

            set
            {
                _odemedeMi = value;
            }
        }
        #endregion
        #region Objects
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataReader dr;
        #endregion
        #region Sql
        SqlConnection conn = new SqlConnection(cMain.connStr);
        #endregion
        #region Metods
        public bool AdisyonDetayEkle(cAdisyon cA)
        {
            bool sonuc = false;
           
            SqlCommand comm = new SqlCommand("Insert into AdisyonDetay (SalonID,UrunID,KullaniciID,Tarih,Kuver,Adet,Fiyat,MasaNo,AdisyonID,AdisyonAcikMi,OdemedeMi) values (@SalonID,@UrunID,@KullaniciID,@Tarih,@Kuver,@Adet,@Fiyat,@MasaNo,@AdisyonID,@AdisyonAcikMi,@OdemedeMi)", conn);
            comm.Parameters.Add("@SalonID", SqlDbType.Int).Value = cA._salonId;
            comm.Parameters.Add("@UrunID", SqlDbType.Int).Value = cA._urunId;
            comm.Parameters.Add("@KullaniciID", SqlDbType.Int).Value = cA._kullaniciId;
            comm.Parameters.Add("@Tarih", SqlDbType.DateTime).Value = cA._tarih;
            comm.Parameters.Add("@Kuver", SqlDbType.Int).Value = cA._kuver;
            comm.Parameters.Add("@Adet", SqlDbType.Int).Value = cA._adet;
            comm.Parameters.Add("@Fiyat", SqlDbType.Money).Value = cA._fiyat;
            comm.Parameters.Add("@MasaNo", SqlDbType.VarChar).Value = cA._masaNo;
            comm.Parameters.Add("@AdisyonID", SqlDbType.Int).Value = cA._adisyonID;
            comm.Parameters.Add("@AdisyonAcikMi", SqlDbType.Bit).Value = cA._adisyonAcikMi;
            comm.Parameters.Add("@OdemedeMi", SqlDbType.Bit).Value = cA._odemedeMi;

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
        public int AdisyonEkle(int kullaniciID)
        {
            int adisyonID = 0;

            SqlCommand comm = new SqlCommand("Insert into Adisyon (KullaniciID) values (@KullaniciID); SELECT scope_identity()", conn);
            comm.Parameters.Add("@KullaniciID", SqlDbType.Int).Value = kullaniciID;
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                    adisyonID = Convert.ToInt32(comm.ExecuteScalar());
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
            return adisyonID;
        }
        public bool AdisyonGuncelleByHesap(int adisyonID)
        {
            bool sonuc = false;
            SqlCommand comm = new SqlCommand("Update AdisyonDetay Set OdemedeMi = 1 where AdisyonID=@AdisyonID ", conn);

            comm.Parameters.Add("@AdisyonID", SqlDbType.Int).Value = adisyonID;
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                    comm.ExecuteNonQuery();
                    sonuc = true;
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
        public void AdisyonuListViewADoldur(ListView lv, int AdisyonID)
        {
            SqlCommand comm = new SqlCommand("Select UrunAdi, Adet,Fiyat,Urunler.ID from AdisyonDetay inner join Urunler on AdisyonDetay.UrunID = Urunler.ID where AdisyonID = @AdisyonID", conn);
            comm.Parameters.Add("@AdisyonID", SqlDbType.Int).Value = AdisyonID;
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                    SqlDataReader dr = comm.ExecuteReader();
                    if (dr.HasRows)
                    {
                        int i = 0;
                        while (dr.Read())
                        {
                            lv.Items.Add(dr[0].ToString());
                            lv.Items[i].SubItems.Add(dr["Adet"].ToString());
                            lv.Items[i].SubItems.Add(dr["Fiyat"].ToString());
                            lv.Items[i].SubItems.Add(dr["ID"].ToString());
                            i++;
                        }
                    }
                    dr.Close();

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


        }       
        public bool AdisyonDetayGuncelleBySiparisEkleme(cAdisyon cA)
        {
            bool sonuc = false;
           
           SqlCommand comm = new SqlCommand("Insert into AdisyonDetay (SalonID,UrunID,KullaniciID,Tarih,Kuver,Adet,Fiyat,MasaNo,AdisyonID,AdisyonAcikMi,OdemedeMi) values (@SalonID,@UrunID,@KullaniciID,@Tarih,@Kuver,@Adet,@Fiyat,@MasaNo,@AdisyonID,@AdisyonAcikMi,@OdemedeMi)", conn);
            comm.Parameters.Add("@SalonID", SqlDbType.Int).Value = cA._salonId;
            comm.Parameters.Add("@UrunID", SqlDbType.Int).Value = cA._urunId;
            comm.Parameters.Add("@KullaniciID", SqlDbType.Int).Value = cA._kullaniciId;
            comm.Parameters.Add("@Tarih", SqlDbType.DateTime).Value = cA._tarih;
            comm.Parameters.Add("@Kuver", SqlDbType.Int).Value = cA._kuver;
            comm.Parameters.Add("@Adet", SqlDbType.Int).Value = cA._adet;
            comm.Parameters.Add("@Fiyat", SqlDbType.Money).Value = cA._fiyat;
            comm.Parameters.Add("@MasaNo", SqlDbType.VarChar).Value = cA._masaNo;
            comm.Parameters.Add("@AdisyonID", SqlDbType.Int).Value = cA._adisyonID;
            comm.Parameters.Add("@AdisyonAcikMi", SqlDbType.Bit).Value = cA._adisyonAcikMi;
            comm.Parameters.Add("@OdemedeMi", SqlDbType.Bit).Value = cA._odemedeMi;

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
        public string GuncellenecekAdisyonaAitMasaNoYuCek(int AdisyonID)
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
        public bool GuncellenecekAdisyonOdemedeMi(int AdisyonID)
        {
            bool sonuc = false;
            SqlCommand comm = new SqlCommand("Select OdemedeMi from AdisyonDetay where AdisyonID=@AdisyonID", conn);
            comm.Parameters.Add("@AdisyonID", SqlDbType.Int).Value = AdisyonID;
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                    sonuc = Convert.ToBoolean(comm.ExecuteScalar());
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

        public bool AdisyonDurumunuGuncelleBySatis(int AdisyonID)
        {
            bool sonuc = false;
            SqlCommand comm = new SqlCommand("Update AdisyonDetay Set AdisyonAcikMi = 0 where AdisyonID = @AdisyonID", conn);
            comm.Parameters.Add("@AdisyonID", SqlDbType.Int).Value = AdisyonID;
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

        #endregion
    }
}
