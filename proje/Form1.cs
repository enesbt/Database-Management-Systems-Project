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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        NpgsqlConnection baglanti = new NpgsqlConnection("*");


        private void btnEkle_Click(object sender, EventArgs e)
        {

            string ktip = cboxktipi.SelectedItem.ToString();

            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("INSERT INTO public.kullanici(kullaniciadi,sifre,isim,soyisim,email,telefonno,yas,kullanicitipi)  values('" + txtAdi.Text + "','" + txtPassword.Text + "','" + txtisim.Text + "','" + txtsoyisim.Text + "','" + txtemail.Text + "','" + mtxttelno.Text + "','" + mtxtyas.Text + "','" + ktip + "') returning kullaniciid", baglanti);
            int id = (int)komut.ExecuteScalar();
            baglanti.Close();

            if (ktip == "M")
            {
                string mtip = cboxmtipi.SelectedItem.ToString();
                baglanti.Open();
                NpgsqlCommand komut2 = new NpgsqlCommand("INSERT INTO public.musteri(musteriid,musteriTipi,isteksayac) VALUES ('" + id.ToString() + "','" + mtip + "','"+0+"');", baglanti);
                komut2.ExecuteNonQuery();
                baglanti.Close();
                txtAdi.Clear();
                txtPassword.Clear();
                txtisim.Clear();
                txtsoyisim.Clear();
                mtxttelno.Clear();
                txtemail.Clear();
                mtxttelno.Clear();
                mtxtyas.Clear();
                
            }
            else
            {
                baglanti.Open();
                NpgsqlCommand komut3 = new NpgsqlCommand("INSERT INTO public.surucu(surucuid) VALUES ('" + id.ToString() + "');", baglanti);
                komut3.ExecuteNonQuery();
                baglanti.Close();
                txtAdi.Clear();
                txtPassword.Clear();
                txtisim.Clear();
                txtsoyisim.Clear();
                mtxttelno.Clear();
                txtemail.Clear();
                mtxttelno.Clear();
                mtxtyas.Clear();

            }

        }

        private void btngirisyap_Click(object sender, EventArgs e)
        {
            string adi = txtGirisAdi.Text.ToString();
            string pass = txtGirisPassword.Text.ToString();

            baglanti.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select * from u_login('" + adi + "','" + pass + "')", baglanti);
            int res = (int)cmd.ExecuteScalar();
            baglanti.Close();
            if (res == 0)
            {
                MessageBox.Show("kullanici adi veya şifre hatalı");

            }
            else
            {
                baglanti.Open();
                NpgsqlCommand cmd2 = new NpgsqlCommand("select * from getKtipi('" + res+ "')", baglanti);
                string ktipi = cmd2.ExecuteScalar().ToString();
                baglanti.Close();
                if(ktipi=="M")
                {
                    Form2 yeni = new Form2(res);
                    this.Hide();
                    yeni.Show();
                }

                else
                {

                    Form3 yeni = new Form3(res);
                    this.Hide();
                    yeni.Show();
                }
                                 
            }                     
        }

        private void button2_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            string sorgu = "select * from musteri inner join kullanici on kullanici.kullaniciid = musteri.musteriid ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from kullanici where kullanicitipi = 'M' and " +
              " kullanici.isim like '%" + textBox1.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from kullanici where kullanicitipi ='S'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from kullanici where kullanicitipi = 'S' and " +
           " kullanici.isim like '%" + textBox2.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sorgu = "select fatura.faturaid,fatura.sure,fatura.bahsis,fatura.ucret,\r\n        seyehatistegi.gidilecekkonum,seyehatistegi.binilecekkonum,\r\n        kullanici.isim,kullanici.soyisim,kullanici.kullanicitipi,\r\n        kart.kartnumarasi\r\nfrom fatura\r\ninner join seyehatistegi\r\non fatura.tamamlanmisid = seyehatistegi.istekid\r\ninner join kullanici\r\non seyehatistegi.musteriid = kullanici.kullaniciid\r\nor seyehatistegi.surucuid = kullanici.kullaniciid\r\ninner join kart\r\non fatura.kartid = kart.kartid";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            string sorgu = "select fatura.faturaid,fatura.sure,fatura.bahsis,fatura.ucret,\r\n        seyehatistegi.gidilecekkonum,seyehatistegi.binilecekkonum,\r\n        kullanici.isim,kullanici.soyisim,kullanici.kullanicitipi,\r\n        kart.kartnumarasi\r\nfrom fatura\r\ninner join seyehatistegi\r\non fatura.tamamlanmisid = seyehatistegi.istekid\r\ninner join kullanici\r\non seyehatistegi.musteriid = kullanici.kullaniciid\r\nor seyehatistegi.surucuid = kullanici.kullaniciid\r\ninner join kart\r\non fatura.kartid = kart.kartid\r\n\r\nwhere seyehatistegi.gidilecekkonum like '%"+textBox3.Text+"%' ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string sorgu = "select modelyili,renk,kapasite,isim,soyisim from arac inner join surucu \r\non surucu.surucuid = arac.surucuid \r\ninner join kullanici\r\non kullanici.kullaniciid = surucu.surucuid \r\n";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView4.DataSource = ds.Tables[0];
            baglanti.Close();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string sorgu = "select modelyili,renk,kapasite,isim,soyisim from arac inner join surucu \r\non surucu.surucuid = arac.surucuid \r\ninner join kullanici\r\non kullanici.kullaniciid = surucu.surucuid \r\n where kullanici.isim like '%"+textBox4.Text+"%' ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView4.DataSource = ds.Tables[0];
            baglanti.Close();

        }

        private void button9_Click(object sender, EventArgs e) //il ekle
        {
            baglanti.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("call ilekle('"+textBox5.Text+"')", baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("il eklendi");
            textBox5.Text = "";
        }

        private void button10_Click(object sender, EventArgs e) //ilce ekle
        {
            baglanti.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("call ilceekle('" + textBox6.Text + "','"+textBox7.Text+"')", baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("ilce eklendi");
            textBox6.Text = "";
            textBox7.Text = "";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("call musterisil()", baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("SİLİNDİ");
         
        }

        private void button12_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("call ehliyetsil()", baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("SİLİNDİ");
        }

        private void button13_Click(object sender, EventArgs e) //kullanici listele
        {

            string sorgu = "select * from kullanici";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView5.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void button14_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("delete from kullanici where kullaniciid= '" + dataGridView5.CurrentRow.Cells[0].Value + "'", baglanti);
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kullanici silindi");
        }

        private void button15_Click(object sender, EventArgs e) //eski araç listele
        {
            string sorgu = "select * from eskiarac";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView6.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from eskikullanici";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView7.DataSource = ds.Tables[0];
            baglanti.Close();
        }
    }
}
