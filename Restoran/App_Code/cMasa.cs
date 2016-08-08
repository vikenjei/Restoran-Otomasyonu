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
    class cMasa
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
        private string _masaNo;
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


        public DataTable OdemedeOlanlariGetirSalonID(int SalonID)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select MasaNo,AdisyonID from AdisyonDetay where SalonID = @SalonID and OdemedeMi=1 and AdisyonAcikMi=1", conn);
            da.SelectCommand.Parameters.Add("@SalonID", SqlDbType.Int).Value = SalonID;

            da.Fill(dt);


            return dt;
        }

        

        public DataTable DoluButonlaraBilgileriBas(int SalonID)
        {

            //burdan devam et
            SqlDataAdapter da = new SqlDataAdapter("Select AdisyonDetay.MasaNo, AdisyonDetay.AdisyonID, Kullanicilar.Adi + ' '  + Kullanicilar.Soyadi as adsoyad,AdisyonDetay.Tarih,Kuver from AdisyonDetay inner join Kullanicilar on AdisyonDetay.KullaniciID = Kullanicilar.ID where SalonID=@SalonID and AdisyonAcikMi = 1", conn);

            da.SelectCommand.Parameters.Add("@SalonID", SqlDbType.Int).Value = SalonID;

            conn.Open();
            da.Fill(dt);



            return dt;
        }


        #endregion
    }
}
