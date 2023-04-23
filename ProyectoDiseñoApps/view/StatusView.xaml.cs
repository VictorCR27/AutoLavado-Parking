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
        int costoParqueo;
        private object id;

        public object Id { get; private set; }

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
                    SqlCommand command = new SqlCommand("UPDATE Servicios SET estado = 0, HoraSalida = @HoraSalida WHERE id = @id", connectionDB.connect);
                    command.Parameters.AddWithValue("@id", rowView["id"]);
                    command.Parameters.AddWithValue("@HoraSalida", DateTime.Now);

                    // Corrige el valor del parámetro estado en el segundo comando SQL
                    SqlCommand command2 = new SqlCommand("UPDATE EspacioParqueo SET estado = 0 WHERE Descripcion = @ParqueoEspacio", connectionDB.connect);
                    command2.Parameters.AddWithValue("@ParqueoEspacio", rowView["ParqueoEspacio"]);

                    command.ExecuteNonQuery();

                    command2.ExecuteNonQuery(); // Ejecuta el segundo comando SQL
                    LoadData(); // Actualiza el DataGrid con los cambios más recientes

                    using (SqlCommand command3 = new SqlCommand("SELECT " +
                                                                "CASE " +
                                                                "WHEN TipoServicio = 'Servicio de parqueo' " +
                                                                "THEN DATEDIFF(MINUTE, HoraEntrada, HoraSalida) * 500 ELSE Costo END AS Total FROM (" +
                                                                 "SELECT *, DATEDIFF(MINUTE, HoraEntrada, HoraSalida) AS DiferenciaEnMinutos FROM Servicios) " +
                                                                 "AS Subquery WHERE Id = @Id",connectionDB.connect))
                    {
                        connectionDB.connect.Open();

                        command3.Parameters.AddWithValue("@id", rowView["id"]); // Asegúrate de asignar un valor a la variable 'id'

                        // Ejecutar la consulta y obtener el resultado
                        using (SqlDataReader reader = command3.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                costoParqueo = reader.GetInt32(0); // Obtener el valor de la columna 'Total'
                            }
                            else
                            {
                                throw new Exception("No se encontró el registro con el Id especificado.");
                            }
                        }
                    }

                    using (SqlCommand command4 = new SqlCommand("UPDATE Servicios SET Costo = @costoParqueo WHERE id = @id", connectionDB.connect))
                    {
                        command4.Parameters.AddWithValue("@costoParqueo", costoParqueo);
                        command4.Parameters.AddWithValue("@id", rowView["id"]);
                        command4.ExecuteNonQuery();
                    }

                    MessageBox.Show("Total a pagar: ₡" + costoParqueo);


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