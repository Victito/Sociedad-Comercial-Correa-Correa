using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using prueba.Vista;
using SociedadCorreaCorrea.Data;
using SociedadCorreaCorrea.Models;
using SociedadCorreaCorrea.ViewModels;
using SociedadCorreaCorrea.ViewsModels;
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
using System.Windows.Shapes;

namespace SociedadCorreaCorrea.Views
{
    /// <summary>
    /// Interaction logic for ActualizarProductos.xaml
    /// </summary>
    public partial class ActualizarProductos : MetroWindow
    {
        private Producto _productoSeleccionado;

        public ActualizarProductos(Producto productoSeleccionado)
        {
            InitializeComponent();
            NombreUsuario.Text = UserSession.NombreUsuario;
            _productoSeleccionado = productoSeleccionado;

            // Rellenar los campos con la información del producto seleccionado
            CargarDatosProducto();
        }

        private void CargarDatosProducto()
        {
            txtIdFactura.Text = _productoSeleccionado.IdFactura.ToString();
            txtIdProducto.Text = _productoSeleccionado.IdProducto.ToString();
            txtNumeroFactura.Text = _productoSeleccionado.NumeroFactura.ToString();
            txtCodigoProducto.Text = _productoSeleccionado.CodigoProducto;
            txtDescripcion.Text = _productoSeleccionado.Descripcion;
            txtNSerie.Text = _productoSeleccionado.NSerie;
            txtCantidad.Text = _productoSeleccionado.Cantidad.ToString();
            txtPrecioUnitario.Text = _productoSeleccionado.PrecioUnitario.ToString();
            txtDescuento.Text = _productoSeleccionado.Descuento.ToString();
            txtTotal.Text = _productoSeleccionado.Total.ToString(); // Si es necesario
        }

        private void GuardarCambios_Click(object sender, RoutedEventArgs e)
        {
            // Actualizar las propiedades del producto seleccionado
            _productoSeleccionado.IdFactura = int.Parse(txtIdFactura.Text);
            _productoSeleccionado.IdProducto = int.Parse(txtIdProducto.Text);
            _productoSeleccionado.NumeroFactura = int.Parse(txtNumeroFactura.Text);
            _productoSeleccionado.CodigoProducto = txtCodigoProducto.Text;
            _productoSeleccionado.Descripcion = txtDescripcion.Text;
            _productoSeleccionado.NSerie = txtNSerie.Text;
            _productoSeleccionado.Cantidad = int.Parse(txtCantidad.Text);
            _productoSeleccionado.PrecioUnitario = decimal.Parse(txtPrecioUnitario.Text);
            _productoSeleccionado.Descuento = int.Parse(txtDescuento.Text);
            _productoSeleccionado.Total = _productoSeleccionado.PrecioUnitario * _productoSeleccionado.Cantidad * (1 - _productoSeleccionado.Descuento / 100.0m);

            // Crear una instancia del ViewModel para manejar la actualización
            var viewModel = new ActualizarProductosViewModel(new ContextoSMMS());
            viewModel.ActualizarProducto(_productoSeleccionado); // Método para actualizar en la base de datos

            // Mostrar un mensaje de éxito
            MessageBox.Show("Producto actualizado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

            // Crear y mostrar la ventana de RegistroFacturas
            var ProductosFacturas = new ProductoFacturas(int.Parse(txtIdFactura.Text));
            ProductosFacturas.Show();

            // Cierra la ventana de ActualizarProductos
            this.Close();
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
        private async void CerrarSesion_Click(object sender, MouseButtonEventArgs e)
        {
            // Aquí, la ventana CerrarSesion se muestra de manera modal
            CerrarSesion ventanaCerrarSesion = new CerrarSesion();
            ventanaCerrarSesion.ShowDialog();

        }
    }
}
