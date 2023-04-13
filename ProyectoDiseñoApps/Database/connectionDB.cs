using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDiseñoApps
{
    class connectionDB
    {
        //Establishes connection to AUTOLAVADOCR database.
        public SqlConnection connect = new SqlConnection("Data Source=.; Initial Catalog= AUTOLAVADOCR; Integrated Security= True");
    }
}
