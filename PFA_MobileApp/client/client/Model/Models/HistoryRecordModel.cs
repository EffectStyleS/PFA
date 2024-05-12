namespace client.Model.Models;

/// <summary>
/// Модель записи истории
/// </summary>
public class HistoryRecordModel
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Значение
    /// </summary>
    public decimal Value { get; set; }
    
    /// <summary>
    /// Дата
    /// </summary>
    public DateTime Date { get; set; }
}