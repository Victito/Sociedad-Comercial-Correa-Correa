using SociedadCorreaCorrea.Data;
using SociedadCorreaCorrea.ViewModels;
using SociedadCorreaCorrea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using iText.Commons.Actions.Contexts;
using SociedadCorreaCorrea.Views;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using MahApps.Metro.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Globalization;
using System.ComponentModel;
using MahApps.Metro.Controls.Dialogs;
using System.Diagnostics;

namespace SociedadCorreaCorrea.ViewsModels
{
    internal class EditarInformacionEmpleadoViewModel : BaseViewModel , INotifyPropertyChanged
    {
        private readonly ContextoSMMS _contexto;
        private MetroWindow _window;

        private Empleado _empleado;
        private TrabajadoresViewModel _trabajadoresViewModel;

        public Empleado Empleado
        {
            get { return _empleado; }
            set { _empleado = value; OnPropertyChanged(nameof(Empleado)); }
        }

        // Nueva propiedad para almacenar la nueva fecha de nacimiento
        public DateTime? _fechaNacimientoNueva;

        public DateTime? FechaNacimientoNueva
        {
            get => _fechaNacimientoNueva;
            set
            {
                if (_fechaNacimientoNueva != value)
                {
                    _fechaNacimientoNueva = value;
                    OnPropertyChanged(nameof(FechaNacimientoNueva));
                }
            }
        }

        // Nueva propiedad para almacenar la nueva fecha de nacimiento
        public DateTime? _fechaContratacionNueva;

        public DateTime? FechaContratacionNueva
        {
            get => _fechaContratacionNueva;
            set
            {
                if (_fechaContratacionNueva != value)
                {
                    _fechaContratacionNueva = value;
                    OnPropertyChanged(nameof(FechaContratacionNueva));
                }
            }
        }


        public ObservableCollection<Turno> ListaTurno { get; set; } = new ObservableCollection<Turno>();
        public ObservableCollection<Puesto> ListaPuesto { get; set; } = new ObservableCollection<Puesto>();
        public ObservableCollection<Sucursal> ListaSucursal { get; set; } = new ObservableCollection<Sucursal>();

        public ICommand GuardarCambiosCommand { get; }


        public EditarInformacionEmpleadoViewModel(Empleado empleado, MetroWindow window)
        {
            _empleado = empleado;
            _window = window;
            _empleado = empleado;
            Debug.WriteLine($"Ventana actual referenciada: {_window.GetType().Name}");
            _contexto = new ContextoSMMS();
            Empleado = empleado;

            GuardarCambiosCommand = new RelayCommand(SalvarCambios);
            InicializarAsync();
        }

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
        private async Task SalvarCambios()
        {
            try
            {
                var contexto = new ContextoSMMS();
                contexto.Empleados.Update(_empleado);
                await contexto.SaveChangesAsync();

                MessageBox.Show("¡Cambios guardados exitosamente!");

                // Cerrar la ventana y regresar a Trabajadores
                Application.Current.Windows.OfType<EditarInformacionEmpleado>().FirstOrDefault()?.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}");
            }
        }

        private async void SalvarCambios(object obj)
        {
            try
            {
                Debug.WriteLine($"Referenciando ventana: {_window.GetType().Name}");

                // Validar que el empleado no sea nulo
                if (_empleado == null)
                {
                    await _window.ShowMessageAsync("Error", "El empleado a guardar no puede ser nulo.");
                    return;
                }

                // Validar el formato del RUT
                if (!ValidarRut(_empleado.RutEmpleado))
                {
                    await _window.ShowMessageAsync("Advertencia", "El formato del RUT no es válido.");
                    return;
                }

                // Asignar la fecha de nacimiento si se seleccionó
                if (FechaNacimientoNueva.HasValue)
                {
                    _empleado.FechaNacimientoEmpleado = DateOnly.FromDateTime(FechaNacimientoNueva.Value);
                }
                else
                {
                }

                // Asignar la fecha de contratación si se seleccionó
                if (FechaContratacionNueva.HasValue)
                {
                    _empleado.FechaContratacionEmpleado = DateOnly.FromDateTime(FechaContratacionNueva.Value);
                }
                else
                {
                }

                // Guardar cambios en la base de datos
                var contexto = new ContextoSMMS();
                contexto.Empleados.Update(_empleado);
                await contexto.SaveChangesAsync();

                await _window.ShowMessageAsync("Éxito", "Cambios guardados exitosamente.");
                var ventanaTrabajadores = new Trabajadores();
                ventanaTrabajadores.Show();
                // Cerrar la ventana actual y regresar a la lista principal de empleados
                _window.Close();


                Debug.WriteLine($"Cambios guardados para el empleado {_empleado.NombreEmpleado}");
            }
            catch (DbUpdateException dbEx)
            {
                Debug.WriteLine($"Error en base de datos: {dbEx.Message}");
                await _window.ShowMessageAsync("Error en Base de Datos", $"No se pudo guardar: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error inesperado al salvar empleado: {ex.Message}");
                await _window.ShowMessageAsync("Error", $"Error inesperado: {ex.Message}");
            }
        }

        private async void InicializarAsync()
        {
            await CargarPuestos();
            await CargarSucursales();
            await CargarTurnos();

        }
        private async Task CargarSucursales()
        {
            try
            {
                var sucursales = await _contexto.Sucursals
                   .Where(p => p.IdEmpresa == GlobalSettings.IdEmpresa)
                   .AsNoTracking()
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
                    .AsNoTracking()
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














        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }


}
