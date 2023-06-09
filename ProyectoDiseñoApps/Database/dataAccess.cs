﻿using ProyectoDiseñoApps.view;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProyectoDiseñoApps.Database
{
    class dataAccess
    {
        private object dbAccess;
        private object contenidoControl;

        #region añadirServicio
        public bool addServicio(String CarroModelo, String CarroPlaca, String TipoServicio, String ParqueoEspacio, String ParqueoHora, Decimal Costo)
        {
            // Establece conexion a la base de datos
            ConnectionDB con = new ConnectionDB();
            if (ConnectionState.Closed == con.connect.State)
            {
                con.connect.Open();
            }

            String query = "INSERT INTO Servicios(CarroModelo, CarroPlaca, TipoServicio, ParqueoEspacio, ParqueoHora, estado, Costo, HoraEntrada) VALUES (@CarroModelo, @CarroPlaca, @TipoServicio, @ParqueoEspacio, @ParqueoHora, 1, @Precio, @HoraEntrada)";
            String query2 = "UPDATE EspacioParqueo SET estado = 1 WHERE descripcion = @ParqueoEspacio";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con.connect))
                {
                    // Añade los valores al query
                    cmd.Parameters.AddWithValue("@CarroModelo", CarroModelo.Trim());
                    cmd.Parameters.AddWithValue("@CarroPlaca", CarroPlaca.Trim());
                    cmd.Parameters.AddWithValue("@TipoServicio", TipoServicio.Trim());
                    cmd.Parameters.AddWithValue("@ParqueoEspacio", ParqueoEspacio.Trim());
                    cmd.Parameters.AddWithValue("@ParqueoHora", ParqueoHora.Trim());
                    cmd.Parameters.AddWithValue("@HoraEntrada", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Precio", Costo);

                    cmd.ExecuteNonQuery();

                    // Ejecuta el segundo query de update para modificar el estado del espacio de parqueo
                    SqlCommand cmd2 = new SqlCommand(query2, con.connect);
                    cmd2.Parameters.AddWithValue("@ParqueoEspacio", ParqueoEspacio.Trim());
                    cmd2.ExecuteNonQuery(); // Ejecuta el segundo query

                    MessageBox.Show($"Servicio Agregado");
                }
                // Cierra la conexión después de usarla
                if (con.connect.State == ConnectionState.Open)
                {
                    con.connect.Close();
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region añadirEmpleado
        public bool addEmpleado(String empleadoNombre, String empleadoCedula, String empleadoCorreo)
        {
            // Establece la conexion a la BD
            ConnectionDB con = new ConnectionDB();
            if (ConnectionState.Closed == con.connect.State)
            {
                con.connect.Open();
            }

            String query = "Insert into Empleados(empleadoNombre,empleadoCorreo,empleadoCedula) VALUES (@empleadoNombre,@empleadoCorreo,@empleadoCedula)";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con.connect))
                {
                    // Añade los valores al query
                    cmd.Parameters.AddWithValue("@empleadoNombre", empleadoNombre.Trim());
                    cmd.Parameters.AddWithValue("@empleadoCorreo", empleadoCorreo.Trim());
                    cmd.Parameters.AddWithValue("@empleadoCedula", empleadoCedula.Trim());

                    cmd.ExecuteNonQuery(); // Ejecuta el query
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
        #endregion



        public DataTable GetEmpleados()
        {
            DataTable dt = new DataTable();

            try
            {
                ConnectionDB con = new ConnectionDB();
                if (con.connect.State == ConnectionState.Closed)
                {
                    con.connect.Open();
                }

                string query = "SELECT * FROM Empleados";
                using (SqlCommand cmd = new SqlCommand(query, con.connect))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                if (con.connect.State == ConnectionState.Open)
                {
                    con.connect.Close();
                }
            }
            catch
            {
                throw;
            }

            return dt;
        }

        public bool removeEmpleado(int empleadoID)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                if (con.connect.State == ConnectionState.Closed)
                {
                    con.connect.Open();
                }

                string query = "DELETE FROM Empleados WHERE ID = @empleadoID";
                using (SqlCommand cmd = new SqlCommand(query, con.connect))
                {
                    cmd.Parameters.AddWithValue("@empleadoID", empleadoID);

                    cmd.ExecuteNonQuery();
                }

                if (con.connect.State == ConnectionState.Open)
                {
                    con.connect.Close();
                }

                return true;
            }
            catch
            {
                throw;
            }
        }


    }
}