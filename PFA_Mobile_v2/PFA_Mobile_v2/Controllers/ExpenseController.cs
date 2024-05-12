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

    /// <summary>
    /// Получает все расходы пользователя
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    /// <returns></returns>
    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetAllUserExpenses(int userId)
    {
        var expenses = (await _expenseService.GetAllUserExpenses(userId)).ToList();
        return Ok(expenses);
    }

    /// <summary>
    /// Получает расход по Id
    /// </summary>
    /// <param name="id">Id расхода</param>
    /// <returns></returns>
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

    /// <summary>
    /// Создает расход
    /// </summary>
    /// <param name="expense">Расход</param>
    /// <returns></returns>
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

    /// <summary>
    /// Обновляет данные расхода
    /// </summary>
    /// <param name="id">Id расхода</param>
    /// <param name="expense">Расход</param>
    /// <returns></returns>
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

    /// <summary>
    /// Удаляет расход
    /// </summary>
    /// <param name="id">Id расхода</param>
    /// <returns></returns>
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

    /// <summary>
    /// Получает превышения бюджетов пользователя
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    /// <returns></returns>
    [HttpGet("difference/{userId:int}")]
    public async Task<ActionResult<List<BudgetOverrunDto>>> GetBudgetsOverruns(int userId)
    {
        var overruns = await _expenseService.GetBudgetOverruns(userId);
        return Ok(overruns);
    }
}