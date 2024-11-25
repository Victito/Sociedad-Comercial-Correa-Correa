using LiveCharts;
using LiveCharts.Wpf;
using SociedadCorreaCorrea.Models;
using SociedadCorreaCorrea.Commands;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using SociedadCorreaCorrea.Data;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace SociedadCorreaCorrea.ViewModels
{
    public class GraficosFacturasViewModel : BaseViewModel
    {
        // Propiedades para los filtros
        public ObservableCollection<Factura> ListaFacturas { get; set; } = new ObservableCollection<Factura>();
        // Propiedad ObservableCollection de proveedores
        public ObservableCollection<string> Proveedores { get; set; } = new ObservableCollection<string>();
        private readonly MetroWindow _window;
        private DateTime? _fechaInicio;
        public DateTime? FechaInicio
        {
            get => _fechaInicio;
            set
            {
                _fechaInicio = value;
                OnPropertyChanged();
                CargarDatos();
            }
        }

        private DateTime? _fechaFin;
        public DateTime? FechaFin
        {
            get => _fechaFin;
            set
            {
                _fechaFin = value;
                OnPropertyChanged();
                CargarDatos();
            }
        }

        private string _filtroProveedor;
        public string FiltroProveedor
        {
            get => _filtroProveedor;
            set
            {
                _filtroProveedor = value;
                OnPropertyChanged();
                CargarDatos();
            }
        }

        // Propiedades para los gráficos
        public SeriesCollection SeriesTotalPorProveedor { get; set; }
        public SeriesCollection SeriesFacturasPorEstado { get; set; }
        public SeriesCollection SeriesFacturacionMensual { get; set; }
        public SeriesCollection SeriesFacturasPorCategoria { get; set; }
        public SeriesCollection SeriesPromedioPorProveedor { get; set; } // Nuevo gráfico

        public string[] LabelsProveedores { get; set; }
        public string[] LabelsMeses { get; set; }
        public string[] LabelsCategorias { get; set; }
        public Func<double, string> Formatter { get; set; }

        // Comando para aplicar los filtros
        public RelayCommand FiltrarCommand { get; set; }

        public GraficosFacturasViewModel(MetroWindow window)
        {
            _window = window;
            // Inicializar los valores y cargar los datos
            CargarDatos();
            CargarProveedores();
            // Inicializar el comando
            FiltrarCommand = new RelayCommand(() => CargarDatos());
        }

        private void CargarProveedores()
        {
            // Filtrar los proveedores según la empresa
            var proveedores = ListaFacturas
                .Where(f => f.IdEmpresa == GlobalSettings.IdEmpresa) // Filtrar por empresa
                .Select(f => f.RazonSocialVendedor)  // Seleccionar la razón social del proveedor
                .Distinct() // Eliminar duplicados
                .ToList();  // Convertir a lista

            // Asignar los proveedores a la propiedad observable
            Proveedores = new ObservableCollection<string>(proveedores);
        }
        private void CargarDatos()
        {
            using (var context = new ContextoSMMS())
            {
                // Cargar todas las facturas una sola vez en ListaFacturas
                ListaFacturas = new ObservableCollection<Factura>(
                    context.Facturas
                        .Where(f => f.IdEmpresa == GlobalSettings.IdEmpresa) // Filtrar por empresa
                        .ToList()
                );
            }

            // Aplicar filtros de fecha y proveedor directamente sobre ListaFacturas
            var facturasFiltradas = ListaFacturas.AsQueryable();

            if (FechaInicio.HasValue)
            {
                facturasFiltradas = facturasFiltradas.Where(f => f.FechaEmision >= DateOnly.FromDateTime(FechaInicio.Value.Date));
            }

            if (FechaFin.HasValue)
            {
                if (FechaInicio.HasValue && FechaFin.Value < FechaInicio.Value)
                {
                    // Mostrar mensaje de error y salir
                    _window.ShowMessageAsync("Error", "La fecha de fin no puede ser menor que la fecha de inicio.");
                    return;
                }
                facturasFiltradas = facturasFiltradas.Where(f => f.FechaEmision <= DateOnly.FromDateTime(FechaFin.Value.Date));
            }

            if (!string.IsNullOrEmpty(FiltroProveedor))
            {
                facturasFiltradas = facturasFiltradas.Where(f => f.RazonSocialVendedor == FiltroProveedor);
            }

            // Gráfico de total facturado por proveedor
            var totalPorProveedor = facturasFiltradas
                .GroupBy(f => f.RazonSocialVendedor)
                .Select(g => new { Proveedor = g.Key, Total = g.Sum(f => f.Total ?? 0) })
                .ToList();

            LabelsProveedores = totalPorProveedor.Select(t => t.Proveedor).ToArray();
            SeriesTotalPorProveedor = new SeriesCollection
    {
        new ColumnSeries
        {
            Title = "Total Facturado",
            Values = new ChartValues<double>(totalPorProveedor.Select(t => (double)t.Total))
        }
    };

            // Gráfico circular de facturas por estado
            var facturasPorEstado = facturasFiltradas
                .GroupBy(f => f.Estado)
                .Select(g => new { Estado = g.Key, Cantidad = g.Count() })
                .ToList();

            SeriesFacturasPorEstado = new SeriesCollection(
                facturasPorEstado.Select(estado => new PieSeries
                {
                    Title = estado.Estado,
                    Values = new ChartValues<int> { estado.Cantidad },
                    DataLabels = true
                })
            );

            // Gráfico de facturación mensual
            var facturacionMensual = facturasFiltradas
                .Where(f => f.FechaEmision.HasValue)
                .GroupBy(f => f.FechaEmision.Value.Month)
                .Select(g => new { Mes = g.Key, Total = g.Sum(f => f.Total ?? 0) })
                .ToList();

            LabelsMeses = facturacionMensual.Select(f => $"Mes {f.Mes}").ToArray();
            SeriesFacturacionMensual = new SeriesCollection
    {
        new LineSeries
        {
            Title = "Facturación Mensual",
            Values = new ChartValues<double>(facturacionMensual.Select(f => (double)f.Total)),
            PointGeometry = DefaultGeometries.Circle,
            PointGeometrySize = 15
        }
    };

            // Gráfico de facturas por categoría
            var facturasPorCategoria = facturasFiltradas
                .GroupBy(f => f.Giro)
                .Select(g => new { Categoria = g.Key, Cantidad = g.Count() })
                .ToList();

            LabelsCategorias = facturasPorCategoria.Select(c => c.Categoria).ToArray();
            SeriesFacturasPorCategoria = new SeriesCollection
    {
        new ColumnSeries
        {
            Title = "Facturas por Categoría",
            Values = new ChartValues<int>(facturasPorCategoria.Select(c => c.Cantidad))
        }
    };

            // Gráfico de promedio de facturas por proveedor
            SeriesPromedioPorProveedor = new SeriesCollection
    {
        new ColumnSeries
        {
            Title = "Promedio por Proveedor",
            Values = new ChartValues<double>(
                totalPorProveedor.Select(t =>
                    (double)t.Total / facturasFiltradas.Count(f => f.RazonSocialVendedor == t.Proveedor))
            )
        }
    };

            // Formato del eje Y
            Formatter = value => value.ToString("N0");
        }
    }
}
