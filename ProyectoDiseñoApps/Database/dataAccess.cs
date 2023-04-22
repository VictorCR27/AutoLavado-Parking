using ProyectoDiseñoApps.view;
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
<<<<<<< HEAD
        public bool addServicio(String CarroModelo, String CarroPlaca, String TipoServicio, String ParqueoEspacio, String ParqueoHora)
        {
=======
        public bool addServicio(String CarroModelo, String CarroPlaca, String TipoServicio, String ParqueoEspacio, String ParqueoHora )
        { 
>>>>>>> 55a3232e71646b93aaa846d27a47a835b6a8697a
            //establece conexion a la base de datos
            ConnectionDB con = new ConnectionDB();
            if (ConnectionState.Closed == con.connect.State)
            {
                con.connect.Open();
            }

            String query = "INSERT INTO Servicios(CarroModelo, CarroPlaca, TipoServicio, ParqueoEspacio, ParqueoHora, estado, Costo) VALUES (@CarroModelo, @CarroPlaca, @TipoServicio, @ParqueoEspacio, @ParqueoHora, 1, @Precio)"; String query2 = "UPDATE EspacioParqueo SET estado = 1 WHERE descripcion = @ParqueoEspacio";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con.connect))

                {
                    //añade los valores al query
                    cmd.Parameters.AddWithValue("@CarroModelo", CarroModelo.Trim());
                    cmd.Parameters.AddWithValue("@CarroPlaca", CarroPlaca.Trim());
                    cmd.Parameters.AddWithValue("@TipoServicio", TipoServicio.Trim());
                    cmd.Parameters.AddWithValue("@ParqueoEspacio", ParqueoEspacio.Trim());
<<<<<<< HEAD
                    cmd.Parameters.AddWithValue("@ParqueoHora", ParqueoHora.Trim());
=======
                    cmd.Parameters.AddWithValue("@ParqueoHora", ParqueoHora);
                    cmd.Parameters.AddWithValue("@Precio", Costo);

                    cmd.ExecuteNonQuery();

                    //Ejecuta el segundo query de update para modificar el estado del espacio de parqueo
                    SqlCommand cmd2 = new SqlCommand(query2, con.connect);
                    cmd2.Parameters.AddWithValue("@ParqueoEspacio", ParqueoEspacio.Trim());
                    cmd2.ExecuteNonQuery(); //ejecuta el segundo query

                    MessageBox.Show($"Servicio Agregado");
        


                }
                // Cierra la conexión después de usarla
                if (con.connect.State == ConnectionState.Open)
                {
                    con.connect.Close();
                }
                return true;
            } catch { throw; }
>>>>>>> 55a3232e71646b93aaa846d27a47a835b6a8697a

                    cmd.ExecuteNonQuery(); //ejecuta el query
                }
                return true;
            }
            catch { throw; }

        }
<<<<<<< HEAD
        #endregion

        #region añadirEmpleado
        public bool addEmpleado(String empleadoNombre, String empleadoCedula, String empleadoCorreo)
        {
            //establece la conexion a la BD
            ConnectionDB con = new ConnectionDB();
            if(ConnectionState.Closed == con.connect.State)
            {
                con.connect.Open();
            }

            String query = "Insert into Empleados(empleadoNombre,empleadoCorreo,empleadoCedula) VALUES (@empleadoNombre,@empleadoCorreo,@empleadoCedula)";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con.connect))
                {
                    //añade los valores al query
                    cmd.Parameters.AddWithValue("@empleadoNombre", empleadoNombre.Trim());
                    cmd.Parameters.AddWithValue("@empleadoCorreo", empleadoCorreo.Trim());
                    cmd.Parameters.AddWithValue("@empleadoCedula", empleadoCedula.Trim());


                    cmd.ExecuteNonQuery(); //ejecuta el query

                }
                return true;
            }
            catch { throw; }
        }

        #endregion

   

=======


>>>>>>> 55a3232e71646b93aaa846d27a47a835b6a8697a
    }

}
