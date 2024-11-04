using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using iText.Kernel.Pdf.Canvas.Wmf;
using System.Windows.Documents;
using Microsoft.EntityFrameworkCore;
using SociedadCorreaCorrea.Commands;
using SociedadCorreaCorrea.Data;
using SociedadCorreaCorrea.Models;
using SociedadCorreaCorrea.ModelsCustoms;
using SociedadCorreaCorrea.ViewModels;
using SociedadCorreaCorrea.Views;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using iText.Commons.Actions.Contexts;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SociedadCorreaCorrea.ViewsModels
{
    public class TrabajadoresViewModel : BaseViewModel
    {
        private readonly ContextoSMMS _contexto;
        public ObservableCollection<Turno> ListaTurno { get; set; } = new ObservableCollection<Turno>();
        public ObservableCollection<Puesto> ListaPuesto { get; set; } = new ObservableCollection<Puesto>();
        public ObservableCollection<Sucursal> ListaSucursal { get; set; } = new ObservableCollection<Sucursal>();
        public ObservableCollection<Empleado> ListaEmpleado { get; set; } = new ObservableCollection<Empleado>();
        public ObservableCollection<InformacionEmpleados> InformacionEmpleado { get; set; } = new ObservableCollection<InformacionEmpleados>();
        public ICommand EliminarEmpleadoCommand { get; }
        public ICommand EditarEmpleadoCommand { get; }
        public ICommand EnviarCorreoCommand { get; }
        private readonly MetroWindow _window;
        // Configuración de SMTP
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _port = 587;
        private readonly string _username = "dosolutionenviocorreo@gmail.com"; // Cambia esto por tu correo
        private readonly string _password = "vtrz nkby zkkp rxbl"; // Cambia esto por tu contraseña

        private int _turnoTrabajoId;
        private string _puestoTrabajo;
        private int _sucursalId;
        private string _apellidoEmpleado;
        private string _rutEmpleado;
        private string _direccionEmpleado;
        private string _telefonoEmpleado;
        private string _correoEmpleado;
        private decimal _salarioEmpleado;
        private DateTime? _fechaNacimientoEmpleado;
        private DateTime? _fechaContratacionEmpleado;
        private string _nombreEmpleado;
        private string _tareasEmpleado;


        public string TareasEmpleado
        {
            get => _tareasEmpleado;
            set { if (_tareasEmpleado != value) { _tareasEmpleado = value; OnPropertyChanged(nameof(TareasEmpleado)); } }
        }
        
        public string ApellidoEmpleado
        {
            get => _apellidoEmpleado;
            set { if (_apellidoEmpleado != value) { _apellidoEmpleado = value; OnPropertyChanged(nameof(ApellidoEmpleado)); } }
        }

        public string RutEmpleado
        {
            get => _rutEmpleado;
            set { if (_rutEmpleado != value) { _rutEmpleado = value; OnPropertyChanged(nameof(RutEmpleado)); } }
        }

        public string DireccionEmpleado
        {
            get => _direccionEmpleado;
            set { if (_direccionEmpleado != value) { _direccionEmpleado = value; OnPropertyChanged(nameof(DireccionEmpleado)); } }
        }

        public string TelefonoEmpleado
        {
            get => _telefonoEmpleado;
            set { if (_telefonoEmpleado != value) { _telefonoEmpleado = value; OnPropertyChanged(nameof(TelefonoEmpleado)); } }
        }

        public string CorreoEmpleado
        {
            get => _correoEmpleado;
            set { if (_correoEmpleado != value) { _correoEmpleado = value; OnPropertyChanged(nameof(CorreoEmpleado)); } }
        }

        public decimal SalarioEmpleado
        {
            get => _salarioEmpleado;
            set { if (_salarioEmpleado != value) { _salarioEmpleado = value; OnPropertyChanged(nameof(SalarioEmpleado)); } }
        }

        public DateTime? FechaNacimientoEmpleado
        {
            get => _fechaNacimientoEmpleado;
            set { if (_fechaNacimientoEmpleado != value) { _fechaNacimientoEmpleado = value; OnPropertyChanged(nameof(FechaNacimientoEmpleado)); } }
        }

        public DateTime? FechaContratacionEmpleado
        {
            get => _fechaContratacionEmpleado;
            set { if (_fechaContratacionEmpleado != value) { _fechaContratacionEmpleado = value; OnPropertyChanged(nameof(FechaContratacionEmpleado)); } }
        }

        public int TurnoTrabajoId
        {
            get => _turnoTrabajoId;
            set { if (_turnoTrabajoId != value) { _turnoTrabajoId = value; OnPropertyChanged(nameof(TurnoTrabajoId)); } }
        }

        public string PuestoTrabajo
        {
            get => _puestoTrabajo;
            set { if (_puestoTrabajo != value) { _puestoTrabajo = value; OnPropertyChanged(nameof(PuestoTrabajo)); } }
        }

        public int IdSucursal
        {
            get => _sucursalId; set { if (_sucursalId != value) { _sucursalId = value; OnPropertyChanged(nameof(IdSucursal)); } }
        }

        public string NombreEmpleado
        {
            get => _nombreEmpleado; set { if (_nombreEmpleado != value) { _nombreEmpleado = value; OnPropertyChanged(); } }
        }

        private string _mensajeCorreo;
        public string MensajeCorreo
        {
            get => _mensajeCorreo;
            set
            {
                _mensajeCorreo = value;
                OnPropertyChanged(nameof(MensajeCorreo)); // Notificar cambio de propiedad
            }
        }

        private string _asuntoCorreo;
        public string AsuntoCorreo
        {
            get => _asuntoCorreo;
            set
            {
                _asuntoCorreo = value;
                OnPropertyChanged(nameof(AsuntoCorreo)); // Notificar cambio de propiedad
            }
        }

        private string _destinatarioCorreo;
        public string DestinatarioCorreo
        {
            get => _destinatarioCorreo;
            set
            {
                _destinatarioCorreo = value;
                OnPropertyChanged(nameof(DestinatarioCorreo)); // Notificar cambio de propiedad
            }
        }

        private string _nuevoTurnoNombreTurno;
        private string _nuevoTurnoDiaSemanaTurno;
        private string _nuevoTurnoHoraInicioTurno;
        private string _nuevoTurnoHoraFinTurno;

        public string NuevoTurnoNombreTurno
        {
            get => _nuevoTurnoNombreTurno;
            set {
                _nuevoTurnoNombreTurno = FormatComboBoxValue(value);
                OnPropertyChanged(); }
        }

        public string NuevoTurnoDiaSemanaTurno
        {
            get => _nuevoTurnoDiaSemanaTurno;
            set { _nuevoTurnoDiaSemanaTurno = FormatComboBoxValue(value); 
                 OnPropertyChanged(); }
        }

        public string NuevoTurnoHoraInicioTurno
        {
            get => _nuevoTurnoHoraInicioTurno;
            set { _nuevoTurnoHoraInicioTurno = value; OnPropertyChanged(); }
        }

        public string NuevoTurnoHoraFinTurno
        {
            get => _nuevoTurnoHoraFinTurno;
            set { _nuevoTurnoHoraFinTurno = value; OnPropertyChanged(); }
        }

        private string _nuevoTurnoHoraInicioAlmuerzo;
        public string NuevoTurnoHoraInicioAlmuerzo
        {
            get => _nuevoTurnoHoraInicioAlmuerzo;
            set
            {
                _nuevoTurnoHoraInicioAlmuerzo = value;
                OnPropertyChanged(nameof(NuevoTurnoHoraInicioAlmuerzo));
            }
        }

        private string _nuevoTurnoHoraFinAlmuerzo;
        public string NuevoTurnoHoraFinAlmuerzo
        {
            get => _nuevoTurnoHoraFinAlmuerzo;
            set
            {
                _nuevoTurnoHoraFinAlmuerzo = value;
                OnPropertyChanged(nameof(NuevoTurnoHoraFinAlmuerzo));
            }
        }


        public ICommand RegistrarTurnoCommand { get; }


        public TrabajadoresViewModel(MetroWindow window)
        {
            _window = window;
            _contexto = new ContextoSMMS(new DbContextOptions<ContextoSMMS>());
            RegistrarEmpleados = new RelayCommand(RegistrarEmpleado);
            EliminarEmpleadoCommand = new RelayCommand(EliminarEmpleado);
            EditarEmpleadoCommand = new RelayCommand(EditarEmpleado);
            EnviarCorreoCommand = new RelayCommand(async () => await EnviarCorreo());
            RegistrarTurnoCommand = new RelayCommand(RegistrarTurno);
            InicializarAsync();

        }

        private async void InicializarAsync()
        {
            await CargarPuestos();
            await CargarTurnos();
            await CargarSucursales();
            CargarInformacionEmpleados();

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

        private async Task CargarPuestos()
        {
            try
            {
                // Carga los puestos filtrando por IdEmpresa y estado activo
                var puestos = await _contexto.Puestos
                    .Where(p => p.IdEmpresa == GlobalSettings.IdEmpresa)
                    .Where(p => p.EstadoPuesto == "Activo")
                    .ToListAsync();

                // Itera sobre los puestos y los añade a la colección
                foreach (var puesto in puestos)
                {
                    // Agrega un nuevo puesto a la lista
                    ListaPuesto.Add(new Puesto
                    {
                        IdPuestos = puesto.IdPuestos, // Asigna el ID del puesto 
                        NombrePuesto = puesto.NombrePuesto
                    });
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones: puedes registrar el error o mostrar un mensaje
                Console.WriteLine($"Error cargando puestos: {ex.Message}");
            }
        }

        // Método asíncrono para cargar los turnos desde la base de datos
        private async Task CargarTurnos()
        {
            try
            {
                // Carga los turnos filtrando por idSucursal
                var turnos = await _contexto.Turnos
                    .Where(turno => turno.IdEmpresa == GlobalSettings.IdEmpresa) // Filtro según la sucursal
                    .ToListAsync();

                // Itera sobre los turnos y los añade a la colección
                foreach (var turno in turnos)
                {
                    // Agrega un nuevo turno a la lista con el nombre combinado
                    ListaTurno.Add(new Turno
                    {
                        IdTurno = turno.IdTurno, // Asigna el ID del turno
                        NombreTurno = $"{turno.NombreTurno} - {turno.DiaSemanaTurno} " +
                              $"({turno.HoraInicioTurno:hh\\:mm} - {turno.HoraFinTurno:hh\\:mm}) " +
                              $"Almuerzo: {turno.HoraAlmuerzoInicioTurno:hh\\:mm} - {turno.HoraAlmuerzoFinTurno:hh\\:mm}"
                    });
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones: puedes registrar el error o mostrar un mensaje
                Console.WriteLine($"Error cargando turnos: {ex.Message}");
            }
        }

        public ICommand RegistrarEmpleados { get; }

        private async void RegistrarEmpleado()
        {
            // Lista para almacenar mensajes de error
            List<string> mensajesErrores = new List<string>();

            // Validaciones
            if (string.IsNullOrWhiteSpace(NombreEmpleado))
            {
                mensajesErrores.Add("El nombre del empleado es requerido.");
            }

            if (string.IsNullOrWhiteSpace(ApellidoEmpleado))
            {
                mensajesErrores.Add("El apellido del empleado es requerido.");
            }

            if (!FechaNacimientoEmpleado.HasValue)
            {
                mensajesErrores.Add("La fecha de nacimiento del empleado es requerida.");
            }

            if (string.IsNullOrWhiteSpace(DireccionEmpleado))
            {
                mensajesErrores.Add("La dirección del empleado es requerida.");
            }

            if (string.IsNullOrWhiteSpace(TelefonoEmpleado))
            {
                mensajesErrores.Add("El teléfono del empleado es requerido.");
            }

            if (string.IsNullOrWhiteSpace(CorreoEmpleado))
            {
                mensajesErrores.Add("El correo del empleado es requerido.");
            }

            if (string.IsNullOrWhiteSpace(PuestoTrabajo))
            {
                mensajesErrores.Add("El puesto de trabajo del empleado es requerido.");
            }

            if (SalarioEmpleado <= 0)
            {
                mensajesErrores.Add("El salario del empleado debe ser mayor a 0.");
            }

            if (!FechaContratacionEmpleado.HasValue)
            {
                mensajesErrores.Add("La fecha de contratación del empleado es requerida.");
            }

            if (string.IsNullOrWhiteSpace(RutEmpleado))
            {
                mensajesErrores.Add("El RUT del empleado es requerido.");
            }

            // Verificar si hay errores
            if (mensajesErrores.Count > 0)
            {
                string mensajeCompleto = string.Join("\n", mensajesErrores);
                await _window.ShowMessageAsync("Errores", mensajeCompleto);
                return; // Salir de la función si hay errores
            }

            try
            {
                using (var context = new ContextoSMMS())
                {
                    var nuevoEmpleado = new Empleado
                    {
                        IdEmpresa = GlobalSettings.IdEmpresa,
                        IdTurno = TurnoTrabajoId,
                        IdSucursal = IdSucursal,
                        IdUsuario = UserSession.Id,
                        NombreEmpleado = NombreEmpleado,
                        ApellidoEmpleado = ApellidoEmpleado,
                        FechaNacimientoEmpleado = FechaNacimientoEmpleado.HasValue ? DateOnly.FromDateTime(FechaNacimientoEmpleado.Value) : (DateOnly?)null,
                        DireccionEmpleado = DireccionEmpleado,
                        TelefonoEmpleado = TelefonoEmpleado,
                        CorreoEmpleado = CorreoEmpleado,
                        PuestoEmpleado = PuestoTrabajo,
                        SalarioEmpleado = SalarioEmpleado,
                        FechaContratacionEmpleado = FechaContratacionEmpleado.HasValue ? DateOnly.FromDateTime(FechaContratacionEmpleado.Value) : (DateOnly?)null,
                        EstatusEmpleado = "Activo",
                        RutEmpleado = RutEmpleado,
                        TareasEmpleado = TareasEmpleado
                    };

                    // Añadir el nuevo empleado al contexto
                    await context.Empleados.AddAsync(nuevoEmpleado);

                    // Guardar cambios en la base de datos
                    await context.SaveChangesAsync();

                    // Notificar que el registro fue exitoso
                    await _window.ShowMessageAsync("Éxito", "Empleado registrado exitosamente.");

                    LimpiarCamposDeTexto();
                }
            }
            catch (DbUpdateException dbEx)
            {
                // Manejo específico para errores de actualización en la base de datos
                await _window.ShowMessageAsync("Error", $"Error al registrar el empleado en la base de datos: {dbEx.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                // Manejo general de excepciones
                await _window.ShowMessageAsync("Error", $"Ocurrió un error inesperado: {ex.Message}");
            }
        }
        private void LimpiarCamposDeTexto()
        {
            // Reiniciar las propiedades a sus valores por defecto
            NombreEmpleado = string.Empty;
            ApellidoEmpleado = string.Empty;
            FechaNacimientoEmpleado = null; // Asignar null para campos de tipo nullable
            DireccionEmpleado = string.Empty;
            TelefonoEmpleado = string.Empty;
            CorreoEmpleado = string.Empty;
            PuestoTrabajo = string.Empty;
            SalarioEmpleado = 0; // O el valor que consideres por defecto
            FechaContratacionEmpleado = null; // Asignar null para campos de tipo nullable
            RutEmpleado = string.Empty;

            // Aquí podrías también limpiar cualquier otro campo adicional que tengas
        }

        // Método para cargar empleados desde la base de datos
        public void CargarInformacionEmpleados()
        {
            using (var contexto = new ContextoSMMS())
            {
                // Cargar empleados junto con sus entidades relacionadas
                var empleados = contexto.Empleados
                    .Include(e => e.IdTurnoNavigation) // Incluye el Turno
                    .Include(e => e.IdSucursalNavigation) // Incluye la Sucursal
                    .Include(e => e.IdEmpresaNavigation) // Incluye la Empresa
                    .Where(e => e.IdEmpresa == GlobalSettings.IdEmpresa) // Filtro adicional
                    .ToList();

                InformacionEmpleado.Clear(); // Limpia la colección antes de agregar nuevos elementos

                foreach (var empleado in empleados)
                {
                    var infoEmpleado = new InformacionEmpleados
                    {
                        Empleado = empleado, // Asigna el objeto empleado directamente
                        InfoTurno = empleado.IdTurnoNavigation == null
                        ? "No tiene un turno asignado"
                        : $"Tipo De Turno: {empleado.IdTurnoNavigation.NombreTurno}, " +
                          $"Día De Semana: {empleado.IdTurnoNavigation.DiaSemanaTurno}, " +
                          $"Hora Inicio: {empleado.IdTurnoNavigation.HoraInicioTurno}, " +
                          $"Hora Fin: {empleado.IdTurnoNavigation.HoraFinTurno}, " +
                          $"Hora Inicio Almuerzo: {empleado.IdTurnoNavigation.HoraAlmuerzoInicioTurno}, " +
                          $"Hora Fin Almuerzo: {empleado.IdTurnoNavigation.HoraAlmuerzoFinTurno}",
                        NotasAdicionales = "Información adicional" // Asigna información adicional si es necesario
                    };
                    InformacionEmpleado.Add(infoEmpleado);
                }
            }
        }

        private void EditarEmpleado(object id)
        {
            if (id is int idEmpleado)
            {
                // Buscar el empleado en la base de datos
                var empleadoSeleccionado = _contexto.Empleados.Find(idEmpleado);
                if (empleadoSeleccionado != null)
                {
                    // Crear el ViewModel para la ventana de edición
                    var viewModel = new EditarInformacionEmpleadoViewModel(empleadoSeleccionado, _window);
                    var ventanaEditar = new EditarInformacionEmpleado(empleadoSeleccionado)
                    {
                        DataContext = viewModel
                    };

                    // Abrir la ventana de edición
                    ventanaEditar.ShowDialog();

                    // Recargar la lista después de editar si es necesario
                    CargarInformacionEmpleados();
                }
                else
                {
                    _window.ShowMessageAsync("Error", "Empleado no encontrado.");
                }
            }
            
        }

        public async void EliminarEmpleado(object? parameter)
        {
            if (parameter is int idEmpleado) // Asegúrate de que el parámetro sea un entero
            {
                using (var contexto = new ContextoSMMS())
                {
                    // Busca el empleado en la base de datos por ID
                    var empleadoAEliminar = await contexto.Empleados.FindAsync(idEmpleado);
                    if (empleadoAEliminar != null)
                    {
                        contexto.Empleados.Remove(empleadoAEliminar);
                        await contexto.SaveChangesAsync(); // Guarda los cambios en la base de datos

                        // Actualiza la ObservableCollection si es necesario
                        var empleadoInfo = InformacionEmpleado.FirstOrDefault(e => e.Empleado.IdEmpleado == idEmpleado);
                        if (empleadoInfo != null)
                        {
                            InformacionEmpleado.Remove(empleadoInfo);
                        }

                        // Muestra el mensaje de éxito
                        await _window.ShowMessageAsync("Éxito", "Empleado eliminado exitosamente.");
                    }
                    else
                    {
                        // Muestra el mensaje de error si no se encuentra el empleado
                        await _window.ShowMessageAsync("Error", "Empleado no encontrado.");
                    }
                }
            }
            else
            {
                // Muestra un mensaje de error si el parámetro no es del tipo esperado
                await _window.ShowMessageAsync("Error", "El parámetro no es del tipo esperado.");
            }
        }

        // Método para enviar correo electrónico
        private async Task EnviarCorreo()
        {
            string destinatario = DestinatarioCorreo; // Usar el destinatario ingresado por el usuario
            string asunto = AsuntoCorreo; // Usar el asunto ingresado por el usuario
            string mensaje = MensajeCorreo; // Usar el mensaje ingresado por el usuario

            try
            {
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(_username);
                    mail.To.Add(destinatario);
                    mail.Subject = asunto;
                    mail.Body = mensaje;
                    mail.IsBodyHtml = true;

                    using (var smtp = new SmtpClient(_smtpServer, _port))
                    {
                        smtp.Credentials = new NetworkCredential(_username, _password);
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                    }
                }

                await _window.ShowMessageAsync("Éxito", "Correo enviado exitosamente.");
            }
            catch (Exception ex)
            {
                await _window.ShowMessageAsync("Error", $"No se pudo enviar el correo: {ex.Message}");
            }
        }

        private async void RegistrarTurno()
        {
            try
            {
                using (var context = new ContextoSMMS())
                {
                    var nuevoTurno = new Turno
                    {
                        NombreTurno = NuevoTurnoNombreTurno,
                        DiaSemanaTurno = NuevoTurnoDiaSemanaTurno,
                        HoraInicioTurno = TimeOnly.FromTimeSpan(TimeSpan.Parse(NuevoTurnoHoraInicioTurno)),
                        HoraFinTurno = TimeOnly.FromTimeSpan(TimeSpan.Parse(NuevoTurnoHoraFinTurno)),
                        HoraAlmuerzoInicioTurno = TimeOnly.FromTimeSpan(TimeSpan.Parse(NuevoTurnoHoraInicioAlmuerzo)),
                        HoraAlmuerzoFinTurno = TimeOnly.FromTimeSpan(TimeSpan.Parse(NuevoTurnoHoraFinAlmuerzo))
                    };

                    // Añadir el nuevo turno al contexto
                    await context.Turnos.AddAsync(nuevoTurno);

                    // Guardar cambios en la base de datos
                    await context.SaveChangesAsync();

                    // Notificar que el registro fue exitoso
                    await _window.ShowMessageAsync("Éxito", "Turno registrado exitosamente.");

                    // Limpiar los campos de texto
                    LimpiarCamposDeTextoTurno();
                }
            }
            catch (DbUpdateException dbEx)
            {
                // Manejo específico para errores de actualización en la base de datos
                await _window.ShowMessageAsync("Error", $"Error al registrar el turno en la base de datos: {dbEx.InnerException?.Message}");
            }
            catch (FormatException formatEx)
            {
                // Manejo de formato incorrecto para las horas
                await _window.ShowMessageAsync("Error", $"Formato de hora incorrecto: {formatEx.Message}");
            }
            catch (Exception ex)
            {
                // Manejo general de excepciones
                await _window.ShowMessageAsync("Error", $"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        private void LimpiarCamposDeTextoTurno()
        {
            NuevoTurnoNombreTurno = string.Empty;
            NuevoTurnoDiaSemanaTurno = string.Empty;
            NuevoTurnoHoraInicioTurno = string.Empty;
            NuevoTurnoHoraFinTurno = string.Empty;
            NuevoTurnoHoraInicioAlmuerzo = string.Empty;
            NuevoTurnoHoraFinAlmuerzo = string.Empty;
        }

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
    }
}
