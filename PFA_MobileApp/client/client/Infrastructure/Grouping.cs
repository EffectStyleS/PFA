using System.Collections.ObjectModel;

namespace client.Infrastructure;

public class Grouping<TK, T>(TK key, IEnumerable<T> items) : ObservableCollection<T>(items)
{
    public TK Key { get; private set; } = key;
}