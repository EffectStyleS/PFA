namespace PFA_Mobile_v2.Domain.Entities;

/// <summary>
/// Временной период
/// </summary>
public class TimePeriod : BaseEntity
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Коллекция бюджетов
    /// </summary>
    public virtual ICollection<Budget> Budgets { get; set; } = new HashSet<Budget>();
}