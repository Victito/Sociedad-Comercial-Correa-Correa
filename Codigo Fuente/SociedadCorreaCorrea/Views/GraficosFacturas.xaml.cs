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
                    CapturarGraficoComoImagen(CartesianChartTotalPorProveedor),
                    CapturarGraficoComoImagen(PieChartFacturasPorEstado),
                    CapturarGraficoComoImagen(CartesianChartFacturacionMensual),
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
    }
}
