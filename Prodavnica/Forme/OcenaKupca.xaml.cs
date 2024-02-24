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
using System.Media;

namespace Prodavnica.Forme
{
    /// <summary>
    /// Interaction logic for OcenaKupca.xaml
    /// </summary>
    public partial class OcenaKupca : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;
        public OcenaKupca()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtOcena.Focus();
            PopuniPadajuceListe();
        }
        public OcenaKupca(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtOcena.Focus();
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
                cmd.Parameters.Add("@Ocena", SqlDbType.Int).Value = txtOcena.Text;
                cmd.Parameters.Add("@Komentar", SqlDbType.NVarChar).Value = txtKomentar.Text;
                cmd.Parameters.Add("@DatumOcene", SqlDbType.DateTime).Value = datum;
                cmd.Parameters.Add("@KupacID", SqlDbType.Int).Value = cbKupac.SelectedValue;
                cmd.Parameters.Add("@KupovinaID", SqlDbType.Int).Value =cbKupovina.SelectedValue;

                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update tblOcenaKupcca
                                        set Ocena = @Ocena, Komentar = @Komentar, DatumOcene = @DatumOcene, 
                                        KupacID=@KupacID, KupovinaID = @KupovinaID where OcenaKupcaID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblOcenaKupcca(Ocena, Komentar, DatumOcene, KupacID, KupovinaID)
                                               values (@Ocena, @Komentar,@DatumOcene, @KupacID, @KupovinaID)";
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
                string vratiKupac = @"select KupacID, Email, Ime+Prezime as Kupac from tblKupac";
                SqlDataAdapter daKupac = new SqlDataAdapter(vratiKupac, konekcija);
                DataTable dtKupac = new DataTable();
                daKupac.Fill(dtKupac);
                cbKupac.ItemsSource = dtKupac.DefaultView;
                daKupac.Dispose();
                dtKupac.Dispose();

                string vratiKupovina = @"select KupovinaID, ZaposleniID, KupacID, OcenaKupcaID,  ArtikalID, DatumKupovine, IznosKupovine,
                NačinPlaćanja as Kupovina from tblKupovina";
                DataTable dtKupovina = new DataTable();
                SqlDataAdapter daKupovina = new SqlDataAdapter(vratiKupovina, konekcija);
                daKupovina.Fill(dtKupovina);
                cbKupovina.ItemsSource = dtKupovina.DefaultView;
                daKupovina.Dispose();
                dtKupovina.Dispose();

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
