using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SociedadCorreaCorrea.ViewModels
{
    public class PantallaCargaViewModel : BaseViewModel
    {
        private ImageSource _gifSource;

        // Propiedad enlazada a AnimatedSource
        public ImageSource GifSource
        {
            get => _gifSource;
            set
            {
                _gifSource = value;
                OnPropertyChanged(); // Notifica cambios a la interfaz
            }
        }

        // Constructor para inicializar la fuente del GIF
        public PantallaCargaViewModel()
        {
            // Ruta del GIF dentro del proyecto
            GifSource = new BitmapImage(new Uri("pack://application:,,,/Resources/animacion_carga.gif"));
        }
    }
}