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

namespace Restoran
{
    public partial class frmYonetim : Form
    {
        public frmYonetim()
        {
            InitializeComponent();
        }
        private void YetkileriGetir()
        {

            cYetkiler cY = new cYetkiler();
            dgvYetkiler.DataSource = cY.YetkileriGetir();

        }
        private void KullanicilariGetir()
        {
            cKullanicilar cK = new cKullanicilar();
            dgvKullanicilar.DataSource = cK.KullanicilariGetir();

        }
        private void UrunleriGetir()
        {
            cUrun cU = new cUrun();
            dgvUrunUrunler.DataSource = cU.UrunleriGetir();

        }
        private void UrunAltGruplariniDoldur()
        {
            cUrunAltGruplari cUAG = new cUrunAltGruplari();
            cUAG.UrunAltGruplariniDoldur(cbUrunUrunAltGruplari);

        }
        private void UrunGruplariniDoldur()
        {
            cUrunGruplari cUG = new cUrunGruplari();
            cUG.UrunGruplariniDoldur(cbUrunUrunGruplari);
        }
        private void SalonlariGetir()
        {
            cSalon cS = new cSalon();
            dgvSalon.DataSource = cS.SalonlariGetir();

        }
        private void OdemeTipleriniGetir()
        {
            cOdemeTipleri cOT = new cOdemeTipleri();
            dgvOdemeTipleri.DataSource = cOT.OdemeTipleriniGetir();


        }

        private void tspiYetkiTanimlama_Click(object sender, EventArgs e)
        {
            YetkileriGetir();
            tc1.SelectTab(0);
        }


        private void tspiKullaniciIslemleri_Click(object sender, EventArgs e)
        {
            KullanicilariGetir();
            tc1.SelectTab(1);
        }
        private void tspiUrunIslemleri_Click(object sender, EventArgs e)
        {
            UrunleriGetir();
            UrunAltGruplariniDoldur();
            UrunGruplariniDoldur();
            tc1.SelectTab(2);
        }
        private void tspiSalonIslemleri_Click(object sender, EventArgs e)
        {
            SalonlariGetir();
            tc1.SelectTab(3);
        }

        private void tspiOdemeTipiIslemleri_Click(object sender, EventArgs e)
        {
            OdemeTipleriniGetir();
            tc1.SelectTab(4);
        }
        private void tabKontrole_Click(object sender, EventArgs e)
        {
            if (tc1.SelectedTab.Text == "Yetki Tanımlama")
            {
                YetkileriGetir();
                cbYetki.SelectedIndex = 0;

            }
            else if (tc1.SelectedTab.Text == "Kullanıcı İşlemleri")
            {
                KullanicilariGetir();

            }
            else if (tc1.SelectedTab.Text == "Ürün İşlemleri")
            {
                UrunleriGetir();
                UrunAltGruplariniDoldur();
                UrunGruplariniDoldur();
                cbUrunAktif.SelectedIndex = 0;
            }
            else if (tc1.SelectedTab.Text == "Salon Masa")
            {
                SalonlariGetir();
            }
            else if (tc1.SelectedTab.Text == "Ödeme Tipleri")
            {
                OdemeTipleriniGetir();
            }
        }

        private void cbUrunUrunAltGruplari_SelectedIndexChanged(object sender, EventArgs e)
        {
            cUrunAltGruplari uag = new cUrunAltGruplari();
            txtUrunUrunAltGrupID.Text = (uag.UrunAltGrupIDGetir(cbUrunUrunAltGruplari.SelectedItem.ToString())).ToString();
        }

        private void cbUrunUrunGruplari_SelectedIndexChanged(object sender, EventArgs e)
        {
            cUrunGruplari ug = new cUrunGruplari();
            txtUrunUrunGrupID.Text = (ug.UrunGrupIDGetir(cbUrunUrunGruplari.SelectedItem.ToString())).ToString();
        }

        private void dgvYetkiler_DoubleClick(object sender, EventArgs e)
        {

            btnYetkiDegistir.Enabled = true;
            btnYetkiKaydet.Enabled = false;
            btnYetkiSil.Enabled = true;
            txtYetkiID.Text = dgvYetkiler.SelectedRows[0].Cells[0].Value.ToString();
            txtYetkiAdi.Text = dgvYetkiler.SelectedRows[0].Cells[1].Value.ToString();
            if (Convert.ToBoolean(dgvYetkiler.SelectedRows[0].Cells[2].Value) == true)               // KONTROL ETTIR
            {
                cbYetki.SelectedIndex = 0;
            }
            else
            {
                cbYetki.SelectedIndex = 1;
            }
        }

        private void dgvKullanicilar_DoubleClick(object sender, EventArgs e)
        {
            btnKullaniciDegistir.Enabled = true;
            btnKullaniciKaydet.Enabled = false;
            btnKullaniciSil.Enabled = true;
            txtKullaniciID.Text = dgvKullanicilar.SelectedRows[0].Cells[0].Value.ToString();
            txtKullaniciYetkiID.Text = dgvKullanicilar.SelectedRows[0].Cells[1].Value.ToString();
            txtKullaniciAdi.Text = dgvKullanicilar.SelectedRows[0].Cells[3].Value.ToString();
            txtKullaniciSoyadi.Text = dgvKullanicilar.SelectedRows[0].Cells[4].Value.ToString();
            txtKullaniciSifre.Text = dgvKullanicilar.SelectedRows[0].Cells[5].Value.ToString();
            txtKullaniciTarih.Text = dgvKullanicilar.SelectedRows[0].Cells[6].Value.ToString();
            cbKullaniciYetkileri.SelectedItem = dgvKullanicilar.SelectedRows[0].Cells[2].Value;
            if (Convert.ToBoolean(dgvKullanicilar.SelectedRows[0].Cells[7].Value) == true)
            {
                cbKullaniciAktif.SelectedIndex = 0;
            }
            else
            {
                cbKullaniciAktif.SelectedIndex = 1;
            }

        }

        private void dgvUrunUrunler_DoubleClick(object sender, EventArgs e)
        {
            btnUrunDegistir.Enabled = true;
            btnUrunKaydet.Enabled = false;
            btnUrunSil.Enabled = true;
            txtUrunUrunAdi.Text = dgvUrunUrunler.SelectedRows[0].Cells["UrunAdi"].Value.ToString();
            txtUrunUrunFiyati.Text = dgvUrunUrunler.SelectedRows[0].Cells["UrunFiyati"].Value.ToString();
            txtUrunUrunGrupID.Text = dgvUrunUrunler.SelectedRows[0].Cells["UrunGrupID"].Value.ToString();
            txtUrunUrunAltGrupID.Text = dgvUrunUrunler.SelectedRows[0].Cells["UrunAltGrupID"].Value.ToString();
            cbUrunUrunGruplari.SelectedItem = dgvUrunUrunler.SelectedRows[0].Cells["UrunGrupAdi"].Value;
            cbUrunUrunAltGruplari.SelectedItem = dgvUrunUrunler.SelectedRows[0].Cells["UrunAltGrupAdi"].Value;
            txtUrunID.Text = dgvUrunUrunler.SelectedRows[0].Cells["ID"].Value.ToString();

            if (Convert.ToBoolean(dgvUrunUrunler.SelectedRows[0].Cells[7].Value) == true)
            {
                cbUrunAktif.SelectedIndex = 0;
            }
            else
            {
                cbUrunAktif.SelectedIndex = 1;
            }
        }

        private void dgvSalon_DoubleClick(object sender, EventArgs e)
        {

        }

        private void dgvOdemeTipleri_DoubleClick(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmYonetim_Load(object sender, EventArgs e)
        {
            YetkileriGetir();
            tc1.SelectTab(0);
            cbYetki.SelectedIndex = 0;
        }

        private void btnYetkiYeni_Click(object sender, EventArgs e)
        {
            YetkiTemizle();

            btnYetkiKaydet.Enabled = true;
        }

        private void btnKullaniciYeni_Click(object sender, EventArgs e)
        {
            KullaniciTemizle();
            btnKullaniciKaydet.Enabled = true;
        }

        private void dtpKullaniciTarih_ValueChanged(object sender, EventArgs e)
        {
            txtKullaniciTarih.Text = dtpKullaniciTarih.Value.ToShortDateString();
        }

        private void btnUrunYeni_Click(object sender, EventArgs e)
        {
            UrunTemizle();
            btnUrunKaydet.Enabled = true;
        }

        #region Temizle
        private void UrunTemizle()
        {
            cbUrunUrunGruplari.SelectedIndex = 0;

            cbUrunUrunAltGruplari.SelectedIndex = 0;
            txtUrunUrunAdi.Clear();
            txtUrunUrunFiyati.Clear();
            //txtUrunUrunGrupID.Clear();
            //txtUrunUrunAltGrupID.Clear();
            cbUrunAktif.SelectedIndex = 0;
            btnUrunDegistir.Enabled = false;
            btnUrunSil.Enabled = false;
            txtUrunID.Clear();

        }

        private void KullaniciTemizle()
        {
            txtKullaniciID.Clear();
            txtKullaniciAdi.Clear();
            txtKullaniciSoyadi.Clear();
            txtKullaniciSifre.Clear();
            txtKullaniciTarih.Text = DateTime.Now.ToShortDateString();
            cbKullaniciAktif.SelectedIndex = 0;
            cbKullaniciYetkileri.SelectedIndex = 1;
            btnKullaniciDegistir.Enabled = false;
            btnKullaniciSil.Enabled = false;
        }

        private void YetkiTemizle()
        {
            txtYetkiAdi.Clear();
            txtYetkiID.Clear();
            txtYetkiAdi.Focus();
            cbYetki.SelectedIndex = 0;
            btnYetkiDegistir.Enabled = false;
            btnYetkiSil.Enabled = false;

        }
        #endregion

        private void btnYetkiKaydet_Click(object sender, EventArgs e)
        {
            if (txtYetkiAdi.Text.Trim() == "")
            {
                MessageBox.Show("Yetki adı girmeden kaydetme işlemi yapılamaz.", "Dikkat!");
            }
            else
            {
                cYetkiler cY = new cYetkiler();
                if (cY.YetkiKontrol(txtYetkiAdi.Text))
                {
                    MessageBox.Show("Aynı isimde bir yetki kayıtlı!!");
                    txtYetkiAdi.Focus();
                }
                else if (cY.YetkiKaydet(txtYetkiAdi.Text, cbYetki.SelectedItem.ToString()))
                {
                    MessageBox.Show("Yeni Yetki Kaydedildi.");
                    YetkileriGetir();
                    btnYetkiYeni_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Yetki Kaydedilemedi.", "Dikkat!! HATA");
                }
            }
        }

        private void btnYetkiDegistir_Click(object sender, EventArgs e)
        {
            cYetkiler y = new cYetkiler();
            y.Id = Convert.ToInt32(txtYetkiID.Text);
            y.YetkiAdi = txtYetkiAdi.Text;
            if (cbYetki.SelectedItem.ToString() == "Evet") y.Aktif = true;
            else { y.Aktif = false; }
            if (y.YetkiKontrol(txtYetkiAdi.Text, txtYetkiID.Text))
            {
                MessageBox.Show("Aynı isimde bir yetki kayıtlı!!");
                txtYetkiAdi.Focus();
            }
            else
            {
                if (y.YetkiDegistir(y))
                {
                    MessageBox.Show("Yetki güncellendi.");
                    YetkileriGetir();
                    btnYetkiYeni_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Yetki güncellemede sorun ile karşılaşıldı.");
                }
            }
        }

        private void btnKullaniciKaydet_Click(object sender, EventArgs e)
        {
            cKullanicilar k = new cKullanicilar();
            k.Adi = txtKullaniciAdi.Text;
            k.Soyadi = txtKullaniciSoyadi.Text;
            k.Sifre = txtKullaniciSifre.Text;
            k.KayitTarihi = Convert.ToDateTime(txtKullaniciTarih.Text);
            k.YetkiID = Convert.ToInt32(txtKullaniciYetkiID.Text);
            if (cbKullaniciAktif.SelectedItem.ToString() == "Evet")
            {
                k.Aktif = true;
            }
            else
            {
                k.Aktif = false;
            }
            if (string.IsNullOrEmpty(txtKullaniciAdi.Text) || string.IsNullOrEmpty(txtKullaniciSoyadi.Text) || string.IsNullOrEmpty(txtKullaniciSifre.Text))
            {
                MessageBox.Show("Kullanıcı adı, soyadı, şifre girmeden kayıt yapılamaz.", "Dikkat!");
            }
            else
            {
                if (k.KullaniciKontrol(txtKullaniciAdi.Text, txtKullaniciSoyadi.Text, cbKullaniciYetkileri.SelectedItem.ToString()))
                {
                    MessageBox.Show("Bu kullanıcı zaten kayıtlı", "Dikkat!!");
                    txtKullaniciAdi.Focus();
                }
                else if (k.KullaniciKaydet(k))
                {
                    MessageBox.Show("Yeni Kullanıcı Kaydedildi.");
                    KullanicilariGetir();
                    btnKullaniciYeni_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Kullanıcı kayıtta hata ile karşılaşıldı", "Dikkat!! HATA");
                }
            }
        }

        private void btnKullaniciDegistir_Click(object sender, EventArgs e)
        {
            cKullanicilar k = new cKullanicilar();
            k.Id = Convert.ToInt32(txtKullaniciID.Text);
            k.Adi = txtKullaniciAdi.Text;
            k.Soyadi = txtKullaniciSoyadi.Text;
            k.Sifre = txtKullaniciSifre.Text;
            k.YetkiID = Convert.ToInt32(dgvKullanicilar.SelectedRows[0].Cells["YetkiID"].Value);
            k.KayitTarihi = Convert.ToDateTime(txtKullaniciTarih.Text);
            if (cbKullaniciAktif.SelectedItem.ToString() == "Evet")
            {
                k.Aktif = true;
            }
            else
            {
                k.Aktif = false;
            }
            if (k.KullaniciKontrol(txtKullaniciAdi.Text, txtKullaniciSoyadi.Text, txtKullaniciYetkiID.Text, txtKullaniciID.Text))
            {
                MessageBox.Show("Aynı isimde ve yetkide kullanıcı kayıtlı!!");
                txtKullaniciAdi.Focus();
            }
            else
            {
                if (k.KullaniciDegistir(k))
                {
                    MessageBox.Show("Kullanici güncellendi.");
                    KullanicilariGetir();
                    btnKullaniciYeni_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Kullanici güncellemede sorun ile karşılaşıldı.");
                }
            }
        }

        private void btnKullaniciSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Silmek İstiyor musunuz?", "SİLİNSİN Mİ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cKullanicilar k = new cKullanicilar();
                bool Sonuc = k.KullaniciSil(Convert.ToInt32(txtKullaniciID.Text));
                if (Sonuc)
                {
                    MessageBox.Show("Kullanıcı bilgileri silindi.");
                    KullanicilariGetir();
                    btnKullaniciYeni_Click(sender, e);
                }
            }
        }

        private void btnUrunKaydet_Click(object sender, EventArgs e)
        {
            cUrun u = new cUrun();
            u.UrunAdi = txtUrunUrunAdi.Text;
            u.UrunFiyati = Convert.ToDouble(txtUrunUrunFiyati.Text);
            u.UrunGrupID = Convert.ToInt32(txtUrunUrunGrupID.Text);
            u.UrunAltGrupID = Convert.ToInt32(txtUrunUrunAltGrupID.Text);
            if (cbUrunAktif.SelectedItem.ToString() == "Evet")
            {
                u.Aktif = true;
            }
            else
            {
                u.Aktif = false;
            }
            if (string.IsNullOrEmpty(txtUrunUrunAdi.Text) || string.IsNullOrEmpty(txtUrunUrunFiyati.Text))
            {
                MessageBox.Show("Urun adı ve fiyatı girmeden kayıt yapılamaz.", "Dikkat!");
            }
            else
            {
                if (u.UrunKontrol(txtUrunUrunAdi.Text, txtUrunUrunGrupID.Text, txtUrunUrunAltGrupID.Text))
                {
                    MessageBox.Show("Bu ürün zaten kayıtlı.", "Dikkat!!");
                    txtUrunUrunAdi.Focus();
                }
                //u.UrunGrupID = Convert.ToInt32(txtUrunUrunGrupID.Text);
                //u.UrunAltGrupID = Convert.ToInt32(txtUrunUrunAltGrupID.Text);
                //u.UrunAdi = txtUrunUrunAdi.Text;
                //u.UrunFiyati = Convert.ToDouble(txtUrunUrunFiyati.Text);
                //u.Aktif = u.Aktif;
                if (u.UrunKaydet(u))
                {
                    MessageBox.Show("Yeni ürün Kaydedildi.");
                    UrunleriGetir();
                    btnUrunYeni_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Ürün kayıtta hata ile karşılaşıldı", "Dikkat!! HATA");
                }
            }
        }

        private void btnUrunSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Silmek İstiyor musunuz?", "SİLİNSİN Mİ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cUrun u = new cUrun();
                bool Sonuc = u.UrunSil(Convert.ToInt32(txtUrunID.Text));
                if (Sonuc)
                {
                    MessageBox.Show("Ürün bilgileri silindi.");
                    UrunleriGetir();
                    btnUrunYeni_Click(sender, e);
                }
            }
        }

        private void btnUrunDegistir_Click(object sender, EventArgs e)
        {
            cUrun u = new cUrun();
            u.UrunAltGrupID = Convert.ToInt32(txtUrunUrunAltGrupID.Text);
            u.UrunGrupID = Convert.ToInt32(txtUrunUrunGrupID.Text);
            u.UrunAdi = txtUrunUrunAdi.Text;
            u.Id = Convert.ToInt32(txtUrunID.Text);
            u.UrunFiyati = Convert.ToDouble(txtUrunUrunFiyati.Text);
            if (cbUrunAktif.SelectedItem.ToString() == "Evet")
            {
                u.Aktif = true;
            }
            else
            {
                u.Aktif = false;
            }
            //if (u.UrunKontrol(txtUrunUrunAdi.Text, cbUrunUrunGruplari.SelectedItem.ToString(), cbUrunUrunAltGruplari.SelectedItem.ToString(), txtUrunID.Text))
            if (u.UrunKontrol(txtUrunUrunAdi.Text, txtUrunUrunGrupID.Text, txtUrunUrunAltGrupID.Text, txtUrunID.Text))
            {
                MessageBox.Show("Aynı isim ve grupta ürün kayıtlı!!");
                txtKullaniciAdi.Focus();
            }
            else
            {
                if (u.UrunDegistir(u))
                {
                    MessageBox.Show("Ürün güncellendi.");
                    UrunleriGetir();
                    btnUrunYeni_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Kullanici güncellemede sorun ile karşılaşıldı.");
                }
            }
        }



        private void btnYetkiSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Silmek İstiyor musunuz?", "SİLİNSİN Mİ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cYetkiler k = new cYetkiler();
                bool Sonuc = k.YetkiSil(Convert.ToInt32(txtYetkiID.Text));
                if (Sonuc)
                {
                    MessageBox.Show("Yetki bilgileri silindi.");
                    YetkileriGetir();
                    btnYetkiYeni_Click(sender, e);
                }
            }
        }

        private void cbKullaniciYetkileri_SelectedIndexChanged(object sender, EventArgs e)
        {
            cYetkiler y = new cYetkiler();
            txtKullaniciYetkiID.Text = (y.YetkiIDGetir(cbKullaniciYetkileri.SelectedItem.ToString())).ToString();
        }

        private void frmYonetim_Load_1(object sender, EventArgs e)
        {
            YetkileriGetir();
        }

        private void frmYonetim_FormClosed(object sender, FormClosedEventArgs e)
        {
            cMain.isLogin = false;
        }

        private void btnSatisGetir_Click(object sender, EventArgs e)
        {
            cSatislar cS = new cSatislar();
            dgvSatislar.DataSource = cS.SatislariGetir();
        }

        private void btnSonGununSatislari_Click(object sender, EventArgs e)
        {
            cSatislar cS = new cSatislar();
            dgvSatislar.DataSource = cS.SonGununSatislariniGetir(DateTime.Now.ToShortDateString());
        }
    }
}
