using System.Globalization;

namespace client.Infrastructure.Converters;

/// <summary>
/// Конвертер для цветового отображения свойств с разными значениями
/// </summary>
public class BalanceConverter : IValueConverter
{
    /// <inheritdoc />
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            decimal decimalValue when decimalValue > Cutoff => Colors.Green,
            decimal decimalValue when decimalValue == Cutoff => Colors.Black,
            _ => Colors.Red
        };
    }

    /// <inheritdoc />
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Разделитель значений
    /// </summary>
    public int Cutoff { get; set; }
}