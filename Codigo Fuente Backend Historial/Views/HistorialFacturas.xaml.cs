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

        private void ActualizarFacturaSeleccionada_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la factura seleccionada del DataGrid
            if (facturasDataGrid.SelectedItem is InformacionFacturas facturaSeleccionada)
            {
                // Crear una nueva instancia de la ventana de actualización
                var actualizarVentana = new ActualizarFacturas(facturaSeleccionada);
                actualizarVentana.Show(); // Abrir como ventana normal
                this.Close();
            }
            else
            {
                // Mostrar una alerta si no se seleccionó ninguna factura
                MessageBox.Show("Por favor, seleccione una factura para actualizar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
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


    }
}
