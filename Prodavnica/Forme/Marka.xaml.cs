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
    /// Interaction logic for Marka.xaml
    /// </summary>
    public partial class Marka : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;
        public Marka()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtNaziv.Focus();
        }
        public Marka(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtNaziv.Focus();
            this.azuriraj = azuriraj;
            this.red = red;
            
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
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
                cmd.Parameters.Add("@NazivMarke", SqlDbType.NVarChar).Value = txtNaziv.Text;

                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update tblMarka
                                        set NazivMarke = @NazivMarke where MarkaID = @id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblMarka(NazivMarke)
                                               values (@NazivMarke)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Unos vrednosti nije validan","greska", MessageBoxButton.OK);
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
