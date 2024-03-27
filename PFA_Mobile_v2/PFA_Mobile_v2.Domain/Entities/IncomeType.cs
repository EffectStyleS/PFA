namespace PFA_Mobile_v2.Domain.Entities;

/// <summary>
/// Тип дохода
/// </summary>
public class IncomeType : BaseEntity
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Коллекция доходов
    /// </summary>
    public virtual ICollection<Income> Incomes { get; set; } = new HashSet<Income>();
    
    /// <summary>
    /// Коллекция запланированных доходов
    /// </summary>
    public virtual ICollection<PlannedIncomes> PlannedIncomes { get; set; } = new HashSet<PlannedIncomes>();
}