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
using System.Windows.Shapes;
using SociedadCorreaCorrea.Models;
using System.Collections.ObjectModel;
using System.Linq;
using SociedadCorreaCorrea.ViewsModels;
using SociedadCorreaCorrea.ViewModels;
using MahApps.Metro.Controls.Dialogs;


namespace SociedadCorreaCorrea.Views
{
    /// <summary>
    /// Interaction logic for ProductoFacturas.xaml
    /// </summary>
    public partial class ProductoFacturas : MetroWindow
    {

        public ProductoFacturas(int facturaId)
        {
            InitializeComponent();
            // Pasamos la ID de la factura al ViewModel
            DataContext = new ProductoFacturasViewModel(facturaId);
        }

        // Método para manejar la eliminación de productos seleccionados
        private void EliminarProductosSeleccionados_Click(object sender, RoutedEventArgs e)
        {
            // Obtener los productos seleccionados del DataGrid
            var seleccionadas = productosDataGrid.SelectedItems; // Asegúrate de que esto sea el nombre correcto del DataGrid de productos

            // Crear una lista de productos seleccionados para pasarlas al ViewModel
            var productosAEliminar = new ObservableCollection<Producto>();

            foreach (var seleccion in seleccionadas)
            {
                // Asegurarse de que el objeto sea de tipo Producto
                if (seleccion is Producto productoSeleccionado)
                {
                    productosAEliminar.Add(productoSeleccionado);
                }
            }

            // Verificar que hay productos seleccionados para eliminar
            if (productosAEliminar.Count > 0)
            {
                // Obtener el IdFactura del primer producto seleccionado (suponiendo que todos los productos seleccionados son de la misma factura)
                var idFactura = productosAEliminar.First().IdFactura;

                // Obtener el ViewModel y eliminar los productos seleccionados
                var viewModel = (ProductoFacturasViewModel)DataContext;
                viewModel.EliminarProductosSeleccionados(productosAEliminar, idFactura);
            }
            else
            {
                // Mensaje de advertencia si no hay productos seleccionados
                MessageBox.Show("Por favor, seleccione al menos un producto para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void ActualizarProducto_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si se seleccionó un producto
            if (productosDataGrid.SelectedItem == null)
            {
                await this.ShowMessageAsync("Advertencia", "Por favor, seleccione un producto para actualizar.");
                return;
            }

            // Obtener el producto seleccionado
            if (productosDataGrid.SelectedItem is Producto productoSeleccionado)
            {
                // Crear y mostrar la ventana de actualización
                var actualizarVentana = new ActualizarProductos(productoSeleccionado);
                actualizarVentana.Show();
                this.Close(); // Cierra la ventana actual, si es necesario
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

    }
}
