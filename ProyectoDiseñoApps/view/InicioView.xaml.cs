using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoDiseñoApps.view
{
    /// <summary>
    /// Interaction logic for InicioView.xaml
    /// </summary>
    public partial class InicioView : UserControl
    {
        

        public InicioView()
        {
            InitializeComponent();
            
            UpdateParkingSpaceStatus();

        }

        

        private void UpdateParkingSpaceStatus()
        {
            // Crear una instancia de StatusView
            StatusView statusView = new StatusView();

            // Obtener el estado de los espacios de parqueo de la base de datos
            // Puedes modificar la consulta SQL para obtener el estado de todos los espacios de parqueo
            DataTable parkingSpaces = statusView.GetData("SELECT estado FROM EspacioParqueo");

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
                        // Agrega más casos según la cantidad de espacios de parqueo
                }
            }
        }


    }
}
