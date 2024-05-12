using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;

namespace PFA_Mobile_v2.Controllers;

/// <summary>
/// Контроллер запланированных расходов
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[EnableCors]
[Authorize]
public class PlannedExpensesController : ControllerBase
{
    private readonly IPlannedExpensesService _plannedExpensesService;

    /// <summary>
    /// Контроллер запланированных расходов
    /// </summary>
    public PlannedExpensesController(IPlannedExpensesService plannedExpensesService)
    {
        _plannedExpensesService = plannedExpensesService;
    }

    /// <summary>
    /// Получает все запланированные расходы бюджета
    /// </summary>
    /// <param name="budgetId">Id бюджета</param>
    /// <returns></returns>
    [HttpGet("budget/{budgetId:int}")]
    public async Task<ActionResult<IEnumerable<PlannedExpensesDto>>> GetAllBudgetPlannedExpenses(int budgetId)
    {
        var plannedExpenses = (await _plannedExpensesService.GetAllBudgetPlannedExpenses(budgetId)).ToList();
        return Ok(plannedExpenses);
    }

    /// <summary>
    /// Получает запланированные расходы по id
    /// </summary>
    /// <param name="id">Id запланированных расходов</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PlannedExpensesDto>> GetById(int id)
    {
        var plannedExpenses = await _plannedExpensesService.GetById(id);
        if (plannedExpenses is null)
        {
            return NotFound();
        }

        return Ok(plannedExpenses);
    }

    /// <summary>
    /// Создает запланированные расходы
    /// </summary>
    /// <param name="plannedExpenses">Запланированные расходы</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<PlannedExpensesDto>> Create([FromBody] PlannedExpensesDto plannedExpenses)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!await _plannedExpensesService.Create(plannedExpenses))
        {
            return NotFound();
        }

        var createdItem = await _plannedExpensesService.GetById((await _plannedExpensesService.GetAllBudgetPlannedExpenses(plannedExpenses.BudgetId)).Max(x => x.Id));
        return Ok(createdItem);
    }

    /// <summary>
    /// Обновляет данные запланированных расходов
    /// </summary>
    /// <param name="id">Id запланированных расходов</param>
    /// <param name="plannedExpenses">Запланированные расходы</param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<int>> Put(int id, [FromBody] PlannedExpensesDto plannedExpenses)
    {
        if (id != plannedExpenses.Id)
        {
            return BadRequest();
        }

        if (!await _plannedExpensesService.Update(plannedExpenses))
        {
            return NotFound();
        }

        return Ok(plannedExpenses.Id);
    }

    /// <summary>
    /// Удаление запланированных расходов
    /// </summary>
    /// <param name="id">Id запланированных расходов</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        if (!await _plannedExpensesService.Exists(id))
        {
            return NotFound();
        }

        await _plannedExpensesService.Delete(id);
        return Ok(id);
    }
}