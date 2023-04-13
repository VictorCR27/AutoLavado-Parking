using System;
using System.Collections.Generic;
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
using UserControl = System.Windows.Controls.UserControl;

namespace ProyectoDiseñoApps.view
{
    /// <summary>
    /// Interaction logic for ServiciosView.xaml
    /// </summary>
    public partial class ServiciosView : UserControl
    {

        public ServiciosView()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Aquí puedes implementar el código para procesar y enviar los datos ingresados.
            System.Windows.MessageBox.Show("Datos enviados");
        }

        private void BronceService_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BasicService_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PremiumService_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WashAndParkingService_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ParkingService_Checked(object sender, RoutedEventArgs e)
        {
            ParkingPlateTextBox.IsEnabled = ParkingService.IsChecked == true;
            ParkingHourTextBox.IsEnabled = ParkingService.IsChecked == true;
        }

        private void ParkingService_Unchecked(object sender, RoutedEventArgs e)
        {
            ParkingPlateTextBox.IsEnabled = false;
            ParkingHourTextBox.IsEnabled = false;
        }

        private void VehicleModelTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void VehiclePlateTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
