using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDiseñoApps.Database
{
    class dataAccess
    {

        #region añadirServicio
        public bool addServicio(String CarroModelo,String CarroPlaca, String TipoServicio, String ParqueoEspacio, String ParqueoHora)
        {
            //establece conexion a la base de datos
            ConnectionDB con = new ConnectionDB();
            if(ConnectionState.Closed == con.connect.State) {
                con.connect.Open();
            }

            String query = "Insert into Servicios(CarroModelo,CarroPlaca,TipoServicio,ParqueoEspacio,ParqueoHora) VALUES (@CarroModelo,@CarroPlaca,@TipoServicio,@ParqueoEspacio,@ParqueoHora)";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con.connect))
                {
                    //añade los valores al query
                    cmd.Parameters.AddWithValue("@CarroModelo", CarroModelo.Trim());
                    cmd.Parameters.AddWithValue("@CarroPlaca", CarroPlaca.Trim());
                    cmd.Parameters.AddWithValue("@TipoServicio", TipoServicio.Trim());
                    cmd.Parameters.AddWithValue("@ParqueoEspacio", ParqueoEspacio.Trim());
                    cmd.Parameters.AddWithValue("@ParqueoHora", ParqueoHora.Trim());
                        
                    cmd.ExecuteNonQuery(); //ejecuta el query
                } return true;
            } catch { throw; }

            #endregion

        }
    }
}
