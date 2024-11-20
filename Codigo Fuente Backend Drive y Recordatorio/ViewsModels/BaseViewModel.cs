using System.ComponentModel;

namespace SociedadCorreaCorrea.ViewModels
{
    /// <summary>
    /// Clase base para los ViewModels, implementa INotifyPropertyChanged.
    /// Proporciona la funcionalidad para notificar a la UI cuando una propiedad ha cambiado.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Evento que se dispara cuando una propiedad cambia.
        /// Lo utiliza la interfaz INotifyPropertyChanged para notificar a la UI.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Método protegido que notifica a la UI cuando el valor de una propiedad cambia.
        /// Si no se proporciona un nombre de propiedad, se usa la propiedad que llama.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad que ha cambiado.</param>
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            // Verifica si hay suscriptores al evento PropertyChanged y lo dispara
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
