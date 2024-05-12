using System.Collections.ObjectModel;

namespace client.Infrastructure;

/// <summary>
/// Группировка
/// </summary>
/// <param name="key">Ключ</param>
/// <param name="items">Элементы</param>
/// <typeparam name="TK">Тип ключа</typeparam>
/// <typeparam name="T">Тип элементов</typeparam>
public class Grouping<TK, T>(TK key, IEnumerable<T> items) : ObservableCollection<T>(items)
{
    public TK Key { get; private set; } = key;
}