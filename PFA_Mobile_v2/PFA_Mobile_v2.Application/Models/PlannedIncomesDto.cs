using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Models;

/// <summary>
/// Объект передачи данных запланированных доходов
/// </summary>
public class PlannedIncomesDto
{
    /// <summary>
    /// Объект передачи данных запланированных доходов
    /// </summary>
    public PlannedIncomesDto() { }

    /// <summary>
    /// Объект передачи данных запланированных доходов
    /// </summary>
    public PlannedIncomesDto(PlannedIncomes plannedIncomes)
    {
        Id           = plannedIncomes.Id;
        Sum          = plannedIncomes.Sum;
        IncomeTypeId = plannedIncomes.IncomeTypeId;
        BudgetId     = plannedIncomes.BudgetId;
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
    /// Id типа дохода
    /// </summary>
    public int IncomeTypeId { get; set; }
    
    /// <summary>
    /// Id бюджета
    /// </summary>
    public int BudgetId { get; set; }
}