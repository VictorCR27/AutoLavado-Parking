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

            // Deshabilita la opción de agregar filas al dataGrid
            dataGrid.CanUserAddRows = false;
        }

        private void LoadData()
        {
            try
            {
                if (connectionDB.connect.State != ConnectionState.Open)
                {
                    connectionDB.connect.Open();
                }

                SqlCommand command = new SqlCommand("SELECT * FROM Servicios WHERE estado = 1", connectionDB.connect);


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
                if (connectionDB.connect.State == ConnectionState.Open)
                {
                    connectionDB.connect.Close();
                }
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
                    SqlCommand command = new SqlCommand("Update Servicios SET estado = 0 WHERE id = @id", connectionDB.connect);
                    command.Parameters.AddWithValue("@id", rowView["id"]);

                    // Corrige el valor del parámetro estado en el segundo comando SQL
                    SqlCommand command2 = new SqlCommand("UPDATE EspacioParqueo SET estado = 0 WHERE Descripcion = @ParqueoEspacio", connectionDB.connect);
                    command2.Parameters.AddWithValue("@ParqueoEspacio", rowView["ParqueoEspacio"]);

                    command.ExecuteNonQuery();
                    command2.ExecuteNonQuery(); // Ejecuta el segundo comando SQL
                    LoadData(); // Actualiza el DataGrid con los cambios más recientes
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al finalizar el servicio: " + ex.Message);
                }
                finally
                {
                    connectionDB.connect.Close();
                }
            }
        }

        public SolidColorBrush GetStatusColor(int estado)
        {
            if (estado == 1)
            {
                return Brushes.Red; // Ocupado
            }
            else
            {
                return Brushes.Green; // Desocupado
            }
        }

        public DataTable GetData(string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (connectionDB.connect.State != ConnectionState.Open)
                {
                    connectionDB.connect.Open();
                }

                SqlCommand command = new SqlCommand(query, connectionDB.connect);
                SqlDataReader reader = command.ExecuteReader();
                dataTable.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
            finally
            {
                if (connectionDB.connect.State == ConnectionState.Open)
                {
                    connectionDB.connect.Close();
                }
            }

            return dataTable;
        }

        

        private void AsignarBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
