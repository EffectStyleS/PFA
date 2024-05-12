using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Controllers;

/// <summary>
/// Контроллер статистики использования системы
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class StatisticsController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IStatisticsService _statisticsService;

    /// <summary>
    /// Контроллер статистики использования системы
    /// </summary>
    public StatisticsController(UserManager<AppUser> userManager, IStatisticsService statisticsService)
    {
        _userManager = userManager;
        _statisticsService = statisticsService;
    }

    /// <summary>
    /// Получает число расходов пользователей
    /// </summary>
    /// <returns></returns>
    [HttpGet("usersexpenses")]
    public async Task<ActionResult<CountStatisticRequestModel>> GetUsersExpensesStatistics()
    {
        var usersExpensesStatistics = await _statisticsService.GetUsersExpensesCount(_userManager);
        return Ok(usersExpensesStatistics);
    }
    
    /// <summary>
    /// Получает число расходов каждого типа 
    /// </summary>
    /// <returns></returns>
    [HttpGet("expensesbytypes")]
    public async Task<ActionResult<CountStatisticRequestModel>> GetExpensesByTypesStatistics()
    {
        var expensesByTypesStatistics = await _statisticsService.GetExpensesByTypeCount();
        return Ok(expensesByTypesStatistics);
    }
    
    /// <summary>
    /// Получает число доходов пользователей
    /// </summary>
    /// <returns></returns>
    [HttpGet("usersincomes")]
    public async Task<ActionResult<CountStatisticRequestModel>> GetUsersIncomesStatistics()
    {
        var usersIncomesStatistics = await _statisticsService.GetUsersIncomesCount(_userManager);
        return Ok(usersIncomesStatistics);
    }
    
    /// <summary>
    /// Получает число доходов каждого типа 
    /// </summary>
    /// <returns></returns>
    [HttpGet("incomesbytypes")]
    public async Task<ActionResult<CountStatisticRequestModel>> GetIncomesByTypesStatistics()
    {
        var incomesByTypesStatistics = await _statisticsService.GetIncomesByTypeCount();
        return Ok(incomesByTypesStatistics);
    }
    
    /// <summary>
    /// Получает число бюджетов пользователей
    /// </summary>
    /// <returns></returns>
    [HttpGet("usersbudgets")]
    public async Task<ActionResult<CountStatisticRequestModel>> GetUsersBudgetsStatistics()
    {
        var usersBudgetsStatistics = await _statisticsService.GetUsersBudgetsCount(_userManager);
        return Ok(usersBudgetsStatistics);
    }
    
    /// <summary>
    /// Получает число бюджетов каждого временного периода 
    /// </summary>
    /// <returns></returns>
    [HttpGet("budgetsbytimeperiods")]
    public async Task<ActionResult<CountStatisticRequestModel>> GetBudgetsByTimePeriodsStatistics()
    {
        var budgetsByTimePeriodsStatistics = await _statisticsService.GetBudgetsByTimePeriodsCount();
        return Ok(budgetsByTimePeriodsStatistics);
    }
    
    /// <summary>
    /// Получает число целей пользователей
    /// </summary>
    /// <returns></returns>
    [HttpGet("usersgoals")]
    public async Task<ActionResult<CountStatisticRequestModel>> GetUsersGoalsStatistics()
    {
        var usersGoalsStatistics = await _statisticsService.GetUsersGoalsCount(_userManager);
        return Ok(usersGoalsStatistics);
    }
}