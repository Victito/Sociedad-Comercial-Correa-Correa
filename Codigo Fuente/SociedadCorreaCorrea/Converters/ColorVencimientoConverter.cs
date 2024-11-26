using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System;
using System.Windows.Media;  // Agregar esta línea

namespace SociedadCorreaCorrea.Converters
{
    public class ColorVencimientoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string color = value as string;
            if (color == null)
                return Brushes.Transparent;  // Valor predeterminado si no se encuentra el color

            switch (color.ToLower())
            {
                case "red":
                    return Brushes.Red;
                case "yellow":
                    return Brushes.Yellow;
                case "green":
                    return Brushes.Green;
                case "transparent":
                    return Brushes.Transparent;
                default:
                    return Brushes.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
