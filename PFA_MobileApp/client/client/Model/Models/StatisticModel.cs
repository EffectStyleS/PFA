namespace client.Model.Models;

/// <summary>
/// Модель статистики
/// </summary>
/// <typeparam name="T">Тип значения</typeparam>
public class StatisticModel<T>
{
    /// <summary>
    /// Название
    /// </summary>
    public string TypeName { get; set; }
    
    /// <summary>
    /// Значение
    /// </summary>
    public T Value { get; set; }
}