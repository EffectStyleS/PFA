using PFA_Mobile_v2.Application.Models;

namespace PFA_Mobile_v2.Application.Services.Interfaces;

/// <summary>
/// Интерфейс сервиса запланированных доходов
/// </summary>
public interface IPlannedIncomesService : ICrud<PlannedIncomesDto>
{
    /// <summary>
    /// Возвращает все запланированные доходов бюджета
    /// </summary>
    /// <param name="budgetId">Id бюджета</param>
    Task<List<PlannedIncomesDto>> GetAllBudgetPlannedIncomes(int budgetId);
}