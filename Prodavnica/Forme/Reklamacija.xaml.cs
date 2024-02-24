using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Interaction logic for Reklamacija.xaml
    /// </summary>
    public partial class Reklamacija : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;
        public Reklamacija()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtRazlog.Focus();
            PopuniPadajuceListe();
        }
        public Reklamacija(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtRazlog.Focus();
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
                DateTime date = (DateTime)DPdatum.SelectedDate;
                string datum = date.ToString("yyyy-MM-dd");
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@DatumPodnošenja", SqlDbType.DateTime).Value = datum;
                cmd.Parameters.Add("@Razlog", SqlDbType.NVarChar).Value =txtRazlog.Text;
                cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = txtStatus.Text;
                cmd.Parameters.Add("@BrojRačuna", SqlDbType.Int).Value = txtBrojRacuna.Text;
                cmd.Parameters.Add("@ZaposleniID", SqlDbType.Int).Value = cbZaposleni.SelectedValue;
                cmd.Parameters.Add("@KupacID", SqlDbType.Int).Value = cbKupac.SelectedValue;
                cmd.Parameters.Add("@ArtikalID", SqlDbType.Int).Value =cbArtikal.SelectedValue;

                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update tblReklamacija
                                        set DatumPodnošenja = @DatumPodnošenja, Razlog = @Razlog, 
                                        Status = @Status, BrojRačuna = @BrojRačuna, ZaposleniID = @ZaposleniID,
                                        KupacID=@KupacID, ArtikalID=@ArtikalID where ReklamacijaID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblReklamacija(DatumPodnošenja, Razlog, Status, BrojRačuna, ZaposleniID, KupacID,ArtikalID)
                                               values (@DatumPodnošenja, @Razlog,@Status, @BrojRačuna, @ZaposleniID, @KupacID, @ArtikalID )";
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

                string vratiZaposleni = @"select ZaposleniID, JMBG, Adresa, RadnoMesto, DatumRođenja, Ime+Prezime as Zaposleni from tblZaposleni";
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
