using PFA_Mobile_v2.Application.Models;

namespace PFA_Mobile_v2.Application.Services.Interfaces;

/// <summary>
/// Интерфейс сервиса целей
/// </summary>
public interface IGoalService : ICrud<GoalDto>
{
    /// <summary>
    /// Возвращает все цели пользователя
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    Task<List<GoalDto>> GetAllUserGoals(int userId);
}