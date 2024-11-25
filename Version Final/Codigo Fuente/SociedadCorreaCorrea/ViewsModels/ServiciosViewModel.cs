using SociedadCorreaCorrea.ViewModels;
using SociedadCorreaCorrea.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SociedadCorreaCorrea.Data;
using SociedadCorreaCorrea.Models;
using System.Collections.ObjectModel;
using iText.Commons.Actions.Contexts;
using Microsoft.EntityFrameworkCore;
using SociedadCorreaCorrea.Views;
using iText.Kernel.Pdf.Canvas.Wmf;
using PdfSharpCore.Pdf.Content.Objects;

namespace SociedadCorreaCorrea.ViewsModels
{
    class ServiciosViewModel : INotifyPropertyChanged
    {

        // Campos privados
        private readonly ContextoSMMS _contexto;
        private string _nombreServicio;
        private decimal _costoServicio;
        private string _empresaServicio;
        private DateTime? _fechaContratacion;
        private DateTime? _fechaPago;
        private int _sucursalId;

        public ObservableCollection<Sucursal> ListaSucursal { get; set; } = new ObservableCollection<Sucursal>();
        public ObservableCollection<Servicio> ListaServicio { get; set; } = new ObservableCollection<Servicio>();


        // Inyección del diálogo
        private readonly MetroWindow _window;

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand EliminarServicioCommand { get; }
        // Propiedades
        // Propiedad NombreServicio
        public string NombreServicio
        {
            get => _nombreServicio;
            set
            {
                if (_nombreServicio != value) // Solo notificamos si el valor cambia
                {
                    _nombreServicio = value;
                    OnPropertyChanged(nameof(NombreServicio)); // Notificar cambio
                }
            }
        }

        // Propiedad CostoServicio
        public decimal CostoServicio
        {
            get => _costoServicio;
            set
            {
                if (_costoServicio != value) // Solo notificamos si el valor cambia
                {
                    _costoServicio = value;
                    OnPropertyChanged(nameof(CostoServicio)); // Notificar cambio
                }
            }
        }

        // Propiedad EmpresaServicio
        public string EmpresaServicio
        {
            get => _empresaServicio;
            set
            {
                if (_empresaServicio != value) // Solo notificamos si el valor cambia
                {
                    _empresaServicio = value;
                    OnPropertyChanged(nameof(EmpresaServicio)); // Notificar cambio
                }
            }
        }

        // Propiedad FechaContratacion
        public DateTime? FechaContratacion
        {
            get => _fechaContratacion;
            set
            {
                if (_fechaContratacion != value) // Solo notificamos si el valor cambia
                {
                    _fechaContratacion = value;
                    OnPropertyChanged(nameof(FechaContratacion)); // Notificar cambio
                }
            }
        }

        // Propiedad FechaPago
        public DateTime? FechaPago
        {
            get => _fechaPago;
            set
            {
                if (_fechaPago != value) // Solo notificamos si el valor cambia
                {
                    _fechaPago = value;
                    OnPropertyChanged(nameof(FechaPago)); // Notificar cambio
                }
            }
        }

        // Propiedad IdSucursal
        public int IdSucursal
        {
            get => _sucursalId;
            set
            {
                if (_sucursalId != value) // Solo notificamos si el valor cambia
                {
                    _sucursalId = value;
                    OnPropertyChanged(nameof(IdSucursal)); // Notificar cambio
                }
            }
        }


        // Comando para agregar el servicio
        public ICommand AgregarServicioCommand { get; }

        // Constructor
        public ServiciosViewModel(MetroWindow window)
        {
            _window = window;
            _contexto = new ContextoSMMS(new DbContextOptions<ContextoSMMS>());
            AgregarServicioCommand = new RelayCommand(AgregarServicio);
            InicializarAsync();
            // Inicializa el comando para eliminar el servicio
            EliminarServicioCommand = new RelayCommand(EliminarServicio);
        }
        private async void InicializarAsync()
        {
            await CargarSucursales();
            await CargarServicios();

        }

        private async Task CargarSucursales()
        {
            try
            {
                var sucursales = await _contexto.Sucursals
                    .Where(p => p.IdEmpresa == GlobalSettings.IdEmpresa)
                    .ToListAsync();

                foreach (var sucursal in sucursales)
                {
                    // Agrega un nuevo puesto a la lista
                    ListaSucursal.Add(new Sucursal
                    {
                        IdSucursal = sucursal.IdSucursal,
                        NombreSucursal = sucursal.NombreSucursal
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando sucursales: {ex.Message}");
            }
        }

    private async Task CargarServicios()
        {
            try
            {
                // Obtener los servicios desde la base de datos filtrados por el ID de la empresa
                var servicios = await _contexto.Servicios
                    .Where(p => p.EmpresaId == GlobalSettings.IdEmpresa)
                    .ToListAsync();

                // Limpiar la lista de servicios antes de agregar los nuevos
                ListaServicio.Clear();

                // Recorrer cada servicio y agregarlo a la lista
                foreach (var servicio in servicios)
                {
                    // Crear un nuevo objeto de tipo Servicio y agregarlo a la lista
                    ListaServicio.Add(new Servicio
                    {
                        NombreServicio = servicio.NombreServicio,
                        CostoServicio = servicio.CostoServicio,
                        EmpresaServicio = servicio.EmpresaServicio,
                        FechaContratacion = servicio.FechaContratacion,
                        FechaPago = servicio.FechaPago,
                        ServicioId = servicio.ServicioId
                    });
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores en caso de que falle la carga
                Console.WriteLine($"Error cargando los servicios: {ex.Message}");
            }
        }

        public async void EliminarServicio(object? parameter)
        {
            if (parameter is int idServicio) // Asegúrate de que el parámetro sea un entero
            {
                using (var contexto = new ContextoSMMS())
                {
                    // Busca el servicio en la base de datos por ID
                    var servicioAEliminar = await contexto.Servicios.FindAsync(idServicio);
                    if (servicioAEliminar != null)
                    {
                        contexto.Servicios.Remove(servicioAEliminar);
                        await contexto.SaveChangesAsync(); // Guarda los cambios en la base de datos

                        // Actualiza la ObservableCollection si es necesario
                        var servicioInfo = ListaServicio.FirstOrDefault(s => s.ServicioId == idServicio);
                        if (servicioInfo != null)
                        {
                            ListaServicio.Remove(servicioInfo);
                        }

                        // Muestra el mensaje de éxito
                        await _window.ShowMessageAsync("Éxito", "Servicio eliminado exitosamente.");
                    }
                    else
                    {
                        // Muestra el mensaje de error si no se encuentra el servicio
                        await _window.ShowMessageAsync("Error", "Servicio no encontrado.");
                    }
                }
            }
            else
            {
                // Muestra un mensaje de error si el parámetro no es del tipo esperado
                await _window.ShowMessageAsync("Error", "El parámetro no es del tipo esperado.");
            }
        }

        public async void AgregarServicio()
        {
            // Lista para almacenar mensajes de error
            List<string> mensajesErrores = new List<string>();

            // Validar campos requeridos
            if (string.IsNullOrWhiteSpace(NombreServicio))
                mensajesErrores.Add("El campo 'Nombre del Servicio' no puede estar vacío.");

            if (CostoServicio <= 0)
                mensajesErrores.Add("El campo 'Costo del Servicio' debe ser un número mayor que 0.");

            if (string.IsNullOrWhiteSpace(EmpresaServicio))
                mensajesErrores.Add("El campo 'Empresa que presta el servicio' no puede estar vacío.");

           if (IdSucursal == 0)
                mensajesErrores.Add("Debe seleccionar una sucursal!.");

            if (!FechaContratacion.HasValue)
                mensajesErrores.Add("El campo 'Fecha de Contratación' no puede estar vacío.");

            if (!FechaPago.HasValue)
                mensajesErrores.Add("El campo 'Fecha de Pago' no puede estar vacío.");

            // Verificar si hay errores
            if (mensajesErrores.Count > 0)
            {
                // Unir los mensajes de error en un solo string
                string mensajeCompleto = string.Join("\n", mensajesErrores);

                // Aquí puedes mostrar los mensajes de error al usuario, por ejemplo, usando un MessageBox o una notificación
                // Ejemplo:
                await _window.ShowMessageAsync("Errores", mensajeCompleto);

                return;
            }

            try
            {
                // Crear una nueva instancia del objeto Servicio
                var nuevoServicio = new Servicio
                {
                    NombreServicio = NombreServicio,
                    CostoServicio = CostoServicio,
                    EmpresaServicio = EmpresaServicio,
                    FechaContratacion = FechaContratacion.HasValue ? DateOnly.FromDateTime(FechaContratacion.Value) : (DateOnly?)null,
                    FechaPago = FechaPago.HasValue ? DateOnly.FromDateTime(FechaPago.Value) : (DateOnly?)null,
                    SucursalId = IdSucursal,
                    EmpresaId = GlobalSettings.IdEmpresa // Aquí puedes agregar el Id de la empresa que corresponda
                };

                // Utilizamos el contexto para agregar el servicio
                using (var context = new ContextoSMMS()) // ContextoSMMS es el nombre de tu clase de contexto
                {
                    // Agregar el nuevo servicio al contexto
                    context.Servicios.Add(nuevoServicio);

                    // Guardar los cambios en la base de datos
                    await context.SaveChangesAsync();
                }

                // Aquí podrías manejar el caso de éxito (ej. mostrar un mensaje al usuario)
                await _window.ShowMessageAsync("Éxito", "El servicio se ha agregado correctamente.");
                LimpiarCampos();
                await CargarServicios();
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que ocurra durante el proceso
                // Aquí podrías mostrar un mensaje de error al usuario
                Console.WriteLine($"Error al agregar el servicio: {ex.Message}");
                await _window.ShowMessageAsync("Error", "Hubo un problema al agregar el servicio.");
            }
        }

        private void LimpiarCampos()
        {
            // Limpiar las propiedades del ViewModel o las variables de los campos
            NombreServicio = string.Empty;
            CostoServicio = 0;
            EmpresaServicio = string.Empty;
            FechaContratacion = null;
            FechaPago = null;
            IdSucursal = 0;

        }
        // Método para notificar cambios de propiedad
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string nombrePropiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombrePropiedad));
        }
   

}
}
