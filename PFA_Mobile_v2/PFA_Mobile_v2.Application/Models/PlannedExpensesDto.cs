using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Models;

/// <summary>
/// Объект передачи данных запланированных расходов
/// </summary>
public class PlannedExpensesDto
{
    /// <summary>
    /// Объект передачи данных запланированных расходов
    /// </summary>
    public PlannedExpensesDto() { }
    
    /// <summary>
    /// Объект передачи данных запланированных расходов
    /// </summary>
    public PlannedExpensesDto(PlannedExpenses plannedExpenses)
    {
        Id             = plannedExpenses.Id;
        Sum            = plannedExpenses.Sum;
        ExpenseTypeId  = plannedExpenses.ExpenseTypeId;
        BudgetId       = plannedExpenses.BudgetId;
    }

    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
    
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
}