using MahApps.Metro.Controls;
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

namespace SociedadCorreaCorrea.Views
{
    /// <summary>
    /// Lógica de interacción para Trabajadores.xaml
    /// </summary>
    public partial class Trabajadores : MetroWindow
    {
        public Trabajadores()
        {
            InitializeComponent();
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
        

        private void HistorialFacturas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                var historialFacturas = new HistorialFacturas();
                historialFacturas.Show();
                this.Close();
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
