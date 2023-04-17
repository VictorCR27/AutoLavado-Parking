using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProyectoDiseñoApps.Database
{
    class dataAccess
    {
        private object dbAccess;
        

        #region añadirServicio
        public bool addServicio(String CarroModelo, String CarroPlaca, String TipoServicio, String ParqueoEspacio, String ParqueoHora )
        { 
            //establece conexion a la base de datos
            ConnectionDB con = new ConnectionDB();
            if (ConnectionState.Closed == con.connect.State)
            {
                con.connect.Open();
            }

            String query = "Insert into Servicios(CarroModelo,CarroPlaca,TipoServicio,ParqueoEspacio,ParqueoHora, estado) VALUES (@CarroModelo,@CarroPlaca,@TipoServicio,@ParqueoEspacio,@ParqueoHora,1)";
            String query2 = "UPDATE EspacioParqueo SET estado = 1 WHERE descripcion = @ParqueoEspacio";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con.connect))
                    
                {
                    //añade los valores al query
                    cmd.Parameters.AddWithValue("@CarroModelo", CarroModelo.Trim());
                    cmd.Parameters.AddWithValue("@CarroPlaca", CarroPlaca.Trim());
                    cmd.Parameters.AddWithValue("@TipoServicio", TipoServicio.Trim());
                    cmd.Parameters.AddWithValue("@ParqueoEspacio", ParqueoEspacio.Trim());
                    cmd.Parameters.AddWithValue("@ParqueoHora", ParqueoHora);

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

            #endregion

        }


    }

}
