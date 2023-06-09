﻿using FontAwesome.Sharp;
using ProyectoDiseñoApps.Database;
using ProyectoDiseñoApps.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using UserControl = System.Windows.Controls.UserControl;

namespace ProyectoDiseñoApps.view
{
    public partial class ServiciosView : UserControl
    {
        private const double PrecioServicioBronce = 10000;
        private const double PrecioServicioBasico = 5000;
        private const double PrecioServicioPremium = 15000;
        private const double PrecioServicioEstacionamientoPremium = 10000;
        private const double PrecioServicioEstacionamiento = 500;

        ConnectionDB con = new ConnectionDB();

        int costoParqueo = 0;

        private ConnectionDB connectionDB;
        private object serviciosTable;
        private object dataGridEmpleados;

        public Action<object, EventArgs> ServiceAdded { get; internal set; }

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

            espacioBox.IsEnabled = false;
            horaBox.IsEnabled = false;

        }

        private void ServicioRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (Cobrotxt == null) return;

            var radioButton = (System.Windows.Controls.RadioButton)sender;
            double precio = 0.0;

            if (radioButton == servicioBronce)
                precio = PrecioServicioBronce;
            else if (radioButton == servicioBasico)
                precio = PrecioServicioBasico;
            else if (radioButton == servicioPremium)
                precio = PrecioServicioPremium;
            else if (radioButton == servicio_estacionamientoPremium)
                precio = PrecioServicioEstacionamientoPremium;
            else if (radioButton == servicioEstacionamiento)
                precio = PrecioServicioEstacionamiento;

            Cobrotxt.Content = $"Total a cobrar: ₡{precio:0,0.00}";
        }

        private void ServicioRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Cobrotxt == null) return;

            var radioButton = (System.Windows.Controls.RadioButton)sender;
            double precio = 0.0;

            if (radioButton == servicioBronce)
                precio = PrecioServicioBronce;
            else if (radioButton == servicioBasico)
                precio = PrecioServicioBasico;
            else if (radioButton == servicioPremium)
                precio = PrecioServicioPremium;
            else if (radioButton == servicio_estacionamientoPremium)
                precio = PrecioServicioEstacionamientoPremium;

            Cobrotxt.Content = $"Total a cobrar: ₡{precio:0,0.00}";
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
            if (espacioBox != null && horaBox != null)
            {
                if (servicio_estacionamientoPremium.IsChecked == true || servicioEstacionamiento.IsChecked == true)
                {
                    espacioBox.IsEnabled = true;
                    horaBox.IsEnabled = true;
                }
            }
        }

        private void estacionamientoPremium_Unchecked(object sender, RoutedEventArgs e)
        {
            if (espacioBox != null && horaBox != null)
            {
                if (servicioEstacionamiento.IsChecked == false)
                {
                    espacioBox.IsEnabled = false;
                    horaBox.IsEnabled = false;
                }
            }
        }

        private void estacionamientoBasico_Checked(object sender, RoutedEventArgs e)
        {
            if (espacioBox != null && horaBox != null)
            {
                if (servicio_estacionamientoPremium.IsChecked == true || servicioEstacionamiento.IsChecked == true)
                {
                    espacioBox.IsEnabled = true;
                    horaBox.IsEnabled = true;
                }
            }
        }

        private void estacionamientoBasico_Unchecked(object sender, RoutedEventArgs e)
        {
            if (espacioBox != null && horaBox != null)
            {
                if (servicio_estacionamientoPremium.IsChecked == false)
                {
                    espacioBox.IsEnabled = false;
                    horaBox.IsEnabled = false;
                }
            }
        }




        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tipoServicio;
            decimal precioServicio;

            if (servicioBronce.IsChecked == true)
            {
                tipoServicio = "Bronce";
                precioServicio = (decimal)PrecioServicioBronce;
            }
            else if (servicioBasico.IsChecked == true)
            {
                tipoServicio = "Básico";
                precioServicio = (decimal)PrecioServicioBasico;
            }
            else if (servicioPremium.IsChecked == true)
            {
                tipoServicio = "Premium";
                precioServicio = (decimal)PrecioServicioPremium;
            }
            else if (servicio_estacionamientoPremium.IsChecked == true)
            {
                tipoServicio = "Servicio de lavado y parqueo";
                precioServicio = (decimal)PrecioServicioEstacionamientoPremium;
            }
            else if (servicioEstacionamiento.IsChecked == true)
            {
                tipoServicio = "Servicio de parqueo";
                precioServicio = (decimal)PrecioServicioEstacionamiento;
            }
            else
            {
                tipoServicio = "No seleccionado";
                precioServicio = 0;
            }


            //añade la informacion a la base de datos
            dataAccess datos = new dataAccess();
            datos.addServicio(modeloBox.Text, placaBox.Text, tipoServicio, espacioBox.Text, horaBox.Text, precioServicio);
        }

        private void StatusBtn_Click(object sender, RoutedEventArgs e)
        {
            StatusView Status = new StatusView();
            Status.Show();
        }

       
        private void servicioEstacionamiento_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}