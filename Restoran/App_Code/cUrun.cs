using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.App_Code
{
    class cUrun
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
        private int _urunAltGrupID;
        private int _urunGrupID;
        private string _urunAdi;
        private double _urunFiyati;
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

        public int UrunAltGrupID
        {
            get
            {
                return _urunAltGrupID;
            }

            set
            {
                _urunAltGrupID = value;
            }
        }

        public int UrunGrupID
        {
            get
            {
                return _urunGrupID;
            }

            set
            {
                _urunGrupID = value;
            }
        }

        public string UrunAdi
        {
            get
            {
                return _urunAdi;
            }

            set
            {
                _urunAdi = value;
            }
        }

        public double UrunFiyati
        {
            get
            {
                return _urunFiyati;
            }

            set
            {
                _urunFiyati = value;
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
        public DataTable UrunleriGetir()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select Urunler.ID,Urunler.UrunAltGrupID,Urunler.UrunGrupID,UrunAdi,UrunGruplari.UrunGrupAdi, UrunAltGruplari.UrunAltGrupAdi,UrunFiyati,Urunler.Aktif  from Urunler inner join UrunGruplari on Urunler.UrunGrupID = UrunGruplari.ID inner join UrunAltGruplari on Urunler.UrunAltGrupID = UrunAltGruplari.ID", conn);
            da.Fill(dt);
            return dt;
        }
        public DataTable UrunleriGetirByUrunAltGrupAdi(string urunAltGrupAdi)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select Urunler.ID,Urunler.UrunAltGrupID,Urunler.UrunGrupID,UrunAdi,UrunGruplari.UrunGrupAdi, UrunAltGruplari.UrunAltGrupAdi,UrunFiyati,Urunler.Aktif  from Urunler inner join UrunGruplari on Urunler.UrunGrupID = UrunGruplari.ID inner join UrunAltGruplari on Urunler.UrunAltGrupID = UrunAltGruplari.ID where UrunAltGrupAdi=@UrunAltGrupAdi", conn);
            da.SelectCommand.Parameters.Add("@urunAltGrupAdi", SqlDbType.VarChar).Value = urunAltGrupAdi;
            da.Fill(dt);
            return dt;
        }

        public double UrunFiyatiniGetir(string urunAdi)
        {
            double fiyat = 0;
            SqlCommand comm = new SqlCommand("Select  UrunFiyati from Urunler Where UrunAdi = @UrunAdi", conn);
            comm.Parameters.Add("@UrunAdi", SqlDbType.VarChar).Value = urunAdi;
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                    fiyat = Convert.ToDouble(comm.ExecuteScalar());

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


            return fiyat;
        }
        public int UrunIdGetir(string urunAdi)
        {
            int id = 0;
            SqlCommand comm = new SqlCommand("Select ID from Urunler Where UrunAdi = @UrunAdi", conn);
            comm.Parameters.Add("@UrunAdi", SqlDbType.VarChar).Value = urunAdi;
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


        internal bool UrunKaydet(cUrun u)
        {
            bool Basarili = false;
            SqlCommand comm = new SqlCommand("Insert into Urunler (UrunAltGrupID,UrunGrupID,UrunAdi,UrunFiyati,Aktif) values (@UrunAltGrupID,@UrunGrupID,@UrunAdi,@UrunFiyati,@Aktif)", conn);
            comm.Parameters.Add("UrunGrupID", SqlDbType.Int).Value = u._urunGrupID;
            comm.Parameters.Add("@UrunAltGrupID", SqlDbType.Int).Value = u._urunAltGrupID;
            comm.Parameters.Add("@UrunAdi", SqlDbType.VarChar).Value = u._urunAdi;
            comm.Parameters.Add("@UrunFiyati", SqlDbType.Money).Value = u._urunFiyati;
            comm.Parameters.Add("@Aktif", SqlDbType.Bit).Value = u._aktif;
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

        internal bool UrunSil(int UrunID)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Delete From Urunler where ID=@ID", conn);
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = UrunID;
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

        internal bool UrunDegistir(cUrun u)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Update Urunler Set UrunAltGrupID=@UrunAltGrupID,UrunGrupID=@UrunGrupID,UrunAdi=@UrunAdi,UrunFiyati=@UrunFiyati,Aktif=@Aktif  where ID=@ID", conn);
            comm.Parameters.Add("@UrunAltGrupID", SqlDbType.Int).Value =u._urunAltGrupID;
            comm.Parameters.Add("@UrunGrupID", SqlDbType.Int).Value = u._urunGrupID;
            comm.Parameters.Add("@UrunAdi", SqlDbType.VarChar).Value = u._urunAdi;
            comm.Parameters.Add("@UrunFiyati", SqlDbType.Money).Value = u._urunFiyati;
            comm.Parameters.Add("@Aktif", SqlDbType.Bit).Value = u._aktif;
            comm.Parameters.Add("@ID", SqlDbType.Int).Value =u._id;
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

        internal bool UrunKontrol(string UrunAdi, string UrunGrupID, string UrunAltGrupID)
        {
            bool Varmi = false;
            SqlCommand comm = new SqlCommand("select count(*) from Urunler where UrunAdi=@UrunAdi and UrunGrupID=UrunGrupID and UrunAltGrupID=@UrunAltGrupID", conn);
            comm.Parameters.Add("@UrunAdi", SqlDbType.VarChar).Value = UrunAdi;
            comm.Parameters.Add("@UrunGrupID", SqlDbType.Int).Value = Convert.ToInt32(UrunGrupID);
            comm.Parameters.Add("@UrunAltGrupID", SqlDbType.Int).Value = Convert.ToInt32(UrunAltGrupID);
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

        internal bool UrunKontrol(string UrunAdi, string UrunGrupID, string UrunAltGrupID, string ID)
        {
            bool Varmi = false;
            SqlCommand comm = new SqlCommand("select count(*) from Urunler where UrunAdi=@UrunAdi and UrunGrupID=UrunGrupID and UrunAltGrupID=@UrunAltGrupID and ID != @ID", conn);
            comm.Parameters.Add("@UrunAdi", SqlDbType.VarChar).Value = UrunAdi;
            comm.Parameters.Add("@UrunGrupID", SqlDbType.Int).Value = Convert.ToInt32(UrunGrupID);
            comm.Parameters.Add("@UrunAltGrupID", SqlDbType.Int).Value = Convert.ToInt32(UrunAltGrupID);
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

        #endregion

    }
}
