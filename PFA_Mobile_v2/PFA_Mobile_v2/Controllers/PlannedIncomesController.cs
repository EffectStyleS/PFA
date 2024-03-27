using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;

namespace PFA_Mobile_v2.Controllers;

/// <summary>
/// Контроллер запланированных доходов
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[EnableCors]
[Authorize]
public class PlannedIncomesController : ControllerBase
{
    private readonly IPlannedIncomesService _plannedIncomesService;

    /// <summary>
    /// Контроллер запланированных доходов
    /// </summary>
    public PlannedIncomesController(IPlannedIncomesService plannedIncomesService)
    {
        _plannedIncomesService = plannedIncomesService;
    }

    // GET: api/<PlannedIncomesController>/budget/budgetId
    [HttpGet("budget/{budgetId:int}")]
    public async Task<ActionResult<IEnumerable<PlannedIncomesDto>>> GetAllBudgetPlannedIncomes(int budgetId)
    {
        var plannedIncomes = (await _plannedIncomesService.GetAllBudgetPlannedIncomes(budgetId)).ToList();
        return Ok(plannedIncomes);
    }

    // GET api/<PlannedIncomesController>/id
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PlannedIncomesDto>> GetById(int id)
    {
        var plannedIncomes = await _plannedIncomesService.GetById(id);
        if (plannedIncomes is null)
        {
            return NotFound();
        }

        return Ok(plannedIncomes);
    }

    // POST api/<PlannedIncomesController>
    [HttpPost]
    public async Task<ActionResult<PlannedIncomesDto>> Create([FromBody] PlannedIncomesDto plannedIncomes)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!await _plannedIncomesService.Create(plannedIncomes))
        {
            return NotFound();
        }

        var createdItem = await _plannedIncomesService.GetById((await _plannedIncomesService.GetAllBudgetPlannedIncomes(plannedIncomes.BudgetId)).Max(x => x.Id));
        return Ok(createdItem);
    }

    // PUT api/<PlannedIncomesController>/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult<int>> Put(int id, [FromBody] PlannedIncomesDto plannedIncomes)
    {
        if (id != plannedIncomes.Id)
        {
            return BadRequest();
        }

        if (!await _plannedIncomesService.Update(plannedIncomes))
        {
            return NotFound();
        }

        return Ok(plannedIncomes.Id);
    }

    // DELETE api/<PlannedIncomesController>/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        if (!await _plannedIncomesService.Exists(id))
        {
            return NotFound();
        }

        await _plannedIncomesService.Delete(id);
        return Ok(id);
    }
}