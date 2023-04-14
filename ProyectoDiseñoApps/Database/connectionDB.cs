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
        public SqlConnection connect = new SqlConnection("Data Source=.; Initial Catalog= DBDiseñoApps; Integrated Security= True");
    }

}
