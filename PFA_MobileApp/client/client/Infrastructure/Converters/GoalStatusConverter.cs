using System.Globalization;
using client.Model.Enums;

namespace client.Infrastructure.Converters;

public class GoalStatusConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var status = (GoalStatuses)(value ?? GoalStatuses.NotStarted);

        return status switch
        {
            GoalStatuses.NotStarted => Colors.Gray,
            GoalStatuses.InProgress => Colors.Gold,
            GoalStatuses.Completed => Colors.Green,
            GoalStatuses.Failed => Colors.Red,
            _ => Colors.Gray
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}