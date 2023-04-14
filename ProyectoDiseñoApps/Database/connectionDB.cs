using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDiseñoApps
{
    class ConnectionDB
    {
        private string connectionString;
        private SqlConnection connection;

        public ConnectionDB()
        {
            connectionString = "Data Source=PC\\SQLEXPRESS;Initial Catalog=Autolavado;User ID=UsuarioAutoLavado;Password=1234;";
            connection = new SqlConnection(connectionString);
        }

        public SqlConnection GetConnection()
        {
            return connection;
        }
    }
}
