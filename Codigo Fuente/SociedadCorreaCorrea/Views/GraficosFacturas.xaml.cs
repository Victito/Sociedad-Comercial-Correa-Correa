using MahApps.Metro.Controls;
using SociedadCorreaCorrea.ViewModels;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;

namespace SociedadCorreaCorrea.Views
{
    public partial class GraficosFacturas : MetroWindow
    {
        public GraficosFacturas()
        {
            InitializeComponent();
            this.StateChanged += MetroWindow_StateChanged;
            DataContext = new GraficosFacturasViewModel(this);

            // Gráfico de Facturas por Categoría (barras)
            SeriesFacturasPorCategoria = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Facturas",
                    Values = new ChartValues<double> { 5, 10, 15 },
                    Fill = new SolidColorBrush(Colors.MediumAquamarine), // Color de barras
                    Stroke = new SolidColorBrush(Colors.Blue), // Borde de las barras
                    StrokeThickness = 2
                }
            };

            LabelsCategorias = new[] { "Categoría 1", "Categoría 2", "Categoría 3" };
            Formatter = value => value.ToString("N");

            // Gráfico de Facturación Mensual (líneas)
            SeriesFacturacionMensual = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Facturación",
                    Values = new ChartValues<double> { 2000, 4000, 6000 },
                    Stroke = new SolidColorBrush(Colors.Red), // Color de línea
                    Fill = new SolidColorBrush(Colors.Transparent), // Fondo transparente
                    PointGeometrySize = 10,
                    PointForeground = new SolidColorBrush(Colors.Red) // Color de los puntos
                }
            };

            LabelsMeses = new[] { "Enero", "Febrero", "Marzo" };

            // Gráfico de Promedio por Proveedor (barras)
            SeriesPromedioPorProveedor = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Promedio",
                    Values = new ChartValues<double> { 1000, 2000, 3000 },
                    Fill = new SolidColorBrush(Colors.MediumSeaGreen), // Color de barras
                    Stroke = new SolidColorBrush(Colors.SeaGreen), // Borde de las barras
                    StrokeThickness = 2
                }
            };

            LabelsProveedores = new[] { "Proveedor A", "Proveedor B", "Proveedor C" };
        }

        // Propiedades públicas para los gráficos
        public SeriesCollection SeriesFacturasPorCategoria { get; set; }
        public SeriesCollection SeriesFacturacionMensual { get; set; }
        public SeriesCollection SeriesPromedioPorProveedor { get; set; }

        public string[] LabelsCategorias { get; set; }
        public string[] LabelsMeses { get; set; }
        public string[] LabelsProveedores { get; set; }
        public Func<double, string> Formatter { get; set; }

        // Importación de la DLL user32 para mover la ventana
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWind, int wMsg, int wParam, int lParam);

        // Métodos de control de la ventana
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

        // Métodos de navegación entre ventanas
        private void IngresarFacturas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                var registroFacturas = new RegistroFacturas();
                registroFacturas.Show();
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

        // Método para capturar gráficos y guardarlos en un PDF
        private void GuardarGraficosEnPDF(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                DefaultExt = "pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                // Crear el documento PDF
                PdfDocument pdf = new PdfDocument();
                PdfPage pdfPage = pdf.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(pdfPage);

                // Capturar gráficos como imágenes
                var chartImages = new[]
                {
                    CapturarGraficoComoImagen(CartesianChartFacturacionMensual),
                    CapturarGraficoComoImagen(CartesianChartTotalPorProveedor),
                    CapturarGraficoComoImagen(CartesianChartFacturasPorCategoria),
                    CapturarGraficoComoImagen(CartesianChartPromedioPorProveedor)
                };

                // Añadir cada imagen al PDF
                double yPosition = 0;
                foreach (var image in chartImages)
                {
                    if (image != null)
                    {
                        using (var stream = new MemoryStream())
                        {
                            BitmapEncoder encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(BitmapFrame.Create(image));
                            encoder.Save(stream);
                            var xImage = XImage.FromStream(() => new MemoryStream(stream.ToArray()));

                            // Dibujar la imagen en la página del PDF
                            gfx.DrawImage(xImage, 0, yPosition, pdfPage.Width, pdfPage.Height / 3);
                            yPosition += pdfPage.Height / 3;
                        }
                    }
                }

                // Guardar el archivo PDF
                pdf.Save(saveFileDialog.FileName);
                MessageBox.Show("Los gráficos han sido guardados en el PDF.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Función para capturar un gráfico como imagen
        private RenderTargetBitmap CapturarGraficoComoImagen(FrameworkElement grafico)
        {
            if (grafico == null) return null;

            // Renderizar el gráfico como imagen
            var renderBitmap = new RenderTargetBitmap(
                (int)grafico.ActualWidth, (int)grafico.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);

            grafico.Measure(new Size(grafico.ActualWidth, grafico.ActualHeight));
            grafico.Arrange(new Rect(new Size(grafico.ActualWidth, grafico.ActualHeight)));

            renderBitmap.Render(grafico);
            return renderBitmap;
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
        private void MetroWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                CartesianChartFacturasPorCategoria.Height = 450;
                CartesianChartTotalPorProveedor.Height = 450;
                CartesianChartFacturacionMensual.Height = 450;
                CartesianChartPromedioPorProveedor.Height = 450;
            }
            else
            {
                CartesianChartFacturasPorCategoria.Height = 270;
                CartesianChartTotalPorProveedor.Height = 270;
                CartesianChartFacturacionMensual.Height = 270;
                CartesianChartPromedioPorProveedor.Height = 270;
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
    }
}
