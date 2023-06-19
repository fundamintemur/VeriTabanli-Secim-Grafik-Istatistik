using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Veri_Tabanlı_Seçim_Grafik_İstatistik
{
    public partial class FrmGrafikler : Form
    {
        public FrmGrafikler()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=LENOVO\SQLEXPRESS;Initial Catalog=DBSECIMPROJE;Integrated Security=True");


        private void FrmGrafikler_Load(object sender, EventArgs e)
        {

            //combooxla  ilçe adları getirme
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLILCE", baglanti);
            SqlDataReader da = komut.ExecuteReader();
            while (da.Read())
            {
                comboBox1.Items.Add(da[1].ToString());
            }
            baglanti.Close();
            //label toplam parti oylarını yazma
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select sum(APARTI),sum(BPARTI),sum(CPARTI),sum(DPARTI),sum(EPARTI) FROM TBLILCE", baglanti);
            SqlDataReader dr = komut2.ExecuteReader();
            while (dr.Read())
            {
                //label7.Text = dr[0].ToString();
                //label8.Text = dr[1].ToString();
                //label9.Text = dr[2].ToString();
                //label10.Text = dr[3].ToString();
                //label11.Text = dr[4].ToString();
                chart1.Series["Partiler"].Points.AddXY("A PARTİ", dr[0]);
                chart1.Series["Partiler"].Points.AddXY("B PARTİ", dr[1]);
                chart1.Series["Partiler"].Points.AddXY("C PARTİ", dr[2]);
                chart1.Series["Partiler"].Points.AddXY("D PARTİ", dr[3]);
                chart1.Series["Partiler"].Points.AddXY("E PARTİ", dr[4]);


            }
            baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select sum(APARTI),sum(BPARTI),sum(CPARTI),sum(DPARTI),sum(EPARTI)from TBLILCE WHERE ILCEAD=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", comboBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblA.Value =int.Parse(dr[0].ToString());
                LblB.Value =int.Parse(dr[1].ToString());
                LblC.Value = int.Parse(dr[2].ToString());
                LblD.Value = int.Parse(dr[3].ToString());
                LblE.Value = int.Parse(dr[4].ToString());


                label7.Text = dr[0].ToString();
                label8.Text = dr[1].ToString();
                label9.Text = dr[2].ToString();
                label10.Text = dr[3].ToString();
                label11.Text = dr[4].ToString();
            }
            baglanti.Close();
        }
    }
}
