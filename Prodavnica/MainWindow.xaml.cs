using Prodavnica.Forme;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Prodavnica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string ucitanaTabela; //cuvace nazive tabela
        Konekcija kon = new Konekcija(); //objekat za povezivanje sa bazom
        SqlConnection konekcija = new SqlConnection(); //povezivanje sa sql
        private bool azuriraj;
        private DataRowView red; //pamti podatke iz reda

        #region select
        private static string artikalSelect = @"select ArtikalID as ID, Naziv as Naziv, Veličina as Veličina, Cena as Cena,
                                      RaspoloživaKoličina as 'Raspoloživa količina' 
                                      from tblArtikal
                                      join tblKategorija on tblArtikal.KategorijaID = tblKategorija.KategorijaID
                                      join tblMarka on tblArtikal.MarkaID = tblMarka.MarkaID
                                      join tblOcenaKupcca on tblArtikal.OcenaKupcaID = tblOcenaKupcca.OcenaKupcaID";

        private static string kategorijaSelect = @"select KategorijaID as ID, NazivKategorije as 'Naziv kategorije', 
                                                OpisKategorije as 'Opis kategorije', RasponCena as 'Raspon cena(max)' 
                                                from tblKategorija";

        private static string kupacSelect = @"select KupacID as ID, Ime as Ime, Prezime as Prezime, Email as Email from tblKupac";

        private static string kupovinaSelect = @"select tblKupovina.KupovinaID as ID, tblKupovina.DatumKupovine as 'Datum kupovine', 
                                                tblKupovina.IznosKupovine as 'Iznos kupovine', tblKupovina.NačinPlaćanja as 'Način plaćanja' 
                                                from tblKupovina
                                               join tblZaposleni on tblKupovina.ZaposleniID = tblZaposleni.ZaposleniID
                                               join tblKupac on tblKupovina.KupacID = tblKupac.KupacID
                                               join tblArtikal on tblKupovina.ArtikalID = tblArtikal.ArtikalID
                                               join tblOcenaKupcca on tblKupovina.OcenaKupcaID = tblOcenaKupcca.OcenaKupcaID";

        private static string markaSelect = @"select markaID as ID, NazivMarke as 'Naziv marke' from tblMarka";

        private static string ocenaKupcaSelect = @"select tblOcenaKupcca.OcenaKupcaID as ID, tblOcenaKupcca.Ocena as Ocena, tblOcenaKupcca.Komentar as Komentar, 
                                               tblOcenaKupcca.DatumOcene as 'Datum ocene' 
                                               from tblOcenaKupcca
                                               join tblKupac on tblOcenaKupcca.KupacID = tblKupac.KupacID
                                               join tblKupovina on tblOcenaKupcca.KupovinaID = tblKupovina.KupovinaID";

        private static string reklamacijaSelect = @"select ReklamacijaID as ID, Razlog as 'Razlog reklamacije', 
                                               DatumPodnošenja as 'Datum podnošenja', Status as 'Status reklamacije',
                                               BrojRačuna as 'Broj računa' 
                                               from tblReklamacija 
                                               join tblArtikal on tblReklamacija.ArtikalID = tblArtikal.ArtikalID
                                               join tblZaposleni on tblReklamacija.ZaposleniID = tblZaposleni.ZaposleniID 
                                               join tblKupac on tblReklamacija.KupacID = tblKupac.KupacID";

        private static string zaposleniSelect = @"select ZaposleniID as ID, Ime as Ime, Prezime as Prezime, JMBG as JMBG, 
                                                Adresa as Adresa, RadnoMesto as 'Radno mesto', DatumRođenja as 'Datum rođenja' 
                                                from tblZaposleni";
        #endregion

        #region select+uslov
        private static string artikalSelectUslov = @"select * from tblArtikal where ArtikalID=";
        private static string kategorijaSelectUslov = @"select * from tblKategorija where KategorijaID=";
        private static string kupacSelectUslov = @"select * from tblKupac where KupacID=";
        private static string kupovinaSelectUslov = @"select * from tblKupovina where KupovinaID=";
        private static string markaSelectUslov = @"select * from tblMarka where MarkaID =";
        private static string ocenaKupcaSelectUslov = @"select * from tblOcenaKupcca where OcenaKupcaID=";
        private static string reklamacijaSelectUslov = @"select * from tblReklamacija where ReklamacijaID=";
        private static string zaposleniSelectUslov = @"select * from tblZaposleni where ZaposleniID=";
        #endregion

        #region delete 
        private static string artikalDelete = @"delete from tblArtikal where ArtikalID=";
        private static string kategorijaDelete = @"delete from tblKategorija where KategorijaID=";
        private static string kupacDelete = @"delete from tblKupac where KupacID=";
        private static string kupovinaDelete = @"delete from tblKupovina where KupovinaID=";
        private static string markaDelete = @"delete from tblMarka where MarkaID =";
        private static string ocenaKupcaDelete = @"delete from tblOcenaKupcca where OcenaKupcaID=";
        private static string reklamacijaDelete = @"delete from tblReklamacija where ReklamacijaID=";
        private static string zaposleniDelete = @"delete from tblZaposleni where ZaposleniID=";
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            UcitajPodatke(artikalSelect);
        }
        private void UcitajPodatke(string selectUpit)
        {
            try
            {
                konekcija.Open(); //otvaranje konekcije sa bazom
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectUpit, konekcija); //objekat koji ce izvrsiti upit nad bazom
                DataTable dataTable = new DataTable(); //objekat koji će sadržati podatke iz baze
                dataAdapter.Fill(dataTable); //popunjava se tabela
                if (grid != null)
                {
                    grid.ItemsSource = dataTable.DefaultView; //dodeljuje ItemsSource svojstvo kontrolnog elementa grid
                                                              //izvoru podataka dataTable.DefaultView
                                                              //koristi se za prikazivanje podataka iz tabele
                }
                ucitanaTabela = selectUpit; // čuvanje prosleđenog SQL upita
                dataAdapter.Dispose(); //oslobadja resurse
                dataTable.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "GREŠKA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        //top grid
        private void btnArtikal(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(artikalSelect);
        }

        private void btnMarka(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(markaSelect);
        }

        private void btnKupac(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(kupacSelect);
        }

        private void btnZaposleni(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(zaposleniSelect);
        }

        private void btnReklamacija(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(reklamacijaSelect);
        }

        private void btnKategorija(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(kategorijaSelect);
        }

        private void btnOcenaKupca(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(ocenaKupcaSelect);
        }

        private void btnKupovina(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(kupovinaSelect);
        }

        //left grid

        private void btnAdd(object sender, RoutedEventArgs e)
        {
            Window prozor;

            if (ucitanaTabela.Equals(artikalSelect)) //proverava se da li je to zeljena forma
            {
                prozor = new Artikal(); //prozor tipa artikal
                prozor.ShowDialog(); //otvara se prozor-forma
                UcitajPodatke(artikalSelect); //osevzavaju se podaci u tabeli
            }
            else if (ucitanaTabela.Equals(kategorijaSelect))
            {
                prozor = new Kategorija();
                prozor.ShowDialog();
                UcitajPodatke(kategorijaSelect);
            }
            else if (ucitanaTabela.Equals(kupacSelect))
            {
                prozor = new Kupac();
                prozor.ShowDialog();
                UcitajPodatke(kupacSelect);
            }
            else if (ucitanaTabela.Equals(kupovinaSelect))
            {
                prozor = new Kupovina();
                prozor.ShowDialog();
                UcitajPodatke(kupovinaSelect);
            }
            else if (ucitanaTabela.Equals(markaSelect))
            {
                prozor = new Marka();
                prozor.ShowDialog();
                UcitajPodatke(markaSelect);
            }
            else if (ucitanaTabela.Equals(ocenaKupcaSelect))
            {
                prozor = new OcenaKupca();
                prozor.ShowDialog();
                UcitajPodatke(ocenaKupcaSelect);
            }
            else if (ucitanaTabela.Equals(reklamacijaSelect))
            {
                prozor = new Reklamacija();
                prozor.ShowDialog();
                UcitajPodatke(reklamacijaSelect);
            }
            else if (ucitanaTabela.Equals(zaposleniSelect))
            {
                prozor = new Zaposleni();
                prozor.ShowDialog();
                UcitajPodatke(zaposleniSelect);
            }
        }

        private void btnEdit(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(artikalSelect))
            {
                Izmena(grid, artikalSelectUslov);
                UcitajPodatke(artikalSelect);
            }
            else if (ucitanaTabela.Equals(kategorijaSelect))
            {
                Izmena(grid, kategorijaSelectUslov); ;
                UcitajPodatke(kategorijaSelect);
            }
            else if (ucitanaTabela.Equals(kupacSelect))
            {
                Izmena(grid, kupacSelectUslov);
                UcitajPodatke(kupacSelect);
            }
            else if (ucitanaTabela.Equals(kupovinaSelect))
            {
                Izmena(grid, kupovinaSelectUslov);
                UcitajPodatke(kupovinaSelect);
            }
            else if (ucitanaTabela.Equals(markaSelect))
            {
                Izmena(grid, markaSelectUslov);
                UcitajPodatke(markaSelect);
            }
            else if (ucitanaTabela.Equals(ocenaKupcaSelect))
            {
                Izmena(grid, ocenaKupcaSelectUslov);
                UcitajPodatke(ocenaKupcaSelect);
            }
            else if (ucitanaTabela.Equals(reklamacijaSelect))
            {
                Izmena(grid, reklamacijaSelectUslov);
                UcitajPodatke(reklamacijaSelect);
            }
            else if (ucitanaTabela.Equals(zaposleniSelect))
            {
                Izmena(grid, zaposleniSelectUslov);
                UcitajPodatke(zaposleniSelect);
            }
        }
        private void Izmena(DataGrid grid,string selectUslov)
        {
            try
            { 
                konekcija.Open();
                azuriraj = true;
                red = (DataRowView)grid.SelectedItems[0]; //selektovan red cuvamo u "red"
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                cmd.CommandText = selectUslov + "@id";
                SqlDataReader citac = cmd.ExecuteReader(); //omogućava iteraciju kroz rezultate
                                                           //upita za čitanje podataka iz baze

                if (citac.Read())
                {
                    if (ucitanaTabela.Equals(artikalSelect))
                    {
                        Artikal prozorArtikal = new Artikal(azuriraj, red);
                        prozorArtikal.txtNaziv.Text = citac["Naziv"].ToString();
                        prozorArtikal.txtVelicina.Text = citac["Veličina"].ToString();
                        prozorArtikal.txtCena.Text = citac["Cena"].ToString();
                        prozorArtikal.txtRaspolozivaKolicina.Text = citac["RaspoloživaKoličina"].ToString();
                        prozorArtikal.cbKategorija.SelectedValue = citac["KategorijaID"].ToString();
                        prozorArtikal.cbMarka.SelectedValue = citac["MarkaID"].ToString();
                        prozorArtikal.cbOcenaKupca.SelectedValue = citac["OcenaKupcaID"].ToString();
                        prozorArtikal.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(kategorijaSelect))
                    {
                        Kategorija prozorKategorija = new Kategorija(azuriraj, red);
                        prozorKategorija.txtNaziv.Text = citac["NazivKategorije"].ToString();
                        prozorKategorija.txtOpis.Text = citac["OpisKategorije"].ToString();
                        prozorKategorija.txtRaspon.Text = citac["RasponCena"].ToString();
                        prozorKategorija.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(kupacSelect))
                    {
                        Kupac prozorKupac = new Kupac(azuriraj, red);
                        prozorKupac.txtIme.Text = citac["Ime"].ToString();
                        prozorKupac.txtPrezime.Text = citac["Prezime"].ToString();
                        prozorKupac.txtEmail.Text = citac["Email"].ToString();
                        prozorKupac.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(kupovinaSelect))
                    {
                        Kupovina prozorKupovina = new Kupovina(azuriraj, red);
                        prozorKupovina.DPdatum.SelectedDate = (DateTime)citac["DatumKupovine"];
                        prozorKupovina.txtIznos.Text = citac["IznosKupovine"].ToString();
                        prozorKupovina.txtNacinPlacanja.Text = citac["NačinPlaćanja"].ToString();
                        prozorKupovina.cbArtikal.Text = citac["ArtikalID"].ToString();
                        prozorKupovina.cbKupac.SelectedValue = citac["KupacID"].ToString();
                        prozorKupovina.cbZaposleni.SelectedValue = citac["ZaposleniID"].ToString();
                        prozorKupovina.cbOcena.SelectedValue = citac["OcenaKupcaID"].ToString();
                        prozorKupovina.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(markaSelect))
                    {
                        Marka prozorMarka = new Marka(azuriraj, red);
                        prozorMarka.txtNaziv.Text = citac["NazivMarke"].ToString();
                        prozorMarka.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(ocenaKupcaSelect))
                    {
                        OcenaKupca prozorOcenaKupca = new OcenaKupca(azuriraj, red);
                        prozorOcenaKupca.txtOcena.Text = citac["Ocena"].ToString();
                        prozorOcenaKupca.txtKomentar.Text = citac["Komentar"].ToString();
                        prozorOcenaKupca.DPdatum.SelectedDate = (DateTime)citac["DatumOcene"];
                        prozorOcenaKupca.cbKupac.SelectedValue = citac["KupacID"].ToString();
                        prozorOcenaKupca.cbKupovina.SelectedValue = citac["KupovinaID"].ToString();
                        prozorOcenaKupca.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(reklamacijaSelect))
                    {
                        Reklamacija prozorReklamacija = new Reklamacija(azuriraj, red);
                        prozorReklamacija.DPdatum.SelectedDate = (DateTime)citac["DatumPodnošenja"];
                        prozorReklamacija.txtRazlog.Text = citac["Razlog"].ToString();
                        prozorReklamacija.txtStatus.Text = citac["Status"].ToString();
                        prozorReklamacija.txtBrojRacuna.Text = citac["BrojRačuna"].ToString();
                        prozorReklamacija.cbArtikal.Text = citac["ArtikalID"].ToString();
                        prozorReklamacija.cbZaposleni.SelectedValue = citac["ZaposleniID"].ToString();
                        prozorReklamacija.cbKupac.SelectedValue = citac["KupacID"].ToString();
                        prozorReklamacija.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(zaposleniSelect))
                    {
                        Zaposleni prozorZaposleni = new Zaposleni(azuriraj, red);
                        prozorZaposleni.txtIme.Text = citac["Ime"].ToString();
                        prozorZaposleni.txtPrezime.Text = citac["Prezime"].ToString();
                        prozorZaposleni.txtJmbg.Text = citac["JMBG"].ToString();
                        prozorZaposleni.txtAdresa.Text = citac["Adresa"].ToString();
                        prozorZaposleni.txtRadnoMesto.Text = citac["RadnoMesto"].ToString();
                        prozorZaposleni.DPdatum.SelectedDate = (DateTime)citac["DatumRođenja"];
                        prozorZaposleni.ShowDialog();
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnDelete(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selektovati red koji želite da izmenite!",
                    "GREŠKA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (grid.SelectedItems.Count > 1)
            {
                MessageBox.Show("Selektovati samo 1 red!", "GREŠKA", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                if (ucitanaTabela.Equals(artikalSelect))
                {
                    Obrisi(grid, artikalDelete);
                    UcitajPodatke(artikalSelect);
                }
                else if (ucitanaTabela.Equals(kategorijaSelect))
                {
                    Obrisi(grid, kategorijaDelete); ;
                    UcitajPodatke(kategorijaSelect);
                }
                else if (ucitanaTabela.Equals(kupacSelect))
                {
                    Obrisi(grid, kupacDelete);
                    UcitajPodatke(kupacSelect);
                }
                else if (ucitanaTabela.Equals(kupovinaSelect))
                {
                    Obrisi(grid, kupovinaDelete);
                    UcitajPodatke(kupovinaSelect);
                }
                else if (ucitanaTabela.Equals(markaSelect))
                {
                    Obrisi(grid, markaDelete);
                    UcitajPodatke(markaSelect);
                }
                else if (ucitanaTabela.Equals(ocenaKupcaSelect))
                {
                    Obrisi(grid, ocenaKupcaDelete);
                    UcitajPodatke(ocenaKupcaSelect);
                }
                else if (ucitanaTabela.Equals(reklamacijaSelect))
                {
                    Obrisi(grid, reklamacijaDelete);
                    UcitajPodatke(reklamacijaSelect);
                }
                else if (ucitanaTabela.Equals(zaposleniSelect))
                {
                    Obrisi(grid, zaposleniDelete);
                    UcitajPodatke(zaposleniSelect);
                }
            }
        }
        private void Obrisi(DataGrid grid, string selectUslov)
        {
            try
            {
                konekcija.Open();
                var selectedRow = (DataRowView)grid.SelectedItem; //dobijamo selektovani red
                MessageBoxResult rezultat = MessageBox.Show("Da li ste sigurni?", "UPOZORENJE",
                                                           MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rezultat == MessageBoxResult.Yes)
                {
                    object a = selectedRow.Row.ItemArray[0]; //Ovde se pretpostavlja da je
                                                             //ID vrednost koja se briše smeštena
                                                             //u prvom polju (indeks 0) selektovanog reda u grid-u

                    int? id = (int?)a; //konvertuje vrednost a u nullable integer
                                       //omogućava rad sa null vrednostima,ukoliko je potrebno

                    SqlCommand cmd = new SqlCommand { Connection = konekcija }; //Kreira se novi objekat SqlCommand
                                                                                //koji će izvršiti SQL upit, povezan
                                                                                //sa otvorenom konekcijom konekcija.

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id; //Ovde se pravi SQL parametar @id tipa
                                                                         //SqlDbType.Int, a zatim se njemu dodeljuje
                                                                         //vrednost ID-ja koji je prethodno
                                                                         //konvertovan. Ovo se radi da bi se izbeglo
                                                                         //direktno umetanje vrednosti u
                                                                         //SQL upit, što može biti opasno zbog
                                                                         //SQL Injection napada.

                    cmd.CommandText = selectUslov + "@id"; //Formira se SQL upit za brisanje. Pretpostavljam da je
                                                           //selectUslov string koji sadrži deo SQL upita za brisanje,
                                                           //a dodaje se parametar @id na kraju tog upita.

                    cmd.ExecuteNonQuery(); //izvršava se komanda 
                    cmd.Dispose(); //oslobađa resurs
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Ne može se obrisati element koji se koristi u drugoj tabeli kao strani kljuc!",
                "GREŠKA", MessageBoxButton.OK, MessageBoxImage.Error);
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