using System.Security.Cryptography;
using System.Text;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SociedadCorreaCorrea.Data;
using SociedadCorreaCorrea.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SociedadCorreaCorrea.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace SociedadCorreaCorrea.ViewsModels
{
    internal class LoginVistaViewModel : INotifyPropertyChanged
    {
        private string _nombre = string.Empty;
        private string _clave = string.Empty;

        // Inyección del diálogo
        private readonly MetroWindow _window;

        public string Nombre
        {
            get => _nombre;
            set
            {
                if (_nombre != value)
                {
                    _nombre = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Clave
        {
            get => _clave;
            set
            {
                if (_clave != value)
                {
                    _clave = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand IniciarSesionCommand { get; }

        public LoginVistaViewModel(MetroWindow window)
        {
            _window = window;
            IniciarSesionCommand = new RelayCommand(IniciarSesion);
        }

        // Método para encriptar la contraseña
        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Convertir la contraseña a un arreglo de bytes y realizar el hash
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convertir el arreglo de bytes a una cadena hexadecimal
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private async void IniciarSesion()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Clave))
                {
                    await _window.ShowMessageAsync("Error", "Todos los campos son obligatorios.");
                    return;
                }

                using var context = new ContextoSMMS();

                // Encriptar la contraseña ingresada
                var hashedPassword = HashPassword(Clave);

                // Comparar la contraseña encriptada con la almacenada en la base de datos
                var usuario = context.Usuarios
                    .Where(u => u.NombreUsuario == Nombre
                                 && u.Clave == hashedPassword
                                 && u.IdEmpresa == GlobalSettings.IdEmpresa) // Usando la variable global
                    .FirstOrDefault();

                if (usuario != null)
                {
                    // Almacena la información en UserSession
                    UserSession.NombreUsuario = usuario.NombreUsuario;
                    UserSession.Rol = usuario.Rol; // Asegúrate de que 'Rol' es una propiedad de tu modelo de usuario
                    await _window.ShowMessageAsync("Bienvenido", $"¡Bienvenido de nuevo {usuario.NombreUsuario}!");
                    // Aquí deberías abrir la ventana MenuPrincipal
                    // var menuPrincipal = new MenuPrincipal();
                    // menuPrincipal.Show();

                    // Cerrar la ventana de inicio de sesión si se desea
                    // Application.Current.MainWindow.Close();
                }
                else
                {
                    await _window.ShowMessageAsync("Error", "Nombre de usuario o clave incorrectos.");
                }
            }
            catch (Exception ex)
            {
                await _window.ShowMessageAsync("Error", $"Ocurrió un error: {ex.Message}");
            }
        }

        // Implementación de INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
