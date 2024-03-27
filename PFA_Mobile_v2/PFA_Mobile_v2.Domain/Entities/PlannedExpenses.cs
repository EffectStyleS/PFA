namespace PFA_Mobile_v2.Domain.Entities;

/// <summary>
/// Запланированные расходы
/// </summary>
public class PlannedExpenses : BaseEntity
{
    /// <summary>
    /// Сумма
    /// </summary>
    public decimal? Sum { get; set; }
    
    /// <summary>
    /// Id типа расхода
    /// </summary>
    public int ExpenseTypeId { get; set; }
    
    /// <summary>
    /// Id бюджета
    /// </summary>
    public int BudgetId { get; set; }

    /// <summary>
    /// Бюджет
    /// </summary>
    public virtual Budget Budget { get; set; }
    
    /// <summary>
    /// Тип расхода
    /// </summary>
    public virtual ExpenseType ExpenseType { get; set; }
}