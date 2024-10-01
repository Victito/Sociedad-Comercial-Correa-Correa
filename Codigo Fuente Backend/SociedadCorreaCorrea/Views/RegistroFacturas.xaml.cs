using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls; // Para usar controles de WPF como Button, TextBox, etc.
using MahApps.Metro.Controls;
using SociedadCorreaCorrea.Models;
using SociedadCorreaCorrea.ViewModels;
using MahApps.Metro.Controls.Dialogs; // Asegúrate de tener esto en tu código
using System.ComponentModel;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Interop;



namespace SociedadCorreaCorrea.Views
{
    public partial class RegistroFacturas : MetroWindow
    {
        // Lista para almacenar los productos seleccionados
        private List<Producto> productos;

        public RegistroFacturas()
        {
            InitializeComponent();
            // Configurar el DataContext con el ViewModel correspondiente
            DataContext = new RegistroFacturaViewModel(this);
        }

        #region Eventos de UI

        /// <summary>
        /// Evento para agregar un nuevo producto a la factura
        /// </summary>
        private async void AgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            // Asegurarse de que el DataContext esté correctamente asignado
            var viewModel = DataContext as RegistroFacturaViewModel;
            if (viewModel == null)
            {
                MessageBox.Show("El ViewModel no está inicializado.");
                return;
            }
            if (TextBoxCodigoProducto.Text.Length > 50)
            {
                await this.ShowMessageAsync("Error", "El Código Producto no puede exceder los 50 caracteres.");
                return;
            }

            if (TextBoxDescripcion.Text.Length > 255)
            {
                await this.ShowMessageAsync("Error", "La Descripción no puede exceder los 255 caracteres.");
                return;
            }

            if (TextBoxNSerie.Text.Length > 50)
            {
                await this.ShowMessageAsync("Error", "El Número de Serie no puede exceder los 50 caracteres.");
                return;
            }

            // Lista para almacenar mensajes de error
            List<string> mensajesErrores = new List<string>();

            // Validaciones de campos de entrada
            if (string.IsNullOrWhiteSpace(TextBoxCodigoProducto.Text))
                mensajesErrores.Add("El campo 'Código Producto' es obligatorio.");

            if (string.IsNullOrWhiteSpace(TextBoxDescripcion.Text))
                mensajesErrores.Add("El campo 'Descripción' es obligatorio.");

            if (string.IsNullOrWhiteSpace(TextBoxNSerie.Text))
                mensajesErrores.Add("El campo 'Número de Serie' es obligatorio.");

            // Validar Cantidad
            if (!int.TryParse(TextBoxCantidad.Text, out int cantidad) || cantidad <= 0)
                mensajesErrores.Add("El campo 'Cantidad' debe ser un número entero mayor a 0.");

            // Validar Precio Unitario
            if (!decimal.TryParse(TextBoxPrecioUnitario.Text, out decimal precioUnitario) || precioUnitario <= 0)
                mensajesErrores.Add("El campo 'Precio Unitario' debe ser un número decimal mayor a 0.");

            // Validar Descuento
            if (!int.TryParse(TextBoxDescuento.Text, out int descuento) ||
                descuento < 0 || descuento > 100)
                mensajesErrores.Add("El campo 'Descuento' debe ser un número entre 0 y 100.");

            // Verificar si hay mensajes de error
            if (mensajesErrores.Count > 0)
            {
                // Combinar los mensajes en uno solo
                string mensajeErrorFinal = string.Join("\n", mensajesErrores);
                await this.ShowMessageAsync("Error", mensajeErrorFinal);
                return;
            }


            // Crear y configurar el nuevo producto con los datos proporcionados
            var nuevoProducto = new Producto
            {
                CodigoProducto = TextBoxCodigoProducto.Text,
                Descripcion = TextBoxDescripcion.Text,
                NSerie = TextBoxNSerie.Text,
                Cantidad = cantidad,
                PrecioUnitario = precioUnitario,
                Descuento = descuento, // Asignar descuento
                Total = (cantidad * precioUnitario) - ((descuento / 100m) * (cantidad * precioUnitario)) // Aplicar el descuento
            };

            // Llamar al método en el ViewModel para agregar el producto a la lista
            viewModel.AgregarProducto(nuevoProducto.CodigoProducto, nuevoProducto.Descripcion, nuevoProducto.NSerie, nuevoProducto.Cantidad, nuevoProducto.PrecioUnitario, nuevoProducto.Descuento, nuevoProducto.Total);

            // Limpiar los campos de entrada después de agregar el producto
            LimpiarCamposProducto();

             // Calcular el total de todos los productos y actualizar el TextBlock
            decimal totalGeneral = viewModel.Productos.Sum(p => p.Total);
            TextBlockTotal.Text = $"Total de Productos: {totalGeneral:C}"; // Formato de moneda
}
        private void EliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el botón que fue clickeado
            var button = sender as Button;
            if (button != null)
            {
                // Obtener el producto seleccionado
                var producto = button.DataContext as Producto;
                if (producto != null)
                {
                    // Asegúrate de que el DataContext esté correctamente asignado
                    var viewModel = DataContext as RegistroFacturaViewModel;
                    if (viewModel != null)
                    {
                        // Llamar al método en el ViewModel para eliminar el producto de la lista
                        viewModel.EliminarProducto(producto);
                        decimal totalGeneral = viewModel.Productos.Sum(p => p.Total);
                        TextBlockTotal.Text = $"Total de Productos: {totalGeneral:C}"; // Formato de moneda
                    }
                }
            }
        }

        /// <summary>
        /// Evento para cargar un archivo de firma digital en formato PDF
        /// </summary>
        private void CargarFirma_Click(object sender, RoutedEventArgs e)
        {
            // Crear un cuadro de diálogo para seleccionar un archivo PDF
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Archivos PDF (*.pdf)|*.pdf", // Filtro para solo mostrar archivos PDF
                Title = "Seleccionar Archivo de Firma"
            };

            // Verificar si se seleccionó un archivo correctamente
            if (openFileDialog.ShowDialog() == true)
            {
                // Almacenar la ruta del archivo en el ViewModel (reemplaza con tu ViewModel si es necesario)
                // var viewModel = this.DataContext as RegistroFacturaViewModel; 
                // viewModel.Firma = openFileDialog.FileName;

                // Mostrar el nombre del archivo en un TextBox si es necesario
                // Si el binding ya maneja esto, no es necesario hacerlo manualmente
            }
        }

        #endregion

        #region Métodos de ayuda

        /// <summary>
        /// Método auxiliar para limpiar los campos de entrada relacionados con los productos
        /// </summary>
        private void LimpiarCamposProducto()
        {
            TextBoxCodigoProducto.Clear();
            TextBoxDescripcion.Clear();
            TextBoxNSerie.Clear();
            TextBoxCantidad.Clear();
            TextBoxDescuento.Clear();
            TextBoxPrecioUnitario.Clear();
        }

        #endregion

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
        
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWind, int wMsg, int wParam, int lParam);
        private void Ventana_MouseBajo(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }
        private void panelControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;


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
            else this.WindowState = WindowState.Normal;
        }









    }
}
