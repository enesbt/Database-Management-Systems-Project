using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace proje
{
    public partial class Form2 : Form
    {
        int id;
        public Form2(int id)
        {
            InitializeComponent();
            this.id = id;
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("*");
        private void Form2_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("SELECT iladi from il",baglanti);
            komut.CommandType = CommandType.Text;
            baglanti.Close();

            baglanti.Open();
            NpgsqlDataReader dr  =  komut.ExecuteReader();
            while (dr.Read())
            {
                iller.Items.Add(dr["iladi"]);
            }


            baglanti.Close();

            baglanti.Open();
            string sorgu = "select adresid,kullaniciid,il.iladi,ilce.ilceadi,acikadres from adres INNER JOIN il on il.ilid = adres.ilid INNER JOIN ilce on ilce.ilceid = adres.ilceid";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ilid, ilceid;

            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("SELECT * from getilceid('"+textBox1.Text+"')",baglanti);
            ilceid = (int)komut.ExecuteScalar();
            baglanti.Close();

            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("SELECT * from getilid('" + iller.SelectedItem.ToString() + "')",baglanti);
            ilid = (int)komut2.ExecuteScalar();
            baglanti.Close();



            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("INSERT INTO adres(kullaniciid,ilid,ilceid,acikadres) values('"+id+"','" + ilid + "','" + ilceid + "','" + richTextBox1.Text + "')",baglanti);
            komut3.ExecuteNonQuery();
            baglanti.Close();




            baglanti.Open();
            string sorgu = "select adresid,kullaniciid,il.iladi,ilce.ilceadi,acikadres from adres INNER JOIN il on il.ilid = adres.ilid INNER JOIN ilce on ilce.ilceid = adres.ilceid";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();

            MessageBox.Show("adres eklendi");
            textBox1.Text = "";

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            iller.SelectedItem = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            richTextBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e) //adres güncelle
        {

            int ilid, ilceid;

            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("SELECT * from getilceid('" + textBox1.Text + "')", baglanti);
            ilceid = (int)komut.ExecuteScalar();
            baglanti.Close();

            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("SELECT * from getilid('" + iller.SelectedItem.ToString() + "')", baglanti);
            ilid = (int)komut2.ExecuteScalar();
            baglanti.Close();




            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("UPDATE adres SET ilid = '" + ilid + "', ilceid= '" + ilceid + "',acikadres='" + richTextBox1.Text + "' WHERE adresid='"+ dataGridView1.CurrentRow.Cells[0].Value +"'" , baglanti);
            komut3.ExecuteNonQuery();
            baglanti.Close();

            baglanti.Open();
            string sorgu = "select adresid,kullaniciid,il.iladi,ilce.ilceadi,acikadres from adres INNER JOIN il on il.ilid = adres.ilid INNER JOIN ilce on ilce.ilceid = adres.ilceid";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e) //adres sil
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("delete from adres where adresid= '"+ dataGridView1.CurrentRow.Cells[0].Value + "'", baglanti);
            komut3.ExecuteNonQuery();
            baglanti.Close();


            baglanti.Open();
            string sorgu = "select adresid,kullaniciid,il.iladi,ilce.ilceadi,acikadres from adres INNER JOIN il on il.ilid = adres.ilid INNER JOIN ilce on ilce.ilceid = adres.ilceid";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e) //kart ekle
        {

            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("INSERT INTO kart(cvv,kartnumarasi,bitistarihi,faturaadresi,musteriid) values ('"+maskedTextBox2.Text+"','"+maskedTextBox1.Text+"','"+textBox2.Text+"','"+dataGridView1.CurrentRow.Cells[0].Value+"','"+id+"') ", baglanti);
            komut3.ExecuteNonQuery();
            baglanti.Close();

        }

        private void button7_Click(object sender, EventArgs e) //kart listele
        {

            baglanti.Open();
            string sorgu = "select kartid,cvv,bitistarihi,kartnumarasi from kart";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            baglanti.Close();
        }

     


        private void button5_Click(object sender, EventArgs e) //kart güncelle
        {

            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("UPDATE kart SET cvv = '" + maskedTextBox2.Text + "', kartnumarasi= '" + maskedTextBox1.Text + "',bitistarihi='" + textBox2.Text + "' WHERE kartid='" + dataGridView2.CurrentRow.Cells[0].Value + "'", baglanti);
            komut3.ExecuteNonQuery();
            baglanti.Close();


        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e) //kart seç
        {
            maskedTextBox1.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            textBox2.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString().Substring(0,10);
            maskedTextBox2.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e) //kart sil
        {

            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("delete from kart where kartid= '" + dataGridView2.CurrentRow.Cells[0].Value + "'", baglanti);
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kart silindi");
            maskedTextBox1.Text = "";
            textBox2.Text = "";
            maskedTextBox2.Text = "";
        }

        private void button8_Click(object sender, EventArgs e)  //seyahat isteği ekle
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("INSERT INTO seyehatistegi(musteriid,gidilecekkonum,binilecekkonum) VALUES ('"+id+"','"+richTextBox2.Text+"','"+richTextBox3.Text+"') returning istekid ", baglanti);
            int istekid = (int)komut.ExecuteScalar();
            baglanti.Close();

            baglanti.Open();
            string tarih = DateTime.Now.ToString("yyyy.MM.dd");

            NpgsqlCommand komut2 = new NpgsqlCommand("INSERT INTO tamamlanmamisseyahat(tamamlanmamisid,rezervasyontarihi) VALUES ('" + istekid + "','" +tarih+ "')  ", baglanti);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            richTextBox2.Text = "";
            richTextBox3.Text = "";
        }

        private void button9_Click(object sender, EventArgs e) //seyahat listele
        {

            baglanti.Open();
            string sorgu = "select istekid,gidilecekkonum,binilecekkonum from seyehatistegi";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            baglanti.Close();

        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e) //seyahat getir
        {
            richTextBox2.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
            richTextBox3.Text = dataGridView3.CurrentRow.Cells[2].Value.ToString();
          
        }

        private void button10_Click(object sender, EventArgs e) //seyahat güncelle
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("UPDATE seyehatistegi SET gidilecekkonum = '" + richTextBox2.Text + "', binilecekkonum= '" + richTextBox3.Text + "'  WHERE istekid='" + dataGridView3.CurrentRow.Cells[0].Value + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("GÜNCELLENDİ");
            richTextBox2.Text = "";
            richTextBox3.Text = "";
        }

        private void button11_Click(object sender, EventArgs e) //seyahat sil
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from seyehatistegi  where istekid= '" + dataGridView3.CurrentRow.Cells[0].Value + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("İSTEK SİLİNDİ");
            richTextBox2.Text = "";
            richTextBox3.Text = "";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
