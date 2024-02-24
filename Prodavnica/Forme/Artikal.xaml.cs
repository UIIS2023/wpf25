using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;

namespace Prodavnica.Forme
{
    /// <summary>
    /// Interaction logic for Artikal.xaml
    /// </summary>
    public partial class Artikal : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;
        public Artikal()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtNaziv.Focus();
            PopuniPadajuceListe();
        }
        public Artikal(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtNaziv.Focus();
            PopuniPadajuceListe();
            this.red = red;
            this.azuriraj = azuriraj;
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
                cmd.Parameters.Add("@Naziv", SqlDbType.NVarChar).Value = txtNaziv.Text;
                cmd.Parameters.Add("@Veličina", SqlDbType.Int).Value = txtVelicina.Text;
                cmd.Parameters.Add("@Cena", SqlDbType.Int).Value = txtCena.Text;
                cmd.Parameters.Add("@RaspoloživaKoličina", SqlDbType.Int).Value = txtRaspolozivaKolicina.Text;
                cmd.Parameters.Add("@KategorijaID", SqlDbType.NVarChar).Value = cbKategorija.SelectedValue;
                cmd.Parameters.Add("@MarkaID", SqlDbType.NVarChar).Value = cbMarka.SelectedValue;
                cmd.Parameters.Add("@OcenaKupcaID", SqlDbType.NVarChar).Value = cbOcenaKupca.SelectedValue;

                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update tblArtikal
                                        set Naziv = @Naziv, Veličina = @Veličina, Cena = @Cena, RaspoloživaKoličina = @RaspoloživaKoličina,
                                        KategorijaID=@KategorijaID, MarkaID=@MarkaID, OcenaKupcaID=@OcenaKupcaID where ArtikalID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblArtikal(Naziv, Veličina, Cena, RaspoloživaKoličina, KategorijaID, MarkaID,OcenaKupcaID)
                                               values (@Naziv, @Veličina,@Cena, @RaspoloživaKoličina, @KategorijaID, @MarkaID, @OcenaKupcaID )";
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
        public void PopuniPadajuceListe()
        {
            try
            {
                konekcija.Open();
                string vratiKategoriju = @"select KategorijaID, RasponCena, 
                                         OpisKategorije, NazivKategorije as Kategorija from tblKategorija";
                DataTable dtKategorija = new DataTable();
                SqlDataAdapter daKategorija = new SqlDataAdapter(vratiKategoriju, konekcija);
                daKategorija.Fill(dtKategorija);
                cbKategorija.ItemsSource = dtKategorija.DefaultView;
                daKategorija.Dispose();
                dtKategorija.Dispose();

                string vratiMarku = @"select MarkaID, NazivMarke as Marka from tblMarka";
                SqlDataAdapter daMarka = new SqlDataAdapter(vratiMarku, konekcija);
                DataTable dtMarka = new DataTable();
                daMarka.Fill(dtMarka);
                cbMarka.ItemsSource = dtMarka.DefaultView;
                daMarka.Dispose();
                dtMarka.Dispose();

                string vratiOcena = @"select OcenaKupcaID, KupovinaID, Ocena, Komentar, KupacID, Ocena, DatumOcene  as OcenaKupca from tblOcenaKupcca";
                SqlDataAdapter daOcena = new SqlDataAdapter(vratiOcena, konekcija);
                DataTable dtOcena = new DataTable();
                daOcena.Fill(dtOcena);
                cbOcenaKupca.ItemsSource = dtOcena.DefaultView;
                daOcena.Dispose();
                dtOcena.Dispose();

            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuće liste nisu popunjene", "GREŠKA", MessageBoxButton.OK, MessageBoxImage.Error);
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
