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
using Google.Apis.Drive.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System.IO;
using Google.Apis.Services;
using Microsoft.Win32;
using Google.Apis.Drive.v3.Data;
using System;
using System.Threading.Tasks;
using GoogleFile = Google.Apis.Drive.v3.Data.File;
using SystemFile = System.IO.File; // Alias para evitar conflictos
using System.Windows;


namespace SociedadCorreaCorrea.ViewsModels
{
    public class TrabajadoresViewModel : BaseViewModel
    {
        private readonly ContextoSMMS _contexto;
        public ObservableCollection<Turno> ListaTurno { get; set; } = new ObservableCollection<Turno>();
        public ObservableCollection<Puesto> ListaPuesto { get; set; } = new ObservableCollection<Puesto>();
        public ObservableCollection<Sucursal> ListaSucursal { get; set; } = new ObservableCollection<Sucursal>();
        public ObservableCollection<TareasDiaria> ListaTareaDiaria { get; set; } = new ObservableCollection<TareasDiaria>();
        public ObservableCollection<Empleado> ListaEmpleado { get; set; } = new ObservableCollection<Empleado>();
        public ObservableCollection<InformacionEmpleados> InformacionEmpleado { get; set; } = new ObservableCollection<InformacionEmpleados>();
        public ICommand EliminarEmpleadoCommand { get; }
        public ICommand EliminarTareaDiariaCommand { get; }
        public ICommand EditarEmpleadoCommand { get; }
        public ICommand EnviarCorreoCommand { get; }
        public ICommand GuardarTareaCommand { get; }
        private DriveService servicioDrive;
        private readonly MetroWindow _window;
        // Configuración de SMTP
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _port = 587;
        private readonly string _username = "dosolutionenviocorreo@gmail.com"; // Cambia esto por tu correo
        private readonly string _password = "vtrz nkby zkkp rxbl"; // Cambia esto por tu contraseña

        private const string NombreArchivoCredenciales = "Resources/credentiales.json"; // Asegúrate de que este archivo esté presente
        private const string NombreAplicacion = "SociedadCorreaCorrea";

        // Usaremos un alcance más amplio para asegurar acceso completo a los archivos
        private readonly string[] Scopes = { DriveService.Scope.Drive }; // Permiso completo de Google Drive


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

        private int _sucursalIdTD;
        public int SucursalIdTD
        {
            get => _sucursalIdTD;
            set
            {
                _sucursalIdTD = value;
                OnPropertyChanged();
            }
        }

        private int _empleadoIdTD;
        public int EmpleadoIdTD
        {
            get => _empleadoIdTD;
            set
            {
                _empleadoIdTD = value; // Aquí está el problema
                OnPropertyChanged();
            }
        }


        private string _nombreTarea = string.Empty;
        public string NombreTarea
        {
            get => _nombreTarea;
            set { _nombreTarea = value; OnPropertyChanged(); }
        }

        private string? _descripcionTarea;
        public string? DescripcionTarea
        {
            get => _descripcionTarea;
            set { _descripcionTarea = value; OnPropertyChanged(); }
        }

        private DateOnly _fechaTarea = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly FechaTarea
        {
            get => _fechaTarea;
            set { _fechaTarea = value; OnPropertyChanged(); }
        }

        private string _horaInicioTarea;
        public string HoraInicioTarea
        {
            get => _horaInicioTarea;
            set { _horaInicioTarea = value; OnPropertyChanged(); }
        }

        private string _horaTerminoTarea;
        public string HoraTerminoTarea
        {
            get => _horaTerminoTarea;
            set { _horaTerminoTarea = value; OnPropertyChanged(); }
        }

        // Propiedad para mostrar el estado de autenticación
        private string _estadoAutenticacion;
        public string EstadoAutenticacion
        {
            get => _estadoAutenticacion;
            set
            {
                _estadoAutenticacion = value;
                OnPropertyChanged();
            }
        }

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
            set
            {
                _nuevoTurnoNombreTurno = FormatComboBoxValue(value);
                OnPropertyChanged();
            }
        }

        public string NuevoTurnoDiaSemanaTurno
        {
            get => _nuevoTurnoDiaSemanaTurno;
            set
            {
                _nuevoTurnoDiaSemanaTurno = FormatComboBoxValue(value);
                OnPropertyChanged();
            }
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

        private string _filePath;

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }

        private byte[] _contenidoArchivoContrato;

        // Propiedad para almacenar el contenido binario del archivo
        public byte[] ContenidoArchivoContrato
        {
            get => _contenidoArchivoContrato;
            set
            {
                _contenidoArchivoContrato = value;
                OnPropertyChanged(nameof(ContenidoArchivoContrato));
            }
        }

        private string _filePath2;

        public string FilePath2
        {
            get { return _filePath2; }
            set
            {
                _filePath2 = value;
                OnPropertyChanged();
            }
        }

        private byte[] _contenidoArchivoHistorialMedico;

        // Propiedad para almacenar el contenido binario del archivo
        public byte[] ContenidoArchivoHistorialMedico
        {
            get => _contenidoArchivoHistorialMedico;
            set
            {
                _contenidoArchivoHistorialMedico = value;
                OnPropertyChanged(nameof(ContenidoArchivoHistorialMedico));
            }
        }

        public ICommand OpenFileCommand { get; }
        public ICommand OpenFileHistorialMedicoCommand { get; }
        public ICommand RegistrarTurnoCommand { get; }
        public ICommand AutenticarCommand { get; private set; }
        public ICommand DesautenticarCommand { get; private set; }

        public TrabajadoresViewModel(MetroWindow window)
        {
            _window = window;
            _contexto = new ContextoSMMS(new DbContextOptions<ContextoSMMS>());
            RegistrarEmpleados = new RelayCommand(RegistrarEmpleado);
            EliminarEmpleadoCommand = new RelayCommand(EliminarEmpleado);
            EliminarTareaDiariaCommand = new RelayCommand(EliminarTareaDiaria);
            EditarEmpleadoCommand = new RelayCommand(EditarEmpleado);
            GuardarTareaCommand = new RelayCommand(RegistrarTarea);
            EnviarCorreoCommand = new RelayCommand(async () => await EnviarCorreo());
            RegistrarTurnoCommand = new RelayCommand(RegistrarTurno);
            OpenFileCommand = new RelayCommand(OpenFile);
            OpenFileHistorialMedicoCommand = new RelayCommand(OpenFile2);


            // Inicialización de comandos
            AutenticarCommand = new RelayCommand(async () => await AutenticarAsync());
            DesautenticarCommand = new RelayCommand(() => Desautenticar());

            // Verificar si ya está autenticado al iniciar
            VerificarEstadoAutenticacion();
            InicializarAsync();


        }

        private async void RegistrarTarea()
        {
            // Lista para almacenar mensajes de error
            List<string> mensajesErrores = new List<string>();

            // Validación de Nombre de la Tarea
            if (string.IsNullOrWhiteSpace(NombreTarea))
            {
                mensajesErrores.Add("El nombre de la tarea es requerido.");
            }

            // Validación de Descripción de la Tarea
            if (string.IsNullOrWhiteSpace(DescripcionTarea))
            {
                mensajesErrores.Add("La descripción de la tarea es requerida.");
            }

            // Validación de Sucursal
            if (SucursalIdTD == 0)
            {
                mensajesErrores.Add("Debes seleccionar una sucursal válida.");
            }

            // Validación de Empleado
            if (EmpleadoIdTD == 0)
            {
                mensajesErrores.Add("Debes seleccionar un empleado válido.");
            }

            // Validación de Fecha (no permitir fecha futura)
            if (FechaTarea > DateOnly.FromDateTime(DateTime.Today))
            {
                mensajesErrores.Add("La fecha de la tarea no puede ser en el futuro.");
            }

            // Validación de horas (Hora de inicio debe ser válida)
            if (string.IsNullOrWhiteSpace(HoraInicioTarea) || !TimeOnly.TryParse(HoraInicioTarea, out _))
            {
                mensajesErrores.Add("La hora de inicio no es válida.");
            }

            // Validación de horas (Hora de término debe ser válida)
            if (string.IsNullOrWhiteSpace(HoraTerminoTarea) || !TimeOnly.TryParse(HoraTerminoTarea, out _))
            {
                mensajesErrores.Add("La hora de término no es válida.");
            }

            // Validación de que la hora de inicio sea antes que la hora de término
            if (TimeOnly.TryParse(HoraInicioTarea, out var horaInicio) && TimeOnly.TryParse(HoraTerminoTarea, out var horaTermino))
            {
                if (horaInicio >= horaTermino)
                {
                    mensajesErrores.Add("La hora de inicio debe ser antes que la hora de término.");
                }
            }

            // Si existen errores, mostrar el mensaje y salir de la función
            if (mensajesErrores.Count > 0)
            {
                string mensajeCompleto = string.Join("\n", mensajesErrores);
                await _window.ShowMessageAsync("Errores", mensajeCompleto);
                return; // Salir de la función si hay errores
            }

            // Si no hay errores, proceder con el registro de la tarea
            try
            {
                using (var contexto = new ContextoSMMS())
                {
                    // Crear nueva instancia de TareasDiaria
                    var nuevaTarea = new TareasDiaria
                    {
                        EmpresaId = GlobalSettings.IdEmpresa,
                        SucursalId = SucursalIdTD,
                        EmpleadoId = EmpleadoIdTD,
                        NombreTarea = NombreTarea,
                        DescripcionTarea = DescripcionTarea,
                        FechaTarea = FechaTarea,
                        HoraInicioTarea = TimeOnly.Parse(HoraInicioTarea),  // O usar TryParse()
                        HoraTerminoTarea = TimeOnly.Parse(HoraTerminoTarea),  // O usar TryParse()
                    };

                    // Añadir la nueva tarea al contexto
                    contexto.TareasDiarias.Add(nuevaTarea);

                    // Intentar guardar los cambios en la base de datos
                    await contexto.SaveChangesAsync();

                    // Mostrar mensaje de éxito
                    await _window.ShowMessageAsync("Éxito", "La tarea se ha registrado correctamente.");

                    // Limpiar los campos después de registrar la tarea
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                // Capturamos cualquier error al guardar y mostramos el mensaje
                await _window.ShowMessageAsync("Error", $"Hubo un problema al registrar la tarea: {ex.Message}");
            }

        }

        /// <summary>
        /// Método para limpiar los campos después de registrar la tarea.
        /// </summary>
        private void LimpiarCampos()
        {
            NombreTarea = string.Empty;
            DescripcionTarea = string.Empty;
            HoraInicioTarea = string.Empty;
            HoraTerminoTarea = string.Empty;
            FechaTarea = DateOnly.FromDateTime(DateTime.Now);  // Resetear la fecha a la fecha actual
            SucursalIdTD = 0;  // Limpiar la sucursal seleccionada
            EmpleadoIdTD = 0;  // Limpiar el empleado seleccionado
                               // Si tienes más campos a limpiar, añádelos aquí.
        }


        private void OpenFile()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName; // Asigna el path del archivo seleccionado
                ContenidoArchivoContrato = SystemFile.ReadAllBytes(FilePath);
            }
        }

        private void OpenFile2()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath2 = openFileDialog.FileName; // Asigna el path del archivo seleccionado
                ContenidoArchivoHistorialMedico = SystemFile.ReadAllBytes(FilePath);
            }
        }
        private async void InicializarAsync()
        {
            await CargarPuestos();
            await CargarTurnos();
            await CargarSucursales();
            CargarInformacionEmpleados();
            await AutenticarAsync();
            CargarTareasDiarias();



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

            // Crear la ventana de carga
            var ventanaCarga = new PantallaCarga
            {
                DataContext = new PantallaCargaViewModel()
            };

            try
            {
                // Mostrar la ventana de carga como modal
                _ = Task.Run(() =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ventanaCarga.ShowDialog();
                    });
                });


                using (var context = new ContextoSMMS())
                {
                    // Concatenar el nombre del empleado con el RUT
                    string nombreCompletoYRut = $"{NombreEmpleado} {ApellidoEmpleado} {RutEmpleado}";

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

                    // Paso 1: Buscar la carpeta "Empleados" en Google Drive
                    var buscarCarpetaEmpleadosRequest = servicioDrive.Files.List();
                    buscarCarpetaEmpleadosRequest.Q = "mimeType = 'application/vnd.google-apps.folder' and name = 'Empleados'"; // Buscar solo carpetas con el nombre "Empleados"
                    buscarCarpetaEmpleadosRequest.Fields = "files(id, name)";

                    // Ejecutar la solicitud para obtener la carpeta "Empleados"
                    var resultadoBusqueda = await buscarCarpetaEmpleadosRequest.ExecuteAsync();

                    // Declarar la variable idCarpetaEmpleados fuera del bloque if
                    string idCarpetaEmpleados = null;

                    // Verificar si la carpeta "Empleados" existe
                    var carpetaEmpleados = resultadoBusqueda.Files?.FirstOrDefault();
                    if (carpetaEmpleados == null)
                    {
                        // Si la carpeta "Empleados" no existe, puedes crearla
                        var crearCarpetaRequest = servicioDrive.Files.Create(new Google.Apis.Drive.v3.Data.File()
                        {
                            Name = "Empleados",
                            MimeType = "application/vnd.google-apps.folder"
                        });
                        var carpetaCreada = await crearCarpetaRequest.ExecuteAsync();
                        idCarpetaEmpleados = carpetaCreada.Id; // ID de la carpeta "Empleados"
                    }
                    else
                    {
                        // Si la carpeta "Empleados" ya existe, obtenemos su ID
                        idCarpetaEmpleados = carpetaEmpleados.Id;
                    }

                    // Verificar que la carpeta "Empleados" existe (ya sea creada o encontrada)
                    if (string.IsNullOrEmpty(idCarpetaEmpleados))
                    {
                        // Si no se obtuvo el ID de la carpeta "Empleados", mostrar un mensaje o manejar el error
                        Console.WriteLine("No se pudo obtener el ID de la carpeta 'Empleados'.");
                        return; // Detener la ejecución si no se puede continuar
                    }

                    // Paso 2: Crear la nueva carpeta para el empleado dentro de "Empleados"
                    var carpetaRequest = servicioDrive.Files.Create(new Google.Apis.Drive.v3.Data.File()
                    {
                        Name = nombreCompletoYRut, // Nombre de la carpeta: Nombre + Apellido + RUT
                        MimeType = "application/vnd.google-apps.folder", // Tipo de archivo para carpeta
                        Parents = new[] { idCarpetaEmpleados } // Especificar la carpeta "Empleados" como la carpeta padre
                    });

                    // Ejecutar la solicitud para crear la nueva carpeta
                    var carpeta = await carpetaRequest.ExecuteAsync();
                    string idCarpeta = carpeta.Id; // ID de la nueva carpeta creada dentro de "Empleados"

                    // Paso 3: Subir los archivos (igual que antes)

                    if (!string.IsNullOrEmpty(FilePath) && SystemFile.Exists(FilePath))
                    {
                        var archivoContrato = new GoogleFile
                        {
                            Name = Path.GetFileName(FilePath),
                            Parents = new[] { idCarpeta } // Especificar la carpeta de destino
                        };

                        using (var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                        {
                            var subirArchivoRequest = servicioDrive.Files.Create(archivoContrato, stream, "application/pdf");
                            subirArchivoRequest.Fields = "id"; // Solo devuelve el ID del archivo creado
                            await subirArchivoRequest.UploadAsync();
                        }
                    }

                    if (!string.IsNullOrEmpty(FilePath2) && SystemFile.Exists(FilePath2))
                    {
                        var archivoHistorialMedico = new GoogleFile
                        {
                            Name = Path.GetFileName(FilePath2),
                            Parents = new[] { idCarpeta } // Especificar la carpeta de destino
                        };

                        using (var stream = new FileStream(FilePath2, FileMode.Open, FileAccess.Read))
                        {
                            var subirArchivoRequest = servicioDrive.Files.Create(archivoHistorialMedico, stream, "application/pdf");
                            subirArchivoRequest.Fields = "id"; // Solo devuelve el ID del archivo creado
                            await subirArchivoRequest.UploadAsync();
                        }
                    }

                    // Guardar cambios en la base de datos
                    await context.SaveChangesAsync();

                }
                // Cerrar la ventana de carga
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (ventanaCarga.IsVisible)
                    {
                        ventanaCarga.Close();
                    }
                });
                // Notificar que la carpeta fue creada correctamente
                await _window.ShowMessageAsync("Éxito", "Empleado registrado y carpeta creada en Google Drive.");

                LimpiarCamposDeTexto();


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
                        NombreEmpleadoCompleto = (empleado.NombreEmpleado ?? "") + " " + (empleado.ApellidoEmpleado ?? ""),
                        IdEmpleado = empleado.IdEmpleado, // Asigna el objeto empleado directamente
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

        private async Task AutenticarAsync()
        {
            try
            {
                UserCredential credenciales;
                var dataStorePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SociedadCorreaCorrea", "TokenStore");

                if (!Directory.Exists(dataStorePath))
                {
                    Directory.CreateDirectory(dataStorePath);
                }

                using (var stream = new FileStream(NombreArchivoCredenciales, FileMode.Open, FileAccess.Read))
                {
                    credenciales = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        Scopes,
                        "usuario",
                        CancellationToken.None,
                        new FileDataStore(dataStorePath, true));

                    if (credenciales?.Token?.IsExpired(credenciales.Flow.Clock) == true)
                    {
                        await credenciales.RefreshTokenAsync(CancellationToken.None);
                        EstadoAutenticacion = "Usuario autenticado correctamente.";
                    }
                    InicializarServicioDrive(credenciales);  // Asegúrate de que las credenciales sean correctas
                    EstadoAutenticacion = "Usuario autenticado correctamente.";

                }
            }
            catch (Exception ex)
            {
                EstadoAutenticacion = $"Error al autenticar: {ex.Message}";
                await _window.ShowMessageAsync("Error", $"No se pudo autenticar: {ex.Message}");
            }
        }

        // Método para desautenticar al usuario
        private void Desautenticar()
        {
            try
            {
                // Ruta para la carpeta de tokens
                var dataStorePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SociedadCorreaCorrea", "TokenStore");
                // Eliminar el archivo de token si existe
                if (Directory.Exists(dataStorePath))
                {
                    Directory.Delete(dataStorePath, true); // Borrar el directorio y los archivos dentro
                }

                EstadoAutenticacion = "Sesión cerrada correctamente.";
            }
            catch (Exception ex)
            {
                EstadoAutenticacion = $"Error al desautenticar: {ex.Message}";
            }
        }

        private void VerificarEstadoAutenticacion()
        {
            try
            {
                // Ruta de almacenamiento para el archivo de token
                var dataStorePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SociedadCorreaCorrea", "TokenStore");
                var tokenFilePath = Path.Combine(dataStorePath, "Google.Apis.Auth.OAuth2.Responses.TokenResponse-usuario");

                // Verificar si la carpeta TokenStore y el archivo de token existen
                if (Directory.Exists(dataStorePath) && SystemFile.Exists(tokenFilePath))
                {
                    // El archivo de token existe, intentemos cargar las credenciales
                    var fileDataStore = new FileDataStore(dataStorePath, true);

                    // Intentamos obtener las credenciales del archivo
                    var token = fileDataStore.GetAsync<UserCredential>("usuario").Result;

                    if (token != null)
                    {
                        // Verificar si el token ha expirado
                        if (token.Token.IsExpired(token.Flow.Clock))
                        {
                            // Si el token ha expirado, intentar refrescarlo
                            token.RefreshTokenAsync(CancellationToken.None).Wait();
                            EstadoAutenticacion = "Token refrescado correctamente.";
                        }
                        else
                        {
                            // Si el token es válido
                            EstadoAutenticacion = "Usuario autenticado.";
                            InicializarServicioDrive(token);  // Inicializamos el servicio de Google Drive
                            return;  // Si todo está bien, no hacemos más
                        }
                    }
                    else
                    {
                        EstadoAutenticacion = "Usuario no autenticado. (Token no encontrado)";
                    }
                }
                else
                {
                    EstadoAutenticacion = "Usuario no autenticado. (Archivo de token no existe)";
                }
            }
            catch (Exception ex)
            {
                // Capturamos cualquier error para poder depurarlo
                EstadoAutenticacion = $"Error al verificar estado de autenticación: {ex.Message}";
            }
        }

        private void InicializarServicioDrive(UserCredential credenciales)
        {
            if (credenciales != null)
            {
                servicioDrive = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credenciales,
                    ApplicationName = NombreAplicacion
                });

                EstadoAutenticacion = "Usuario autenticado correctamente.";
            }
            else
            {
                EstadoAutenticacion = "Error al inicializar el servicio de Google Drive.";
            }
        }

        // Método para cargar los datos desde la base de datos
        private void CargarTareasDiarias()
        {
            using (var contexto = new ContextoSMMS()) // Asegúrate de usar tu clase de contexto
            {
                // Consulta con Include para cargar las llaves foráneas
                var tareas = contexto.TareasDiarias
                    .Include(t => t.Empleado) // Incluye la relación con Empleado
                    .Include(t => t.Empresa) // Incluye la relación con Empresa
                    .Include(t => t.Sucursal) // Incluye la relación con Sucursal
                    .Where(t => t.EmpresaId == GlobalSettings.IdEmpresa) // Filtro adicional
                    .ToList();

                // Limpia la colección antes de agregar los datos
                ListaTareaDiaria.Clear();
                foreach (var tarea in tareas)
                {
                    ListaTareaDiaria.Add(tarea);
                }
            }


        }

        public async void EliminarTareaDiaria(object? parameter)
        {
            if (parameter is int idTareaDiaria) // Asegúrate de que el parámetro sea un entero.
            {
                using (var contexto = new ContextoSMMS())
                {
                    // Busca la tarea diaria en la base de datos por ID.
                    var tareaAEliminar = await contexto.TareasDiarias
                        .FindAsync(idTareaDiaria);

                    if (tareaAEliminar != null)
                    {
                        contexto.TareasDiarias.Remove(tareaAEliminar);
                        await contexto.SaveChangesAsync(); // Guarda los cambios en la base de datos.

                        // Actualiza la ObservableCollection para reflejar los cambios en la vista.
                        var tareaEnLista = ListaTareaDiaria.FirstOrDefault(t => t.TareaDiariaId == idTareaDiaria);
                        if (tareaEnLista != null)
                        {
                            ListaTareaDiaria.Remove(tareaEnLista);
                        }

                        // Muestra el mensaje de éxito.
                        await _window.ShowMessageAsync("Éxito", "Tarea diaria eliminada exitosamente.");
                    }
                    else
                    {
                        // Muestra el mensaje de error si no se encuentra la tarea diaria.
                        await _window.ShowMessageAsync("Error", "Tarea diaria no encontrada.");
                    }
                }
            }
            else
            {
                // Muestra un mensaje de error si el parámetro no es del tipo esperado.
                await _window.ShowMessageAsync("Error", "El parámetro no es del tipo esperado.");
            }
        }
    }
}
