using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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

namespace ProyectoDiseñoApps.view
{
    /// <summary>
    /// Interaction logic for InicioView.xaml
    /// </summary>
    /// 

    public partial class InicioView : UserControl
    {
        private object statusView;
        private object connectionDB;

        public InicioView()
        {
            InitializeComponent();
            
            UpdateParkingSpaceStatus();
            UpdateEmployeeStatus();
        }




        private void UpdateParkingSpaceStatus()
        {
            // Crear una instancia de StatusView
            StatusView statusView = new StatusView();

            // Obtener el estado de los espacios de parqueo de la base de datos
            // Puedes modificar la consulta SQL para obtener el estado de todos los espacios de parqueo
            DataTable parkingSpaces = statusView.GetData("SELECT estado FROM EspacioParqueo");
            DataTable employeeStatusTable = statusView.GetData("SELECT estado FROM Empleados");

            // Actualizar el color de los cuadros de parqueo según el estado
            for (int i = 0; i < parkingSpaces.Rows.Count; i++)
            {
                int estado = Convert.ToInt32(parkingSpaces.Rows[i]["estado"]); // Cambia el cast directo por Convert.ToInt32()
                SolidColorBrush color = statusView.GetStatusColor(estado);

                // Asignar el color según el índice del espacio de parqueo
                switch (i)
                {
                    case 0:
                        space1.Background = color;
                        break;
                    case 1:
                        space2.Background = color;
                        break;
                    case 2:
                        space3.Background = color;
                        break;
                    case 3:
                        space4.Background = color;
                        break;
                    case 4:
                        space5.Background = color;
                        break;
                    case 5:
                        space6.Background = color;
                        break;
                    case 6:
                        space7.Background = color;
                        break;
                    case 7:
                        space8.Background = color;
                        break;
                    case 8:
                        space9.Background = color;
                        break;
                    case 9:
                        space10.Background = color;
                        break;
                    case 10:
                        space11.Background = color;
                        break;
                    case 11:
                        space12.Background = color;
                        break;
                    case 12:
                        space13.Background = color;
                        break;
                    case 13:
                        space14.Background = color;
                        break;
                    case 14:
                        space15.Background = color;
                        break;
                    case 15:
                        space16.Background = color;
                        break;
                    case 16:
                        space17.Background = color;
                        break;
                    case 17:
                        space18.Background = color;
                        break;
                    case 18:
                        space19.Background = color;
                        break;
                    case 19:    
                        space20.Background = color;
                        break;
                        // Agrega más casos según la cantidad de espacios de parqueo

                }
            }
        }


        private void UpdateEmployeeStatus()
        {
            // Crear una instancia de StatusView
            StatusView statusView = new StatusView();

            // Obtener el estado de los empleados de la base de datos
            DataTable employeeStatusTable = statusView.GetData("SELECT estado FROM Empleados");

            // Actualizar el color de los cuadros de empleados según el estado
            for (int i = 0; i < employeeStatusTable.Rows.Count; i++)
            {
                int estado = Convert.ToInt32(employeeStatusTable.Rows[i]["estado"]);

                // Establecer el color de fondo según el estado del empleado
                SolidColorBrush color;
                if (estado == 0)
                {
                    color = Brushes.Green;
                }
                else if (estado == 1)
                {
                    color = Brushes.Red;
                }
                else
                {
                    color = Brushes.Transparent;
                }

                // Obtener el cuadro de empleado correspondiente
                Grid employeeGrid = employeeWrapPanel.Children[i] as Grid;

                // Asignar el color al cuadro de empleado
                (employeeGrid.Children[0] as Border).Background = color;
            }
        }




    }
}
