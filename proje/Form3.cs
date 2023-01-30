using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje
{
    public partial class Form3 : Form
    {
        int id;
        public Form3(int id)
        {
            InitializeComponent();
            this.id = id;
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("*");
        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //tamamlanmamış listele
        {
            baglanti.Open();
            string sorgu = "select seyehatistegi.istekid ,tamamlanmamisseyahat.rezervasyontarihi,\r\n        seyehatistegi.gidilecekkonum, seyehatistegi.binilecekkonum,\r\n        musteri.musteritipi,\r\n        kullanici.isim,kullanici.soyisim,kullanici.telefonno\r\n\r\nfrom tamamlanmamisseyahat \r\nINNER JOIN seyehatistegi ON tamamlanmamisseyahat.tamamlanmamisid = seyehatistegi.istekid\r\ninner join musteri on musteri.musteriid = seyehatistegi.musteriid\r\ninner join kullanici on kullanici.kullaniciid = musteri.musteriid ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();


            baglanti.Open();
            string sorgu2 = "select  seyehatistegi.gidilecekkonum, seyehatistegi.binilecekkonum,\r\n        kullanici.isim,kullanici.soyisim,kullanici.telefonno\r\n\r\nfrom tamamlanmisseyahat \r\nINNER JOIN seyehatistegi ON tamamlanmisseyahat.tamamlanmisid = seyehatistegi.istekid\r\ninner join musteri on musteri.musteriid = seyehatistegi.musteriid\r\ninner join kullanici on kullanici.kullaniciid = musteri.musteriid";
            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(sorgu2, baglanti);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView2.DataSource = ds2.Tables[0];
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e) //istek kabul et
        {

            baglanti.Open();
            NpgsqlCommand cmd3 = new NpgsqlCommand("update  seyehatistegi set surucuid='" + id + "' where istekid='" + dataGridView1.CurrentRow.Cells[0].Value + "' ", baglanti);
            cmd3.ExecuteNonQuery();
            baglanti.Close();

            baglanti.Open();
            NpgsqlCommand cmd5 = new NpgsqlCommand("insert into tamamlanmisseyahat(tamamlanmisid) values('" + dataGridView1.CurrentRow.Cells[0].Value + "') returning tamamlanmisid ", baglanti);
            int tamamid = (int)cmd5.ExecuteScalar();
            baglanti.Close();

            //kart id belirleme
            baglanti.Open();
            NpgsqlCommand cmd6 = new NpgsqlCommand("Select kartid from tamamlanmisseyahat\r\ninner join seyehatistegi on seyehatistegi.istekid = tamamlanmisseyahat.tamamlanmisid\r\ninner join musteri on seyehatistegi.musteriid = musteri.musteriid\r\ninner join kart on musteri.musteriid = kart.musteriid  ", baglanti);
            int kartid = (int)cmd6.ExecuteScalar();
            baglanti.Close();

           
          

            //fatura oluştur

            Random rnd = new Random();
            int sure, bahsis, ucret;
            sure = rnd.Next(20,121); //20 dk- 120 arası 
            bahsis = rnd.Next(10,51); //10 tl 50 tl arasi
            ucret = rnd.Next(40,401); // 40 ile 400 tl arasi


            baglanti.Open();
            NpgsqlCommand cmd4 = new NpgsqlCommand("insert into fatura(kartid,tamamlanmisid,sure,bahsis,ucret) values('"+kartid+"','"+tamamid+"','"+sure+"','"+bahsis+"','"+ucret+"') ", baglanti);
            cmd4.ExecuteNonQuery();
            baglanti.Close();



            baglanti.Open();
            NpgsqlCommand cmd2 = new NpgsqlCommand("delete from tamamlanmamisseyahat where tamamlanmamisid='" + dataGridView1.CurrentRow.Cells[0].Value + "' ", baglanti);
            cmd2.ExecuteNonQuery();
            baglanti.Close();

        }

        private void button5_Click(object sender, EventArgs e) //vardiya ekle
        {

            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("INSERT INTO vardiya(tarih,baslangiczamani,bitiszamani,surucuid) VALUES('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"','"+id+"')  ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("VARDİYA EKLENDİ");
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

        }

        private void button6_Click(object sender, EventArgs e) //Vardiya listele
        {
            baglanti.Open();
            string sorgu = "select * from vardiya";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e) //vardiya seç
        {
            textBox1.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView3.CurrentRow.Cells[3].Value.ToString();
            textBox3.Text = dataGridView3.CurrentRow.Cells[2].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e) //vardiya güncelle
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("UPDATE vardiya SET tarih = '" + textBox1.Text + "', baslangiczamani= '" + textBox2.Text + "', bitiszamani = '"+textBox3.Text+"'  WHERE vardiyaid='" + dataGridView3.CurrentRow.Cells[0].Value + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("GÜNCELLENDİ");
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button3_Click(object sender, EventArgs e) //vardiya sil
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from vardiya  where vardiyaid= '" + dataGridView3.CurrentRow.Cells[0].Value + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("SİLİNDİ");
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

        }

        private void button7_Click(object sender, EventArgs e) //araç ekle
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("INSERT INTO arac(modelyili,renk,kapasite,surucuid) VALUES('" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + id + "')   ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("EKLENDİ");
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();

        }

        private void button11_Click(object sender, EventArgs e) //araç listele
        {
            baglanti.Open();
            string sorgu = "select * from arac  ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView4.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e) //araç seç
        {
            textBox4.Text = dataGridView4.CurrentRow.Cells[1].Value.ToString();
            textBox5.Text = dataGridView4.CurrentRow.Cells[2].Value.ToString();
            textBox6.Text = dataGridView4.CurrentRow.Cells[3].Value.ToString();
        }

        private void button9_Click(object sender, EventArgs e) //araç güncelle
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("UPDATE arac SET modelyili = '" + textBox4.Text + "', renk= '" + textBox5.Text + "', kapasite = '" + textBox6.Text + "'  WHERE aracid='" + dataGridView4.CurrentRow.Cells[0].Value + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("GÜNCELLENDİ");
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private void button8_Click(object sender, EventArgs e) //araç sil
        {

            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from arac  where aracid= '" + dataGridView4.CurrentRow.Cells[0].Value + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("SİLİNDİ");
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private void button13_Click(object sender, EventArgs e) //sigorta ekle
        {
            baglanti.Open();
            NpgsqlCommand cmd5 = new NpgsqlCommand("select aracid from arac where surucuid = '"+id+"'", baglanti);
            int aracid = (int)cmd5.ExecuteScalar();
            baglanti.Close();

            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("INSERT INTO sigorta(aracid,bitistarihi) VALUES('" + aracid + "','" + textBox7.Text + "')  ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("EKLENDİ");
            textBox7.Clear();
        }

        private void button12_Click(object sender, EventArgs e) ///sigorta listele
        {
            baglanti.Open();
            string sorgu = "select * from sigorta INNER JOIN arac on sigorta.aracid = arac.aracid  ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView5.DataSource = ds.Tables[0];
            baglanti.Close();

        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e) //sigorta getir
        {
            textBox7.Text = dataGridView5.CurrentRow.Cells[2].Value.ToString().Substring(0,10);  
        }

        private void button14_Click(object sender, EventArgs e) //sigorta güncelle
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("UPDATE sigorta SET bitistarihi = '" + textBox7.Text + "' WHERE sigortaid='" + dataGridView5.CurrentRow.Cells[0].Value + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("GÜNCELLENDİ");
            textBox7.Clear();

        }

        private void button15_Click(object sender, EventArgs e) //sigorta sil
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from sigorta  where sigortaid= '" + dataGridView5.CurrentRow.Cells[0].Value + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("SİLİNDİ");
            textBox7.Clear();
        }

        private void button16_Click(object sender, EventArgs e) //Ehliyet ekle
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("INSERT INTO ehliyet(bitistarihi,surucuid) VALUES('" + textBox8.Text + "','" +id + "')  ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("EKLENDİ");
            textBox8.Clear();
        }

        private void button17_Click(object sender, EventArgs e) //ehliyet listele
        {
            baglanti.Open();
            string sorgu = "select * from ehliyet inner join surucu on surucu.surucuid = ehliyet.surucuid\r\ninner join kullanici on kullanici.kullaniciid = surucu.surucuid where kullanici.kullaniciid = '"+id+"' ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView6.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e) //seç ehliyet
        {
            textBox8.Text = dataGridView6.CurrentRow.Cells[1].Value.ToString().Substring(0, 10);
        }

        private void button18_Click(object sender, EventArgs e) //ehliyet güncelle
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("UPDATE ehliyet SET bitistarihi = '" + textBox8.Text + "' WHERE ehliyetid='" + dataGridView6.CurrentRow.Cells[0].Value + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("GÜNCELLENDİ");
            textBox8.Clear();

        }

        private void button19_Click(object sender, EventArgs e) //ehliyet sil
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from ehliyet  where ehliyetid= '" + dataGridView6.CurrentRow.Cells[0].Value + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("SİLİNDİ");
            textBox8.Clear();

        }

        private void button20_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 yeni = new Form1();
            yeni.Show();
        }
    }
}
