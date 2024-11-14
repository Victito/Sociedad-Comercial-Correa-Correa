using System;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using SociedadCorreaCorrea.ViewModels;
using SociedadCorreaCorrea.Data;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using SociedadCorreaCorrea.Models;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using static SociedadCorreaCorrea.ViewModels.HistorialFacturasViewModel;
using MahApps.Metro.Controls.Dialogs;

namespace SociedadCorreaCorrea.Views
{
    public partial class HistorialFacturas : MetroWindow
    {
        public HistorialFacturas()
        {
            InitializeComponent();
            DataContext = new HistorialFacturasViewModel(new ContextoSMMS(), this); // Pasar la ventana actual como parámetro
        }

        private void EliminarFacturasSeleccionadas_Click(object sender, RoutedEventArgs e)
        {
            // Obtener las facturas seleccionadas del DataGrid
            var seleccionadas = facturasDataGrid.SelectedItems;

            // Crear una lista de facturas seleccionadas para pasarlas al ViewModel
            var facturasAEliminar = new ObservableCollection<InformacionFacturas>();

            foreach (var seleccion in seleccionadas)
            {
                // Asegurarse de que el objeto sea de tipo InformacionFacturas
                if (seleccion is InformacionFacturas facturaSeleccionada)
                {
                    facturasAEliminar.Add(facturaSeleccionada);
                }
            }

            // Llamar al método de eliminación en el ViewModel
            var viewModel = (HistorialFacturasViewModel)DataContext;
            viewModel.EliminarFacturasSeleccionadas(facturasAEliminar);
        }

        // Método actualizado para manejar la actualización de una factura seleccionada
        private async void ActualizarFacturaSeleccionada_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si se seleccionaron más de una factura
            var seleccionadas = facturasDataGrid.SelectedItems;

            if (seleccionadas.Count > 1)
            {
                // Mostrar mensaje de error si hay más de una factura seleccionada
                await this.ShowMessageAsync("Error!", "Asegúrese de solo seleccionar una factura antes de actualizar.");
                return;
            }

            // Si no hay ninguna factura seleccionada, mostrar un mensaje de advertencia
            if (facturasDataGrid.SelectedItem == null)
            {
                await this.ShowMessageAsync("Advertencia", "Por favor, seleccione una factura para actualizar.");
                return;
            }

            // Obtener la factura seleccionada y proceder con la actualización
            if (facturasDataGrid.SelectedItem is InformacionFacturas facturaSeleccionada)
            {
                // Crear y mostrar la ventana de actualización
                var actualizarVentana = new ActualizarFacturas(facturaSeleccionada);
                actualizarVentana.Show();
                this.Close();
            }
        }

        // Método para manejar la visualización de los productos de una factura seleccionada
        private async void ProductosFacturaSeleccionada_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si se seleccionaron más de una factura
            var seleccionadas = facturasDataGrid.SelectedItems;

            if (seleccionadas.Count > 1)
            {
                // Mostrar mensaje de error si hay más de una factura seleccionada
                await this.ShowMessageAsync("Error!", "Asegúrese de solo seleccionar una factura antes de ver los productos.");
                return;
            }

            // Si no hay ninguna factura seleccionada, mostrar un mensaje de advertencia
            if (facturasDataGrid.SelectedItem == null)
            {
                await this.ShowMessageAsync("Advertencia", "Por favor, seleccione una factura para ver sus productos.");
                return;
            }

            // Obtener la factura seleccionada
            if (facturasDataGrid.SelectedItem is InformacionFacturas facturaSeleccionada)
            {
                // Obtener la ID de la factura seleccionada
                var facturaId = facturaSeleccionada.Factura.IdFactura;

                // Pasar la ID a la nueva vista ProductoFacturas y mostrarla
                var productosVentana = new ProductoFacturas(facturaId);
                productosVentana.Show(); // Mostrar la ventana de productos
                this.Close(); // Cerrar la ventana actual si es necesario
            }
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
        private void CerrarSesion_Click(object sender, MouseButtonEventArgs e)
        {
            // Coloca aquí la lógica para cerrar sesión
            MessageBox.Show("Cerrar sesión");
        }


    }
}
