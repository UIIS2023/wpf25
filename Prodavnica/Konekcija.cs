using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodavnica
{
    class Konekcija
    {
        public SqlConnection KreirajKonekciju()
        {
            SqlConnectionStringBuilder ccnSb = new SqlConnectionStringBuilder
            {
                DataSource = @"MILAASUS\SQLEXPRESS01", //lokalni servera
                InitialCatalog = "WPF projekat", //baza na lokalnom serveru
                IntegratedSecurity = true //koristice se trenutni windows kredencijali za autentifikaciju
            };
            string con = ccnSb.ToString();
            SqlConnection konekcija = new SqlConnection(con);
            return konekcija;
        }
    }
}
