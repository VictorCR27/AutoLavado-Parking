using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoDiseñoApps.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields



        private ViewModelBase _currentChildView;
        private String _caption;
        private IconChar _icon;
        #endregion

        #region Propiedades
        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }



        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public IconChar Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }
        #endregion

        #region Comandos para interacción del usuario
        public ICommand ShowInicioViewCommand { get; }
        public ICommand ShowServiciosViewCommand { get; }

        public ICommand ShowEstadoViewCommand { get; }
        #endregion

        public MainViewModel()
        {
            #region Se inicializan los comandos
            ShowServiciosViewCommand = new ViewModelCommand(ExecuteShowServiciosViewCommand);
            #endregion
            ShowInicioViewCommand = new ViewModelCommand(ExecuteShowInicioViewCommand);

            ShowEstadoViewCommand = new ViewModelCommand(ExecuteShowEstadoViewCommand);


            // Vista predeterminada
            ExecuteShowInicioViewCommand(null);
        }


        private void ExecuteShowInicioViewCommand(object obj)
        {
            CurrentChildView = new InicioViewModel();
            Caption = "Inicio";
            Icon = IconChar.HomeAlt;
        }

        private void ExecuteShowServiciosViewCommand(object obj)
        {
            CurrentChildView = new ServiciosViewModel();
            Caption = "Servicios de lavado";
            Icon = IconChar.CarAlt;
        }

        private void ExecuteShowEstadoViewCommand(object obj)
        {
            CurrentChildView = new EstadoViewModel();
            Caption = "Estado de Servicios";
            Icon = IconChar.Parking;
        }





    }
}
