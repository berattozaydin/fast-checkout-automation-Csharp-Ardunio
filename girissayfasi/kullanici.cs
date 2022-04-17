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
    public partial class kullanici : Form
    {
        string k_ad;
        Boolean guncelleme;
        OleDbConnection baglanti;
        OleDbCommand komut;
        OleDbDataAdapter adaptor;
        DataSet verikumesi;
        public kullanici()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void kullanici_Load(object sender, EventArgs e)
        {
            guncelleme = false;
            baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=marketdbase.accdb");
            veriyukle();
        }
        void veriyukle()
        {
            adaptor = new OleDbDataAdapter("Select * from kullanicilar", baglanti);
            verikumesi = new DataSet();
            baglanti.Open();
            adaptor.Fill(verikumesi, "kullanicilar");
            dataGridView1.DataSource = verikumesi.Tables["kullanicilar"];
            baglanti.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (guncelleme)
            {
                if (textBox1.Text != "" && textBox2.Text != "") { 
                komut = new OleDbCommand();
                komut.CommandText = "Update Kullanicilar set k_ad=@ad1,sifre=@sif where k_ad=@ad";
                komut.Connection = baglanti;
                baglanti.Open();
                
                komut.Parameters.AddWithValue("@ad1", textBox1.Text);
                komut.Parameters.AddWithValue("@sif", textBox2.Text);
                    komut.Parameters.AddWithValue("@ad", k_ad);
                    komut.ExecuteNonQuery();
                baglanti.Close();
                veriyukle();
                }
                else
                {
                    MessageBox.Show("Boş alan bırakılmamalı !");
                }
            }
            else
            {
                MessageBox.Show("Güncelleme yapmak için önce kullanıcı şeçiniz !");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                try {
                    komut = new OleDbCommand();
                    baglanti.Open();
                    komut.CommandText = "Insert Into kullanicilar ([k_ad],sifre) Values (@ad,@sif)";
                    komut.Parameters.AddWithValue("@ad", textBox1.Text);
                    komut.Parameters.AddWithValue("@sif", textBox2.Text);
                    komut.Connection = baglanti;
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    veriyukle();
                    MessageBox.Show("Kullanıcı Eklendi.");
                }
                catch (Exception)
                {
                    MessageBox.Show("Hata");
                }




                baglanti.Close();
            }
            else
            {
                MessageBox.Show("Boş alan bırakılmamalı !");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (guncelleme)
            {
                komut = new OleDbCommand();
                komut.CommandText = "Delete From Kullanicilar where k_ad=@ad";
                komut.Connection = baglanti;
                komut.Parameters.AddWithValue("@ad", k_ad);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                veriyukle();
            }
            else
            {
                MessageBox.Show("Silme yapmak için önce kullanıcı şeçiniz !");
            }
        }

        private void DGrid_RowMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            guncelleme = true;
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            k_ad= dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

        }
    }
}
