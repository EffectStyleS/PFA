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

    // GET: api/<PlannedExpensesController>/budget/budgetId
    [HttpGet("budget/{budgetId:int}")]
    public async Task<ActionResult<IEnumerable<PlannedExpensesDto>>> GetAllBudgetPlannedExpenses(int budgetId)
    {
        var plannedExpenses = (await _plannedExpensesService.GetAllBudgetPlannedExpenses(budgetId)).ToList();
        return Ok(plannedExpenses);
    }

    // GET api/<PlannedExpensesController>/id
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

    // POST api/<PlannedExpensesController>
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

    // PUT api/<PlannedExpensesController>/5
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

    // DELETE api/<PlannedExpensesController>/5
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