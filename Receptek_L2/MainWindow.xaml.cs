using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Receptek_L2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection;
        string connectionString;
        public MainWindow()
        {
            
            connectionString = ConfigurationManager.ConnectionStrings["Receptek_L2.Properties.Settings.AdatbazisConnectionString"].ConnectionString;

            InitializeComponent();

            Receptekbetoltese();
        }

        private void Receptekbetoltese()
        {
            #region Comment
            /*
                connection = new SqlConnection(connectionString);
                connection.Open();
                // ....
                connection.Close();
            */
            /*
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Receptek;", connection))
                    {

                    }
                }
            */
            #endregion

            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Receptek;", connection))
            {
                DataTable receptekTabla = new DataTable();
                adapter.Fill(receptekTabla);
                ReceptekLista.ItemsSource = receptekTabla.AsDataView();
                ReceptekLista.DisplayMemberPath = "Nev";
                ReceptekLista.SelectedValuePath = "Id";
            }
        }

        private void ReceptekLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EgyReceptLekerdezese();
            HozzavalokListazasa();
        }

        private void HozzavalokListazasa()
        {
            string query = "SELECT [Hozzavalok].Nev, [ReceptekHozzavalok].Mertekegyseg, [ReceptekHozzavalok].Mennyiseg " +
                " FROM [Hozzavalok]" +
                " INNER JOIN [ReceptekHozzavalok] ON [Hozzavalok].Id = [ReceptekHozzavalok].HozzavaloId" +
                " WHERE [ReceptekHozzavalok].ReceptId = @ReceptId ;";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            using ( SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@ReceptId", ReceptekLista.SelectedValue);
                DataTable hozzavalokTable = new DataTable();
                adapter.Fill(hozzavalokTable);
                HozzavalokLista.DataContext = hozzavalokTable.AsDataView();
            }
        }

        private void EgyReceptLekerdezese()
        {
            string query = "SELECT [ElkeszitesIdo], [Elkeszites] FROM [Receptek] WHERE [Id] = @ReceptId;";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@ReceptId", ReceptekLista.SelectedValue);
                DataTable egyRecept = new DataTable();
                adapter.Fill(egyRecept);
                DataRow[] DataRows = egyRecept.Select();
                foreach (var row in DataRows)
                {
                    Ido.Content = row["ElkeszitesIdo"].ToString();
                    Elkeszites.Content = row["Elkeszites"].ToString();
                }
            }
        }
    }
}
