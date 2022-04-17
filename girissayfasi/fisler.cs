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
    public partial class fisler : Form
    {
        OleDbConnection baglanti;
        OleDbCommand komut;
        OleDbDataAdapter adaptor;
        DataSet verikumesi;

        void veriyukle()
        {
            adaptor = new OleDbDataAdapter("Select * from fisler", baglanti);
            verikumesi = new DataSet();
            baglanti.Open();
            adaptor.Fill(verikumesi, "fisler");
            dataGridView1.DataSource = verikumesi.Tables["fisler"];
            baglanti.Close();
        }
        public fisler()
        {
            InitializeComponent();
        }

        private void fisler_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=marketdbase.accdb");
            veriyukle();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            komut = new OleDbCommand();
            komut.CommandText = "Delete From fisler where tarih=@tar";
            komut.Connection = baglanti;
            komut.Parameters.AddWithValue("@tar", dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString());
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            veriyukle();

        }
    }
}
