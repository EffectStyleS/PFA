using Microsoft.AspNetCore.Identity;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Services.Interfaces;

/// <summary>
/// Интерфейс сервиса статистики использования системы
/// </summary>
public interface IStatisticsService
{
    /// <summary>
    /// Получает число расходов пользователей
    /// </summary>
    /// <param name="userManager">Менеджер пользователей</param>
    /// <returns></returns>
    Task<List<CountStatisticRequestModel>> GetUsersExpensesCount(UserManager<AppUser> userManager);
    
    /// <summary>
    /// Получает число расходов каждого типа
    /// </summary>
    /// <returns></returns>
    Task<List<CountStatisticRequestModel>> GetExpensesByTypeCount();
    
    /// <summary>
    /// Получает число доходов пользователей
    /// </summary>
    /// <param name="userManager">Менеджер пользователей</param>
    /// <returns></returns>
    Task<List<CountStatisticRequestModel>> GetUsersIncomesCount(UserManager<AppUser> userManager);
    
    /// <summary>
    /// Получает число доходов каждого типа
    /// </summary>
    /// <returns></returns>
    Task<List<CountStatisticRequestModel>> GetIncomesByTypeCount();
    
    /// <summary>
    /// Получает число бюджетов пользователей
    /// </summary>
    /// <param name="userManager">Менеджер пользователей</param>
    /// <returns></returns>
    Task<List<CountStatisticRequestModel>> GetUsersBudgetsCount(UserManager<AppUser> userManager);
    
    /// <summary>
    /// Получает число бюджетов каждого временного периода
    /// </summary>
    /// <returns></returns>
    Task<List<CountStatisticRequestModel>> GetBudgetsByTimePeriodsCount();

    /// <summary>
    /// Получает число целей пользователей
    /// </summary>
    /// <param name="userManager">Менеджер пользователей</param>
    /// <returns></returns>
    Task<List<CountStatisticRequestModel>> GetUsersGoalsCount(UserManager<AppUser> userManager);
}