﻿using SociedadCorreaCorrea.Commands;
using SociedadCorreaCorrea.Data;
using SociedadCorreaCorrea.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.IO;
using Microsoft.Win32; // Necesario para SaveFileDialog
using System.Windows;

namespace SociedadCorreaCorrea.ViewModels
{
    public class ProductoFacturasViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<Producto> _productos;
        public ObservableCollection<Producto> Productos { get; set; }

        public ObservableCollection<Producto> ProductosFiltrados { get; set; }

        private readonly ContextoSMMS _context; // Asegúrate de reemplazar esto con tu contexto de base de datos

        // Propiedades para los filtros
        private string _codigoProductoFiltro;
        private string _numeroSerieFiltro;
        private string _cantidadFiltro;

        public string CodigoProductoFiltro
        {
            get => _codigoProductoFiltro;
            set
            {
                _codigoProductoFiltro = value;
                OnPropertyChanged(nameof(CodigoProductoFiltro));
                FiltrarProductos(); // Filtrar cuando el valor cambia
            }
        }

        public string NumeroSerieFiltro
        {
            get => _numeroSerieFiltro;
            set
            {
                _numeroSerieFiltro = value;
                OnPropertyChanged(nameof(NumeroSerieFiltro));
                FiltrarProductos(); // Filtrar cuando el valor cambia
            }
        }

        public string CantidadFiltro
        {
            get => _cantidadFiltro;
            set
            {
                _cantidadFiltro = value;
                OnPropertyChanged(nameof(CantidadFiltro));
                FiltrarProductos(); // Filtrar cuando el valor cambia
            }
        }

        public ICommand FiltrarCommand { get; private set; }
        // Comando para generar el CSV
        public ICommand GenerarCsvCommand { get; }


        public ProductoFacturasViewModel(int idFactura)
        {
            _context = new ContextoSMMS(); // Inicializa tu contexto
            Productos = new ObservableCollection<Producto>();
            ProductosFiltrados = new ObservableCollection<Producto>();

            // Inicializar el comando para filtrar
            FiltrarCommand = new RelayCommand(FiltrarProductos);
            // Inicialización del comando para generar CSV
            GenerarCsvCommand = new RelayCommand(GenerarCsv);

            // Cargar los productos relacionados con la factura
            CargarProductos(idFactura);
        }

        private void CargarProductos(int idFactura)
        {
            using (var _context = new ContextoSMMS())
            {
                var productosFactura = _context.Productos
                    .Where(p => p.IdFactura == idFactura)
                    .ToList();

                foreach (var producto in productosFactura)
                {
                    Productos.Add(producto);
                }

                // Inicializar ProductosFiltrados
                ProductosFiltrados = new ObservableCollection<Producto>(Productos);
            }
        }

        private void GenerarCsv()
        {
            // Verificar si no hay productos filtrados
            if (ProductosFiltrados == null || ProductosFiltrados.Count == 0)
            {
                return; // Salir del método si no hay información
            }

            // Crear una instancia del cuadro de diálogo de guardar archivo
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos CSV (*.csv)|*.csv"; // Filtrar solo archivos CSV
            saveFileDialog.DefaultExt = ".csv"; // Establecer la extensión por defecto

            // Mostrar el cuadro de diálogo y verificar si el usuario seleccionó un archivo
            bool? resultado = saveFileDialog.ShowDialog();

            if (resultado == true) // El usuario seleccionó un archivo
            {
                // Obtener la ruta seleccionada
                string rutaArchivo = saveFileDialog.FileName;

                // Crear un StringBuilder para construir el contenido CSV
                var sb = new StringBuilder();

                // Agregar el encabezado del CSV (con los nombres de las columnas)
                sb.AppendLine("NumeroFactura,IdProducto,CodigoProducto,Descripcion,NSerie,Cantidad,PrecioUnitario,Descuento,Total,IdFactura,IdSucursal,IdEmpresa");

                // Agregar cada producto como una línea en el archivo CSV
                foreach (var producto in ProductosFiltrados)
                {
                    sb.AppendLine($"{producto.NumeroFactura},{producto.IdProducto},{producto.CodigoProducto},{producto.Descripcion},{producto.NSerie},{producto.Cantidad},{producto.PrecioUnitario},{producto.Descuento},{producto.Total},{producto.IdFactura},{producto.IdSucursal},{producto.IdEmpresa}");
                }

                // Escribir el contenido en un archivo CSV
                File.WriteAllText(rutaArchivo, sb.ToString());
            }
            else
            {
                // Aquí podrías manejar el caso cuando el usuario cancela la operación, si lo necesitas.
            }
        }


        public void FiltrarProductos()
        {
            var productosFiltrados = Productos.Where(p =>
                (string.IsNullOrEmpty(CodigoProductoFiltro) || p.CodigoProducto.Contains(CodigoProductoFiltro)) &&
                (string.IsNullOrEmpty(NumeroSerieFiltro) || p.NSerie.Contains(NumeroSerieFiltro)) &&
                (string.IsNullOrEmpty(CantidadFiltro) || p.Cantidad.ToString() == CantidadFiltro))
                .ToList();

            ProductosFiltrados.Clear();
            foreach (var producto in productosFiltrados)
            {
                ProductosFiltrados.Add(producto);
            }
        }

        // Método para eliminar productos seleccionados
        public async Task EliminarProductosSeleccionados(ObservableCollection<Producto> productosSeleccionados, int idFactura)
        {
            if (productosSeleccionados == null || productosSeleccionados.Count == 0)
                return;

            // Eliminar los productos del contexto
            foreach (var producto in productosSeleccionados.ToList())
            {
                _context.Productos.Remove(producto);
                Debug.WriteLine($"Producto eliminado: ID {producto.IdProducto}");
            }

            // Guardar los cambios en la base de datos
            _context.SaveChanges();

            // Actualizar la factura asociada
            ActualizarFactura(idFactura);

            // Actualizar la lista de productos en la interfaz
            ActualizarInformacionProductos(idFactura);

            // Mostrar mensaje de éxito usando MessageBox
            MessageBox.Show("Los productos seleccionados han sido eliminados correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Método para actualizar la factura después de eliminar productos
        private void ActualizarFactura(int idFactura)
        {
            // Obtener todos los productos relacionados con la factura
            var productosRestantes = _context.Productos
                                              .Where(p => p.IdFactura == idFactura)
                                              .ToList(); // Traer a memoria los productos restantes

            // Intentar encontrar la factura correspondiente
            var factura = _context.Facturas.FirstOrDefault(f => f.IdFactura == idFactura);
            if (factura == null) return; // Si no se encuentra la factura, salimos

            // Actualizar el total y la cantidad basados en los productos restantes
            if (productosRestantes != null && productosRestantes.Count > 0)
            {
                factura.Total = productosRestantes.Sum(p => p.Total); // Sumar el total de los productos restantes
                factura.Cantidad = productosRestantes.Sum(p => p.Cantidad); // Sumar la cantidad de los productos restantes
                factura.PrecioUnitario = productosRestantes.Sum(p => p.PrecioUnitario); // Ajuste en caso de que sea necesario
            }
            else
            {
                // Si no quedan productos, se actualiza a cero
                factura.Total = 0;
                factura.Cantidad = 0;
                factura.PrecioUnitario = 0; // Asegúrate de que esto sea correcto según tu lógica
            }

            // Guardar los cambios en la base de datos
            _context.SaveChanges();
            Debug.WriteLine($"Factura actualizada: ID {factura.IdFactura}, Nuevo Total: {factura.Total}, Nueva Cantidad: {factura.Cantidad}");
        }

        // Método para actualizar la información de productos
        private void ActualizarInformacionProductos(int idFactura)
        {
            Productos.Clear(); // Limpiar la lista de productos actual
            var nuevosProductos = GetInformacionProductos(idFactura).ToList(); // Obtener nuevos productos asociados a la factura
            foreach (var nuevoProducto in nuevosProductos)
            {
                Productos.Add(nuevoProducto); // Agregar nuevos productos a la lista
            }

            FiltrarProductos(); // Aplicar filtros si es necesario
            Debug.WriteLine($"InformacionProductos actualizadas: {Productos.Count}"); // Muestra el número actualizado de InformacionProductos
        }

        // Método que obtiene la información de los productos asociados a la factura
        private IEnumerable<Producto> GetInformacionProductos(int idFactura)
        {
            return _context.Productos.Where(p => p.IdFactura == idFactura).ToList(); // Obtener productos de la base de datos por ID de factura
        }
    }
}
