namespace PFA_Mobile_v2.Domain.Entities;

/// <summary>
/// Бюджет
/// </summary>
public class Budget : BaseEntity
{
    /// <summary>
    /// Название бюджета
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Начальная дата бюджета
    /// </summary>
    public DateTime StartDate { get; set; }
    
    /// <summary>
    /// Id временного периода бюджета
    /// </summary>
    public int TimePeriodId { get; set; }
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Временной период
    /// </summary>
    public virtual TimePeriod TimePeriod { get; set; }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public virtual AppUser User { get; set; }

    /// <summary>
    /// Коллекция запланированных расходов
    /// </summary>
    public virtual ICollection<PlannedExpenses> PlannedExpenses { get; set; } = new HashSet<PlannedExpenses>();
    
    /// <summary>
    /// Коллекция запланированных доходов
    /// </summary>
    public virtual ICollection<PlannedIncomes> PlannedIncomes { get; set; } = new HashSet<PlannedIncomes>();
}