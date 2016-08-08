using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restoran
{
    public partial class frmIntro : Form
    {
        public frmIntro()
        {
            InitializeComponent();
        }

        private void timerStart_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1.0)   // Eğer formun opacity değer % 100 den az ise;
            {

                this.Opacity += 0.06;   // bu değeri % 10 arttır..
            }
            else
            {
                timerStart.Enabled = false; // % 100 olduğunda timer duruyor.
                timerClose.Enabled = true; // Butona basıldığı anda timer kapanış (timerclose) tetikleniyor.
            }
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0.0)    // Kapanıış timeri için eğer değer % 0?dan büyük ise;
            {

                this.Opacity -= 0.07;   // Yüzde 10 olarak azalt..
            }
            else
            {
              
              
                timerClose.Enabled = false; // Yüzde sıfır değerine ulaşıldığında, bu kısım çalışıyor, timer kapama               duruyor.
               

               
                this.Close();  // Forum kapatılıyor.
                frmMain frm = new frmMain();
                frm.ShowDialog();
            }
        }
        private void frmIntro_Load(object sender, EventArgs e)
        {
            timerStart.Enabled = true; // Form load olayında öncelikle form açılış timeri başlatılıyor.

            this.Opacity = 0.0;  // Form load olurken ilk anda opacity değeri 0.0 yani yüzde 0 veriliyor.
        }

        private void frmIntro_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMain frm = new frmMain();
            frm.ShowDialog();
        }
    }
}
