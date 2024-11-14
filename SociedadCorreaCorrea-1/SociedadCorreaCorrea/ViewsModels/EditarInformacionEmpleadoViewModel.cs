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

namespace SociedadCorreaCorrea.ViewsModels
{
    internal class EditarInformacionEmpleadoViewModel : BaseViewModel , INotifyPropertyChanged
    {
        private readonly ContextoSMMS _contexto;

        public Empleado Empleado { get; set; }

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

        private readonly MetroWindow _window;

        public EditarInformacionEmpleadoViewModel(Empleado empleado, MetroWindow window)
        {
            _contexto = new ContextoSMMS();
            _window = window;
            Empleado = empleado;

            GuardarCambiosCommand = new RelayCommand(SalvarCambios);
            InicializarAsync();
        }

        private void SalvarCambios(object obj)
        {
            try
            {
                // Verificar que el empleado no sea nulo antes de intentar actualizar
                if (Empleado == null)
                {
                    MessageBox.Show("El empleado a guardar no puede ser nulo.", "Error de Operación", MessageBoxButton.OK, MessageBoxImage.Error);
                    return; // Salir del método si el empleado es nulo
                }
                MessageBox.Show($"La fecha de nacimiento del empleado es: {Empleado.FechaNacimientoEmpleado}", "Información del Empleado", MessageBoxButton.OK, MessageBoxImage.Information);
                // Capturar la fecha de nacimiento desde el DatePicker
                var nuevaFechaNacimiento = FechaNacimientoNueva;

                // Verifica si nuevaFechaNacimiento tiene un valor
                if (nuevaFechaNacimiento.HasValue)
                {
                    // Muestra la fecha que se está guardando
                    MessageBox.Show($"La nueva fecha de nacimiento es: {nuevaFechaNacimiento.Value.ToString("d")}", "Fecha de Nacimiento", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Asignar la nueva fecha de nacimiento al empleado
                    Empleado.FechaNacimientoEmpleado = DateOnly.FromDateTime(nuevaFechaNacimiento.Value);
                }
                else
                {
                    // Si no hay valor, puedes mostrar un mensaje o simplemente no hacer nada
                    MessageBox.Show("No se ha seleccionado una nueva fecha de nacimiento.", "Información", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                var nuevaFechaContratacion = FechaContratacionNueva;
                // Verifica si nuevaFechaNacimiento tiene un valor
                if (nuevaFechaContratacion.HasValue)
                {
                    // Muestra la fecha que se está guardando
                    MessageBox.Show($"La nueva fecha de contratacion es es: {nuevaFechaContratacion.Value.ToString("d")}", "Fecha de Nacimiento", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Asignar la nueva fecha de nacimiento al empleado
                    Empleado.FechaContratacionEmpleado = DateOnly.FromDateTime(nuevaFechaContratacion.Value);
                }
                else
                {
                    // Si no hay valor, puedes mostrar un mensaje o simplemente no hacer nada
                    MessageBox.Show("No se ha seleccionado una nueva fecha de nacimiento.", "Información", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                // Guardar cambios en la base de datos
                _contexto.Empleados.Update(Empleado);
                _contexto.SaveChanges();

                // Mensaje de éxito (opcional)
                MessageBox.Show("Cambios guardados exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Manejo de excepciones de concurrencia
                MessageBox.Show("Error de concurrencia: " + ex.Message, "Error de Concurrencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (DbUpdateException ex)
            {
                // Manejo de excepciones al intentar guardar los cambios
                MessageBox.Show("Error al actualizar la base de datos: " + ex.Message, "Error de Actualización", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException ex)
            {
                // Manejo de errores de operación inválida
                MessageBox.Show("Error de operación: " + ex.Message, "Error de Operación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otra excepción no controlada
                MessageBox.Show("Se produjo un error inesperado: " + ex.Message, "Error Inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void InicializarAsync()
        {
            await CargarPuestos();
            await CargarSucursales();
            CargarTurnos();

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
