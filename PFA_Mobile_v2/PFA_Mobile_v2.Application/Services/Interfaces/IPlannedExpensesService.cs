using PFA_Mobile_v2.Application.Models;

namespace PFA_Mobile_v2.Application.Services.Interfaces;

/// <summary>
/// Интерфейс сервиса запланированных расходов
/// </summary>
public interface IPlannedExpensesService : ICrud<PlannedExpensesDto>
{
    /// <summary>
    /// Возвращает все запланированные расходы бюджета
    /// </summary>
    /// <param name="budgetId">Id бюджета</param>
    Task<List<PlannedExpensesDto>> GetAllBudgetPlannedExpenses(int budgetId);
}