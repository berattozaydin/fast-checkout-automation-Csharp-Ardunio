using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace girissayfasi
{
    public partial class Giris : Form
    {


        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=marketdbase.accdb");

        public Giris()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
           
            OleDbCommand komut = new OleDbCommand("SELECT * FROM kullanicilar WHERE k_ad='"+textBox1.Text+"' and sifre='"+textBox2.Text+"'", baglanti);
            
            OleDbDataReader okuyucu = komut.ExecuteReader();
            
            if (okuyucu.Read())
            {
                baglanti.Close();
                Form2 ys = new Form2();
                this.Hide();
                ys.ShowDialog();
              
                this.Close();

            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
                textBox1.Text = "";
                textBox2.Text = "";
                baglanti.Close();
            }

           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                this.button1_Click(sender,e);

            }
        }

        private void textbox1_keypress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                this.button1_Click(sender, e);

            }
        }

        private void textbox2_keypress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                this.button1_Click(sender, e);

            }
        }
    }
}
