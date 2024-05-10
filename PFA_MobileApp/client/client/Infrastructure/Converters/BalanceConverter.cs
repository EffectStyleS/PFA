using System.Globalization;

namespace client.Infrastructure.Converters;

/// <summary>
/// Конвертер для разного отображения свойств с разными значениями
/// </summary>
public class BalanceConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            decimal decimalValue when decimalValue > Cutoff => Colors.Green,
            decimal decimalValue when decimalValue == Cutoff => Colors.Black,
            _ => Colors.Red
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }

    public int Cutoff { get; set; }
}