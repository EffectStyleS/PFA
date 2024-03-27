using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;

namespace PFA_Mobile_v2.Controllers;

/// <summary>
/// Контроллер типов расходов
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[EnableCors]
[Authorize]
public class ExpenseTypeController : ControllerBase
{
    private readonly IExpenseTypeService _expenseTypeService;

    /// <summary>
    /// Контроллер типов расходов
    /// </summary>
    public ExpenseTypeController(IExpenseTypeService expenseTypeService)
    {
        _expenseTypeService = expenseTypeService;
    }

    // GET: api/<ExpenseTypeController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExpenseTypeDto>>> GetAll()
    {
        var expenseTypes = (await _expenseTypeService.GetAll()).ToList();
        return Ok(expenseTypes);
    }

    // GET api/<ExpenseTypeController>/id
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ExpenseTypeDto>> GetById(int id)
    {
        var expenseType = await _expenseTypeService.GetById(id);
        if (expenseType is null)
        {
            return NotFound();
        }

        return Ok(expenseType);
    }

    // POST api/<ExpenseTypeController>
    [HttpPost]
    public async Task<ActionResult<ExpenseTypeDto>> Create([FromBody] ExpenseTypeDto expenseType)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!await _expenseTypeService.Create(expenseType))
        {
            return NotFound();
        }

        var createdItem = await _expenseTypeService.GetById((await _expenseTypeService.GetAll()).Max(x => x.Id));
        return Ok(createdItem);
    }

    // PUT api/<ExpenseTypeController>/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult<int>> Put(int id, [FromBody] ExpenseTypeDto expenseType)
    {
        if (id != expenseType.Id)
        {
            return BadRequest();
        }

        if (!await _expenseTypeService.Update(expenseType))
        {
            return NotFound();
        }

        return Ok(expenseType.Id);
    }

    // DELETE api/<ExpenseTypeController>/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        if (!await _expenseTypeService.Exists(id))
        {
            return NotFound();
        }

        await _expenseTypeService.Delete(id);
        return Ok(id);
    }
}