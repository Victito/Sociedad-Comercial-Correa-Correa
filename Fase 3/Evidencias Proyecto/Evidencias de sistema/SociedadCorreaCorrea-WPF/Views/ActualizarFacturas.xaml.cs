using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using prueba.Vista;
using SociedadCorreaCorrea.Data;
using SociedadCorreaCorrea.Models;
using SociedadCorreaCorrea.ViewModels;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace SociedadCorreaCorrea.Views
{
    public partial class ActualizarFacturas : MetroWindow
    {
        private readonly ContextoSMMS _contexto;

        private InformacionFacturas _facturaSeleccionada;

        public ActualizarFacturas(InformacionFacturas facturaSeleccionada)
        {
            // Inicializar el contexto antes de usarlo
            _contexto = new ContextoSMMS();
            InitializeComponent();
            _facturaSeleccionada = facturaSeleccionada;
            NombreUsuario.Text = UserSession.NombreUsuario;

            // Rellenar los campos con la información de la factura seleccionada
            CargarDatosFactura();
            CargarSucursales();
        }

        private void CargarSucursales()
        {
            // Cargar sucursales activas y pertenecientes a la empresa actual
            var sucursales = _contexto.Sucursals
                                      .Where(s => s.IdEmpresa == GlobalSettings.IdEmpresa)
                                      .ToList();

            // Asignar la lista al ItemsSource del ComboBox
            cmbSucursales.ItemsSource = sucursales;

            // Configurar DisplayMemberPath y SelectedValuePath
            cmbSucursales.DisplayMemberPath = "NombreSucursal"; // Propiedad que quieres mostrar
            cmbSucursales.SelectedValuePath = "IdSucursal"; // Propiedad única del objeto

            // Seleccionar el valor correspondiente al IdSucursal de _facturaSeleccionada
            if (_facturaSeleccionada != null && _facturaSeleccionada.Factura != null)
            {
                cmbSucursales.SelectedValue = _facturaSeleccionada.Factura.IdSucursal;
            }
        }


        private void CargarDatosFactura()
        {
            // Rellenar los campos con los datos de la factura
            txtId.Text = _facturaSeleccionada.Factura.IdFactura.ToString();
            txtCliente.Text = _facturaSeleccionada.Factura.RazonSocial;
            txtTotal.Text = _facturaSeleccionada.Factura.Total.ToString();
            txtNumeroFactura.Text = _facturaSeleccionada.Factura.NumeroFactura.ToString();
            txtRutVendedor.Text = _facturaSeleccionada.Factura.RutVendedor;
            txtGiroVendedor.Text = _facturaSeleccionada.Factura.GiroVendedor;
            txtRazonSocialVendedor.Text = _facturaSeleccionada.Factura.RazonSocialVendedor;
            txtRazonSocial.Text = _facturaSeleccionada.Factura.RazonSocial;
            txtRutCliente.Text = _facturaSeleccionada.Factura.RutEmisor;
            txtGiroCliente.Text = _facturaSeleccionada.Factura.Giro;
            txtDireccion.Text = _facturaSeleccionada.Factura.Direccion;
            txtComuna.Text = _facturaSeleccionada.Factura.Comuna;
            txtCiudad.Text = _facturaSeleccionada.Factura.Ciudad;
            txtEntregarEn.Text = _facturaSeleccionada.Factura.EntregarEn;
            // Asignar Fecha Actual Emisión (convertir a cadena)
            txtFechaActualEmision.Text = _facturaSeleccionada.Factura.FechaEmision?.ToString() ?? "N/A";
            // Asignar Fecha Actual Vencimiento (convertir a cadena)
            txtFechaActualVencimiento.Text = _facturaSeleccionada.Factura.FechaVencimiento?.ToString() ?? "N/A";
            txtCobrador.Text = _facturaSeleccionada.Factura.Cobrador;
            txtNotaVenta.Text = _facturaSeleccionada.Factura.NotaVenta.ToString();
            txtOrdenCompra.Text = _facturaSeleccionada.Factura.OrdenCompra;
            txtCondiciones.Text = _facturaSeleccionada.Factura.Condiciones;
            txtGuiaDespacho.Text = _facturaSeleccionada.Factura.GuiaDespacho;
            txtPrecioUnitario.Text = _facturaSeleccionada.Factura.PrecioUnitario.ToString();
            txtCantidad.Text = _facturaSeleccionada.Factura.Cantidad.ToString();
            txtEstado.Text = _facturaSeleccionada.Factura.Estado;
        }

        private async void GuardarCambios_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Crear una nueva instancia de InformacionFacturas con los datos actualizados
                var facturaActualizada = new InformacionFacturas
                {
                    Factura = new Factura
                    {
                        IdFactura = int.Parse(txtId.Text),
                        RazonSocial = txtCliente.Text,
                        Total = decimal.Parse(txtTotal.Text),
                        IdSucursal = (int)cmbSucursales.SelectedValue,
                        NumeroFactura = int.Parse(txtNumeroFactura.Text),
                        RutVendedor = txtRutVendedor.Text,
                        GiroVendedor = txtGiroVendedor.Text,
                        RazonSocialVendedor = txtRazonSocialVendedor.Text,
                        RutEmisor = txtRutCliente.Text,
                        Giro = txtGiroCliente.Text,
                        Direccion = txtDireccion.Text,
                        Comuna = txtComuna.Text,
                        Ciudad = txtCiudad.Text,
                        EntregarEn = txtEntregarEn.Text,
                        FechaEmision = dpFechaEmision.SelectedDate.HasValue
                            ? DateOnly.FromDateTime(dpFechaEmision.SelectedDate.Value)
                            : (DateOnly?)null,
                        FechaVencimiento = dpFechaVencimiento.SelectedDate.HasValue
                            ? DateOnly.FromDateTime(dpFechaVencimiento.SelectedDate.Value)
                            : (DateOnly?)null,
                        Cobrador = txtCobrador.Text,
                        NotaVenta = int.Parse(txtNotaVenta.Text),
                        OrdenCompra = txtOrdenCompra.Text,
                        Condiciones = txtCondiciones.Text,
                        GuiaDespacho = txtGuiaDespacho.Text,
                        PrecioUnitario = decimal.Parse(txtPrecioUnitario.Text),
                        Cantidad = int.Parse(txtCantidad.Text),
                        Estado = txtEstado.Text
                    }
                };

                // Crear una instancia del ViewModel para manejar la actualización
                var viewModel = new ActualizarFacturasViewModel(new ContextoSMMS(),this);

                // Llamar al método asincrónico para actualizar la factura
                await viewModel.ActualizarFacturaAsync(facturaActualizada);

            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Error en los datos ingresados: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al actualizar la factura: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
        private void HistorialFacturas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var historialFacturas = new HistorialFacturas();
                historialFacturas.Show();

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
