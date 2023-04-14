using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDiseñoApps
{
    public class ConnectionDB 
    {
        private SqlConnection connection;
        private string connectionString = "data source=PC\\SQLEXPRESS;initial catalog=Autolavado; user id=UserAutoLavado;password=1234;";

        public ConnectionDB()
        {
            connection = new SqlConnection(connectionString);
        }

        public SqlConnection GetConnection()
        {
            return connection;
        }

    }

}
