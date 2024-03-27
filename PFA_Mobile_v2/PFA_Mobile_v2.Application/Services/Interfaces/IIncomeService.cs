using PFA_Mobile_v2.Application.Models;

namespace PFA_Mobile_v2.Application.Services.Interfaces;

/// <summary>
/// Интерфейс сервиса доходов
/// </summary>
public interface IIncomeService : ICrud<IncomeDto>
{
    /// <summary>
    /// Возвращает все доходы пользователя
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    Task<List<IncomeDto>> GetAllUserIncomes(int userId);
}