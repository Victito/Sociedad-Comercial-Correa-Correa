using MahApps.Metro.Controls;
using SociedadCorreaCorrea.ViewsModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace prueba.Vista
{
    /// <summary>
    /// Lógica de interacción para la ventana de inicio de sesión (LoginVista.xaml).
    /// </summary>
    public partial class LoginVista : MetroWindow
    {
        #region Constructor

        /// <summary>
        /// Constructor de la ventana LoginVista. Inicializa los componentes y asigna el ViewModel como DataContext.
        /// </summary>
        public LoginVista()
        {
            InitializeComponent();
            DataContext = new LoginVistaViewModel(this); // Pasar la instancia de la ventana al ViewModel
        }

        #endregion

        #region Métodos de Control de Ventana

        /// <summary>
        /// Permite arrastrar la ventana cuando el mouse se presiona y mueve.
        /// </summary>
        private void Ventana_MouseBajo(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove(); // Mover la ventana al arrastrar con el botón izquierdo del mouse
        }

        /// <summary>
        /// Minimiza la ventana al hacer clic en el botón de minimizar.
        /// </summary>
        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized; // Minimizar la ventana
        }

        /// <summary>
        /// Cierra la aplicación al hacer clic en el botón de cerrar.
        /// </summary>
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Cerrar la aplicación
        }

        #endregion

        #region Eventos de Control de Usuario

        /// <summary>
        /// Evento que se dispara cuando cambia el texto en el cuadro de texto de usuario.
        /// Actualmente vacío, pero puede ser útil para validaciones o lógica futura.
        /// </summary>
        private void txtusuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Puedes agregar aquí lógica de validación del usuario si es necesario
        }

        /// <summary>
        /// Evento que se dispara cuando cambia la contraseña en el PasswordBox.
        /// Actualiza la propiedad 'Clave' en el ViewModel.
        /// </summary>
        private void txtContraseña_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                // Actualizar la propiedad 'Clave' en el ViewModel
                var viewModel = this.DataContext as LoginVistaViewModel;
                if (viewModel != null)
                {
                    viewModel.Clave = passwordBox.Password; // Asignar el valor de la contraseña al ViewModel
                }
            }
        }

        /// <summary>
        /// Evento que se dispara cuando se hace clic en el botón de navegación a la página de facturas.
        /// Implementar la lógica de navegación aquí.
        /// </summary>
        private void NavigateToFacturasPage(object sender, RoutedEventArgs e)
        {
            // Aquí puedes añadir lógica para navegar a otra página, como la página de facturas
        }

        #endregion
    }
}
