using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfAppIContract
{
    /// <summary>
    /// Конвертер для преобразования boolean значений в цвета для индикаторов
    /// Используется в XAML для привязки цветов индикаторов Pre/Post условий
    /// </summary>
    public class BoolToColorConverter : IValueConverter
    {
        /// <summary>
        /// Преобразует boolean в цвет: true → зеленый, false → красный
        /// </summary>
        /// <param name="value">Boolean значение (true/false)</param>
        /// <returns>Зеленый цвет если true, красный если false, серый если не boolean</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Brushes.Green : Brushes.Red;
            }
            return Brushes.Gray;
        }

        /// <summary>
        /// Обратное преобразование не реализовано (не требуется в нашем сценарии)
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}