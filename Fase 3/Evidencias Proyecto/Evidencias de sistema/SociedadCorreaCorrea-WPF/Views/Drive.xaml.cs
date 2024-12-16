using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using SociedadCorreaCorrea.ViewModels;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using MahApps.Metro.Controls.Dialogs;
using prueba.Vista;
using Microsoft.Web.WebView2.Wpf; // Asegúrate de importar este espacio de nombres

namespace SociedadCorreaCorrea.Views
{
    /// <summary>
    /// Lógica de interacción para Servicios.xaml
    /// </summary>
    public partial class Drive : MetroWindow
    {
        public Drive()
        {
            InitializeComponent();
            DataContext = new DriveViewModel(this);
            NombreUsuario.Text = UserSession.NombreUsuario;
            // Asegúrate de que WebView2 esté correctamente inicializado
            WebViewGoogleDrive.EnsureCoreWebView2Async(null);
            // Registrar el evento de inicialización de WebView2
            InicializarWebView2();

        }

        // Método para inicializar WebView2
        private async void InicializarWebView2()
        {
            try
            {
                // Intentar asegurar la inicialización de WebView2
                await WebViewGoogleDrive.EnsureCoreWebView2Async();


            }
            catch (Exception ex)
            {
                // Si hay un error, mostramos detalles más completos
                string errorDetalles = ObtenerDetallesError(ex);
                MostrarMensaje("Error", $"No se pudo inicializar WebView2: {errorDetalles}");
            }
        }

        // Método para mostrar los mensajes
        private void MostrarMensaje(string titulo, string mensaje)
        {
            // Mostrar el mensaje en la interfaz de usuario
            MessageBox.Show(mensaje, titulo, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // Método para obtener detalles completos del error
        private string ObtenerDetallesError(Exception ex)
        {
            // Devuelve el mensaje completo de la excepción y la pila de llamadas
            return $"Mensaje: {ex.Message}\nPila de llamadas:\n{ex.StackTrace}";
        }


        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void Ventana_MouseBajo(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HistorialFacturas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var historialFacturas = new HistorialFacturas();
                historialFacturas.Show();

                // Cierra la ventana de MainMenu
                this.Close();
            }
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMaximizar_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

        private void IngresarFacturas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                var registroFacturas = new RegistroFacturas();
                registroFacturas.Show();
                this.Close();
            }
        }
        private void Drive_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var Drive = new Drive();
                Drive.Show();

                // Cierra la ventana de MainMenu
                this.Close();
            }
        }
        private void Servicios_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var Servicios = new Servicios();
                Servicios.Show();

                // Cierra la ventana de MainMenu
                this.Close();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.ContextMenu != null)
            {
                button.ContextMenu.IsOpen = true;
            }
        }
        private async void CerrarSesion_Click(object sender, MouseButtonEventArgs e)
        {
            // Aquí, la ventana CerrarSesion se muestra de manera modal
            CerrarSesion ventanaCerrarSesion = new CerrarSesion();
            ventanaCerrarSesion.ShowDialog();

        }
        private void Trabajadores_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var Trabajadores = new Trabajadores();
                Trabajadores.Show();

                // Cierra la ventana de MainMenu
                this.Close();
            }
        }
        private void DatosEstadisticosFacturas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var datosEstadisticos = new GraficosFacturas();
                datosEstadisticos.Show();

                // Cierra la ventana de MainMenu
                this.Close();
            }
        }
        private void Inicio_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var MainMenu = new MainMenu();
                MainMenu.Show();

                // Cierra la ventana de MainMenu
                this.Close();
            }
        }


    }
}
