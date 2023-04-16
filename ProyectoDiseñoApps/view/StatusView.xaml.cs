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
using FontAwesome.Sharp;


namespace ProyectoDiseñoApps.view
{
    /// <summary>
    /// Interaction logic for StatusView.xaml
    /// </summary>
    public partial class StatusView : Window

    {
        ConnectionDB con = new ConnectionDB();

        private ConnectionDB connectionDB;

        public StatusView()
        {
            InitializeComponent();
            connectionDB = new ConnectionDB();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                connectionDB.connect.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Servicios", connectionDB.connect);
                SqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                dataGrid.ItemsSource = dataTable.DefaultView;
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
            finally
            {
                connectionDB.connect.Close();
            }
        }

        private void FinalizarBtn_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = (DataRowView)dataGrid.SelectedItem;
            if (rowView != null)
            {
                try
                {
                    connectionDB.connect.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM Servicios WHERE id = @id", connectionDB.connect);
                    command.Parameters.AddWithValue("@id", rowView["id"]);
                    command.ExecuteNonQuery();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el servicio: " + ex.Message);
                }
                finally
                {
                    connectionDB.connect.Close();
                }
            }
        }

        private void AsignarBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
