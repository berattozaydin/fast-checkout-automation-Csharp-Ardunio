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
    public partial class settings : Form
    {


        int band;
        string com;
        Boolean setcom;

        public int Band{
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
        public settings()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://beratozaydin.net");
        }

        private void settings_Load(object sender, EventArgs e)
        {

            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
                comboBox1.Items.Add(port);
            for (byte i = 0; i < 7; i++)
            {
                comboBox2.Items.Add(Convert.ToString(300 * (int)Math.Pow(2, i)));
            }
            comboBox1.SelectedItem = com;
            comboBox2.SelectedItem = Convert.ToString(band);
            setcom = false;
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
           if(comboBox1.Text == "" && comboBox2.Text == "")
            {
                MessageBox.Show("Port veya Band Eksik!");
            }
           else
            {
                try {

                    serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                    serialPort1.Close();
                    MessageBox.Show("Com Port Ayarları Kaydedildi.");
                    com = comboBox1.Text;
                    band = Convert.ToInt32(comboBox2.Text);
                    setcom = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Bağlanılan Com Cihazinda Bir Sorun Oluştu. Ayarlar Kaydedilemiyor !");
                    
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hakkindasayfa frm = new hakkindasayfa();
            frm.Show();
        }
    }
}
