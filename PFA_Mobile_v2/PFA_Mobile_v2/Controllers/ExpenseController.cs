using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;

namespace PFA_Mobile_v2.Controllers;

/// <summary>
/// Контроллер расходов
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[EnableCors]
[Authorize]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    /// <summary>
    /// Контроллер расходов
    /// </summary>
    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    // GET: api/<ExpenseController>/user/userId
    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetAllUserExpenses(int userId)
    {
        var expenses = (await _expenseService.GetAllUserExpenses(userId)).ToList();
        return Ok(expenses);
    }

    // GET api/<ExpenseController>/id
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ExpenseDto>> GetById(int id)
    {
        var expense = await _expenseService.GetById(id);
        if (expense is null)
        {
            return NotFound();
        }

        return Ok(expense);
    }

    // POST api/<ExpenseController>
    [HttpPost]
    public async Task<ActionResult<ExpenseDto>> Create([FromBody] ExpenseDto expense)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!await _expenseService.Create(expense))
        {
            return NotFound();
        }

        var createdItem =
            await _expenseService.GetById((await _expenseService.GetAllUserExpenses(expense.UserId)).Max(x => x.Id));
        
        return Ok(createdItem);
    }

    // PUT api/<ExpenseController>/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult<int>> Put(int id, [FromBody] ExpenseDto expense)
    {
        if (id != expense.Id)
        {
            return BadRequest();
        }

        if (!await _expenseService.Update(expense))
        {
            return NotFound();
        }

        return Ok(expense.Id);
    }

    // DELETE api/<ExpenseController>/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        if (!await _expenseService.Exists(id))
        {
            return NotFound();
        }

        await _expenseService.Delete(id);
        return Ok(id);
    }

    // GET api/<ExpenseController>/difference/userId
    [HttpGet("difference/{userId:int}")]
    public async Task<ActionResult<List<BudgetOverrunDto>>> GetBudgetsOverruns(int userId)
    {
        var overruns = await _expenseService.GetBudgetOverruns(userId);
        return Ok(overruns);
    }
}