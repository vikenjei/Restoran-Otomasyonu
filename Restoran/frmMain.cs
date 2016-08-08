using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Restoran.App_Code;
using System.Data.SqlClient;

namespace Restoran
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        #region Değişkenlerimiz
        private string sifre = string.Empty;
        private string where = string.Empty;
        private string urunAltGrupAdi = string.Empty;
        cKullanicilar cK = new cKullanicilar();

        int satisAdet = 0;
        int eklenenAdet = 0;
        double eklenenFiyat = 0;
        int urunTiklanan = 0;

        int yeniSatisAdet = 0;
        int yeniEklenenAdet = 0;
        double yeniEklenenFiyat = 0;
        int yeniUrunTiklanan = 0;



        #endregion
        private void frmMain_Load(object sender, EventArgs e)
        {
            SalonDuzenGeriPanelleriKapat();
            this.WindowState = FormWindowState.Maximized;
            this.ControlBox = false;
            pnlLogin.Visible = true;
            pnlLogin.Size = this.Size;
            //pnlLogin.BringToFront();
            //pnlBaloDuzen.SendToBack();
            tmrMain.Start();
            tsplblKullanici.Text = "Kullanıcı :";
            tsplblWhere.Text = "GİRİŞ YAP";
        }

        #region Salon ve Main Geri Click Eventları
        private void btnSalonGeri_Click(object sender, EventArgs e)
        {
            pnlSalon.Visible = false;
            pnlLogin.Visible = true;
            pnlLogin.Size = this.Size;
            tsplblKullanici.Text = "Kullanıcı :";// + " " + cK.Adi + " " + cK.Soyadi;
            tsplblWhere.Text = "GİRİŞ YAP";

            if (cMain.isLogin)
            {
                cMain.isLogin = false;
                sifre = string.Empty;
            }
            else
            {
                sifre = string.Empty;
            }
        }
        private void btnMainGeri_Click(object sender, EventArgs e)
        {
            pnlBaloDuzen.Update();
            pnlBaloDuzen.Refresh();
            ToplamTemizle();
            if (cMain.fromWhere == "balo")
            {
                pnlMain.Visible = false;
                pnlBaloDuzen.Visible = true;
                pnlBaloDuzen.Size = this.Size;
                tsplblKullanici.Text = "Kullanıcı :" + " " + cK.Adi + " " + cK.Soyadi;
                tsplblWhere.Text = "BALO SALON";

                cSalon cS = new cSalon();
                cMain.salonID = cS.salonIDGetir(cMain.fromWhere);
                tsplblWhere.Text = "BALO SALON";
                where = tsplblWhere.Text;
                OdemedeOlanlariSariyaBoya(pnlBaloDuzen);
                DoluButonlaraDetayBas(pnlBaloDuzen);
              
            }
            else if (cMain.fromWhere == "italyan")
            {
                pnlMain.Visible = false;
                pnlItalyanDuzen.Visible = true;
                pnlItalyanDuzen.Size = this.Size;
                tsplblKullanici.Text = "Kullanıcı :" + " " + cK.Adi + " " + cK.Soyadi;
                tsplblWhere.Text = "ITALYAN CAFE";

                cSalon cS = new cSalon();
                cMain.salonID = cS.salonIDGetir(cMain.fromWhere);
                tsplblWhere.Text = "ITALYAN CAFE";
                where = tsplblWhere.Text;

                DoluButonlaraDetayBas(pnlItalyanDuzen);
                OdemedeOlanlariSariyaBoya(pnlItalyanDuzen);

            }
            else if (cMain.fromWhere == "restaurant")
            {
                pnlMain.Visible = false;
                pnlRestaurantDuzen.Visible = true;
                pnlRestaurantDuzen.Size = this.Size;
                tsplblKullanici.Text = "Kullanıcı :" + " " + cK.Adi + " " + cK.Soyadi;
                tsplblWhere.Text = "RESTAURANT";

                cSalon cS = new cSalon();
                cMain.salonID = cS.salonIDGetir(cMain.fromWhere);
                tsplblWhere.Text = "RESTAURANT";
                where = tsplblWhere.Text;
                DoluButonlaraDetayBas(pnlRestaurantDuzen);
                OdemedeOlanlariSariyaBoya(pnlRestaurantDuzen);
            }
            else if (cMain.fromWhere == "nakky")
            {
                pnlMain.Visible = false;
                pnlRestaurantDuzen.Visible = false;
                pnlNakkyDuzen.Visible = true;
                pnlNakkyDuzen.Size = this.Size;
                tsplblKullanici.Text = "Kullanıcı :" + " " + cK.Adi + " " + cK.Soyadi;
                tsplblWhere.Text = "NAKKY BAR";

                cSalon cS = new cSalon();
                cMain.salonID = cS.salonIDGetir(cMain.fromWhere);
                tsplblWhere.Text = "NAKKY BAR";
                where = tsplblWhere.Text;
                DoluButonlaraDetayBas(pnlNakkyDuzen);
                OdemedeOlanlariSariyaBoya(pnlNakkyDuzen);
            }
            else if (cMain.fromWhere == "vip")
            {
                pnlMain.Visible = false;
                pnlRestaurantDuzen.Visible = false;
                pnlVipDuzen.Visible = true;
                pnlVipDuzen.Size = this.Size;
                tsplblKullanici.Text = "Kullanıcı :" + " " + cK.Adi + " " + cK.Soyadi;
                tsplblWhere.Text = "VIP SALON";

                cSalon cS = new cSalon();
                cMain.salonID = cS.salonIDGetir(cMain.fromWhere);
                tsplblWhere.Text = "VIP SALON";
                where = tsplblWhere.Text;
                DoluButonlaraDetayBas(pnlVipDuzen);
                OdemedeOlanlariSariyaBoya(pnlVipDuzen);

                cMasa cM = new cMasa();
                DataTable dt = new DataTable();
                dt = cM.OdemedeOlanlariGetirSalonID(cMain.salonID);
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < pnlVipDuzen.Controls.Count; i++)
                    {

                        if (pnlVipDuzen.Controls[i].GetType() == typeof(Button))
                        {
                            if (pnlVipDuzen.Controls[i].Text == row["MasaNo"].ToString())
                            {
                                pnlVipDuzen.Controls[i].BackColor = Color.Yellow;
                                pnlVipDuzen.Controls[i].Text = "ÖDEMEDE";
                                pnlVipDuzen.Controls[i].ForeColor = Color.Black;
                            }
                        }
                    }
                }
            }
        }
        #endregion
        #region Salon Masalardan Geri Click Eventları
        private void btnBaloMasaGeri_Click(object sender, EventArgs e)
        {
            SalonDuzenGeriPanelleriKapat();
            tsplblKullanici.Text = "Kullanıcı :" + " " + cK.Adi + " " + cK.Soyadi;
            tsplblWhere.Text = "SALON SEÇİNİZ";

        }
        private void btnItalyanMasaGeri_Click(object sender, EventArgs e)
        {
            if (cMain.fromWhere == "italyan")
            {
                SalonDuzenGeriPanelleriKapat();
                tsplblKullanici.Text = "Kullanıcı :" + " " + cK.Adi + " " + cK.Soyadi;
                tsplblWhere.Text = "SALON SEÇİNİZ";
            }

        }
        private void btnNakkyMasaGeri_Click(object sender, EventArgs e)
        {
            if (cMain.fromWhere == "nakky")
            {
                SalonDuzenGeriPanelleriKapat();
                tsplblKullanici.Text = "Kullanıcı :" + " " + cK.Adi + " " + cK.Soyadi;
                tsplblWhere.Text = "SALON SEÇİNİZ";
            }
        }
        private void btnVipSalonGeri_Click(object sender, EventArgs e)
        {
            if (cMain.fromWhere == "vip")
            {
                SalonDuzenGeriPanelleriKapat();

                tsplblKullanici.Text = "Kullanıcı :" + " " + cK.Adi + " " + cK.Soyadi;
                tsplblWhere.Text = "SALON SEÇİNİZ";
            }
        }
        private void btnRestaurantGeri_Click(object sender, EventArgs e)
        {
            if (cMain.fromWhere == "restaurant")
            {
                SalonDuzenGeriPanelleriKapat();
                tsplblKullanici.Text = "Kullanıcı :" + " " + cK.Adi + " " + cK.Soyadi;
                tsplblWhere.Text = "SALON SEÇİNİZ";

            }
        }

        #endregion
        #region Salonlara Click Eventları
        private void btnItalyanCafe_Click(object sender, EventArgs e)
        {
            pnlItalyanDuzen.Size = this.Size;
            pnlItalyanDuzen.Visible = true;
            pnlLogin.Visible = false;
            pnlSalon.Visible = false;
            cMain.fromWhere = "italyan";
            cSalon cS = new cSalon();
            cMain.salonID = cS.salonIDGetir(cMain.fromWhere);
            tsplblWhere.Text = "ITALYAN CAFE";
            where = tsplblWhere.Text;

            OdemedeOlanlariSariyaBoya(pnlItalyanDuzen);
            DoluButonlaraDetayBas(pnlItalyanDuzen);


        }
        private void btnRestaurant_Click(object sender, EventArgs e)
        {
            pnlRestaurantDuzen.Size = this.Size;
            pnlRestaurantDuzen.Visible = true;
            pnlLogin.Visible = false;
            pnlSalon.Visible = false;
            pnlBaloDuzen.Visible = false;
            pnlItalyanDuzen.Visible = false;
            cMain.fromWhere = "restaurant";
            cSalon cS = new cSalon();
            cMain.salonID = cS.salonIDGetir(cMain.fromWhere);
            tsplblWhere.Text = "RESTAURANT";
            where = tsplblWhere.Text;

            OdemedeOlanlariSariyaBoya(pnlRestaurantDuzen);
            DoluButonlaraDetayBas(pnlRestaurantDuzen);

        }
        private void btnNakkyBar_Click(object sender, EventArgs e)
        {
            pnlNakkyDuzen.Size = this.Size;
            pnlNakkyDuzen.Visible = true;
            pnlRestaurantDuzen.Visible = false;
            pnlLogin.Visible = false;
            pnlSalon.Visible = false;
            pnlBaloDuzen.Visible = false;
            pnlItalyanDuzen.Visible = false;
            cMain.fromWhere = "nakky";
            cSalon cS = new cSalon();
            cMain.salonID = cS.salonIDGetir(cMain.fromWhere);
            tsplblWhere.Text = "NAKKY BAR";
            where = tsplblWhere.Text;

            OdemedeOlanlariSariyaBoya(pnlNakkyDuzen);
            DoluButonlaraDetayBas(pnlNakkyDuzen);
        }
        private void btnVip_Click(object sender, EventArgs e)
        {
            pnlVipDuzen.Size = this.Size;
            pnlVipDuzen.Visible = true;
            pnlLogin.Visible = false;
            pnlSalon.Visible = false;
            pnlBaloDuzen.Visible = false;
            pnlItalyanDuzen.Visible = false;
            pnlRestaurantDuzen.Visible = false;
            cMain.fromWhere = "vip";
            cSalon cS = new cSalon();
            cMain.salonID = cS.salonIDGetir(cMain.fromWhere);
            tsplblWhere.Text = "VIP SALON";
            where = tsplblWhere.Text;

            OdemedeOlanlariSariyaBoya(pnlVipDuzen);
            DoluButonlaraDetayBas(pnlVipDuzen);
        }
        private void btnBaloSalon_Click(object sender, EventArgs e)
        {
            pnlBaloDuzen.Size = this.Size;
            pnlBaloDuzen.Visible = true;
            pnlLogin.Visible = false;
            pnlSalon.Visible = false;
            cMain.fromWhere = "balo";
            cSalon cS = new cSalon();
            cMain.salonID = cS.salonIDGetir(cMain.fromWhere);
            tsplblWhere.Text = "BALO SALON";
            where = tsplblWhere.Text;

            OdemedeOlanlariSariyaBoya(pnlBaloDuzen);
            DoluButonlaraDetayBas(pnlBaloDuzen);
        }

        #endregion
        // YAZILAN METODLAR
        #region Metods
        private void OdemedeOlanlariSariyaBoya(Panel pnl)
        {
            cMasa cM = new cMasa();
            DataTable dt = new DataTable();

            dt = cM.OdemedeOlanlariGetirSalonID(cMain.salonID);
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < pnl.Controls.Count; i++)
                {

                    if (pnl.Controls[i].GetType() == typeof(Button))
                    {
                        if (pnl.Controls[i].Text == row["MasaNo"].ToString())
                        {
                            pnl.Controls[i].BackColor = Color.Yellow;
                            pnl.Controls[i].Tag = row["AdisyonID"].ToString();
                            pnl.Controls[i].Text = pnl.Controls[i].Text + Environment.NewLine + "HESAP ALINDI";
                            pnl.Controls[i].ForeColor = Color.Black;
                        }
                        else if (pnl.Controls[i].Text.Count() < 10)
                        {
                            pnl.Controls[i].BackColor = Color.Green;
                            pnl.Controls[i].ForeColor = Color.WhiteSmoke;
                        }
                    }
                }
            }
        }

        private void DoluButonlaraDetayBas(Panel pnl)

        {

            cMasa cM = new cMasa();
            DataTable dt = new DataTable();


            dt = cM.DoluButonlaraBilgileriBas(cMain.salonID);
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < pnl.Controls.Count; i++)
                {

                    if (pnl.Controls[i].GetType() == typeof(Button))
                    {
                        if (pnl.Controls[i].Text == row["MasaNo"].ToString())
                        {
                            pnl.Controls[i].BackColor = Color.Red;
                            pnl.Controls[i].ForeColor = Color.Black;
                            pnl.Controls[i].Tag = row["AdisyonID"].ToString();
                            pnl.Controls[i].Text = pnl.Controls[i].Text + Environment.NewLine + row["adsoyad"].ToString() + Environment.NewLine + row["Tarih"].ToString() + Environment.NewLine + "Kuver : " + row["kuver"].ToString();
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Salonda-Masa Düzeninden Geri Tıklandığı Zaman Salon Seçebileceğimiz Paneli Açar
        /// </summary>
        private void SalonDuzenGeriPanelleriKapat()
        {
            pnlBaloDuzen.Visible = false;
            pnlItalyanDuzen.Visible = false;
            pnlRestaurantDuzen.Visible = false;
            pnlNakkyDuzen.Visible = false;
            pnlVipDuzen.Visible = false;
            pnlMain.Visible = false;
            pnlSalon.Visible = true;
            pnlSalon.Size = this.Size;
        }
        /// <summary>
        /// Login Ekranındaki Butonları Kontrol Eder. Gir-Çık Butonu Hariç Rakamsal Butonları Şifre Değişkeninde Toplar.
        /// </summary>
        /// <param name="sender"> Button a cast edip name'inden hangi butona basıldığını yakalarız.</param>
        /// <param name="e"></param>
        private void butonaTikla(object sender, EventArgs e)
        {

            Button btn = (Button)sender;

            if (btn.Name != "btnGir")
            {
                sifre += btn.Text;
            }
            else if (btn.Name == "btnIptal")
            {
                sifre = string.Empty;
            }
            else
            {
                //salondan geri düşünce tekrar gir deyince giriyor. son kullanıcıyı temizlemek lazım.Burdan bak

                //OK
                if (cMain.isLogin)
                {
                    sifre = string.Empty;
                }
                else
                {
                    if (sifre != "")
                    {
                        cK.Sifre = sifre;
                        if (cK.KullaniciBulBySifre(cK))
                        {
                            cYetkiler cY = new cYetkiler();
                            if (cY.YetkiGetirByYetkiID(cK.YetkiID) == "Admin")
                            {
                                frmYonetim frm = new frmYonetim();
                                frm.Size = this.Size;
                                frm.ShowDialog();
                                cMain.isLogin = false;
                                sifre = string.Empty;
                            }
                            else
                            {
                                cMain.isLogin = true;
                                pnlLogin.Visible = false;
                                pnlSalon.Visible = true;
                                pnlSalon.Size = this.Size;
                                tsplblKullanici.Text = "Kullanıcı :" + " " + cK.Adi + " " + cK.Soyadi;
                                cMain.girenKullaniciID = cK.Id;
                                tsplblGununTarihi.Text = DateTime.Now.ToString();
                                tsplblWhere.Text = "SALON SEÇİMİ";
                                pnlSalon.BackgroundImage = this.BackgroundImage;
                            }
                        }
                        else
                        {
                            sifre = string.Empty;
                            MessageBox.Show("Kullanıcı YOK", "Şifre Hatalı");
                        }
                    }
                    else
                    {
                        sifre = string.Empty;
                        MessageBox.Show("Kullanıcı YOK", "Şifre Hatalı");
                    }

                }
            }
        }
        /// <summary>
        /// SalonDüzenlerdeki buttonların hepsi bu metod a bağlı. Basılan masanın nerden(hangi salona ait) basıldığını yakalar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMasalaraTikla(object sender, EventArgs e)
        {
            lvSatis.Items.Clear();
            Button btn = (Button)sender;
            cMain.masaNo = btn.Text;
            UrunAltGruplariniButonlaraBas();
            if (cMain.fromWhere == "balo")
            {
                pnlBaloDuzen.Visible = false;
                pnlMain.Visible = true;
                //
                pnlMain.Size = this.Size;
                cMain.tiklananAdisyon = Convert.ToInt32(btn.Tag);
                cAdisyon cA = new cAdisyon();
                cA.AdisyonuListViewADoldur(lvSatis, cMain.tiklananAdisyon);
                ToplamHesapla();
            }
            else if (cMain.fromWhere == "italyan")
            {
                pnlItalyanDuzen.Visible = false;
                pnlMain.Visible = true;
                pnlMain.Size = this.Size;
                cMain.tiklananAdisyon = Convert.ToInt32(btn.Tag);
                cAdisyon cA = new cAdisyon();
                cA.AdisyonuListViewADoldur(lvSatis, cMain.tiklananAdisyon);
                ToplamHesapla();
            }
            else if (cMain.fromWhere == "restaurant")
            {
                pnlRestaurantDuzen.Visible = false;
                pnlMain.Visible = true;
                pnlMain.Size = this.Size;
                cMain.tiklananAdisyon = Convert.ToInt32(btn.Tag);
                cAdisyon cA = new cAdisyon();
                cA.AdisyonuListViewADoldur(lvSatis, cMain.tiklananAdisyon);
                ToplamHesapla();
            }
            else if (cMain.fromWhere == "nakky")
            {
                pnlNakkyDuzen.Visible = false;
                pnlMain.Visible = true;
                pnlMain.Size = this.Size;
               
                cMain.tiklananAdisyon = Convert.ToInt32(btn.Tag);
                cAdisyon cA = new cAdisyon();
                cA.AdisyonuListViewADoldur(lvSatis, cMain.tiklananAdisyon);
                ToplamHesapla();
            }
            else if (cMain.fromWhere == "vip")
            {
                pnlVipDuzen.Visible = false;
                pnlMain.Visible = true;
                pnlMain.Size = this.Size;
                cMain.tiklananAdisyon = Convert.ToInt32(btn.Tag);
                cAdisyon cA = new cAdisyon();
                cA.AdisyonuListViewADoldur(lvSatis, cMain.tiklananAdisyon);
                ToplamHesapla();

            }
            tsplblWhere.Text = where + " " + btn.Text.ToUpper();
        }
        /// <summary>
        /// Metod; Database de aktif olan ürün alt gruplarını bulur ve dinamik buton oluşturup bu butonlara name ve tex olarak bağlar.Basılan her button için bir "click" event ı oluşturup butona basıldığında işlem yapmamızı sağlar.
        /// </summary>
        private void UrunAltGruplariniButonlaraBas()
        {
            int alt = 50;
            int sol = 1;
            int bol;
            pnlUrunGruplari.Controls.Clear();
            cUrunAltGruplari cUAG = new cUrunAltGruplari();
            DataTable dt = cUAG.UrunAltGruplariniGetir();
            int count = dt.Rows.Count;
            int i = 0;
            bol = Convert.ToInt32(Math.Ceiling(Math.Sqrt(count)));
            foreach (DataRow row in dt.Rows)
            {
                Button btn = new Button();
                btn.AutoSize = false;
                btn.BackColor = Color.DarkTurquoise;
                btn.Size = new Size(pnlUrunGruplari.Width / bol, pnlUrunGruplari.Height / (bol * 2));
                btn.Name = "btn_" + row[1].ToString();
                btn.Text = row[1].ToString();

                btn.Location = new Point(sol, alt);
                pnlUrunGruplari.Controls.Add(btn);
                sol += btn.Width + 5;
                if (sol + pnlUrunGruplari.Width / bol > pnlUrunGruplari.Width) // bunu yapmasaydık butonlar yan yana dizilir alt satıra geçmezdi
                {
                    sol = 1;
                    alt += pnlUrunGruplari.Height / (bol * 2) + 5;
                }
                btn.Click += new EventHandler(UrunGruplari_Click);
                i++;
            }
        }
        /// <summary>
        /// Metod; ürün altgruplarından tıklanan button a bağlı olarak, ürünleri dinamik button oluşturarak name ve text olarak basar. tıklanan alt gruba bağlı kaç ürün varsa o kadar button oluşturur ve kısıtlandığı yere bu buttonları sıralar. Her oluşturduğu button için bir click event ı yaratıp buttonlara tıklandığında iş yapmamızı sağlar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UrunGruplari_Click(object sender, EventArgs e)
        {
            int alt = 10;
            int sol = 1;
            int bol;
            int i = 0;
            Button buton = (Button)sender; // tıklanan butonu yakala
            urunAltGrupAdi = buton.Text;
            //MessageBox.Show(btn.Text + " isimli butona tıkladınız");
            //Sql sorgusuyla altgrubu yakala ürünleri bas.
            //OK
            cUrun cU = new cUrun();
            DataTable dt = cU.UrunleriGetirByUrunAltGrupAdi(urunAltGrupAdi);
            int count = dt.Rows.Count;
            bol = Convert.ToInt32(Math.Ceiling(Math.Sqrt(count)));
            pnlUrunGruplari.Controls.Clear();
            foreach (DataRow row in dt.Rows)
            {
                Button btn = new Button();
                btn.AutoSize = false;
                btn.BackColor = Color.Turquoise;
                btn.Size = new Size(pnlUrunGruplari.Width / (bol * 2), pnlUrunGruplari.Height / (bol * 2));
                //btn.Size = new Size(150, 150);
                btn.Name = "btn_" + row[3].ToString();
                btn.Text = row[3].ToString();
                //btn.Font = new Font(btn.Font.FontFamily.Name, 18);
                btn.Location = new Point(sol, alt);
                pnlUrunGruplari.Controls.Add(btn);
                sol += btn.Width + 5;
                if (sol + pnlUrunGruplari.Width / bol > pnlUrunGruplari.Width) // bunu yapmasaydık butonlar yan yana dizilir alt satıra geçmezdi
                                                                               //OK
                {
                    sol = 1;
                    alt += pnlUrunGruplari.Height / (bol * 2) + 5;
                }
                btn.Click += new EventHandler(Urunler_Click);
                i++;

            }
            //Geri Butonu oluştur.
            //OK
            Button btnUrunGeri = new Button();
            btnUrunGeri.AutoSize = false;
            btnUrunGeri.BackColor = Color.CadetBlue;
            btnUrunGeri.Size = new Size(pnlUrunGruplari.Width / bol, pnlUrunGruplari.Height / (bol * 2));
            btnUrunGeri.Name = "btnUrunGeri";
            btnUrunGeri.Text = "Geri";
            btnUrunGeri.Location = new Point(sol, alt);
            pnlUrunGruplari.Controls.Add(btnUrunGeri);
            if (sol + pnlUrunGruplari.Width / bol > pnlUrunGruplari.Width) // bunu yapmasaydık butonlar yan yana dizilir alt satıra geçmezdi
            {
                sol = 1;
                alt += pnlUrunGruplari.Height / (bol * 2) + 5;
            }
            btnUrunGeri.Click += new EventHandler(Urunler_Click);
        }
        /// <summary>
        /// Oluşturduğumuz dinamik buttonlu ürünlere tıklandığında ne yapacağımızı yazaalım buraya.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Urunler_Click(object sender, EventArgs e)
        {
            int eklenenUrunID = 0;


            Button buton = (Button)sender;
            if (buton.Text == "Geri")
            {
                UrunAltGruplariniButonlaraBas();
            }
            else /*(cMain.tiklananAdisyon == 0)*/
            {
                cUrun cU = new cUrun();
                DataTable dt = new DataTable();
                eklenenFiyat = cU.UrunFiyatiniGetir(buton.Text);

                eklenenUrunID = cU.UrunIdGetir(buton.Text);
                bool varmi = false;

                for (int i = 0; i < lvSatis.Items.Count; i++)
                {

                    if (lvSatis.Items[i].SubItems[0].Text == buton.Text)
                    {
                        eklenenAdet = satisAdet;
                        //MessageBox.Show("zaten var");
                        eklenenAdet = Convert.ToInt32(lvSatis.Items[i].SubItems[1].Text);
                        eklenenAdet = satisAdet + eklenenAdet;
                        lvSatis.Items[i].SubItems[1].Text = eklenenAdet.ToString();

                        eklenenFiyat = eklenenAdet * eklenenFiyat;
                        lvSatis.Items[i].SubItems[2].Text = eklenenFiyat.ToString();
                        lvSatis.Items[i].SubItems[3].Text = eklenenUrunID.ToString();
                        varmi = true;
                        ToplamHesapla();
                        break;
                    }
                }
                if (varmi == false)
                {
                    if (satisAdet > 1)
                    {
                        lvSatis.Items.Add(buton.Text);
                        lvSatis.Items[urunTiklanan].SubItems.Add(satisAdet.ToString());
                        eklenenFiyat = satisAdet * eklenenFiyat;
                        lvSatis.Items[urunTiklanan].SubItems.Add(eklenenFiyat.ToString());
                        lvSatis.Items[urunTiklanan].SubItems.Add(eklenenUrunID.ToString());
                        urunTiklanan++;
                        ToplamHesapla();
                    }
                    else
                    {

                        satisAdet = 1;
                        lvSatis.Items.Add(buton.Text);
                        lvSatis.Items[urunTiklanan].SubItems.Add(satisAdet.ToString());
                        lvSatis.Items[urunTiklanan].SubItems.Add(eklenenFiyat.ToString());
                        lvSatis.Items[urunTiklanan].SubItems.Add(eklenenUrunID.ToString());
                        urunTiklanan++;
                        ToplamHesapla();
                    }
                }
            }
            if (cMain.tiklananAdisyon > 0)
            {
                cUrun cU = new cUrun();
                DataTable dt = new DataTable();
                yeniEklenenFiyat = cU.UrunFiyatiniGetir(buton.Text);
                int yeniEklenenUrunID = cU.UrunIdGetir(buton.Text);
                bool yeniVarmi = false;

                for (int i = 0; i < lvEklenenUrunler.Items.Count; i++)
                {

                    if (lvEklenenUrunler.Items[i].SubItems[0].Text == buton.Text)
                    {
                        yeniEklenenAdet = yeniSatisAdet;
                        //MessageBox.Show("zaten var");
                        yeniEklenenAdet = Convert.ToInt32(lvEklenenUrunler.Items[i].SubItems[1].Text);
                        yeniEklenenAdet = yeniSatisAdet + yeniEklenenAdet;
                        lvEklenenUrunler.Items[i].SubItems[1].Text = yeniEklenenAdet.ToString();

                        yeniEklenenFiyat = yeniEklenenAdet * yeniEklenenFiyat;
                        lvEklenenUrunler.Items[i].SubItems[2].Text = yeniEklenenFiyat.ToString();
                        lvEklenenUrunler.Items[i].SubItems[3].Text = yeniEklenenUrunID.ToString();
                        yeniVarmi = true;

                        break;
                    }
                }
                if (yeniVarmi == false)
                {
                    if (yeniSatisAdet > 1)
                    {
                        lvEklenenUrunler.Items.Add(buton.Text);
                        lvEklenenUrunler.Items[yeniUrunTiklanan].SubItems.Add(yeniSatisAdet.ToString());
                        yeniEklenenFiyat = yeniSatisAdet * yeniEklenenFiyat;
                        lvEklenenUrunler.Items[yeniUrunTiklanan].SubItems.Add(yeniEklenenFiyat.ToString());
                        lvEklenenUrunler.Items[yeniUrunTiklanan].SubItems.Add(yeniEklenenUrunID.ToString());
                        yeniUrunTiklanan++;
                        ToplamHesapla();
                    }
                    else
                    {

                        yeniSatisAdet = 1;
                        lvEklenenUrunler.Items.Add(buton.Text);
                        lvEklenenUrunler.Items[yeniUrunTiklanan].SubItems.Add(yeniSatisAdet.ToString());
                        lvEklenenUrunler.Items[yeniUrunTiklanan].SubItems.Add(yeniEklenenFiyat.ToString());
                        lvEklenenUrunler.Items[yeniUrunTiklanan].SubItems.Add(yeniEklenenUrunID.ToString());
                        yeniUrunTiklanan++;

                    }
                }


            }
        }
        private void ToplamHesapla()
        {
            if (lvSatis.Items.Count > 0)
            {
                double urunFiyatToplam = 0;
                for (int i = 0; i < lvSatis.Items.Count; i++)
                {

                    urunFiyatToplam += Convert.ToDouble(lvSatis.Items[i].SubItems[2].Text);
                    txtAraToplam.Text = urunFiyatToplam.ToString();
                    txtKdv.Text = ((urunFiyatToplam * 18) / 100).ToString();
                    txtToplam.Text = (Convert.ToDouble(txtAraToplam.Text) + Convert.ToDouble(txtKdv.Text)).ToString();
                }
            }
            satisAdet = 1;
            urunTiklanan = lvSatis.Items.Count;
        }
        private void ToplamTemizle()
        {

            txtAraToplam.Text = "0";
            txtKdv.Text = "0";
            txtToplam.Text = (Convert.ToDouble(txtAraToplam.Text) + Convert.ToDouble(txtKdv.Text)).ToString();
        }
        protected void SatisRakamlarinaTikla(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            // yakala
            satisAdet = Convert.ToInt32(btn.Text);
            yeniSatisAdet = Convert.ToInt32(btn.Text);
            lblKontrolAdet.Text = satisAdet.ToString();
        }
        protected void SatisArttirAzalt(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == "-")
            {
                cUrun cU = new cUrun();
                eklenenAdet = Convert.ToInt32(lvSatis.SelectedItems[0].SubItems[1].Text) - 1;
                if (eklenenAdet == 0)
                {
                    eklenenAdet = 1;
                }
                lvSatis.SelectedItems[0].SubItems[1].Text = eklenenAdet.ToString();
                lvSatis.SelectedItems[0].SubItems[2].Text = (eklenenAdet * cU.UrunFiyatiniGetir(lvSatis.SelectedItems[0].Text)).ToString();



            }
            else if (btn.Text == "+")
            {
                cUrun cU = new cUrun();
                eklenenAdet = Convert.ToInt32(lvSatis.SelectedItems[0].SubItems[1].Text) + 1;
                lvSatis.SelectedItems[0].SubItems[1].Text = eklenenAdet.ToString();
                lvSatis.SelectedItems[0].SubItems[2].Text = (eklenenAdet * cU.UrunFiyatiniGetir(lvSatis.SelectedItems[0].Text)).ToString();
                //urunFiyatToplam += Convert.ToDouble(lvSatis.Items[0].SubItems[2].Text);

            }
            else if (btn.Text == "X")
            {
                lvSatis.Items.RemoveAt(lvSatis.SelectedIndices[0]);
                //toplam fiyatları güncelle.

                //cUrun cU = new cUrun();
                //eklenenAdet = Convert.ToInt32(lvSatis.SelectedItems[0].SubItems[1].Text) + 1;
                //lvSatis.SelectedItems[0].SubItems[1].Text = eklenenAdet.ToString();
                //lvSatis.SelectedItems[0].SubItems[2].Text = (eklenenAdet * cU.UrunFiyatiniGetir(lvSatis.SelectedItems[0].Text)).ToString();
                //urunFiyatToplam += Convert.ToDouble(lvSatis.Items[0].SubItems[2].Text);
                //txtAraToplam.Text = urunFiyatToplam.ToString();
                //txtKdv.Text = ((urunFiyatToplam * 18) / 100).ToString();
                //txtToplam.Text = (Convert.ToDouble(txtAraToplam.Text) + Convert.ToDouble(txtKdv.Text)).ToString();
            }
            ToplamHesapla();
        }
        private void btn_KeyPress(object sender, KeyPressEventArgs e)
        {
            //keypress i yakala klavyeden girilebilsin.
            // Button btn = new Button();
            //btn = (Button)sender;

            // text'e girilen sayının integera sığmayacak kadar büyükse işlem yapmamasını sağlamalıyız.
            // bu yüzden ilk öncelikle harf yazılmasını önlemek gerek.
            //int basilantus = Convert.ToInt32(e.KeyChar);
            //// eğer 48 ile 57 arası DEĞİLSE tuş basımını iptal etmek gerekir.
            //if (!(basilantus >= 48 && basilantus <= 57))
            //{
            //    // tuş basımını engelledik.
            //    e.Handled = true;
            //}
            //else
            //{
            //    // eğer sayıya basılmışsa burası çalışacaktır.

            //    if (btn.Text != "Gir")
            //    {
            //        sifre += btn.Text;
            //    }
            //    else if (btn.Text == "Çık")
            //    {
            //        sifre = string.Empty;
            //    }
            //    else
            //    {
            //        //salondan geri düşünce tekrar gir deyince giriyor. son kullanıcıyı temizlemek lazım.Burdan bak

            //        //OK
            //        if (cMain.isLogin)
            //        {
            //            sifre = string.Empty;
            //        }
            //        else
            //        {
            //            if (sifre != "")
            //            {
            //                cK.Sifre = sifre;
            //                if (cK.KullaniciBulBySifre(cK) > 0)
            //                {
            //                    cMain.isLogin = true;

            //                    pnlLogin.Visible = false;
            //                    pnlSalon.Visible = true;
            //                    pnlSalon.Size = this.Size;
            //                    tsplblKullanici.Text = "Kullanıcı :" + " " + cK.Adi + " " + cK.Soyadi;
            //                    tsplblGununTarihi.Text = DateTime.Now.ToString();
            //                    tsplblWhere.Text = "SALON SEÇİMİ";
            //                }
            //                else
            //                {
            //                    sifre = string.Empty;
            //                    MessageBox.Show("Kullanıcı YOK", "Şifre Hatalı");
            //                }
            //            }
            //            else
            //            {
            //                sifre = string.Empty;
            //                MessageBox.Show("Kullanıcı YOK", "Şifre Hatalı");
            //            }

            //        }
            //    }



            //    // ve burda bir kontrol yapmamız gerek.
            //    // text'e girilen değer acaba int'e sığacakmı yoksa program hatamı(istisna) verecek.
            //    // eğer hata(istisna) vericekse tuş basımını engellemeliyiz.
            //    try
            //    {
            //        // int değerini aşarsa bu satır hataya(istisna) neden olucaktır.
            //        int textdekiDeger = int.Parse(btn.Text);
            //    }
            //    catch (OverflowException)
            //    {
            //        // hata eğer overflow ise yani taşmışssa tuş basımını engelledik.
            //        // istersek son olabileceği rakamı text e güncelletebiliriz.
            //        btn.Text = int.MaxValue.ToString();
            //        e.Handled = true;
            //    }
            //}
        }
        protected void OdemeTipleri_Click(object sender, EventArgs e)
        {
            grpOdemeTipleri.Visible = false;
            Button btn = (Button)sender;
            cSatislar cS = new cSatislar();
            if (lvSatis.Items.Count > 0)
            {
                for (int i = 0; i < lvSatis.Items.Count; i++)
                {
                    cS.SalonID = cMain.salonID;
                    cS.UrunID = Convert.ToInt32(lvSatis.Items[i].SubItems[3].Text);
                    cS.KullaniciID = cMain.girenKullaniciID;
                    cS.OdemeTipiID = Convert.ToInt32(btn.Tag);
                    cS.Tarih = DateTime.Now;
                    cS.Fiyat = Convert.ToDouble(lvSatis.Items[0].SubItems[2].Text);
                    cS.Adet = Convert.ToInt32(lvSatis.Items[i].SubItems[1].Text);
                    cS.Tutar = Convert.ToDouble(cS.Fiyat * cS.Adet);
                    cS.Kuver = 3;//default 3
                    cS.MasaNo = cS.SatisYapilacakMasayiCek(cMain.tiklananAdisyon);
                    cS.AdisyonID = cMain.tiklananAdisyon;

                    if (cS.SatisEkle(cS))
                    {
                        MessageBox.Show("Satis Basariyla Kaydedildi");
                        // AdisyonDetay dan Adisyon Acikmi'yi Kapat .
                        cAdisyon ca = new cAdisyon();
                        ca.AdisyonDurumunuGuncelleBySatis(cMain.tiklananAdisyon);
                    }
                    else
                    {
                        MessageBox.Show("Satış Başarısız ");
                    }
                }
            }
        }
        #endregion


        private void lvSatis_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSatisArttir.Enabled = true;
            btnSatisAzalt.Enabled = true;
            //arttır azalt
            eklenenAdet = Convert.ToInt32(lvSatis.Items[0].SubItems[1].Text);
            eklenenFiyat = Convert.ToDouble(lvSatis.Items[0].SubItems[2].Text);
        }
        private void btnSatisSiparisGec_Click(object sender, EventArgs e)
        { //adisyona kaydet masanın rengini ayarla // OK
            cAdisyon cA = new cAdisyon();
            if (cA.GuncellenecekAdisyonOdemedeMi(cMain.tiklananAdisyon)) //masa ödemede
            {
                MessageBox.Show("Bu Masaya Ekleme Yapamazsınız Lütfen Yeni Masa Açın");
                pnlMain.Visible = false;
                pnlSalon.Visible = true;
                pnlSalon.Size = this.Size;
                tsplblWhere.Text = "SALON SEÇİMİ";

            }
            else // masa ödemede değil güncellenebilir.
            {
                //Sipariş Güncelliyorsa
                if (cMain.tiklananAdisyon > 0) // sipariş zaten var demektir, Güncelliyordur.
                {
                    //eski sipariş bilgilerini database de kayıtlı adisyonID ye göre.yeni eklenen siparişleri aynı mantıkla gözükmeyen bir listview da tutup o adisyon ID ye güncelleyeceğiz
                    //Adisyon Güncelle ( yeni listview daki itemleri ekle)
                    if (lvEklenenUrunler.Items.Count > 0)
                    {
                        for (int i = 0; i < lvEklenenUrunler.Items.Count; i++)
                        {
                            cA.SalonId = cMain.salonID;
                            cA.UrunId = Convert.ToInt32(lvEklenenUrunler.Items[i].SubItems[3].Text);
                            cA.KullaniciId = cMain.girenKullaniciID;
                            cA.Tarih = DateTime.Now;
                            cA.Kuver = 3;//kuver girilecek bir ayar yap
                            cA.Adet = Convert.ToInt32(lvEklenenUrunler.Items[i].SubItems[1].Text);
                            cA.Fiyat = Convert.ToDouble(lvEklenenUrunler.Items[0].SubItems[2].Text);
                            cA.MasaNo = cA.GuncellenecekAdisyonaAitMasaNoYuCek(cMain.tiklananAdisyon);
                            cA.AdisyonID = cMain.tiklananAdisyon;
                            cA.AdisyonAcikMi = true;
                            cA.OdemedeMi = false;
                            if (cA.AdisyonDetayGuncelleBySiparisEkleme(cA))
                            {
                                MessageBox.Show("Yeni Sipariş Başarıyla Yollandı");
                            }
                            else
                            {
                                MessageBox.Show(" Yeni Sipariş Ekleme Başarısız ");
                            }
                        }
                    }
                }

                else  //ilk kez sipariş geçiyorsa  //OK;
                {
                    int eklenenAdisyon = cA.AdisyonEkle(cMain.girenKullaniciID);

                    if (lvSatis.Items.Count > 0)
                    {
                        for (int i = 0; i < lvSatis.Items.Count; i++)
                        {
                            cA.SalonId = cMain.salonID;
                            cA.UrunId = Convert.ToInt32(lvSatis.Items[i].SubItems[3].Text);
                            cA.KullaniciId = cMain.girenKullaniciID;
                            cA.Tarih = DateTime.Now;
                            cA.Kuver = 3;//kuver girilecek bir ayar yap
                            cA.Adet = Convert.ToInt32(lvSatis.Items[i].SubItems[1].Text);
                            cA.Fiyat = Convert.ToDouble(lvSatis.Items[0].SubItems[2].Text);
                            cA.MasaNo = cMain.masaNo;
                            cA.AdisyonID = eklenenAdisyon;
                            cA.AdisyonAcikMi = true;
                            cA.OdemedeMi = false;
                            if (cA.AdisyonDetayEkle(cA))
                            {
                                MessageBox.Show("Sipariş Başarıyla Yollandı");
                            }
                            else
                            {
                                MessageBox.Show("Sipariş Başarısız ");
                            }
                        }
                    }
                }
            }
        }
        private void btnSatisOdemeAl_Click(object sender, EventArgs e)
        {
            grpOdemeTipleri.Visible = true;
            int alt = 10;
            int sol = 1;
            int bol;
            int i = 0;
            Button buton = (Button)sender; // tıklanan butonu yakala
            urunAltGrupAdi = buton.Text;
            //MessageBox.Show(btn.Text + " isimli butona tıkladınız");
            //Sql sorgusuyla altgrubu yakala ürünleri bas.
            //OK
            cOdemeTipleri cOT = new cOdemeTipleri();
            DataTable dt = cOT.OdemeTipleriniGetir();
            int count = dt.Rows.Count;

            bol = Convert.ToInt32(Math.Ceiling(Math.Sqrt(count)));
            grpOdemeTipleri.Controls.Clear();
            foreach (DataRow row in dt.Rows)
            {
                Button btn = new Button();
                btn.AutoSize = false;
                btn.Size = new Size(grpOdemeTipleri.Width / bol, grpOdemeTipleri.Height / (bol * 2));
                //btn.Size = new Size(150, 150);
                btn.Name = "btn_" + row[1].ToString();
                btn.Tag = row[0].ToString();
                btn.Text = row[1].ToString();
                //btn.Font = new Font(btn.Font.FontFamily.Name, 18);
                btn.Location = new Point(sol, alt);
                grpOdemeTipleri.Controls.Add(btn);
                sol += btn.Width + 5;
                if (sol + grpOdemeTipleri.Width / bol > grpOdemeTipleri.Width) // bunu yapmasaydık butonlar yan yana dizilir alt satıra geçmezdi
                                                                               //OK
                {
                    sol = 1;
                    alt += grpOdemeTipleri.Height / (bol * 2) + 5;
                }
                btn.Click += new EventHandler(OdemeTipleri_Click);
                i++;

            }

        }
        private void btnSatisHesap_Click(object sender, EventArgs e)
        {
            cAdisyon cA = new cAdisyon();
            cA.AdisyonGuncelleByHesap(cMain.tiklananAdisyon);
        }
        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //
        }
        private void tmrMain_Tick(object sender, EventArgs e)
        {
            tsplblGununTarihi.Text = DateTime.Now.ToString();
        }
    }
}
