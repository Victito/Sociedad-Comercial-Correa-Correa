using SociedadCorreaCorrea.Models;
using SociedadCorreaCorrea.Data; // Asegúrate de incluir el contexto
using System.Collections.ObjectModel;
using System.Linq;
using SociedadCorreaCorrea.Commands;
using System.Windows.Input;
using System.Windows;
using System.Numerics;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore; // Asegúrate de incluir esto
using System.Threading.Tasks;

namespace SociedadCorreaCorrea.ViewModels
{
    public class RegistroFacturaViewModel : BaseViewModel
    {
        private Sucursal _sucursalSeleccionada;
        public ObservableCollection<Sucursal> Sucursales { get; set; }
        public ObservableCollection<Producto> Productos { get; set; }

        // Inyección del diálogo
        private readonly MetroWindow _window;

        public Sucursal SucursalSeleccionada
        {
            get { return _sucursalSeleccionada; }
            set
            {
                _sucursalSeleccionada = value;
                OnPropertyChanged();
            }
        }

        public RegistroFacturaViewModel(MetroWindow window)
        {
            // Aquí cargas las sucursales automáticamente
            _window = window;

            Productos = new ObservableCollection<Producto>();
            CargarSucursales();
            SaveCommand = new RelayCommand(GuardarFactura);
            SelectedTab = 0; // Inicialmente, selecciona el primer tab
        }

        public void AgregarProducto(string codigo, string descripcion, string nSerie, int cantidad, decimal precioUnitario, int Descuento, decimal total)
        {
            var producto = new Producto
            {
                CodigoProducto = codigo,
                Descripcion = descripcion,
                NSerie = nSerie,
                Cantidad = cantidad,
                Descuento = Descuento,
                PrecioUnitario = precioUnitario,
                Total = total
            };
            Productos.Add(producto);

        }

        public void EliminarProducto(Producto producto)
        {
            if (Productos.Contains(producto))
            {
                Productos.Remove(producto);
            }
        }

        // Método para vaciar toda la lista de productos
        public void VaciarProductos()
        {
            Productos.Clear();
        }

        private void CargarSucursales()
        {
            using (var context = new ContextoSMMS()) // Reemplaza YourDbContext con el nombre de tu contexto
            {
                // Obtén las sucursales filtrando por IdEmpresa
                var sucursalesFiltradas = context.Sucursals
                    .Where(s => s.IdEmpresa == GlobalSettings.IdEmpresa)
                    .ToList();

                Sucursales = new ObservableCollection<Sucursal>(sucursalesFiltradas);
            }
        }
        private string _rutVendedor;
        private string _giroVendedor;
        private string _razonsocialVendedor;
        private string _razonSocial;
        private string _rutEmisor;
        private string _giro;
        private string _direccion;
        private string _comuna;
        private string _ciudad;
        private string _entregarEn;
        private DateTime? _fechaEmision;
        private DateTime? _fechaVencimiento;
        private string _cobrador;
        private int? _notaVenta;
        private string _ordenCompra;
        private string _condiciones;
        private string _guiaDespacho;
        private decimal? _precioUnitario;
        private int? _cantidad;
        private int? _numerofactura;
        private decimal? _total;
        private string _estado;
        private decimal _totalFactura;

        private string _nombre;
        private string _recinto;
        private DateTime? _fecha;
        private string _rut;

        private int _selectedTab;


        public int SelectedTab
        {
            get => _selectedTab;
            set
            {
                if (_selectedTab != value)
                {
                    _selectedTab = value;
                    OnPropertyChanged();
                }
            }
        }
        public string GiroVendedor
        {
            get => _giroVendedor;
            set { _giroVendedor = value; OnPropertyChanged(); }
        }

        public string RutVendedor
        {
            get => _rutVendedor;
            set { _rutVendedor = value; OnPropertyChanged(); }
        }

        public string RazonSocialVendedor
        {
            get => _razonsocialVendedor;
            set { _razonsocialVendedor = value; OnPropertyChanged(); }
        }

        public string RazonSocial
        {
            get => _razonSocial;
            set
            {
                if (_razonSocial != value)
                {
                    _razonSocial = value;
                    OnPropertyChanged(nameof(RazonSocial));
                }
            }
        }

        public string RutEmisor
        {
            get => _rutEmisor;
            set { _rutEmisor = value; OnPropertyChanged(); }
        }

        public string Giro
        {
            get => _giro;
            set { _giro = value; OnPropertyChanged(); }
        }

        public string Direccion
        {
            get => _direccion;
            set { _direccion = value; OnPropertyChanged(); }
        }

        public string Comuna
        {
            get => _comuna;
            set { _comuna = value; OnPropertyChanged(); }
        }

        public string Ciudad
        {
            get => _ciudad;
            set { _ciudad = value; OnPropertyChanged(); }
        }

        public string EntregarEn
        {
            get => _entregarEn;
            set { _entregarEn = value; OnPropertyChanged(); }
        }

        public DateTime? FechaEmision
        {
            get => _fechaEmision;
            set { _fechaEmision = value; OnPropertyChanged(); }
        }

        public DateTime? FechaVencimiento
        {
            get => _fechaVencimiento;
            set { _fechaVencimiento = value; OnPropertyChanged(); }
        }

        public string Cobrador
        {
            get => _cobrador;
            set { _cobrador = value; OnPropertyChanged(); }
        }

        public int? NotaVenta
        {
            get => _notaVenta;
            set { _notaVenta = value; OnPropertyChanged(); }
        }

        public int? NumeroFactura
        {
            get => _numerofactura;
            set { _numerofactura = value; OnPropertyChanged(); }
        }

        public string OrdenCompra
        {
            get => _ordenCompra;
            set { _ordenCompra = value; OnPropertyChanged(); }
        }

        public string Condiciones
        {
            get => _condiciones;
            set { _condiciones = value; OnPropertyChanged(); }
        }

        public string GuiaDespacho
        {
            get => _guiaDespacho;
            set { _guiaDespacho = value; OnPropertyChanged(); }
        }

        public decimal? PrecioUnitario
        {
            get => _precioUnitario;
            set { _precioUnitario = value; OnPropertyChanged(); }
        }

        public int? Cantidad
        {
            get => _cantidad;
            set { _cantidad = value; OnPropertyChanged(); }
        }

        public decimal? Total
        {
            get => _total;
            set { _total = value; OnPropertyChanged(); }
        }

        // Propiedad Estado con lógica de formateo
        public string Estado
        {
            get => _estado;
            set
            {
                // Formatear el valor antes de asignarlo
                _estado = FormatComboBoxValue(value);
                OnPropertyChanged();
            }
        }

        // Nuevas propiedades para Acuses
        public string Nombre
        {
            get => _nombre;
            set { _nombre = value; OnPropertyChanged(); }
        }

        public string Recinto
        {
            get => _recinto;
            set { _recinto = value; OnPropertyChanged(); }
        }

        public DateTime? Fecha
        {
            get => _fecha;
            set { _fecha = value; OnPropertyChanged(); }
        }

        public string Rut { get => _rut; set { _rut = value; OnPropertyChanged(); } }

        // Método para formatear el valor de un combobox
        private string FormatComboBoxValue(string value)
        {
            if (value == null)
                return null;

            // Buscar el índice del ':'
            int colonIndex = value.IndexOf(':');
            if (colonIndex >= 0)
            {
                // Retornar el texto después del ':'
                return value.Substring(colonIndex + 1).Trim();
            }

            // Retornar el valor sin cambios si no se encuentra ':'
            return value.Trim();
        }

        public ICommand SaveCommand { get; }

        public bool ValidarRut(string rut)
        {
            // Elimina puntos y guiones del RUT
            rut = rut.Replace(".", "").Replace("-", "").ToUpper();

            // Verifica si el RUT está vacío
            if (string.IsNullOrEmpty(rut))
            {
                return false;
            }

            // Separa el número del dígito verificador
            string rutNumeros = rut.Length > 1 ? rut.Substring(0, rut.Length - 1) : "";
            char dv = rut.Length > 1 ? rut[rut.Length - 1] : '0';

            // Verifica si el dígito verificador es válido
            if (!char.IsDigit(dv) && dv != 'K')
            {
                return false;
            }

            // Calcula el dígito verificador esperado
            int suma = 0;
            int factor = 2;

            for (int i = rutNumeros.Length - 1; i >= 0; i--)
            {
                suma += (rutNumeros[i] - '0') * factor;
                factor = factor == 7 ? 2 : factor + 1;
            }

            int dvEsperado = 11 - (suma % 11);
            char dvEsperadoChar = dvEsperado == 10 ? 'K' : (dvEsperado == 11 ? '0' : (char)(dvEsperado + '0'));

            // Compara el dígito verificador calculado con el dígito verificador ingresado
            return dv == dvEsperadoChar;
        }

        private async void GuardarFactura()
        {
            using (var context = new ContextoSMMS())
            {
                try
                {
                    // Verificar que haya al menos un producto en la lista
                    if (Productos == null || Productos.Count == 0)
                    {
                        await _window.ShowMessageAsync("Error", "Para registrar una factura tiene que por lo menos tener 1 producto asignado.");
                        return;
                    }

                    // Lista para almacenar mensajes de error
                    List<string> mensajesErrores = new List<string>();

                    // Validar campos requeridos
                    if (string.IsNullOrWhiteSpace(RazonSocial))
                        mensajesErrores.Add("El campo 'Razón Social' no puede estar vacío.");

                    if (string.IsNullOrWhiteSpace(RutEmisor))
                        mensajesErrores.Add("El campo 'RUT Emisor' no puede estar vacío.");
                    else if (!ValidarRut(RutEmisor))
                        mensajesErrores.Add("El RUT ingresado no existe! Intente con otro nuevamente.");

                    if (string.IsNullOrWhiteSpace(Giro))
                        mensajesErrores.Add("El campo 'Giro' no puede estar vacío.");

                    if (string.IsNullOrWhiteSpace(Direccion))
                        mensajesErrores.Add("El campo 'Dirección' no puede estar vacío.");

                    if (string.IsNullOrWhiteSpace(Comuna))
                        mensajesErrores.Add("El campo 'Comuna' no puede estar vacío.");

                    if (string.IsNullOrWhiteSpace(Ciudad))
                        mensajesErrores.Add("El campo 'Ciudad' no puede estar vacío.");

                    if (string.IsNullOrWhiteSpace(GiroVendedor))
                        mensajesErrores.Add("El campo 'Giro Vendedor' no puede estar vacío.");

                    if (string.IsNullOrWhiteSpace(RutVendedor))
                        mensajesErrores.Add("El campo 'RUT del vendedor' no puede estar vacío.");
                    else if (!ValidarRut(RutVendedor))
                        mensajesErrores.Add("El RUT ingresado no existe! Intente con otro nuevamente.");

                    if (string.IsNullOrWhiteSpace(RazonSocialVendedor))
                        mensajesErrores.Add("El campo 'Razon social del vendedor' no puede estar vacío.");

                    if (string.IsNullOrWhiteSpace(EntregarEn))
                        mensajesErrores.Add("El campo 'Entregar En' no puede estar vacío.");

                    if (!FechaEmision.HasValue)
                        mensajesErrores.Add("El campo 'Fecha Emisión' no puede estar vacío.");

                    if (!FechaVencimiento.HasValue)
                        mensajesErrores.Add("El campo 'Fecha Vencimiento' no puede estar vacío.");

                    if (string.IsNullOrWhiteSpace(Cobrador))
                        mensajesErrores.Add("El campo 'Cobrador' no puede estar vacío.");

                    if (NotaVenta <= 0)
                        mensajesErrores.Add("El campo 'Nota de Venta' debe ser un número mayor que 0.");

                    if (NumeroFactura == 0)
                        mensajesErrores.Add("El campo 'Numero de Factura' debe tener informacion.");

                    if (string.IsNullOrWhiteSpace(OrdenCompra))
                        mensajesErrores.Add("El campo 'Orden de Compra' no puede estar vacío.");

                    if (string.IsNullOrWhiteSpace(Condiciones))
                        mensajesErrores.Add("El campo 'Condiciones' no puede estar vacío.");

                    if (string.IsNullOrWhiteSpace(GuiaDespacho))
                        mensajesErrores.Add("El campo 'Guía de Despacho' no puede estar vacío.");

                    // Nuevas validaciones para los campos de Acuses
                    if (string.IsNullOrWhiteSpace(Nombre))
                        mensajesErrores.Add("El campo 'Nombre' no puede estar vacío.");

                    if (string.IsNullOrWhiteSpace(Recinto))
                        mensajesErrores.Add("El campo 'Recinto' no puede estar vacío.");

                    if (!Fecha.HasValue)
                        mensajesErrores.Add("El campo 'Fecha' no puede estar vacío.");

                    if (string.IsNullOrWhiteSpace(Rut))
                        mensajesErrores.Add("El campo 'RUT' no puede estar vacío.");
                    else if (!ValidarRut(Rut))
                        mensajesErrores.Add("El RUT ingresado no existe! Intente con otro nuevamente.");

                    // Verificar si hay errores
                    if (mensajesErrores.Count > 0)
                    {
                        string mensajeCompleto = string.Join("\n", mensajesErrores);
                        await _window.ShowMessageAsync("Errores", mensajeCompleto);
                        return;
                    }

                    // Calcular el total de todos los productos
                    decimal totalFactura = 0;
                    int totalCantidad = 0;
                    decimal totalPrecioUnitario = 0;

                    foreach (var producto in Productos)
                    {
                        // Calcular el total del producto después de aplicar el descuento
                        decimal totalProducto = (producto.PrecioUnitario * producto.Cantidad) -
                                                ((producto.Descuento / 100m) * (producto.PrecioUnitario * producto.Cantidad));

                        // Sumar el total del producto a la variable totalFactura
                        totalFactura += totalProducto;

                        // Sumar la cantidad del producto
                        totalCantidad += producto.Cantidad;
                        totalPrecioUnitario += producto.PrecioUnitario;
                    }

                    var nuevaFactura = new Factura
                    {
                        IdUsuario = UserSession.Id, // Asegúrate de que GlobalSettings tenga el IdUsuario correcto
                        IdSucursal = SucursalSeleccionada?.IdSucursal,
                        IdEmpresa = GlobalSettings.IdEmpresa,
                        RazonSocial = RazonSocial,
                        RutEmisor = RutEmisor,
                        Giro = Giro,
                        Direccion = Direccion,
                        Comuna = Comuna,
                        Ciudad = Ciudad,
                        EntregarEn = EntregarEn,
                        FechaEmision = FechaEmision.HasValue ? DateOnly.FromDateTime(FechaEmision.Value) : (DateOnly?)null,
                        FechaVencimiento = FechaVencimiento.HasValue ? DateOnly.FromDateTime(FechaVencimiento.Value) : (DateOnly?)null,
                        Cobrador = Cobrador,
                        NotaVenta = NotaVenta,
                        OrdenCompra = OrdenCompra,
                        Condiciones = Condiciones,
                        GuiaDespacho = GuiaDespacho,
                        PrecioUnitario = totalPrecioUnitario,
                        Cantidad = totalCantidad,
                        Total = totalFactura,
                        Estado = Estado,
                        RutVendedor = RutVendedor,
                        GiroVendedor = GiroVendedor,
                        RazonSocialVendedor = RazonSocialVendedor,
                        NumeroFactura = NumeroFactura
                    };

                    context.Facturas.Add(nuevaFactura);
                    await context.SaveChangesAsync(); // Cambiado a SaveChangesAsync para usar async

                    // Ahora, iterar sobre los productos asociados en el ViewModel y asignarlos a la factura
                    foreach (var producto in Productos)
                    {
                        var nuevoProducto = new Producto
                        {
                            CodigoProducto = producto.CodigoProducto,
                            Descripcion = producto.Descripcion,
                            NSerie = producto.NSerie,
                            Cantidad = producto.Cantidad,
                            Descuento = producto.Descuento,
                            PrecioUnitario = producto.PrecioUnitario,
                            Total = producto.Total,
                            IdFactura = nuevaFactura.IdFactura, // Asignar la relación con la factura recién guardada
                            IdEmpresa = GlobalSettings.IdEmpresa, // Asignar la relación con la Empresa
                            IdSucursal = SucursalSeleccionada?.IdSucursal, // Asignar la relación con la Sucrusal
                            NumeroFactura = NumeroFactura
                        };

                        // Agregar el producto al contexto
                        context.Productos.Add(nuevoProducto);
                    }

                    // Guardar todos los productos
                    await context.SaveChangesAsync(); // Cambiado a SaveChangesAsync para usar async

                    // Crear un nuevo Acuse
                    var nuevoAcuse = new Acuse
                    {
                        Nombre = Nombre,
                        Recinto = Recinto,
                        Fecha = Fecha.HasValue ? DateOnly.FromDateTime(Fecha.Value) : (DateOnly?)null,
                        Rut = Rut,
                        IdFactura = nuevaFactura.IdFactura, // Asignar la relación con la factura recién guardada
                        IdEmpresa = GlobalSettings.IdEmpresa, // Asignar la relación con la Empresa
                        IdSucursal = SucursalSeleccionada?.IdSucursal, // Asignar la relación con la Sucrusal
                        NumeroFactura = NumeroFactura
                    };

                    // Agregar el nuevo Acuse al contexto
                    context.Acuses.Add(nuevoAcuse); // Asegúrate de que el DbSet se llama Acuses
                    await context.SaveChangesAsync(); // Cambiado a SaveChangesAsync para usar async

                    await _window.ShowMessageAsync("Éxito", "Factura registrada exitosamente.");
                    ReiniciarEstado();
                }
                catch (DbUpdateException dbEx)
                {
                    // Manejo de errores relacionados con la base de datos
                    var mensajeError = $"Ocurrió un error al guardar los datos: {dbEx.Message}";
                    if (dbEx.InnerException != null)
                    {
                        mensajeError += $"\nDetalles: {dbEx.InnerException.Message}";
                    }
                    await _window.ShowMessageAsync("Error de base de datos", mensajeError);
                }
                catch (Exception ex)
                {
                    // Manejo de cualquier otro tipo de error
                    var mensajeError = $"Ocurrió un error inesperado: {ex.Message}";
                    if (ex.InnerException != null)
                    {
                        mensajeError += $"\nDetalles: {ex.InnerException.Message}";
                    }
                    await _window.ShowMessageAsync("Error inesperado", mensajeError);
                }
            }
        }


        private void ReiniciarEstado()
        {
            // Restablecer todas las propiedades a sus valores iniciales
            RazonSocial = string.Empty;
            RutEmisor = string.Empty;
            Giro = string.Empty;
            Direccion = string.Empty;
            Comuna = string.Empty;
            Ciudad = string.Empty;
            EntregarEn = string.Empty;
            FechaEmision = null; // o DateTime.MinValue dependiendo de tu lógica
            FechaVencimiento = null; // o DateTime.MinValue dependiendo de tu lógica
            Cobrador = string.Empty;
            NotaVenta = null;
            OrdenCompra = string.Empty;
            Condiciones = string.Empty;
            GuiaDespacho = string.Empty;

            // Nuevas propiedades de Acuses
            Nombre = string.Empty;
            Recinto = string.Empty;
            Fecha = null; // o DateTime.MinValue dependiendo de tu lógica
            Rut = string.Empty;

            // Si tienes una lista de productos, también puedes limpiarla
            Productos.Clear(); // Asegúrate de que Productos sea una ObservableCollection
        }
    }
}
