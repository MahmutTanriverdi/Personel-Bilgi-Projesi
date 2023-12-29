﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp3
{
    public partial class FrmAnaForm : Form
    {
        public FrmAnaForm()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;

        SqlConnection baglanti = new SqlConnection("server=localhost; Initial Catalog=PersonelVeriTabani;Integrated Security=SSPI");

        void temizle()
        {
            txtId.Text="";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtMeslek.Text = "";
            mskMaas.Text = "";
            cmbSehir.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            txtAd.Focus();

        }
        void griddoldur()
        {
            con = new SqlConnection("server=localhost; Initial Catalog=PersonelVeriTabani;Integrated Security=SSPI");
            da = new SqlDataAdapter("Select * From Tbl_Personel", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "Tbl_Personel");
            dataGridView1.DataSource = ds.Tables["Tbl_Personel"];
            con.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Personel(PerAd,PerSoyad,PerSehir,PerMaas,PerMeslek,PerDurum) values (@p1,@p2,@p3,@p4,@p5,@p6)",baglanti);
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", cmbSehir.Text);
            komut.Parameters.AddWithValue("@p4", mskMaas.Text);
            komut.Parameters.AddWithValue("@p5", txtMeslek.Text);
            komut.Parameters.AddWithValue("@p6", label8.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Eklendi");
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                label8.Text = "True";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked == true)
            {
                label8.Text = "False";
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbSehir.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            mskMaas.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            label8.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtMeslek.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            if(label8.Text == "True")
            {
                radioButton1.Checked = true;
            }
            if(label8.Text == "False")
            {
                radioButton2.Checked = true;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutsil = new SqlCommand("Delete from Tbl_Personel Where PerId=@k1",baglanti);
            komutsil.Parameters.AddWithValue("@k1", txtId.Text);
            komutsil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Silindi");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutGuncelle = new SqlCommand("Update Tbl_Personel Set PerAd = @a1," +
                "PerSoyad = @a2, PerSehir = @a3, PerMaas = @a4, PerDurum = @a5, PerMeslek = @a6 where PerId = @a7",baglanti);
            komutGuncelle.Parameters.AddWithValue("@a1", txtAd.Text);
            komutGuncelle.Parameters.AddWithValue("@a2", txtSoyad.Text);
            komutGuncelle.Parameters.AddWithValue("@a3", cmbSehir.Text);
            komutGuncelle.Parameters.AddWithValue("@a4", mskMaas.Text);
            komutGuncelle.Parameters.AddWithValue("@a5", label8.Text);
            komutGuncelle.Parameters.AddWithValue("@a6", txtMeslek.Text);
            komutGuncelle.Parameters.AddWithValue("@a7", txtId.Text);
            komutGuncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Güncellendi");
        }

        private void btnIstatistik_Click(object sender, EventArgs e)
        {
            Frmistatistik fr = new Frmistatistik();
            fr.Show();
        }

        private void btnGrafikler_Click(object sender, EventArgs e)
        {
            FrmGrafikler frg = new FrmGrafikler();
            frg.Show();
        }
    }
}
