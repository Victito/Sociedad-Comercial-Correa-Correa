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

namespace SociedadCorreaCorrea.Views
{
    public partial class GraficosFacturas : MetroWindow
    {
        public GraficosFacturas()
        {
            InitializeComponent();
            DataContext = new GraficosFacturasViewModel(this);
        }

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

                // Definir márgenes
                double margin = 20;

                // Capturar gráficos como imágenes
                var chartImages = new[]
                {
            CapturarGraficoComoImagen(CartesianChartTotalPorProveedor),
            CapturarGraficoComoImagen(CartesianChartFacturacionMensual),
            CapturarGraficoComoImagen(CartesianChartFacturasPorCategoria),
            CapturarGraficoComoImagen(CartesianChartPromedioPorProveedor)
        };

                foreach (var image in chartImages)
                {
                    if (image != null)
                    {
                        PdfPage pdfPage = pdf.AddPage();  // Añadir nueva página para cada gráfico
                        XGraphics gfx = XGraphics.FromPdfPage(pdfPage);

                        // Obtener el tamaño de la página actual
                        double pageHeight = pdfPage.Height;
                        double pageWidth = pdfPage.Width;

                        // Obtener la relación de aspecto de la imagen
                        double imageAspectRatio = image.PixelWidth / (double)image.PixelHeight;

                        // Calcular las dimensiones de la imagen manteniendo su relación de aspecto
                        double chartWidth = pageWidth - 2 * margin;  // Ancho máximo permitido (restando márgenes)
                        double chartHeight = chartWidth / imageAspectRatio;  // Altura correspondiente para mantener la relación de aspecto

                        // Si la altura calculada excede el alto de la página, ajustamos el alto al máximo posible y recalculamos el ancho
                        if (chartHeight > pageHeight - 2 * margin)
                        {
                            chartHeight = pageHeight - 2 * margin;  // Altura máxima permitida
                            chartWidth = chartHeight * imageAspectRatio;  // Recalcular ancho manteniendo la relación de aspecto
                        }

                        // Calcular la posición centrada en la página
                        double xPosition = (pageWidth - chartWidth) / 2;
                        double yPosition = (pageHeight - chartHeight) / 2;

                        using (var stream = new MemoryStream())
                        {
                            // Crear el stream para capturar la imagen con alta calidad
                            BitmapEncoder encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(BitmapFrame.Create(image));
                            encoder.Save(stream);

                            // Crear la imagen desde el stream
                            var xImage = XImage.FromStream(() => new MemoryStream(stream.ToArray()));

                            // Dibujar la imagen en la página del PDF, manteniendo la relación de aspecto
                            gfx.DrawImage(xImage, xPosition, yPosition, chartWidth, chartHeight);
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

            // Renderizar el gráfico como imagen con resolución alta
            var renderBitmap = new RenderTargetBitmap(
                (int)grafico.ActualWidth * 2,  // Multiplicamos por 2 para mejorar la resolución
                (int)grafico.ActualHeight * 2,
                192d, 192d,  // DPI más alto para capturar con mejor calidad
                PixelFormats.Pbgra32);

            grafico.Measure(new Size(grafico.ActualWidth, grafico.ActualHeight));
            grafico.Arrange(new Rect(new Size(grafico.ActualWidth, grafico.ActualHeight)));

            renderBitmap.Render(grafico);
            return renderBitmap;
        }

    }
}
