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
    /// Interaction logic for Zaposleni.xaml
    /// </summary>
    public partial class Zaposleni : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;
        public Zaposleni()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtIme.Focus();
        }
        public Zaposleni(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtIme.Focus();
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
                cmd.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = txtIme.Text;
                cmd.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = txtPrezime.Text;
                cmd.Parameters.Add("@JMBG", SqlDbType.Int).Value = txtJmbg.Text;
                cmd.Parameters.Add("@RadnoMesto", SqlDbType.NVarChar).Value = txtRadnoMesto.Text;
                cmd.Parameters.Add("@DatumRođenja", SqlDbType.DateTime).Value = datum;

                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update tblZaposleni
                                        set Ime = @Ime, Prezime = @Prezime, 
                                        JMBG = @JMBG, RadnoMesto = @RadnoMesto,
                                        DatumRođenja = @DatumRođenja where ZaposleniID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblZaposleni(Ime, Prezime, JMBG, RadnoMesto, DatumRođenja)
                                               values (@Ime, @Prezime,@JMBG,@RadnoMesto,@DatumRođenja)";
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
