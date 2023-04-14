using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using UserControl = System.Windows.Controls.UserControl;

namespace ProyectoDiseñoApps.view
{
    public partial class ServiciosView : UserControl
    {
        public ServiciosView()
        {
            InitializeComponent();

            BronceService.Click += BronceService_Click;
            BasicService.Click += BasicService_Click;
            PremiumService.Click += PremiumService_Click;
            WashAndParkingService.Checked += WashAndParkingService_Checked;
            WashAndParkingService.Unchecked += WashAndParkingService_Checked;
            ParkingService.Checked += ParkingService_Checked;
            ParkingService.Unchecked += ParkingService_Unchecked;
            InsertarDatos.Click += InsertarDatos_Click;
        }

        
        private void InsertarDatos_Click(object sender, RoutedEventArgs e)
        {
            string modelo = Modelotxt.Text;
            string placa = Placatxt.Text;
            string tipoServicio;

            if (BronceService.IsChecked == true)
            {
                tipoServicio = "Bronce";
            }
            else if (BasicService.IsChecked == true)
            {
                tipoServicio = "Básico";
            }
            else if (PremiumService.IsChecked == true)
            {
                tipoServicio = "Premium";
            }
            else if (WashAndParkingService.IsChecked == true)
            {
                tipoServicio = "Servicio de lavado y parqueo";
            }
            else if (ParkingService.IsChecked == true)
            {
                tipoServicio = "Servicio de parqueo";
            }
            else
            {
                tipoServicio = "No seleccionado";
            }

            string espacioParqueo = ParqueoEspaciotxt.Text;
            string horaParqueo = ParqueoHoratxt.Text;

            ConnectionDB dbConnection = new ConnectionDB();
            SqlConnection connection = dbConnection.GetConnection();

            try
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Servicios (CarroModelo, CarroPlaca, TipoServicio,ParqueoEspacio,ParqueoHora) VALUES (@CarroModelo, @CarroPlaca, @TipoServicio,@ParqueoEspacio,@ParqueoHora)", connection))
                {
                    command.Parameters.AddWithValue("@CarroModelo", Modelotxt.Text);
                    command.Parameters.AddWithValue("@CarroPlaca", Placatxt.Text);
                    command.Parameters.AddWithValue("@TipoServicio", tipoServicio);
                    command.Parameters.AddWithValue("@ParqueoEspacio", ParqueoEspaciotxt.Text);
                    command.Parameters.AddWithValue("@ParqueoHora", ParqueoHoratxt.Text);


                    int rowsAffected = command.ExecuteNonQuery();
                    MessageBox.Show($"{rowsAffected} filas insertadas.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
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