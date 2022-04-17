using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace girissayfasi
{
    public partial class Form2 : Form
    {
        int band;
        string com;
        Boolean setcom;

        public int Band
        {
            get { return band; }
            set { band = value; }
        }
        public string Com
        {
            get { return com; }
            set { com = value; }
        }
        public Boolean Setcom
        {
            get { return setcom; }
            set { setcom = value; }
        }

        public Form2()
        {
            
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            band=0;
            com="";
            setcom=false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            KullaniciArayuz kulary = new KullaniciArayuz();
            this.Hide();
            kulary.Band = band;
            kulary.Com = com;
            kulary.Setcom = setcom;
            kulary.ShowDialog();
            this.Close();

        }

        public SerialPort getcomtru()
        {
            return serialPort1;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            settings form3 = new settings();
            form3.Band = band;
            form3.Com = com;
            form3.Setcom = setcom;
            form3.ShowDialog();
            band = form3.Band;
            com = form3.Com;
            setcom = form3.Setcom;
            
            if (setcom)
            {
                label1.Text = "Com Port Ayarlandı";
                label1.ForeColor = Color.Green;
                
            }
            else
            {
                label1.Text = "Com Cihazı Ayarlanmadı !";
                label1.ForeColor = Color.Red;
                
            }
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            urunekle formurun = new urunekle();
            formurun.Band = band;
            formurun.Setcom = setcom;
            formurun.Com = com;
            this.Hide();
            formurun.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            kullanici kullaniciform = new kullanici();
            this.Hide();
            kullaniciform.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            fisler formfis = new fisler();
            this.Hide();
            formfis.ShowDialog();
            this.Show();
        }
    }
}
