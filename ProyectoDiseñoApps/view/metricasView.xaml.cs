using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace ProyectoDiseñoApps.view
{
    /// <summary>
    /// Interaction logic for metricasView.xaml
    /// </summary>
    public partial class metricasView : Window
    {

       

        private ConnectionDB connectionDB;

        public metricasView()
        {
            InitializeComponent();
        }

        ConnectionDB con = new ConnectionDB();

        
        private void Ganancias_Click(object sender, RoutedEventArgs e)
        {

            string query = "SELECT SUM(Costo) FROM Servicios;";

            con.connect.Open();

            using (SqlCommand command = new SqlCommand(query, con.connect))
            {
                int ganancias = (int)command.ExecuteScalar();

                MessageBox.Show("Ganancias totales: ₡" + ganancias.ToString());
            }

            con.connect.Close();

        }


        private void Transacciones_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT MAX(Id) FROM Servicios;";

            con.connect.Open();

            // Crea una conexión a la base de datos y un objeto SqlCommand para ejecutar la consulta
            using (SqlCommand command = new SqlCommand(sql, con.connect))
            {
                // Abre la conexión a la base de datos y ejecuta la consulta
                
                int ultimoId = (int)command.ExecuteScalar();

                // Muestra el último ID en un MessageBox
                MessageBox.Show("Total de transacciones: " + ultimoId.ToString());
            }

            con.connect.Close();

        }


        private void totalEmpleados_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT MAX(ID) FROM Empleados;";

            con.connect.Open();

            // Crea una conexión a la base de datos y un objeto SqlCommand para ejecutar la consulta
            using (SqlCommand command = new SqlCommand(sql, con.connect))
            {
                // Abre la conexión a la base de datos y ejecuta la consulta
                
                int ultimoId = (int)command.ExecuteScalar();

                // Muestra el último ID en un MessageBox
                MessageBox.Show("Total de empleados: " + ultimoId.ToString());
            }

            con.connect.Close();

        }
    }
}
