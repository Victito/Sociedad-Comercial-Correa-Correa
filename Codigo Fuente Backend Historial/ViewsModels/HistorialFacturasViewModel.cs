using System;
using System.Linq;
using System.Collections.ObjectModel;
using SociedadCorreaCorrea.Data;
using SociedadCorreaCorrea.Models;
using System.Diagnostics;  // Para el debug
using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.ComponentModel;

namespace SociedadCorreaCorrea.ViewModels
{
    class HistorialFacturasViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly ContextoSMMS _context;
        private readonly MetroWindow _window;

        public ObservableCollection<Factura> Facturas { get; set; }
        public ObservableCollection<InformacionFacturas> InformacionFactura { get; set; }
        private string _filtroRazonSocial;
        private string _filtroEstado;

        public string FiltroRazonSocial
        {
            get => _filtroRazonSocial;
            set
            {
                _filtroRazonSocial = value;
                OnPropertyChanged(nameof(FiltroRazonSocial));
                AplicarFiltros();
            }
        }

        public string FiltroEstado
        {
            get => _filtroEstado;
            set
            {
                _filtroEstado = value;
                OnPropertyChanged(nameof(FiltroEstado));
                AplicarFiltros();
            }
        }

        public ObservableCollection<InformacionFacturas> InformacionFacturaFiltrada { get; set; }

        public HistorialFacturasViewModel(ContextoSMMS context, MetroWindow window)
        {
            _context = context;
            _window = window;

            Facturas = new ObservableCollection<Factura>(GetFacturas().ToList());
            Debug.WriteLine($"Facturas materializadas: {Facturas.Count}");  // Muestra el número de facturas

            InformacionFactura = new ObservableCollection<InformacionFacturas>(GetInformacionFacturas().ToList());
            InformacionFacturaFiltrada = new ObservableCollection<InformacionFacturas>(InformacionFactura);
            Debug.WriteLine($"InformacionFacturas creadas: {InformacionFactura.Count}");  // Muestra el número de InformacionFacturas
        }

        private IQueryable<Factura> GetFacturas()
        {
            var facturas = _context.Facturas;
            Debug.WriteLine($"Facturas encontradas en la base de datos: {facturas.Count()}");  // Muestra cuántas facturas se encontraron en la base de datos
            return facturas;
        }

        private IQueryable<InformacionFacturas> GetInformacionFacturas()
        {
            var informacionFacturas = from factura in GetFacturas()
                                      select new InformacionFacturas
                                      {
                                          Factura = factura
                                      };
            Debug.WriteLine($"InformacionFacturas generadas: {informacionFacturas.Count()}");  // Muestra cuántas InformacionFacturas se generaron
            return informacionFacturas;
        }

        private void AplicarFiltros()
        {
            InformacionFacturaFiltrada.Clear();
            var FiltradoEstadoLimpio = FormatComboBoxValue(FiltroEstado);
            var filtrada = InformacionFactura.Where(f =>
                (string.IsNullOrEmpty(FiltroRazonSocial) || f.Factura.RazonSocial.Contains(FiltroRazonSocial, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(FiltradoEstadoLimpio) || f.Factura.Estado.Contains(FiltradoEstadoLimpio, StringComparison.OrdinalIgnoreCase))
            );

            foreach (var item in filtrada)
            {
                InformacionFacturaFiltrada.Add(item);
            }

            Debug.WriteLine($"InformacionFacturas filtradas: {InformacionFacturaFiltrada.Count}"); // Muestra el número de InformacionFacturas filtradas
        }

        private string FormatComboBoxValue(string value)
        {
            if (value == null)
                return null;

            int colonIndex = value.IndexOf(':');
            if (colonIndex >= 0)
            {
                return value.Substring(colonIndex + 1).Trim();
            }

            return value.Trim();
        }

        public void EliminarFacturasSeleccionadas(ObservableCollection<InformacionFacturas> facturasSeleccionadas)
        {
            if (facturasSeleccionadas == null || facturasSeleccionadas.Count == 0)
                return;

            foreach (var facturaInfo in facturasSeleccionadas.ToList())
            {
                var facturaId = facturaInfo.Factura.IdFactura; // Obtener la ID de la factura seleccionada

                var productosAEliminar = _context.Productos.Where(p => p.IdFactura == facturaId).ToList();
                if (productosAEliminar.Count > 0)
                {
                    _context.Productos.RemoveRange(productosAEliminar);
                    Debug.WriteLine($"Productos eliminados relacionados con Factura ID: {facturaId}");
                }

                var acusesAEliminar = _context.Acuses.Where(a => a.IdFactura == facturaId).ToList();
                if (acusesAEliminar.Count > 0)
                {
                    _context.Acuses.RemoveRange(acusesAEliminar);
                    Debug.WriteLine($"Acuses eliminados relacionados con Factura ID: {facturaId}");
                }

                _context.Facturas.Remove(facturaInfo.Factura);
                Debug.WriteLine($"Factura eliminada: {facturaId}");
            }

            _context.SaveChanges();
            ActualizarInformacionFacturas();
        }

        private void ActualizarInformacionFacturas()
        {
            InformacionFactura.Clear();
            var nuevasFacturas = GetInformacionFacturas().ToList();
            foreach (var nuevaFactura in nuevasFacturas)
            {
                InformacionFactura.Add(nuevaFactura);
            }

            AplicarFiltros();
            Debug.WriteLine($"InformacionFacturas actualizadas: {InformacionFactura.Count}"); // Muestra el número actualizado de InformacionFacturas
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class InformacionFacturas
    {
        public Factura Factura { get; set; }
    }
}
