namespace PFA_Mobile_v2.Application.Models;

/// <summary>
/// Модель запроса бюджета
/// </summary>
public class BudgetRequestModel
{
    /// <summary>
    /// Бюджет
    /// </summary>
    public BudgetDto Budget { get; set; } = new();

    /// <summary>
    /// Запланированные расходы бюджета
    /// </summary>
    public List<PlannedExpensesDto> PlannedExpenses { get; set; } = [];

    /// <summary>
    /// Запланированные дохода бюджета
    /// </summary>
    public List<PlannedIncomesDto> PlannedIncomes { get; set;  } = [];
}