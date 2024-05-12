using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;

namespace PFA_Mobile_v2.Controllers;

/// <summary>
/// Контроллер бюджетов
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[EnableCors]
[Authorize]
public class BudgetController : ControllerBase
{
    private readonly IBudgetService _budgetService;
    private readonly IPlannedExpensesService _plannedExpensesService;
    private readonly IPlannedIncomesService _plannedIncomesService;

    /// <summary>
    /// Контроллер бюджетов
    /// </summary>
    public BudgetController(IBudgetService budgetService, IPlannedExpensesService plannedExpensesService,
        IPlannedIncomesService plannedIncomesService)
    {
        _budgetService = budgetService;
        _plannedExpensesService = plannedExpensesService;
        _plannedIncomesService = plannedIncomesService;
    }

    /// <summary>
    /// Получает все бюджеты пользователя
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    /// <returns></returns>
    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<IEnumerable<BudgetDto>>> GetAllUserBudgets(int userId)
    {
        var budgets = (await _budgetService.GetAllUserBudgets(userId)).ToList();
        return Ok(budgets);
    }

    /// <summary>
    /// Получает бюджет по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public Task<BudgetDto?> GetById(int id)
    {
        return _budgetService.GetById(id);
    }

    /// <summary>
    /// Создает бюджет
    /// </summary>
    /// <param name="budget">Модель бюджета</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<BudgetRequestModel>> Create([FromBody]BudgetRequestModel budget)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!await _budgetService.Create(budget.Budget))
        {
            return NotFound();
        }

        var budgetId = (await _budgetService.GetById(
                (await _budgetService.GetAllUserBudgets(budget.Budget.UserId)).Max(x => x.Id)))!.Id;
        
        foreach (var plannedExpenses in budget.PlannedExpenses)
        {
            plannedExpenses.BudgetId = budgetId;
            if (!await _plannedExpensesService.Create(plannedExpenses))
            {
                return NotFound();
            }
        }

        foreach (var plannedIncomes in budget.PlannedIncomes)
        {
            plannedIncomes.BudgetId = budgetId;
            if (!await _plannedIncomesService.Create(plannedIncomes))
            {
                return NotFound();
            }
        }

        var createdBudgetRequestModel = new BudgetRequestModel
        {
            Budget = (await _budgetService.GetById((await _budgetService.GetAllUserBudgets(budget.Budget.UserId)).Max(x => x.Id)))!,
            PlannedExpenses = await _plannedExpensesService.GetAllBudgetPlannedExpenses(budget.Budget.UserId),
            PlannedIncomes = await _plannedIncomesService.GetAllBudgetPlannedIncomes(budget.Budget.UserId)
        };

        return Ok(createdBudgetRequestModel);
    }

    /// <summary>
    /// Обновляет данные бюджета
    /// </summary>
    /// <param name="id">Id бюджета</param>
    /// <param name="budget">Модель бюджета</param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<int>> Put(int id, [FromBody]BudgetRequestModel budget)
    {
        if (id != budget.Budget.Id)
        {
            return BadRequest();
        }

        if (!await _budgetService.Update(budget.Budget))
        {
            return NotFound();
        }

        foreach (var plannedExpenses in budget.PlannedExpenses)
        {
            if (!await _plannedExpensesService.Update(plannedExpenses))
            {
                return NotFound();
            }
        }

        foreach (var plannedIncomes in budget.PlannedIncomes)
        {
            if (!await _plannedIncomesService.Update(plannedIncomes))
            {
                return NotFound();
            }
        }

        return Ok(budget.Budget.Id);
    }

    /// <summary>
    /// Удаляет бюджет
    /// </summary>
    /// <param name="id">Id бюджета</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        if (!await _budgetService.Exists(id))
        {
            return NotFound();
        }

        await _budgetService.Delete(id);
        return Ok(id);
    }
}