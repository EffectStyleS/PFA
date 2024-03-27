namespace PFA_Mobile_v2.Domain.Entities;

/// <summary>
/// Тип расхода
/// </summary>
public class ExpenseType : BaseEntity
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Коллекция расходов
    /// </summary>
    public virtual ICollection<Expense> Expenses { get; set; } = new HashSet<Expense>();
    
    /// <summary>
    /// Коллекция запланированных расходов
    /// </summary>
    public virtual ICollection<PlannedExpenses> PlannedExpenses { get; set; } = new HashSet<PlannedExpenses>();
}