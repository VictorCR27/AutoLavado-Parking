using FontAwesome.Sharp;
using ProyectoDiseñoApps.Database;
using ProyectoDiseñoApps.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using UserControl = System.Windows.Controls.UserControl;

namespace ProyectoDiseñoApps.view
{
    public partial class ServiciosView : UserControl
    {

        ConnectionDB con = new ConnectionDB();

        private ConnectionDB connectionDB;
        public ServiciosView()
        {
            InitializeComponent();

            horaBox.Text = DateTime.Now.ToString("hh:mm tt"); // hh:mm tt muestra la hora en formato de 12 horas con AM o PM
            servicio_estacionamientoPremium.Checked += estacionamientoPremium_Checked;
            servicio_estacionamientoPremium.Unchecked += estacionamientoPremium_Unchecked;
            servicioEstacionamiento.Checked += estacionamientoBasico_Checked;
            servicioEstacionamiento.Unchecked += estacionamientoBasico_Unchecked;

            Loaded += MainWindow_Loaded;

            connectionDB = new ConnectionDB();

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectionDB con = new ConnectionDB();
            con.connect.Open();

            string query = "SELECT descripcion FROM EspacioParqueo WHERE estado = 0";
            SqlCommand command = new SqlCommand(query, con.connect);
            SqlDataReader reader = command.ExecuteReader();

            List<string> lista = new List<string>();

            while (reader.Read())
            {
                lista.Add(reader["descripcion"].ToString());
            }

            espacioBox.ItemsSource = lista;

            reader.Close();
            con.connect.Close();
        }





        #region----------------------------------------------------------------Componentes



        private void estacionamientoPremium_Checked(object sender, RoutedEventArgs e)
        {
            espacioBox.IsEnabled = servicio_estacionamientoPremium.IsChecked == true;
            horaBox.IsEnabled = servicio_estacionamientoPremium.IsChecked == true;
        }

        private void estacionamientoPremium_Unchecked(object sender, RoutedEventArgs e)
        {
            espacioBox.IsEnabled = false;
            horaBox.IsEnabled = false;
        }

        private void estacionamientoBasico_Checked(object sender, RoutedEventArgs e)
        {
            espacioBox.IsEnabled = servicioEstacionamiento.IsChecked == true;
            horaBox.IsEnabled = servicioEstacionamiento.IsChecked == true;

        }

        private void estacionamientoBasico_Unchecked(object sender, RoutedEventArgs e)
        {
            espacioBox.IsEnabled = false;
            horaBox.IsEnabled = false;
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
       
            string tipoServicio;

            //TO DO: reemplazar esto por un switch o algo mas simple
            if (servicioBronce.IsChecked == true)
            {
                tipoServicio = "Bronce";
            }
            else if (servicioBasico.IsChecked == true)
            {
                tipoServicio = "Básico";
            }
            else if (servicioPremium.IsChecked == true)
            {
                tipoServicio = "Premium";
            }
            else if (servicio_estacionamientoPremium.IsChecked == true)
            {
                tipoServicio = "Servicio de lavado y parqueo";
            }
            else if (servicioEstacionamiento.IsChecked == true)
            {
                tipoServicio = "Servicio de parqueo";
            }
            else
            {
                tipoServicio = "No seleccionado";
            }

           
            //añade la informacion a la base de datos
            dataAccess datos = new dataAccess();
            datos.addServicio(modeloBox.Text, placaBox.Text, tipoServicio, espacioBox.Text, horaBox.Text);
        }

        private void StatusBtn_Click(object sender, RoutedEventArgs e)
        {
            StatusView Status = new StatusView();
            Status.Show();
        }

    }
}