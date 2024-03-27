using PFA_Mobile_v2.Application.Models;

namespace PFA_Mobile_v2.Application.Services.Interfaces;

/// <summary>
/// Интерфейс сервиса расходов
/// </summary>
public interface IExpenseService : ICrud<ExpenseDto>
{
    /// <summary>
    /// Возвращает все расходы пользователя
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    Task<List<ExpenseDto>> GetAllUserExpenses(int userId);

    /// <summary>
    /// Возвращает превышения бюджетов
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    Task<List<BudgetOverrunDto>> GetBudgetOverruns(int userId);
}