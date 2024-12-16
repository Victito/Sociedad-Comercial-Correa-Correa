using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using prueba.Vista;
using SociedadCorreaCorrea.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SociedadCorreaCorrea.Views
{
    /// <summary>
    /// Lógica de interacción para Trabajadores.xaml
    /// </summary>
    public partial class Trabajadores : MetroWindow
    {
        public Trabajadores()
        {
            InitializeComponent();
            NombreUsuario.Text = UserSession.NombreUsuario;
            this.DataContext = new TrabajadoresViewModel(this); // Establecer el DataContext
        }
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void Ventana_MouseBajo(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMaximizar_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

        private void IngresarFacturas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                var registroFacturas = new RegistroFacturas();
                registroFacturas.Show();
                this.Close();
            }
        }

        private void DatosEstadisticosFacturas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var datosEstadisticos = new GraficosFacturas();
                datosEstadisticos.Show();

                // Cierra la ventana de MainMenu
                this.Close();
            }
        }
        

        private void HistorialFacturas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                var historialFacturas = new HistorialFacturas();
                historialFacturas.Show();
                this.Close();
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Drive_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var Drive = new Drive();
                Drive.Show();

                // Cierra la ventana de MainMenu
                this.Close();
            }
        }
        private void Servicios_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var Servicios = new Servicios();
                Servicios.Show();

                // Cierra la ventana de MainMenu
                this.Close();
            }
        }
        private async void CerrarSesion_Click(object sender, MouseButtonEventArgs e)
        {
            // Aquí, la ventana CerrarSesion se muestra de manera modal
            CerrarSesion ventanaCerrarSesion = new CerrarSesion();
            ventanaCerrarSesion.ShowDialog();

        }
        private void Trabajadores_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var Trabajadores = new Trabajadores();
                Trabajadores.Show();

                // Cierra la ventana de MainMenu
                this.Close();
            }
        }
        private void Inicio_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var MainMenu = new MainMenu();
                MainMenu.Show();

                // Cierra la ventana de MainMenu
                this.Close();
            }
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Verifica si el texto ingresado no es un número
            e.Handled = !IsTextAllowed(e.Text);
        }

        // Método que verifica si el texto es un número
        private bool IsTextAllowed(string text)
        {
            // Permitir solo números
            return int.TryParse(text, out _) || decimal.TryParse(text, out _);
        }

        private bool isValidating = false;

        private void TextBoxRutEmisor_LostFocus(object sender, RoutedEventArgs e)
        {
            if (isValidating) return; // Si ya estamos validando, salir

            isValidating = true; // Marcar que estamos validando

            var textBox = sender as TextBox;
            string input = textBox.Text;

            // Elimina caracteres no numéricos y permite 'K' solo como último carácter
            input = new string(input.Where(c => char.IsDigit(c) || c == 'K').ToArray());

            // Verificar que el último carácter sea válido
            if (input.Length > 0 && !char.IsDigit(input.Last()) && input.Last() != 'K')
            {
                textBox.BorderBrush = new SolidColorBrush(Colors.Red);
                isValidating = false; // Restablecer la bandera
                return; // Salir si el último carácter no es válido
            }

            // Formatear el RUT
            if (input.Length > 0)
            {
                string formattedRut = FormatearRut(input);
                textBox.Text = formattedRut;
            }

            // Validar el RUT
            if (!ValidarRut(input))
            {
                // Cambiar el color del borde a rojo
                textBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                // Cambiar el color del borde a verde si el RUT es válido
                textBox.BorderBrush = new SolidColorBrush(Colors.Green);
            }

            isValidating = false; // Restablecer la bandera
        }

        private string FormatearRut(string rut)
        {
            // Verificar la longitud mínima del RUT
            if (string.IsNullOrWhiteSpace(rut) || rut.Length < 8)
            {
                return rut; // Retorna el RUT original o puedes elegir retornar una cadena vacía
            }

            // Agregar puntos y guión
            if (rut.Length > 1)
            {
                string rutSinDV = rut.Length > 1 ? rut.Substring(0, rut.Length - 1) : rut;
                char dv = rut.Last();

                // Formatear con puntos y guión
                return string.Format("{0}.{1}.{2}-{3}",
                    rutSinDV.Substring(0, rutSinDV.Length - 6),
                    rutSinDV.Substring(rutSinDV.Length - 6, 3),
                    rutSinDV.Substring(rutSinDV.Length - 3, 3),
                    dv);
            }

            return rut; // En caso de que sea un solo dígito
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

        private void TextBoxRutEmisor_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                // Obtener el texto actual
                string currentText = textBox.Text;

                // Filtrar solo 'K', 'k', números del 0 al 9, '-' y '.'
                string modifiedText = new string(currentText
                    .Where(c => c == 'K' || c == 'k' || char.IsDigit(c) || c == '-' || c == '.')
                    .Select(c => char.ToUpper(c)).ToArray());

                // Actualizar el TextBox solo si es necesario
                if (currentText != modifiedText)
                {
                    textBox.Text = modifiedText;

                    // Mover el cursor al final del texto
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

    }
}
