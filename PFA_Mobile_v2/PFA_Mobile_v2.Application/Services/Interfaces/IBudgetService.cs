using PFA_Mobile_v2.Application.Models;

namespace PFA_Mobile_v2.Application.Services.Interfaces;

/// <summary>
/// Интерфейс сервиса бюджетов
/// </summary>
public interface IBudgetService : ICrud<BudgetDto>
{
    /// <summary>
    /// Возвращает все бюджеты пользователя
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    Task<List<BudgetDto>> GetAllUserBudgets(int userId);
}