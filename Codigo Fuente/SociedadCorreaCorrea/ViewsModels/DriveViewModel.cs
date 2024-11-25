using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SociedadCorreaCorrea.ViewModels
{
    public class DriveViewModel : BaseViewModel
    {
        private readonly MetroWindow _window;
        private DriveService servicioDrive;
        private const string NombreArchivoCredenciales = "Resources/credentiales.json"; // Ruta a las credenciales de Google
        private const string NombreAplicacion = "SociedadCorreaCorrea";
        private readonly string[] Scopes = { DriveService.Scope.Drive }; // Permiso completo de Google Drive

        // Propiedades de comando
        public ICommand AutenticarCommand { get; private set; }

        // Para WebView2
        public WebView2 WebViewGoogleDrive { get; set; }

        // Constructor
        public DriveViewModel(MetroWindow window)
        {
            _window = window;
            AutenticarCommand = new RelayCommand(async () => await AutenticarAsync());
        }

        // Método para autenticar al usuario con Google
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

                // Abrir el archivo de credenciales
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
                    }

                    // Inicializar el servicio de Google Drive
                    InicializarServicioDrive(credenciales);
                }

                // Navegar a Google Drive una vez autenticado
                _window.Dispatcher.Invoke(() =>
                {
                    // Validar que WebView2 esté correctamente cargado
                    if (WebViewGoogleDrive != null)
                    {
                        WebViewGoogleDrive.CoreWebView2InitializationCompleted += CoreWebView2InitializationCompleted;
                        WebViewGoogleDrive.Source = new Uri("https://drive.google.com");
                    }
                    else
                    {
                        MostrarMensaje("Error", "No se ha encontrado el control WebView2.");
                    }
                });
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", $"No se pudo autenticar: {ex.Message}");
            }
        }

        // Inicializa el servicio de Google Drive con las credenciales
        private void InicializarServicioDrive(UserCredential credenciales)
        {
            servicioDrive = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credenciales,
                ApplicationName = NombreAplicacion
            });
        }

        // Método para mostrar mensajes en el UI
        private void MostrarMensaje(string titulo, string mensaje)
        {
            _window.Dispatcher.Invoke(() =>
            {
                // Mostrar un mensaje en la ventana (asegúrate de tener el control de MahApps Metro)
                _window.ShowMessageAsync(titulo, mensaje);
            });
        }

        // Event handler para la finalización de la inicialización de WebView2
        private void CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                MostrarMensaje("Éxito", "WebView2 se ha inicializado correctamente.");
            }
            else
            {
                MostrarMensaje("Error", "No se pudo inicializar WebView2.");
            }
        }
    }
}
