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
    /// Interaction logic for Kupovina.xaml
    /// </summary>
    public partial class Kupovina : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;
        public Kupovina()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            DPdatum.Focus();
            PopuniPadajuceListe();
        }
        public Kupovina(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            DPdatum.Focus();
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
            DateTime date = (DateTime)DPdatum.SelectedDate;
            string datum = date.ToString("yyyy-MM-dd");

            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@DatumKupovine", SqlDbType.DateTime).Value = datum;
                cmd.Parameters.Add("@IznosKupovine", SqlDbType.Int).Value = txtIznos.Text;
                cmd.Parameters.Add("@NačinPlaćanja", SqlDbType.NVarChar).Value = txtNacinPlacanja.Text;
                cmd.Parameters.Add("@ArtikalID", SqlDbType.NVarChar).Value = cbArtikal.SelectedValue;
                cmd.Parameters.Add("@KupacID", SqlDbType.NVarChar).Value = cbKupac.SelectedValue;
                cmd.Parameters.Add("@ZaposleniID", SqlDbType.NVarChar).Value = cbZaposleni.SelectedValue;
                cmd.Parameters.Add("@OcenaKupcaID", SqlDbType.NVarChar).Value = cbOcena.SelectedValue;

                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update tblKupovina
                                        set DatumKupovine = @DatumKupovine, IznosKupovine = @IznosKupovine, NačinPlaćanja = @NačinPlaćanja, ArtikalID = @ArtikalID, KupacID = @KupacID,
                                        ZaposleniID=@ZaposleniID, OcenaKupcaID=@OcenaKupcaID where KupovinaID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblKupovina(DatumKupovine, IznosKupovine, NačinPlaćanja, ArtikalID, KupacID, ZaposleniID,OcenaKupcaID)
                                               values (@DatumKupovine, @IznosKupovine,@NačinPlaćanja, @ArtikalID, @KupacID, @ZaposleniID, @OcenaKupcaID )";
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
                string vratiArtikal = @"select ArtikalID, Veličina, Cena, RaspoloživaKoličina, KategorijaID, MarkaID, OcenaKupcaID, Naziv as Artikal from tblArtikal";
                DataTable dtArtikal = new DataTable();
                SqlDataAdapter daArtikal = new SqlDataAdapter(vratiArtikal, konekcija);
                daArtikal.Fill(dtArtikal);
                cbArtikal.ItemsSource = dtArtikal.DefaultView;
                daArtikal.Dispose();
                dtArtikal.Dispose();

                string vratiKupac = @"select KupacID, Email, Ime+Prezime as Kupac from tblKupac";
                SqlDataAdapter daKupac = new SqlDataAdapter(vratiKupac, konekcija);
                DataTable dtKupac = new DataTable();
                daKupac.Fill(dtKupac);
                cbKupac.ItemsSource = dtKupac.DefaultView;
                daKupac.Dispose();
                dtKupac.Dispose();

                string vratiOcena = @"select OcenaKupcaID, Komentar, KupacID, KupovinaID, Ocena, DatumOcene  as OcenaKupca from tblOcenaKupcca";
                SqlDataAdapter daOcena = new SqlDataAdapter(vratiOcena, konekcija);
                DataTable dtOcena = new DataTable();
                daOcena.Fill(dtOcena);
                cbOcena.ItemsSource = dtOcena.DefaultView;
                daOcena.Dispose();
                dtOcena.Dispose();

                string vratiZaposleni = @"select ZaposleniID, JMBG, Adresa, RadnoMesto, Ime+Prezime as Zaposleni from tblZaposleni";
                SqlDataAdapter daZaposleni = new SqlDataAdapter(vratiZaposleni, konekcija);
                DataTable dtZaposleni = new DataTable();
                daZaposleni.Fill(dtZaposleni);
                cbZaposleni.ItemsSource = dtZaposleni.DefaultView;
                daZaposleni.Dispose();
                dtZaposleni.Dispose();

            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuće liste nisu popunjene", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
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
