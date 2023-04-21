using ProyectoDiseñoApps.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProyectoDiseñoApps.view
{
    /// <summary>
    /// Interaction logic for logView.xaml
    /// </summary>
    public partial class logView : Window
    {
        public logView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //establece conexion a la BD
            ConnectionDB con = new ConnectionDB();
            if (ConnectionState.Closed == con.connect.State)
            {
                con.connect.Open();
            }

            String query = "SELECT * FROM Empleados where empleadoCorreo=@user and empleadoCedula=@password";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con.connect))
                {
                    //añade los valores al query
                    cmd.Parameters.AddWithValue("@user", txtUser.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", txtPass.Text.Trim());

                    cmd.ExecuteNonQuery(); //ejecuta el query
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if(count  > 0)
                    {
                        MainWindow main = new MainWindow();
                        this.Close();
                        main.Show();

                    } else {
                        MessageBox.Show("Username or password are not correct");
                    }
                }
            }
            catch { throw; }
        }
    }
}
