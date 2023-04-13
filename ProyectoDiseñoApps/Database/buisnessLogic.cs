using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoDiseñoApps.Database
{
    //to create a connection between Data Access Layer and Presentation (UI) Layer
    //TODO: HACER QUE SIRVA TANTO PARA EMPLEADOS COMO PARA CLIENTES
    class buisnessLogic
    {

        #region saveItems

        public bool saveItems(String NOMBRE, String APELLIDOS, String CEDULA, String CORREO)
        {
            try
            {
                dataAccess objdal = new dataAccess(); //creates a DAL object to access its functions
                return objdal.addEmployees(NOMBRE, APELLIDOS, CEDULA, CORREO); //passes values to DAL
            }  catch (Exception e) {
                DialogResult result = MessageBox.Show(e.Message.ToString());
                return false;

            }
        }

        #endregion

        #region getItems
        public DataTable getItems()
        {
            try
            {
                dataAccess objdal = new dataAccess(); //creates a DAL object to access its functions
                return objdal.readEmployeesTable();

            } catch (Exception e)
            {
                DialogResult result = MessageBox.Show(e.Message.ToString());
                return null;
            }
        }

        #endregion

    }
}
