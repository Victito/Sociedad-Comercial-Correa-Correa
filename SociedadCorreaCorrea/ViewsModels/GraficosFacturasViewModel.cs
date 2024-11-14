using LiveCharts;
using LiveCharts.Wpf;
using SociedadCorreaCorrea.Models;
using SociedadCorreaCorrea.Commands;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using SociedadCorreaCorrea.Data;
using MahApps.Metro.Controls;

namespace SociedadCorreaCorrea.ViewModels
{
    public class GraficosFacturasViewModel : BaseViewModel
    {
        // Propiedades para los filtros
        public ObservableCollection<string> Proveedores { get; set; }
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

            // Inicializar el comando
            FiltrarCommand = new RelayCommand(() => CargarDatos());
        }

        private void CargarDatos()
        {
            using (var context = new ContextoSMMS())
            {
                // Cargar los proveedores
                Proveedores = new ObservableCollection<string>(context.Facturas
                    .Select(f => f.RazonSocialVendedor)
                    .Distinct()
                    .ToList());

                // Aplicar los filtros de fecha y proveedor
                var facturasFiltradas = context.Facturas.AsQueryable();

                // Validar fechas y aplicar filtros
                if (FechaInicio.HasValue)
                {
                    facturasFiltradas = facturasFiltradas.Where(f => f.FechaEmision >= DateOnly.FromDateTime(FechaInicio.Value.Date));
                }

                if (FechaFin.HasValue)
                {
                    if (FechaInicio.HasValue && FechaFin.Value < FechaInicio.Value)
                    {
                        // Si la fecha de fin es menor que la de inicio, no hacemos nada o mostramos un mensaje
                        return; // Puedes agregar un mensaje de error aquí si lo deseas
                    }
                    facturasFiltradas = facturasFiltradas.Where(f => f.FechaEmision <= DateOnly.FromDateTime(FechaFin.Value.Date));
                }

                if (!string.IsNullOrEmpty(FiltroProveedor))
                {
                    facturasFiltradas = facturasFiltradas.Where(f => f.RazonSocialVendedor == FiltroProveedor);
                }

                // Gráfico de total facturado por proveedor
                var totalPorProveedor = facturasFiltradas.GroupBy(f => f.RazonSocialVendedor)
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
                var facturasPorEstado = facturasFiltradas.GroupBy(f => f.Estado)
                    .Select(g => new { Estado = g.Key, Cantidad = g.Count() })
                    .ToList();

                SeriesFacturasPorEstado = new SeriesCollection();
                foreach (var estado in facturasPorEstado)
                {
                    SeriesFacturasPorEstado.Add(new PieSeries
                    {
                        Title = estado.Estado,
                        Values = new ChartValues<int> { estado.Cantidad },
                        DataLabels = true
                    });
                }

                // Gráfico de facturación mensual
                var facturacionMensual = facturasFiltradas.GroupBy(f => f.FechaEmision.Value.Month)
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

                // Gráfico de facturas por categoría (si tienes esa información)
                var facturasPorCategoria = facturasFiltradas.GroupBy(f => f.Giro)
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
                        Values = new ChartValues<double>(totalPorProveedor.Select(t => (double)t.Total / (facturasFiltradas.Count(f => f.RazonSocialVendedor == t.Proveedor) == 0 ? 1 : facturasFiltradas.Count(f => f.RazonSocialVendedor == t.Proveedor))))
                    }
                };

                // Formato del eje Y
                Formatter = value => value.ToString("N0");
            }
        }
    }
}
