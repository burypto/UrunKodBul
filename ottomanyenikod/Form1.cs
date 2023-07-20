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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ottomanyenikod
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=OcstrLogo;Initial Catalog=ottoman;User ID=sa;Password=Logo123");
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void verilerigoster(string veriler)
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            verilerigoster(" select * from ottomankod");
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand cmd = new SqlCommand("insert into ottomankod (ParcaKodu,ÜrünMüşterileri,UretimTezgahi) values (@kod,@musteri,@tezgah)", baglan);
            cmd.Parameters.AddWithValue("@kod", textBox1.Text);
            cmd.Parameters.AddWithValue("@musteri", textBox2.Text);
        
            cmd.Parameters.AddWithValue("@tezgah", textBox7.Text);
            cmd.ExecuteNonQuery();
            verilerigoster("Select *From ottomankod");
            baglan.Close();

            textBox1.Clear();
            textBox2.Clear();
          
            textBox7.Clear();

            textBox1.Focus();
        }
        
        

        private void btnsil_Click(object sender, EventArgs e)
        {
            
                baglan.Open();

                // Diğer kodlar...

                // Satırı silmek için SqlCommand nesnesini kullanın
                SqlCommand deleteCmd = new SqlCommand("DELETE FROM ottomankod WHERE ParcaKodu = @kod", baglan);
                deleteCmd.Parameters.AddWithValue("@kod", textBox8.Text);
                deleteCmd.ExecuteNonQuery();

                // Diğer kodlar...

                baglan.Close();

                textBox1.Clear();
                textBox2.Clear();
                
                textBox7.Clear();

                textBox1.Focus();
            

        }
        private void VerileriAra(string arananKelime)
        {
            DataTable dt = new DataTable();
            
            SqlCommand komut = new SqlCommand("SELECT * FROM ottomankod WHERE ParcaKodu LIKE @arananKelime ", baglan);
            komut.Parameters.AddWithValue("@arananKelime", "%" + arananKelime + "%");
            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(dt);
            baglan.Close();

            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglan.Open();
          
            string arananKelime = textBox9.Text; // Eklenen ParcaKodu'nu aranacak kelime olarak kullanın
            VerileriAra(arananKelime);

            baglan.Close();
            textBox1.Clear();
            textBox2.Clear();
           
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();

            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Veritabanı bağlantısını aç
            baglan.Open();

            // SQL sorgusu için parametreleri belirle
            string query = "UPDATE ottomankod SET ParcaKodu=@ParcaKodu, ÜrünMüşterileri=@UrunMusterileri, MalzemeCinsi=@MalzemeCinsi, MalzemeKesiti=@MalzemeKesiti, NetAğırlık=@NetAgirlik, KesimAğırlığı=@KesimAgirligi, UretimTezgahi=@UretimTezgahi WHERE ParcaKodu=@ParcaKoduParam";

            // SqlCommand nesnesini oluştur ve parametreleri ekle
            SqlCommand komut = new SqlCommand(query, baglan);
            komut.Parameters.AddWithValue("@ParcaKodu", textBox1.Text);
            komut.Parameters.AddWithValue("@UrunMusterileri", textBox2.Text);
   
            komut.Parameters.AddWithValue("@UretimTezgahi", textBox7.Text);
            komut.Parameters.AddWithValue("@ParcaKoduParam", textBox1.Text);

            // SQL sorgusunu çalıştır
            komut.ExecuteNonQuery();

            // Veritabanı bağlantısını kapat
            baglan.Close();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilialan = dataGridView1.SelectedCells[0].RowIndex;
            string ParcaKodu = dataGridView1.Rows[secilialan].Cells[0].Value.ToString();
            string ÜrünMüşterileri = dataGridView1.Rows[secilialan].Cells[1].Value.ToString();
           
            string UretimTezgahi = dataGridView1.Rows[secilialan].Cells[2].Value.ToString();

            textBox1.Text = ParcaKodu;
            textBox2.Text = ÜrünMüşterileri;
         
            textBox7.Text = UretimTezgahi;


        }
    }
}
