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
using System.ComponentModel.Design;


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
        private object idE;
        private object empleadoId;

        public object Id { get; private set; }
        public object idEm { get; private set; }

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

                // Obtener el empleadoId del registro seleccionado
                empleadoId = rowView["EmpleadoAsignado"];

                try
                {
                    connectionDB.connect.Open();
                    SqlCommand command = new SqlCommand("UPDATE Servicios SET estado = 0, EmpleadoAsignado = 0, HoraSalida = @HoraSalida WHERE id = @id", connectionDB.connect);
                    command.Parameters.AddWithValue("@id", rowView["id"]);
                    command.Parameters.AddWithValue("@HoraSalida", DateTime.Now);

                    // Actualizar el estado del empleado a ocupado (estado = 1).
                    SqlCommand commandEstado = new SqlCommand("UPDATE Empleados SET Estado = 0 WHERE ID = @empleadoId", connectionDB.connect);
                    commandEstado.Parameters.AddWithValue("@empleadoId", empleadoId);
                    commandEstado.ExecuteNonQuery();


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
                                                                 "AS Subquery WHERE Id = @Id", connectionDB.connect))
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
            DataRowView rowView = (DataRowView)dataGrid.SelectedItem;
            if (rowView != null)
            {
                try
                {
                    if (connectionDB.connect.State != ConnectionState.Open)
                    {
                        connectionDB.connect.Open();
                    }

                    int empleadoId = -1;

                    using (SqlCommand commandEm = new SqlCommand("SELECT TOP 1 ID FROM Empleados WHERE Estado = 0", connectionDB.connect))
                    {
                        SqlDataReader reader = commandEm.ExecuteReader();
                        if (reader.Read())
                        {
                            empleadoId = reader.GetInt32(0);
                        }
                        reader.Close();
                    }

                    if (empleadoId != -1)
                    {
                        // Obtener el ID del servicio del registro seleccionado en el dataGrid.
                        int servicioId = Convert.ToInt32(rowView["id"]);

                        // Asignar el empleado desocupado al servicio seleccionado.
                        SqlCommand commandE = new SqlCommand("UPDATE Servicios " +
                                                              "SET EmpleadoAsignado = @empleadoId, empleadoNombre = (" +
                                                                  "SELECT empleadoNombre " +
                                                                  "FROM Empleados " +
                                                                  "WHERE ID = @empleadoId" +
                                                              ") " +
                                                              "WHERE id = @servicioId",
                                                              connectionDB.connect);
                        commandE.Parameters.AddWithValue("@empleadoId", empleadoId);
                        commandE.Parameters.AddWithValue("@servicioId", servicioId);
                        commandE.ExecuteNonQuery();

                        // Actualizar el estado del empleado a ocupado (estado = 1).
                        SqlCommand commandEstado = new SqlCommand("UPDATE Empleados SET Estado = 1 WHERE ID = @empleadoId", connectionDB.connect);
                        commandEstado.Parameters.AddWithValue("@empleadoId", empleadoId);
                        commandEstado.ExecuteNonQuery();

                        MessageBox.Show("Empleado con ID " + empleadoId + " ha sido asignado.");

                        LoadData(); // Actualiza el DataGrid con los cambios más recientes
                    }
                    else
                    {
                        MessageBox.Show("No hay empleados disponibles.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al asignar empleado: " + ex.Message);
                }
                finally
                {
                    if (connectionDB.connect.State == ConnectionState.Open)
                    {
                        connectionDB.connect.Close();
                    }
                }
            }
        }




        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}