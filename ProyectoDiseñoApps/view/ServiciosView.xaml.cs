using ProyectoDiseñoApps.Database;
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

    
            servicio_estacionamientoPremium.Checked += estacionamientoPremium_Checked;
            servicio_estacionamientoPremium.Unchecked += estacionamientoPremium_Unchecked;
            servicioEstacionamiento.Checked += estacionamientoBasico_Checked;
            servicioEstacionamiento.Unchecked += estacionamientoBasico_Unchecked;
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
    }
}