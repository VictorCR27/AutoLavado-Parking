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
        #region addEmployees

        public bool addEmployees(String NOMBRE, String APELLIDOS, String CEDULA, String CORREO)
        {
            //establishes a connection utilizing connectionDB class.
            connectionDB con = new connectionDB();

            
            if(ConnectionState.Closed == con.connect.State) //checks connection state
            {
                con.connect.Open(); //opens connection, if connection is closed
            }

            //MSQL query template
            string query = "Insert into EMPLEADOS(NOMBRE,APELLIDOS,CEDULA,CORREO)values(@NOMBRE,@APELLIDOS,@CEDULA,@CORREO)";

            //trys to send the query to the database, in order to add information
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con.connect))
                {
                    //sets attributes to parameters stablished on query.
                    cmd.Parameters.AddWithValue("@NOMBRE", NOMBRE.Trim());
                    cmd.Parameters.AddWithValue("@APELLIDOS", APELLIDOS.Trim());
                    cmd.Parameters.AddWithValue("@CEDULA", CEDULA.Trim());
                    cmd.Parameters.AddWithValue("@CORREO", CORREO.Trim());
                }
                return true;
            } catch
            {
                throw;
            }

        }
        #endregion

        #region readEmployees
        public DataTable readEmployeesTable()
        {
            //establishes a connection utilizing connectionDB class.
            connectionDB con = new connectionDB();


            if (ConnectionState.Closed == con.connect.State) //checks connection state
            {
                con.connect.Open(); //opens connection, if connection is closed
            }

            //query selector template
            String query = "SELECT * FROM EMPLEADOS";

            SqlCommand cmd = new SqlCommand(query, con.connect);

            try
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable(); //creates a data table
                    sda.Fill(dt); //fills data table with all records from DB
                    return dt; //returns data table
                }
            } catch
            {
                throw;
            }

        }

        #endregion
    }
}
