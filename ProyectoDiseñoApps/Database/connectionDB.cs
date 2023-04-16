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
        public SqlConnection connect = new SqlConnection("data source=PC\\SQLEXPRESS;initial catalog=Autolavado; user id=UserAutoLavado;password=1234;");
    }

}
