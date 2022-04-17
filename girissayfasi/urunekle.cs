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
    public partial class urunekle : Form
    {
        
        Boolean dataduzenle = false;
        OleDbConnection baglanti;
        OleDbCommand komut;
        OleDbDataAdapter adaptor;
        DataSet verikumesi;
        int no;
        int band;
        string com;
        Boolean setcom;
        string serialData;
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
        public urunekle()
        {
            InitializeComponent();
        }

     
       
        private void button1_Click(object sender, EventArgs e)
        {
            try {
                if(textBox1.Text!=""&& textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "") { 
                komut = new OleDbCommand();
                baglanti.Open();
                komut.CommandText= "INSERT INTO urunler (barkod_no,ismi,fiyati,adeti)  VALUES (@no,@ismi,@fiy,@ade)";
                komut.Parameters.AddWithValue("@no", Convert.ToUInt32(textBox1.Text));
                komut.Parameters.AddWithValue("@ismi", textBox2.Text);
                komut.Parameters.AddWithValue("@fiy", Convert.ToDouble(textBox3.Text));
                komut.Parameters.AddWithValue("@ade", Convert.ToUInt32(textBox4.Text));
                komut.Connection = baglanti;
                komut.ExecuteNonQuery();
                MessageBox.Show("Ürün Eklendi.");
                baglanti.Close();
                veriyukle();
                }
                else
                {
                    MessageBox.Show("Her hangi bir hane boş bırakılamaz !");
                }
            }
            catch (Exception ex)
            {

                baglanti.Close();
                MessageBox.Show("Aynı barkod numarasında ürün eklediniz !");
            }
            

        }
        void veriyukle()
        {
            adaptor = new OleDbDataAdapter("Select * from urunler", baglanti);
            verikumesi = new DataSet();
            baglanti.Open();
            adaptor.Fill(verikumesi, "urunler");
            dataGridView1.DataSource = verikumesi.Tables["urunler"];
            baglanti.Close();
        }
        private void urunekle_Load(object sender, EventArgs e)
        {
            if (setcom)
            {
                try
                {
                    serialPort1.PortName = com;
                    serialPort1.BaudRate = band;
                    serialPort1.Open();
                    serialPort1.Write("2");
                    serialPort1.DataReceived += new SerialDataReceivedEventHandler(SerialPort1_DataReceived);
                    label7.Text = "Com Port Bağlı.";
                }
                catch (Exception)
                {
                    MessageBox.Show("Com Bağlanırken Bir Hata Oluştu !");
                }
            }
            
            button5.Enabled = false;
            button4.Enabled = false;
            button2.Enabled = false;
            button2.BackColor = Color.DarkOliveGreen;
            baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=marketdbase.accdb");
            veriyukle();
            
        }

        private void urunekle_Closing(object sender, FormClosingEventArgs e)
        {
            if (setcom) { 
            serialPort1.Write("0");
            serialPort1.Close();
            }
        }
       
       
        private void button2_Click(object sender, EventArgs e)
        {
            label6.Text = "Ürün Ekleme Modundasınız";
            dataduzenle = false;
            button3.Enabled = true;
            button2.BackColor = Color.Green;
            button2.Enabled = false;
            button3.BackColor = Color.Yellow;
            button1.Enabled = true;
            button4.Enabled = false;
            button5.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            string kayit = "update urunler set [barkod_no]=@num1,ismi=@ismi,fiyati=@fiy,adeti=@adet where [barkod_no]=@num2";
            komut = new OleDbCommand(kayit,baglanti);
            baglanti.Open();
            
            komut.Parameters.AddWithValue("@num1", Convert.ToUInt32(textBox1.Text));
            komut.Parameters.AddWithValue("@ismi", textBox2.Text);
            komut.Parameters.AddWithValue("@fiy", Convert.ToDouble(textBox3.Text));
            komut.Parameters.AddWithValue("@adet", Convert.ToUInt32(textBox4.Text));
            komut.Parameters.AddWithValue("@num2", no);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıtlar Güncellendi.");
            veriyukle();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            label6.Text = "Ürün Düzünlenme Modundasınız";
            dataduzenle = Enabled;
            button2.Enabled = true;
            button2.BackColor = Color.Lime;
            button3.Enabled = false;
            button3.BackColor = Color.Gold;
            button1.Enabled = false;
            
        }

        private void DataGrid1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
            if (dataduzenle)
            {
                button5.Enabled = true;
                button4.Enabled = true;
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                this.no = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
               
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string kayit = "Delete From urunler where barkod_no=@num1";
            komut = new OleDbCommand(kayit, baglanti);
            komut.Parameters.AddWithValue("@num1", Convert.ToInt32(this.no));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            veriyukle();
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try { 
            serialData = serialPort1.ReadLine();
            this.Invoke(new EventHandler(displayData_event));
            }
            catch (Exception)
            {

            }


        }
        private void displayData_event(object sender, EventArgs e)
        {
            if (button3.Enabled) { textBox1.Text  = serialData ;}
            else
            {
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("SELECT * FROM urunler WHERE barkod_no=@num", baglanti);
                komut.Parameters.AddWithValue("@num", Convert.ToInt32(serialData));

                OleDbDataReader okuyucu = komut.ExecuteReader();

                if (okuyucu.Read())
                {
                    
                    button5.Enabled = true;
                    button4.Enabled = true;
                    textBox1.Text = serialData;
                    this.no = Convert.ToInt32(serialData);
                    //datagridisrow(); Düzeltilmeli !
                    textBox2.Text=okuyucu["ismi"].ToString();
                    textBox3.Text = okuyucu["fiyati"].ToString();
                    textBox4.Text = okuyucu["adeti"].ToString();

                    baglanti.Close();
                }
                else {
                    MessageBox.Show("Girdiğiniz Barkod Numaralı ürün DataBase Kayıtlı Değildir! Düzenleme yapmak için önce ürünü kaydediniz.");
                    baglanti.Close();

                }
            }
        }
        public void datagridisrow() {
        
            for(int i = dataGridView1.Rows.Count; i >= 0; i--) { 
            if (Convert.ToInt32(dataGridView1.Rows[i-1].Cells[0]) == no)
            {
                dataGridView1.Rows[i].Selected = true;
            }
            }
        }

      

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != ',') { 
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
