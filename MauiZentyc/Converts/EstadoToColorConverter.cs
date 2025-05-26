using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiZentyc.Converts
{
    public class EstadoToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string estado)
            {
                return estado switch
                {
                    "Completado" => Color.FromArgb("#4CAF50"),  // Verde
                    "En Proceso" => Color.FromArgb("#2196F3"),   // Azul
                    "Pendiente" => Color.FromArgb("#FFC107"),    // Amarillo
                    "Cancelado" => Color.FromArgb("#F44336"),    // Rojo
                    _ => Colors.Black
                };
            }
            return Colors.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
