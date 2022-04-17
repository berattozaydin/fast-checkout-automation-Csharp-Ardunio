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
using System.IO.Ports;

namespace girissayfasi
{
    public partial class KullaniciArayuz : Form
    {
        Boolean paracekmemod;
        int urunsayisi;
        int band;
        string com;
        Boolean setcom;
        OleDbConnection baglanti;
        OleDbCommand komut;
        OleDbDataReader okuyucu;
        string serialData;
        odemebekleniyor formodeme;
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
        public KullaniciArayuz()
        {
            InitializeComponent();
        }
       
        public void buttonkapa()
        {
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            label2.Visible = true;
        }
        public void buttonac()
        {
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            label2.Visible = false;
        }
        private void KullaniciArayuz_Load(object sender, EventArgs e)
        {
            string sWidth = Screen.PrimaryScreen.Bounds.Width.ToString();
            string sHeight = Screen.PrimaryScreen.Bounds.Height.ToString();
            panel1.Location = new Point((Convert.ToInt32(sWidth)-1000)/2, (Convert.ToInt32(sHeight)-715)/2);
            formodeme = new odemebekleniyor();
            if (setcom)
            {
                try
                {
                    serialPort1.PortName = com;
                    serialPort1.BaudRate = band;
                    serialPort1.Open();
                    serialPort1.Write("2");
                    serialPort1.DataReceived += new SerialDataReceivedEventHandler(SerialPort1_DataReceived);
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("Com Bağlanırken Bir Hata Oluştu !");
                }
            }
            paracekmemod = false;
            baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=marketdbase.accdb");
            urunsayisi = 0;
            buttonkapa();
        }
        public Boolean enoughstock(int barkod,int adet)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("SELECT * FROM urunler WHERE barkod_no=@num", baglanti);
            komut.Parameters.AddWithValue("@num", barkod);
            okuyucu = komut.ExecuteReader();
            okuyucu.Read();
            if (Convert.ToInt32(okuyucu["adeti"].ToString()) > adet)
            {
                baglanti.Close();
                return true;
                
            }
            else {
                baglanti.Close();
                return false;
                
            }
        }
        public Boolean issameingrid(string x)
        {
            for(int i = 0; i < urunsayisi; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == x)
                {
                    return true;
                }
            }
            return false;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "1";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "2";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "3";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "4";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "5";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text += "6";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text += "7";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text += "8";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text += "9";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text += "0";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="")
            textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
        }

        private void textbox_1_keypress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="") { 
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("SELECT * FROM urunler WHERE barkod_no=@num", baglanti);
            komut.Parameters.AddWithValue("@num", Convert.ToInt32(textBox1.Text));
            okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())
            {

                if (Convert.ToInt32(okuyucu["adeti"].ToString()) > 0) {
                    if (issameingrid(textBox1.Text))
                    {
                        int i = 0;
                        while (i < urunsayisi)
                        {
                            if(dataGridView1.Rows[i].Cells[0].Value.ToString() == textBox1.Text)
                            break;
                            i++;
                        }
                            double para = Convert.ToDouble(okuyucu["fiyati"].ToString());
                            baglanti.Close();
                            if ( enoughstock(Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString()),Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString())) ) {
                                dataGridView1.Rows[i].Cells[3].Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value.ToString()) + (Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value.ToString()) / Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString()));
                                dataGridView1.Rows[i].Cells[2].Value = 1 + Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());
                                label4.Text = Convert.ToString(Convert.ToDouble(label4.Text) + para);
                            }
                    else
                    {
                         MessageBox.Show("Girdiğiniz ürünün stoğu bulunmamaktadır!");
                    }
                            
                        }
                    else {
                        
                        dataGridView1.Rows.Insert(0, textBox1.Text, okuyucu["ismi"].ToString(), 1, okuyucu["fiyati"].ToString());
                            label4.Text= Convert.ToString(Convert.ToDouble(label4.Text)+ Convert.ToDouble(okuyucu["fiyati"].ToString()));
                        
                        baglanti.Close();
                        urunsayisi++;

                    }
                    
                }
               else
                {
                    MessageBox.Show("Girdiğiniz ürünün stoğu bulunmamaktadır!");
                    baglanti.Close();
                }

                textBox1.Text = "";


            }
            else
            {
                MessageBox.Show("Girdiğiniz Barkod Numarası Hatalıdır!");
                textBox1.Text = "";
                baglanti.Close();

            }
            }
            else
            {
                MessageBox.Show("Barkod No girilmedi !");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (urunsayisi > 0) {
                if (setcom == true) {
                    
                    serialPort1.Write("1");
                serialPort1.Write(label4.Text);
                paracekmemod = true;
                    formodeme.ShowDialog();
                }
                else
                {
                    paraodeme formpara = new paraodeme();
                    formpara.Para = Convert.ToDouble(label4.Text);
                    formpara.ShowDialog();
                    if (formpara.Paraodendi) { 
                        paraodendi();
                    MessageBox.Show("Paranız Ödendi. Alışverişiniz için teşekkür ederiz.");
                    }
                    else
                        MessageBox.Show("Paranız Ödenemedi!");
                }
            }
            else
            {
                MessageBox.Show("Ürün eklemediniz !");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (enoughstock(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString()), Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString()))) {
                double para = (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString()) / Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString()));
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value = Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString())+ (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString())/ Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString()));
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value = 1 + Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString());
                label4.Text = Convert.ToString(Convert.ToDouble(label4.Text) + para);
            }
            else
                MessageBox.Show("Yeteri Kadar Stok Kalmadı !");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString()) != 1) {
                double para = (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString()) / Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString()));
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value = Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString()) - (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString()) / Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString()));
            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString()) - 1;
                label4.Text = Convert.ToString(Convert.ToDouble(label4.Text) - para);
            }
            else
                button2_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label4.Text=Convert.ToString(Convert.ToDouble(label4.Text)-Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString()));
            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            urunsayisi--;
            if (urunsayisi == 0)
                buttonkapa();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            buttonac();
        }

        
        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                serialData = serialPort1.ReadLine();
                this.Invoke(new EventHandler(displayData_event));
            }
            catch (Exception)
            {

            }


        }
        private void programreset()
        {
            urunsayisi = 0;
            dataGridView1.Rows.Clear();
               
            
            buttonkapa();
            paracekmemod = false;
            label4.Text = "0000,00";
            textBox1.Text = "";
        }
        private void displayData_event(object sender, EventArgs e)
        {
            if (paracekmemod == true)
            {
                serialPort1.Write("0");
                
                if (serialData == "1\r") {

                    formodeme.Close();
                    odemebitti formbitti = new odemebitti();
                    formbitti.ShowDialog();
                    paraodendi();

                    
                
                    
                   

                }
                else
                {
                    formodeme.Close();
                    MessageBox.Show("Paranız Ödenemedi!");
                }
                serialPort1.Write("2");
                paracekmemod = false;

            }
            else { 
            textBox1.Text = serialData;
            button16_Click(sender, e);
            }
        }
        public void paraodendi()
        {
            DateTime now = DateTime.Now;
            int barkodno;
            int adet;
            string sorgu = "SELECT * FROM urunler WHERE barkod_no=@num";
            string kayit = "update urunler set adeti=@adet where [barkod_no]=@num";
            string fiskayit = "INSERT INTO fisler ([barkod_no],tarih,adet)  VALUES (@num,@tar,@ad)";
            for (int i = 0; i < urunsayisi; i++)
            {
                baglanti.Open();
                komut = new OleDbCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@num", Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString()));
                okuyucu = komut.ExecuteReader();
                okuyucu.Read();
                barkodno = Convert.ToInt32(okuyucu["barkod_no"].ToString());
                adet = Convert.ToInt32(okuyucu["adeti"].ToString());
                baglanti.Close();
                baglanti.Open();
                komut = new OleDbCommand(kayit, baglanti);
                komut.Parameters.AddWithValue("@adet", Convert.ToInt32(adet - Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString())));
                komut.Parameters.AddWithValue("@num", Convert.ToInt32(barkodno));
                komut.ExecuteNonQuery();
                baglanti.Close();
                baglanti.Open();
                komut = new OleDbCommand(fiskayit, baglanti);
                komut.Parameters.AddWithValue("@num", Convert.ToUInt32(barkodno));
                komut.Parameters.AddWithValue("@tar", Convert.ToString(now));
                komut.Parameters.AddWithValue("@ad", Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString()));
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
            
            programreset();
        }
        private void formclosing(object sender, FormClosingEventArgs e)
        {
            if (setcom)
            {
                serialPort1.Write("0");
                serialPort1.Close();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

       

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
