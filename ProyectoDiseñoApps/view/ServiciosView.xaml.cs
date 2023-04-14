using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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
    public partial class ServiciosView : UserControl
    {
        ConnectionDB DB;

        public ServiciosView()
        {
            InitializeComponent();

            DB = new ConnectionDB();

            SubmitButton.Click += SubmitButton_Click; // Agregar esta línea para manejar el evento Click del botón de envío
            BronceService.Click += BronceService_Click;
            BasicService.Click += BasicService_Click;
            PremiumService.Click += PremiumService_Click;
            WashAndParkingService.Checked += WashAndParkingService_Checked;
            WashAndParkingService.Unchecked += WashAndParkingService_Checked;
            ParkingService.Checked += ParkingService_Checked;
            ParkingService.Unchecked += ParkingService_Unchecked;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string modelo = Modelotxt.Text;
            string placa = Placatxt.Text;
            string TipoServicio;

            if (BronceService.IsChecked == true)
            {
                TipoServicio = "Bronce";
            }
            else if (BasicService.IsChecked == true)
            {
                TipoServicio = "Básico";
            }
            else if (PremiumService.IsChecked == true)
            {
                TipoServicio = "Premium";
            }
            else if (WashAndParkingService.IsChecked == true)
            {
                TipoServicio = "Servicio de lavado y parqueo";
            }
            else if (ParkingService.IsChecked == true)
            {
                TipoServicio = "Servicio de parqueo";
            }
            else
            {
                TipoServicio = "No seleccionado";
            }

            string EspacioParqueo = ParqueoEspaciotxt.Text;
            string HoraParqueo = ParqueoHoratxt.Text;

            ConnectionDB con = new ConnectionDB();
            SqlConnection connection = con.GetConnection();

            try
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Servicios (CarroModelo, CarroPlaca, TipoServicio, ParqueoEspacio, ParqueoHora) VALUES (@CarroModelo, @CarroPlaca, @TipoServicio,@ParqueoEspacio,@ParqueoHora)", connection))
                {
                    command.Parameters.AddWithValue("@CarroModelo", modelo);
                    command.Parameters.AddWithValue("@CarroPlaca", placa);
                    command.Parameters.AddWithValue("@TipoServicio", TipoServicio);
                    command.Parameters.AddWithValue("@ParqueoEspacio", EspacioParqueo);
                    command.Parameters.AddWithValue("@ParqueoHora", HoraParqueo);

                    int rowsAffected = command.ExecuteNonQuery();
                    System.Windows.MessageBox.Show($"{rowsAffected} filas insertadas.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        #region----------------------------------------------------------------Componentes

        private void BronceService_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BasicService_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PremiumService_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WashAndParkingService_Checked(object sender, RoutedEventArgs e)
        {
            ParqueoEspaciotxt.IsEnabled = WashAndParkingService.IsChecked == true;
            ParqueoHoratxt.IsEnabled = WashAndParkingService.IsChecked == true;
        }

        private void WashAndParkingService_Unchecked(object sender, RoutedEventArgs e)
        {
            ParqueoEspaciotxt.IsEnabled = false;
            ParqueoHoratxt.IsEnabled = false;
        }

        private void ParkingService_Checked(object sender, RoutedEventArgs e)
        {
            ParqueoEspaciotxt.IsEnabled = ParkingService.IsChecked == true;
            ParqueoHoratxt.IsEnabled = ParkingService.IsChecked == true;
        }

        private void ParkingService_Unchecked(object sender, RoutedEventArgs e)
        {
            ParqueoEspaciotxt.IsEnabled = false;
            ParqueoHoratxt.IsEnabled = false;
        }

        #endregion

        private void Modelotxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Placatxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}