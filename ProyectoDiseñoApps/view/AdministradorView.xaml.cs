using ProyectoDiseñoApps.Database;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoDiseñoApps.view
{
    /// <summary>
    /// Interaction logic for AdministradorView.xaml
    /// </summary>
    public partial class AdministradorView : System.Windows.Controls.UserControl
    {
        public AdministradorView()
        {
            InitializeComponent();
     
            LoadData();

            // Deshabilita la opción de agregar filas al dataGrid
            empleadosDataGrid.CanUserAddRows = false;
        }

        
        

    private void LoadData()
        {
            try
            {
                dataAccess db = new dataAccess();
                DataTable dt = db.GetEmpleados(); // Asumiendo que has creado un método GetEmpleados() en la clase dataAccess para obtener los empleados de la base de datos
                empleadosDataGrid.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (empleadosDataGrid.SelectedItem != null)
                {
                    DataRowView row = (DataRowView)empleadosDataGrid.SelectedItem;
                    int id = Convert.ToInt32(row["Id"]);
                    dataAccess db = new dataAccess();
                    db.removeEmpleado(id);
                    LoadData();
                    System.Windows.MessageBox.Show("Empleado eliminado correctamente.");
                }
                else
                {
                    System.Windows.MessageBox.Show("Por favor, seleccione un empleado para eliminar.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error al eliminar empleado: " + ex.Message);
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nombreBox.Text) || string.IsNullOrWhiteSpace(cedulaBox.Text) || string.IsNullOrWhiteSpace(correoBox.Text))
            {
                System.Windows.MessageBox.Show("Por favor, completa todos los campos antes de añadir un empleado.");
            }
            else
            {
                try
                {
                    dataAccess db = new dataAccess();
                    db.addEmpleado(nombreBox.Text, cedulaBox.Text, correoBox.Text);
                    LoadData();
                    System.Windows.MessageBox.Show("Empleado añadido correctamente.");
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error al añadir empleado: " + ex.Message);
                }
            }
        }

        private void metricas_Click(object sender, RoutedEventArgs e)
        {
            metricasView metricas = new metricasView();
            metricas.Show();
        }
    }
}
