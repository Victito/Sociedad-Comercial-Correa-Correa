using MahApps.Metro.Controls;
using SociedadCorreaCorrea.Data;
using SociedadCorreaCorrea.Models;
using SociedadCorreaCorrea.ViewModels;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace SociedadCorreaCorrea.Views
{
    public partial class ActualizarFacturas : MetroWindow
    {
        private InformacionFacturas _facturaSeleccionada;

        public ActualizarFacturas(InformacionFacturas facturaSeleccionada)
        {
            InitializeComponent();
            _facturaSeleccionada = facturaSeleccionada;

            // Rellenar los campos con la información de la factura seleccionada
            CargarDatosFactura();
        }

        private void CargarDatosFactura()
        {
            // Rellenar los campos con los datos de la factura
            txtId.Text = _facturaSeleccionada.Factura.IdFactura.ToString();
            txtCliente.Text = _facturaSeleccionada.Factura.RazonSocial;
            txtTotal.Text = _facturaSeleccionada.Factura.Total.ToString();

            txtIdSucursal.Text = _facturaSeleccionada.Factura.IdSucursal.ToString();
            txtRutVendedor.Text = _facturaSeleccionada.Factura.RutVendedor;
            txtGiroVendedor.Text = _facturaSeleccionada.Factura.GiroVendedor;
            txtRazonSocialVendedor.Text = _facturaSeleccionada.Factura.RazonSocialVendedor;
            txtRazonSocial.Text = _facturaSeleccionada.Factura.RazonSocial;
            txtRutCliente.Text = _facturaSeleccionada.Factura.RutEmisor;
            txtGiroCliente.Text = _facturaSeleccionada.Factura.Giro;
            txtDireccion.Text = _facturaSeleccionada.Factura.Direccion;
            txtComuna.Text = _facturaSeleccionada.Factura.Comuna;
            txtCiudad.Text = _facturaSeleccionada.Factura.Ciudad;
            txtEntregarEn.Text = _facturaSeleccionada.Factura.EntregarEn;
            // Asignar las fechas al DatePicker
            // Asignar las fechas utilizando DateOnly
            dpFechaEmision.SelectedDate = _facturaSeleccionada.Factura.FechaEmision.HasValue
                ? DateTime.SpecifyKind(_facturaSeleccionada.Factura.FechaEmision.Value.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc)
                : (DateTime?)null;

            dpFechaVencimiento.SelectedDate = _facturaSeleccionada.Factura.FechaVencimiento.HasValue
                ? DateTime.SpecifyKind(_facturaSeleccionada.Factura.FechaVencimiento.Value.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc)
                : (DateTime?)null;


            txtCobrador.Text = _facturaSeleccionada.Factura.Cobrador;
            txtNotaVenta.Text = _facturaSeleccionada.Factura.NotaVenta.ToString();
            txtOrdenCompra.Text = _facturaSeleccionada.Factura.OrdenCompra;
            txtCondiciones.Text = _facturaSeleccionada.Factura.Condiciones;
            txtGuiaDespacho.Text = _facturaSeleccionada.Factura.GuiaDespacho;
            txtPrecioUnitario.Text = _facturaSeleccionada.Factura.PrecioUnitario.ToString();
            txtCantidad.Text = _facturaSeleccionada.Factura.Cantidad.ToString();
            txtEstado.Text = _facturaSeleccionada.Factura.Estado;
        }

        private void GuardarCambios_Click(object sender, RoutedEventArgs e)
        {
            // Crear una nueva instancia de InformacionFacturas con los datos actualizados
            var facturaActualizada = new InformacionFacturas
            {
                Factura = new Factura
                {
                    IdFactura = int.Parse(txtId.Text),
                    RazonSocial = txtCliente.Text,
                    Total = decimal.Parse(txtTotal.Text),
                    IdSucursal = int.Parse(txtIdSucursal.Text),
                    RutVendedor = txtRutVendedor.Text,
                    GiroVendedor = txtGiroVendedor.Text,
                    RazonSocialVendedor = txtRazonSocialVendedor.Text,
                    RutEmisor = txtRutCliente.Text,
                    Giro = txtGiroCliente.Text,
                    Direccion = txtDireccion.Text,
                    Comuna = txtComuna.Text,
                    Ciudad = txtCiudad.Text,
                    EntregarEn = txtEntregarEn.Text,
                    // Convertir las fechas de DateTime a DateOnly
                    FechaEmision = dpFechaEmision.SelectedDate.HasValue
                ? DateOnly.FromDateTime(dpFechaEmision.SelectedDate.Value)
                : (DateOnly?)null,

                    FechaVencimiento = dpFechaVencimiento.SelectedDate.HasValue
                ? DateOnly.FromDateTime(dpFechaVencimiento.SelectedDate.Value)
                : (DateOnly?)null,
                    Cobrador = txtCobrador.Text,
                    // Convierte txtNotaVenta a int
                    NotaVenta = int.Parse(txtNotaVenta.Text),
                    OrdenCompra = txtOrdenCompra.Text,
                    Condiciones = txtCondiciones.Text,
                    GuiaDespacho = txtGuiaDespacho.Text,
                    PrecioUnitario = decimal.Parse(txtPrecioUnitario.Text),
                    Cantidad = int.Parse(txtCantidad.Text),
                    Estado = txtEstado.Text
                }
            };

            // Crear una instancia del ViewModel para manejar la actualización
            var viewModel = new ActualizarFacturasViewModel(new ContextoSMMS());
            viewModel.ActualizarFactura(facturaActualizada); // Método para actualizar en la base de datos

            // Mostrar un mensaje de éxito
            MessageBox.Show("Factura actualizada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

            // Crear y mostrar la ventana de RegistroFacturas
            var historialFacturas = new HistorialFacturas();
            historialFacturas.Show();

            // Cierra la ventana de ActualizarFacturas
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


    }
}
