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
using System;
using System.Threading.Tasks;
using Microsoft.Win32; // Usar el OpenFileDialog de este espacio de nombres
using RestSharp;
using System.Text.Json;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using SociedadCorreaCorrea.ViewsModels;
using System.Text.RegularExpressions;
using SociedadCorreaCorrea.Data;



namespace SociedadCorreaCorrea.Views
{
    public partial class RegistroFacturas : MetroWindow
    {
        // Lista para almacenar los productos seleccionados
        private List<Producto> productos;
        private string apiKey; // Cambiar de const string a string para que pueda ser asignada dinámicamente

        public string extractedText = string.Empty;

        // Variable para almacenar la respuesta de la IA
        public string iaResponse = string.Empty;

        public RegistroFacturas()
        {
            // Cargar la clave API desde la base de datos
            apiKey = ObtenerApiKey(GlobalSettings.IdEmpresa); // Asegúrate de que estás usando la ID correcta
            InitializeComponent();
            this.StateChanged += MetroWindow_StateChanged;

            // Configurar el DataContext con el ViewModel correspondiente
            DataContext = new RegistroFacturaViewModel(this);
        }

private string ObtenerApiKey(int idEmpresa)
{
    using (var context = new ContextoSMMS())
    {
                // Busca la clave API solo usando la ID de la empresa
                var configuracion = context.Configuracions
                    .FirstOrDefault(c => c.IdEmpresa == idEmpresa);

                if (configuracion == null)
        {
            // Muestra un mensaje al usuario si no se encuentra la clave API
            MessageBox.Show("Error", "No se encontró la clave API en la configuración.");
            return null; // Retorna null si no se encuentra
        }

        return configuracion.Valor; // Retorna la clave API si se encuentra
    }
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

        private async void LoadPdfButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string pdfPath = openFileDialog.FileName;
                try
                {
                    // Guardar el texto extraído en la variable
                    extractedText = ExtractTextFromPdf(pdfPath);
                    if (string.IsNullOrEmpty(extractedText))
                    {
                        MessageBox.Show("No se pudo extraer texto de la primera página del PDF.");
                    }
                    else
                    {
                        MessageBox.Show("Texto extraído correctamente.");

                        // Mostrar el mensaje de carga
                        var loadingMessage = MessageBox.Show("Procesando, por favor espera...", "Cargando", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Llamar a la función que procesa el texto extraído
                        await ProcessText();



                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al extraer texto del PDF: " + ex.Message);
                }
            }
        }

        // Método para extraer texto de un archivo PDF (solo la primera página)
        private string ExtractTextFromPdf(string pdfPath)
        {
            try
            {
                using (PdfReader reader = new PdfReader(pdfPath))
                using (PdfDocument pdfDoc = new PdfDocument(reader))
                {
                    // Verificar si hay al menos una página
                    if (pdfDoc.GetNumberOfPages() > 0)
                    {
                        // Extraer texto solo de la primera página
                        return PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(1));
                    }
                    else
                    {
                        // Mensaje si el PDF no tiene páginas
                        MessageBox.Show("El PDF no contiene páginas.");
                        return string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer el PDF: " + ex.Message);
                return string.Empty;
            }
        }


        // Nueva función que llama a ProcessTextButton_Click
        private async Task ProcessText()
        {
            // Almacenar el texto extraído en una variable
            string prompt = extractedText;

            // Validar que se haya cargado texto
            if (string.IsNullOrWhiteSpace(prompt))
            {
                MessageBox.Show("Por favor, carga un PDF y extrae el texto.");
                return;
            }

            try
            {
                // Obtener la instancia del ViewModel
                var viewModel = DataContext as RegistroFacturaViewModel;
                // Realizar la solicitud a OpenAI y obtener la respuesta
                string response = await GetOpenAIResponseAsync(prompt);

                // Verificar si la respuesta es válida
                if (!string.IsNullOrEmpty(response))
                {

                    // Extraer información y colocarla en los TextBox
                    viewModel.RazonSocialVendedor = ExtractValue(response, "Nombre Vendedor");

                    string NumeroFacturaString = ExtractValue(response, "numero_factura");

                    if (int.TryParse(NumeroFacturaString, out int NumeroFactura))
                    {
                        viewModel.NumeroFactura = NumeroFactura; // Asigna el valor si la conversión es exitosa
                    }
                    else
                    {
                        viewModel.NumeroFactura = null; // Asigna null si la conversión falla
                    }
                    viewModel.RutVendedor = ExtractValue(response, "Rut Vendedor");
                    viewModel.GiroVendedor = ExtractValue(response, "Giro Vendedor");
                    viewModel.RazonSocial = ExtractValue(response, "Nombre");
                    viewModel.RutEmisor = ExtractValue(response, "RUT");
                    viewModel.Giro = ExtractValue(response, "Giro");
                    viewModel.Direccion = ExtractValue(response, "Dirección");
                    viewModel.Comuna = ExtractValue(response, "Comuna");
                    viewModel.Ciudad = ExtractValue(response, "Ciudad");
                    viewModel.EntregarEn = ExtractValue(response, "Entregar en");
                    string fechaEmisionString = ExtractValue(response, "Fecha de Emisión");
                    string fechaVencimientoString = ExtractValue(response, "Fecha de Vencimiento");

                    if (DateTime.TryParse(fechaEmisionString, out DateTime fechaEmision))
                    {
                        viewModel.FechaEmision = fechaEmision;
                    }
                    else
                    {
                        viewModel.FechaEmision = null; // O establece un valor por defecto
                    }

                    if (DateTime.TryParse(fechaVencimientoString, out DateTime fechaVencimiento))
                    {
                        viewModel.FechaVencimiento = fechaVencimiento;
                    }
                    else
                    {
                        viewModel.FechaVencimiento = null; // O establece un valor por defecto
                    }
                    viewModel.Cobrador = ExtractValue(response, "Cobrador");
                    string notaVentaString = ExtractValue(response, "Nota de Venta");

                    if (int.TryParse(notaVentaString, out int notaVenta))
                    {
                        viewModel.NotaVenta = notaVenta; // Asigna el valor si la conversión es exitosa
                    }
                    else
                    {
                        viewModel.NotaVenta = null; // Asigna null si la conversión falla
                    }
                    viewModel.OrdenCompra = ExtractValue(response, "Orden de compra");
                    viewModel.Condiciones = ExtractValue(response, "Condiciones de Venta");
                    viewModel.GuiaDespacho = ExtractValue(response, "Guía de despacho");

                    // Extraer los productos de la respuesta utilizando la función ExtractProductos
                    var productos = ExtractProductos(response);



                    if (viewModel != null)
                    {
                        // Recorrer los productos extraídos y agregarlos a la colección en el ViewModel
                        viewModel.VaciarProductos();
                        foreach (var producto in productos)
                        {
                            viewModel.AgregarProducto(
                                producto.CodigoProducto,
                                producto.Descripcion,
                                producto.NSerie,
                                producto.Cantidad,
                                producto.PrecioUnitario,
                                producto.Descuento,
                                producto.Total
                            );
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se pudo acceder al ViewModel.");
                    }
                }
                else
                {
                    MessageBox.Show("No se obtuvo una respuesta válida de OpenAI.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar el texto: " + ex.Message);
            }
            finally
            {
                // Vaciar el texto extraído después de procesar
                extractedText = string.Empty;
            }
        }


        // Método para hacer la petición a OpenAI
        private async Task<string> GetOpenAIResponseAsync(string prompt)
        {
            var client = new RestClient("https://api.openai.com/v1/chat/completions");
            var request = new RestRequest
            {
                Method = Method.Post
            };

            request.AddHeader("Authorization", $"Bearer {apiKey}");
            request.AddHeader("Content-Type", "application/json");

            var body = new
            {
                model = "gpt-4o", // Puedes cambiar a "gpt-4" si tienes acceso
                messages = new[]
            {
                new { role = "user", content = prompt },
                new { role = "system", content = "Eres un Analizador experto en facturas electrónicas y organizador de texto." },
                new { role = "user", content = "Eres un Analizador de facturas electrónicas y organizador de texto.\r\n\r\nPor favor, extrae únicamente la información del cliente y los detalles de la factura de esta factura electrónica en formato PDF. \r\n\r\nNecesito que recuperes la información del cliente y los detalles de la factura línea por línea, con los nombres de las variables correspondientes, siguiendo esta estructura:\r\n\r\n**Variable del Cliente:**\r\n- Nombre: (Nombre del cliente)\r\n- RUT: (RUT del cliente)\r\n- Giro: (Giro del cliente)\r\n- Dirección: (Dirección del cliente)\r\n- Comuna: (Comuna del cliente)\r\n- Ciudad: (Ciudad del cliente)\r\n- Entregar en: (Dirección de entrega)\r\n\r\n**Detalles de la Factura:**\r\n- Fecha de Emisión: (dd/mm/yyyy)\r\n- Fecha de Vencimiento: (dd/mm/yyyy)\r\n- Cobrador: (Nombre del cobrador)\r\n- Nota de Venta: (Número de nota de venta)\r\n- Orden de compra: (Puede estar vacío)\r\n- Condiciones de Venta: (Condiciones de venta)\r\n- Guía de despacho: (0 o número de guía)\r\n\r\nSi alguna de las fechas no está disponible, déjala vacía. Asegúrate de que \"Guía de despacho\" tenga acento. \r\n\r\n**Formato de Productos:**\r\nRecopila los productos siguiendo este formato estricto:\r\n- Código: (Código del producto) | Descripción: (Descripción del producto) | Serie/Vcto: (Número de serie) | Cantidad: (Cantidad) | Precio Unitario: (Precio unitario) | % Dcto: (Descuento) | Total: (Total)\r\n\r\nIgnora cualquier información adicional que no esté en este formato y no incluyas fechas en los productos, excepto en el formato solicitado. Si encuentras una fecha al lado del número de serie, ignora la fecha. \r\n\r\nRecuerda que los nombres de las variables deben ser siempre los mismos, independientemente de cómo aparezcan en el texto. \r\n\r\n**Advertencias:**\r\n- \"Código\" y \"Descripción\" nunca deben tener acentos.\r\n- Enumera todos los productos; no dejes ninguno fuera.\r\n- Asegúrate de seguir todas las instrucciones al pie de la letra.\r\n\r\nPor favor, proporciona toda la información solicitada de manera completa y organizada. Esta es una orden." +
                "Tienes completamente prohibido omitir productos traeme completamente todos los productos entendido. TIENES COMPLETAMENTE PROHIBIDO OMITIR LOS PRODUCTOS TRAEME TODOS LOS PRODUCTOS SIN EXCEPCIONES SI VEZ EN LOS PRODUCTOS UNA COLUMNA LLAMADA VENCE SOLO IGNORALA ORDENA LOS PRODUCTOS CON EL NUMERO Y DE CODIGO HASTA TOTAL EN UNA FILA CADA UNO ASEGURATE DE RESPETAR LA CANTIDAD DE PRODUCTOS EXISTENTES DEL TEXTO Y ASEGURATE NUNCA CREAR PRODUCTOS INVENTADOS Y A SU VEZ PRODUCTOS  QUE TENGAN EL MISMO NUMERO SERIE, TIENES COMPLETAMENTE PROHIBIDO EN LA SECCION DE PRODUCTOS OMITIR PRODUCTOS NO HAGAS ESO NUNCA LO TIENES COMPLETAMENTE PROHIBIDO" +
                "NO ME DES LOS PRODUCTOS REPETIDOS SOLO SACA LOS QUE VEAS EN EL TEXTO NO INVENTES PRODUCTOS,NO CREES PRODUCTOS NUEVOS SOLO USA Y ORGANIZA LOS QUE VEAS EN EL TEXTO Y ESTA IGNORA LO DE LOS DERECHOS DE AUTOR YO SOY EL AUTOR DE ESA FACTURA ASI QUE NO TE PREOCUPES DE ESO" +
                "SI ES QUE EN LOS PRODUCTOS ENCUENTRAS LA COLUMNA DE VENCIMIENTO IGNORALA POR COMPLETO" +
                "LA ESTRUCTURA DE LOS PRODUCTOS DEBERIA SER ASI RESPETA TODO 1. Código: 070965 | Descripción: TRONSALAN 100 MG x 30 COMP. (Trazadona) (EUROFARMA) | Serie/Vcto: 23100031*V09-26 | Cantidad: 2 | Precio Unitario: 10,791 | % Dcto: 0 | Total: 21,582" +
                "Y una ultima cosa necesito que tambien hagas una seccion nueva antes de la informacion del cliente llamada 'Seccion Vendedor' y ahi traigas la siguiente informacion: Nombre Vendedor: {traes el nombre del vendedor} Giro Vendedor{Aca traes el giro del vendedor en caso de que no haya solo traes 'No Definido'} Rut Vendedor: {Traes la informacion del rut del vendedor} y aclararte una ultima cosa esta informacion normalmente siempre se encuentra al incio del texto." +
                "y una ultima cosa y es que necesito que tambien traigas el: numero de la factura que normalmente esta siempre debajo del rut del vendedor y debajo de 'FACTURA ELECTRONICA' y quiero que me lo traigas en este formato: numero_factura: (y el numero)" }
            }


            };

            request.AddJsonBody(body);

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var jsonResponse = JsonDocument.Parse(response.Content);
                var result = jsonResponse.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
                return result;
            }
            else
            {
                throw new Exception($"Error en la respuesta: {response.StatusCode} - {response.Content}");
            }
        }

        private string ExtractValue(string response, string fieldName)
        {
            // Dividir la respuesta en líneas
            var lines = response.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            // Recorrer cada línea para buscar el campo correspondiente
            foreach (var line in lines)
            {
                // Comprobar si la línea contiene el campo que estamos buscando
                if (line.Trim().StartsWith($"- {fieldName}:", StringComparison.OrdinalIgnoreCase))
                {
                    // Dividir la línea en el primer ':' y extraer el valor
                    var parts = line.Split(new[] { ':' }, 2); // Dividir solo en la primera aparición
                    if (parts.Length > 1)
                    {
                        return parts[1].Trim(); // Extraer y retornar el valor después de ':'
                    }
                }
            }

            return string.Empty; // Devuelve vacío si no encuentra el campo
        }

        private List<Producto> ExtractProductos(string response)
        {
            var productos = new List<Producto>();

            // Separar la respuesta en líneas
            var lines = response.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            // Expresiones regulares para cada campo del producto
            var codigoRegex = new Regex(@"Código:\s*(\S+)");
            var descripcionRegex = new Regex(@"Descripción:\s*(.*?)\s*\|");
            var serieVctoRegex = new Regex(@"Serie/Vcto:\s*([\w*]+(-\d+)?)"); // Modificado para capturar formatos con y sin guion
            var cantidadRegex = new Regex(@"Cantidad:\s*(\d+)");
            var precioUnitarioRegex = new Regex(@"Precio Unitario:\s*([\d,.]+)");
            var descuentoRegex = new Regex(@"% Dcto:\s*(-|\d+)?");
            var totalRegex = new Regex(@"Total:\s*([\d,.]+)");

            // Buscar las líneas que contienen productos
            foreach (var line in lines)
            {
                try
                {
                    var producto = new Producto();

                    // Extraer cada campo utilizando su respectiva expresión regular
                    var codigoMatch = codigoRegex.Match(line);
                    if (codigoMatch.Success)
                    {
                        producto.CodigoProducto = codigoMatch.Groups[1].Value.Trim();
                    }

                    var descripcionMatch = descripcionRegex.Match(line);
                    if (descripcionMatch.Success)
                    {
                        producto.Descripcion = descripcionMatch.Groups[1].Value.Trim();
                    }

                    var serieVctoMatch = serieVctoRegex.Match(line);
                    if (serieVctoMatch.Success)
                    {
                        producto.NSerie = serieVctoMatch.Groups[1].Value.Trim(); // Captura correctamente tanto el formato con guion como sin guion
                    }

                    var cantidadMatch = cantidadRegex.Match(line);
                    if (cantidadMatch.Success)
                    {
                        producto.Cantidad = int.Parse(cantidadMatch.Groups[1].Value.Trim());
                    }

                    var precioUnitarioMatch = precioUnitarioRegex.Match(line);
                    if (precioUnitarioMatch.Success)
                    {
                        producto.PrecioUnitario = ParseDecimal(precioUnitarioMatch.Groups[1].Value.Trim());
                    }

                    var descuentoMatch = descuentoRegex.Match(line);
                    if (descuentoMatch.Success)
                    {
                        producto.Descuento = string.IsNullOrWhiteSpace(descuentoMatch.Groups[1].Value) || descuentoMatch.Groups[1].Value.Trim() == "-"
                            ? 0
                            : int.Parse(descuentoMatch.Groups[1].Value.Trim());
                    }

                    var totalMatch = totalRegex.Match(line);
                    if (totalMatch.Success)
                    {
                        producto.Total = ParseDecimal(totalMatch.Groups[1].Value.Trim());
                    }

                    // Solo agregar el producto si se han extraído todos los campos requeridos
                    if (!string.IsNullOrEmpty(producto.CodigoProducto) && !string.IsNullOrEmpty(producto.Descripcion))
                    {
                        productos.Add(producto);
                    }
                }
                catch (Exception ex)
                {
                    // Opcional: registrar el error en un log o mostrar un mensaje más específico
                    Console.WriteLine($"Error al procesar la línea: {line} - Excepción: {ex.Message}");
                }
            }

            // Mensaje que indica que la extracción fue exitosa
            MessageBox.Show("Productos extraídos correctamente. Total de productos: " + productos.Count);

            return productos;
        }




        // Método para convertir texto a decimal
        private decimal ParseDecimal(string value)
        {
            // Quitar espacios y manejar separadores decimales
            value = value.Trim().Replace(".", "").Replace(",", ".");
            decimal result;

            if (decimal.TryParse(value, out result))
            {
                return result;
            }

            // Si hay un error en el formato, lanzar una excepción
            throw new FormatException($"El valor '{value}' no es un formato decimal válido.");
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
        private void CerrarSesion_Click(object sender, MouseButtonEventArgs e)
        {
            // Coloca aquí la lógica para cerrar sesión
            MessageBox.Show("Cerrar sesión");
        }


        private void MetroWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                RutTextBox.Width = 450;
                NombreSucursal.Width = 450;
                RazonSocialTextBox.Width = 450;
                GiroTextBox.Width = 450;
                DireccionTextBox.Width = 450;
                ComunaTextBox.Width = 450;
                EstadoBox.Width = 450;
                RutVendedorTextBox.Width = 450;
                GiroVendedorTextBox.Width = 450;
                CiudadTextBox.Width = 450;
                EntregarEnTextBox.Width = 450;
                FechaEmisionTextBox.Width = 450;
                VencimientoTextBox.Width = 450;
                CobradorTextBox.Width = 450;
                OrdenDeCompraTextBox.Width = 450;
                CondicionesVentaTextBox.Width = 450;
                GuiaDespachoTextBox.Width = 450;
                NotaVentaTextBox.Width = 450;
                RazonSocialVendedorTextBox.Width = 450;
            }
            else
            {
                RutTextBox.Width = 250;
                NombreSucursal.Width = 250;
                RazonSocialTextBox.Width = 250;
                GiroTextBox.Width = 250;
                DireccionTextBox.Width = 250;
                ComunaTextBox.Width = 250;
                EstadoBox.Width = 250;
                RutVendedorTextBox.Width = 250;
                GiroVendedorTextBox.Width = 250;
                CiudadTextBox.Width = 250;
                EntregarEnTextBox.Width = 250;
                FechaEmisionTextBox.Width = 250;
                VencimientoTextBox.Width = 250;
                CobradorTextBox.Width = 250;
                OrdenDeCompraTextBox.Width = 250;
                CondicionesVentaTextBox.Width = 250;
                GuiaDespachoTextBox.Width = 250;
                NotaVentaTextBox.Width = 250;
                RazonSocialVendedorTextBox.Width = 250;
            }
        }
    }
}
