using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;  // Asegúrate de que usas la librería de MahApps.Metro si estás utilizando `MetroWindow`.
using MahApps.Metro.Controls.Dialogs;
using prueba.Vista;


namespace SociedadCorreaCorrea.Views
{
    /// <summary>
    /// Interaction logic for CerrarSesion.xaml
    /// </summary>
    public partial class CerrarSesion : MetroWindow
    {
        public CerrarSesion()
        {
            MostrarConfirmacionCierreSesion();
            InitializeComponent();

        }


        private async void MostrarConfirmacionCierreSesion()
        {
            // Usamos `this` porque estamos dentro de un MetroWindow
            var result = await this.ShowMessageAsync(
                "Confirmar cierre de sesión",       // Título del mensaje
                "¿Estás seguro que quieres cerrar sesión?", // Contenido del mensaje
                MessageDialogStyle.AffirmativeAndNegative // Estilo con botones "Sí" y "No"
            );

            // Evaluar la respuesta del usuario
            if (result == MessageDialogResult.Affirmative)
            {
                // Si el usuario selecciona "Sí", cerramos todas las ventanas abiertas
                bool loginVistaAbierta = false;  // Variable para controlar la creación de LoginVista

                foreach (Window window in Application.Current.Windows)
                {
                    window.Close();  // Cierra cada ventana abierta

                    // Solo creamos y mostramos LoginVista una vez después de cerrar las ventanas
                    if (!loginVistaAbierta)
                    {
                        LoginVista loginVista = new LoginVista();
                        loginVista.Show();
                        loginVistaAbierta = true; // Marcamos que LoginVista ya ha sido abierta
                    }
                }
            }

            // Cierra la ventana actual después de completar el proceso
            this.Close();
        }

    }


}
