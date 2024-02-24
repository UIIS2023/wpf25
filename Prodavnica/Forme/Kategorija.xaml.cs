using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Prodavnica.Forme
{
    /// <summary>
    /// Interaction logic for Kategorija.xaml
    /// </summary>
    public partial class Kategorija : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;
        public Kategorija()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtNaziv.Focus();
        }
        public Kategorija(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtNaziv.Focus();
            this.red = red;
            this.azuriraj = azuriraj; ;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@NazivKategorije", SqlDbType.NVarChar).Value = txtNaziv.Text;
                cmd.Parameters.Add("@OpisKategorije", SqlDbType.NVarChar).Value = txtOpis.Text;
                cmd.Parameters.Add("@RasponCena", SqlDbType.Int).Value = int.Parse(txtRaspon.Text);

                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update tblKategorija
                                        set NazivKategorije = @NazivKategorije, OpisKategorije = @OpisKategorije, 
                                        RasponCena = @RasponCena where KategorijaID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblKategorija(NazivKategorije, OpisKategorije, RasponCena)
                                               values (@NazivKategorije, @OpisKategorije,@RasponCena )";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
    }
}
