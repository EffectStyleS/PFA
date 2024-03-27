namespace PFA_Mobile_v2.Application.Models;

/// <summary>
/// Объект передачи данных превышений бюджета
/// </summary>
public class BudgetOverrunDto
{
    /// <summary>
    /// Название бюджета
    /// </summary>
    public string? BudgetName { get; set; }
    
    /// <summary>
    /// Тип расхода
    /// </summary>
    public string? ExpenseType { get; set; }
    
    /// <summary>
    /// Разница между запланированными и актуальными расходами
    /// </summary>
    public decimal? Difference { get; set; }
}