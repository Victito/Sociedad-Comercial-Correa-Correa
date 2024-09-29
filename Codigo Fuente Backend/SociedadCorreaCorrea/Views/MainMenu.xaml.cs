using MahApps.Metro.Controls;
using SociedadCorreaCorrea.Views;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace SociedadCorreaCorrea.Views
{
    /// <summary>
    /// Lógica de interacción para la ventana del menú principal (MainMenu.xaml).
    /// </summary>
    public partial class MainMenu : MetroWindow
    {
        #region Constructor

        /// <summary>
        /// Constructor de la ventana MainMenu. Inicializa los componentes y establece el tamaño máximo de la ventana.
        /// </summary>
        public MainMenu()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight; // Establece la altura máxima al tamaño de la pantalla
        }

        #endregion

        #region DLL Import para Mover Ventana

        /// <summary>
        /// Importación de la función SendMessage de user32.dll para mover la ventana cuando el mouse es arrastrado.
        /// </summary>
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        /// <summary>
        /// Evento para mover la ventana cuando el usuario hace clic y arrastra con el mouse.
        /// </summary>
        /// <param name="sender">Objeto que dispara el evento.</param>
        /// <param name="e">Argumentos del evento del mouse.</param>
        private void Ventana_MouseBajo(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0); // Mueve la ventana con el mensaje 161 (WM_NCLBUTTONDOWN)
        }

        #endregion

        #region Eventos de la Ventana

        /// <summary>
        /// Ajusta la altura máxima de la ventana cuando el mouse entra en el panel de control.
        /// Esto asegura que la ventana no exceda el tamaño de la pantalla cuando esté maximizada.
        /// </summary>
        /// <param name="sender">Objeto que dispara el evento.</param>
        /// <param name="e">Argumentos del evento del mouse.</param>
        private void panelControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight; // Actualiza la altura máxima
        }

        /// <summary>
        /// Evento que se dispara al hacer clic en el botón de cerrar. Cierra la aplicación.
        /// </summary>
        /// <param name="sender">Objeto que dispara el evento.</param>
        /// <param name="e">Argumentos del evento del clic.</param>
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Cierra la aplicación
        }

        /// <summary>
        /// Evento que se dispara al hacer clic en la opción de "Ingresar Facturas". Abre la ventana de registro de facturas.
        /// </summary>
        /// <param name="sender">Objeto que dispara el evento.</param>
        /// <param name="e">Argumentos del evento del mouse.</param>
        private void IngresarFacturas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var registroFacturas = new RegistroFacturas();
                registroFacturas.Show();

                // Cierra la ventana de MainMenu
                this.Close();
            }
        }

        #endregion
    }
}
