using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace girissayfasi
{
    public partial class paraodeme : Form
    {
        Boolean paraodendi;
        double para;
        public Boolean Paraodendi
        {
            get { return paraodendi; }
        }
        public double Para
        {
            get { return para; }
            set { para = value; }
        }

        public paraodeme()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            paraodendi = true;
            this.Close();
        }

        private void paraodeme_Load(object sender, EventArgs e)
        {
            label1.Text = Convert.ToString(para);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            paraodendi = false;
            this.Close();
        }
    }
}
