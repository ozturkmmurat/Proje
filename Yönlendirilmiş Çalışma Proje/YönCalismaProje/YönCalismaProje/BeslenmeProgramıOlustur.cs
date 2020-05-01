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
namespace YönCalismaProje
{
    public partial class BeslenmeProgramıOlustur : Form
    {
       

        public BeslenmeProgramıOlustur()
        {
            InitializeComponent();
        }
        public static string idtut;
        public void BeslenmeProgramıOlustur_Load(object sender, EventArgs e)
        {
            dateTime_Programbaslangic.Visible = false;
            lbl_usbb.Visible = false;
            lbl_usbts.Visible = false;
            lbl_Programsurehesapla.Visible = false;
            lbl_usbb.Text = dateTime_Programbaslangic.Value.ToString("ddd, MMM d, yyyy"); 
            lbl_usbts.Text = dateTime_Programbitis.Value.ToString("ddd, MMM d, yyyy");
            Tablolustur();
        }

        SqlConnection Baglantim = new SqlConnection("Data Source = localhost; Initial Catalog = Yon_Calismaproje; Integrated Security = True");
        void Tablolustur()
        {
            idtut = idtut.ToString();


            SqlConnection Baglantim = new SqlConnection("Data Source = localhost; Initial Catalog = Yon_Calismaproje; Integrated Security = True");
            Baglantim.Open();
            SqlDataAdapter sqlVericekme = new SqlDataAdapter("SELECT No,Program_Baslangic,Program_Bitis,Program_Kalansure,Beslenme_Programi from Beslenme_Programı_tbl where Beslenme_Programı_tbl.id like '" + idtut.ToString() + "%'", Baglantim);
            DataTable sqlVerialma = new DataTable();
            sqlVericekme.Fill(sqlVerialma);
            dataGridBeslenme.DataSource = sqlVerialma;
            Baglantim.Close();

        }
       



        private void btn_Beslenmeprog_Click(object sender, EventArgs e)
        {
            if (txt_Beslenmeekle.Text == "" || txt_Beslenmeekle.Text == null)
            {
                MessageBox.Show("Beslenme programı listesine boş bilgi ekleyemezsiniz !", "Uyarı");
            }
            else
            {
                List_Beslenmeprogrami.Items.Add(txt_Beslenmeekle.Text);
                txt_Beslenmeekle.Clear();
            }
        }

        private void btn_Olustur_Click(object sender, EventArgs e)
        {
            DateTime lbl_usbb2 = Convert.ToDateTime(lbl_usbb.Text);
            DateTime lbl_usbts2 = Convert.ToDateTime(lbl_usbts.Text);

            if (lbl_usbb.Text == lbl_usbts.Text)
            {
                MessageBox.Show("Başlangıç tarihi ile bitiş tarihi aynı olamaz.");
            }
            else if (lbl_usbb2 > lbl_usbts2)
            {

                MessageBox.Show("Bitiş tarihi Başlangıç tarihinden daha küçük olamaz", "Uyarı");
            }
            else if (List_Beslenmeprogrami.Items.Count != 0 && List_Beslenmeprogrami.Items.Count != 0)
            {


                if (lbl_usbb.Text == lbl_usbts.Text)
                {
                    MessageBox.Show("Başlangıç tarihi ile bitiş tarihi aynı olamaz.");
                }
                else if (lbl_usbb2 > lbl_usbts2)
                {

                    MessageBox.Show("Bitiş tarihi Başlangıç tarihinden daha küçük olamaz", "Uyarı");
                }
                else if (List_Beslenmeprogrami.Items.Count != 0 && List_Beslenmeprogrami.Items.Count != 0)
                {


                    string sql = "INSERT  INTO Beslenme_Programı_tbl (id,Program_Baslangic,Program_Bitis,Program_Kalansure,Beslenme_Programi) VALUES (@id,@Program_Baslangic,@Program_Bitis,@Program_Kalansure,@Beslenme_Programi)";
                    string date1 = dateTime_Programbaslangic.Value.ToString("ddd, MMM d, yyyy"); ;
                    string date2 = dateTime_Programbitis.Value.ToString("ddd, MMM d, yyyy"); ;


                    SqlCommand komut2;
                    string beslenmeDeneme;
                    beslenmeDeneme = List_Beslenmeprogrami.Items.ToString();

                    DateTime Programbaslangic;
                    DateTime Programbitis;
                    TimeSpan Programsurehesapla;

                    Programbaslangic = Convert.ToDateTime(dateTime_Programbaslangic.Value.ToShortDateString());
                    Programbitis = Convert.ToDateTime(dateTime_Programbitis.Value.ToShortDateString());

                    Programsurehesapla = Programbitis - Programbaslangic;
                    lbl_Programsurehesapla.Text = (Programsurehesapla.ToString());
                    foreach (string program in List_Beslenmeprogrami.Items)
                    {
                        komut2 = new SqlCommand(sql, Baglantim);
                        komut2.Parameters.AddWithValue("id", idtut.ToString());
                        komut2.Parameters.AddWithValue("Program_Baslangic", date1);
                        komut2.Parameters.AddWithValue("Program_Bitis", date2);
                        komut2.Parameters.AddWithValue("@Program_Kalansure", lbl_Programsurehesapla.Text);                    
                        komut2.Parameters.AddWithValue("@Beslenme_Programi", program);
                        Baglantim.Open();
                        komut2.ExecuteNonQuery();
                        Baglantim.Close();


                    }

                    MessageBox.Show("Kayıt Eklendi");
                }
                else
                {
                    MessageBox.Show("Listbox' ta Veri Yok");
                }
                Tablolustur();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTime_Programbitis_ValueChanged(object sender, EventArgs e)
        {
            lbl_usbts.Text = dateTime_Programbitis.Value.ToString("ddd, MMM d, yyyy"); ;

        }
        void SecerekSil(int Kullanici_İd){
            SqlCommand komut;
            string sql = "DELETE FROM Beslenme_Programı_tbl WHERE No=@No";
            komut = new SqlCommand(sql, Baglantim);
            komut.Parameters.AddWithValue("@No", Kullanici_İd);
            Baglantim.Open();
            komut.ExecuteNonQuery();
            Baglantim.Close();
        }
        private void btn_Sil_Click(object sender, EventArgs e)
        {
          
            foreach (DataGridViewRow drow in dataGridBeslenme.SelectedRows)  //Seçili Satırları Silme
            {
                int numara = Convert.ToInt32(drow.Cells[0].Value);
                SecerekSil(numara);
                
            }
            Tablolustur();
        }

        private void btn_Guncelle_Click(object sender, EventArgs e)
        {
            Baglantim.Open();
            SqlCommand GüncelleKomut = new SqlCommand("update Beslenme_Programı_tbl set Program_Baslangic='" + dataGridBeslenme.CurrentRow.Cells[1].Value.ToString() + "' ,Program_Bitis='" + dataGridBeslenme.CurrentRow.Cells[2].Value.ToString() + "', Program_KalanSure='" + dataGridBeslenme.CurrentRow.Cells[3].Value.ToString() + "' ,Beslenme_Programi='" + dataGridBeslenme.CurrentRow.Cells[4].Value.ToString() + "'where No = '" + dataGridBeslenme.CurrentRow.Cells[0].Value.ToString() + "'", Baglantim);
            GüncelleKomut.ExecuteNonQuery();
            Baglantim.Close();
        }
    }

}

