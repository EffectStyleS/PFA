namespace PFA_Mobile_v2.Domain.Entities;

/// <summary>
/// Запланированные доходы
/// </summary>
public class PlannedIncomes : BaseEntity
{
    /// <summary>
    /// Сумма
    /// </summary>
    public decimal? Sum { get; set; }
    
    /// <summary>
    /// Id типа дохода
    /// </summary>
    public int IncomeTypeId { get; set; }
    
    /// <summary>
    /// Id бюджета
    /// </summary>
    public int BudgetId { get; set; }

    /// <summary>
    /// Бюджет
    /// </summary>
    public virtual Budget Budget { get; set; }
    
    /// <summary>
    /// Тип дохода
    /// </summary>
    public virtual IncomeType IncomeType { get; set; }
}