using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class StatisticsController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IStatisticsService _statisticsService;

    /// <summary>
    /// Контроллер аккаунтов
    /// </summary>
    public StatisticsController(UserManager<AppUser> userManager, IStatisticsService statisticsService)
    {
        _userManager = userManager;
        _statisticsService = statisticsService;
    }

    [HttpGet("usersexpenses")]
    public async Task<ActionResult<CountStatisticRequestModel>> GetUsersExpensesStatistics()
    {
        var usersExpensesStatistics = await _statisticsService.GetUsersExpensesCount(_userManager);
        return Ok(usersExpensesStatistics);
    }
    
    [HttpGet("expensesbytypes")]
    public async Task<ActionResult<CountStatisticRequestModel>> GetExpensesByTypesStatistics()
    {
        var expensesByTypesStatistics = await _statisticsService.GetExpensesByTypeCount();
        return Ok(expensesByTypesStatistics);
    }
    
    [HttpGet("usersincomes")]
    public async Task<ActionResult<CountStatisticRequestModel>> GetUsersIncomesStatistics()
    {
        var usersIncomesStatistics = await _statisticsService.GetUsersIncomesCount(_userManager);
        return Ok(usersIncomesStatistics);
    }
    
    [HttpGet("incomesbytypes")]
    public async Task<ActionResult<CountStatisticRequestModel>> GetIncomesByTypesStatistics()
    {
        var incomesByTypesStatistics = await _statisticsService.GetIncomesByTypeCount();
        return Ok(incomesByTypesStatistics);
    }
    
    [HttpGet("usersbudgets")]
    public async Task<ActionResult<CountStatisticRequestModel>> GetUsersBudgetsStatistics()
    {
        var usersBudgetsStatistics = await _statisticsService.GetUsersBudgetsCount(_userManager);
        return Ok(usersBudgetsStatistics);
    }
    
    [HttpGet("budgetsbytimeperiods")]
    public async Task<ActionResult<CountStatisticRequestModel>> GetBudgetsByTimePeriodsStatistics()
    {
        var budgetsByTimePeriodsStatistics = await _statisticsService.GetBudgetsByTimePeriodsCount();
        return Ok(budgetsByTimePeriodsStatistics);
    }
    
    [HttpGet("usersgoals")]
    public async Task<ActionResult<CountStatisticRequestModel>> GetUsersGoalsStatistics()
    {
        var usersGoalsStatistics = await _statisticsService.GetUsersGoalsCount(_userManager);
        return Ok(usersGoalsStatistics);
    }
}