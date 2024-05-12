using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;

namespace PFA_Mobile_v2.Controllers;

/// <summary>
/// Контроллер доходов
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[EnableCors]
[Authorize]
public class IncomeController : ControllerBase
{
    private readonly IIncomeService _incomeService;
    
    /// <summary>
    /// Контроллер доходов
    /// </summary>
    public IncomeController(IIncomeService incomeService)
    {
        _incomeService = incomeService;
    }

    /// <summary>
    /// Получает все доходы пользователя
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    /// <returns></returns>
    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<IEnumerable<IncomeDto>>> GetAllUserIncomes(int userId)
    {
        var incomes = (await _incomeService.GetAllUserIncomes(userId)).ToList();
        return Ok(incomes);
    }

    /// <summary>
    /// Получает доход по Id
    /// </summary>
    /// <param name="id">Id дохода</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<IncomeDto>> GetById(int id)
    {
        var income = await _incomeService.GetById(id);
        if (income is null)
        {
            return NotFound();
        }

        return Ok(income);
    }

    /// <summary>
    /// Создает доход
    /// </summary>
    /// <param name="income">Доход</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<IncomeDto>> Create([FromBody] IncomeDto income)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!await _incomeService.Create(income))
        {
            return NotFound();
        }

        var createdItem = await _incomeService.GetById((await _incomeService.GetAllUserIncomes(income.UserId)).Max(x => x.Id));
        return Ok(createdItem);
    }

    /// <summary>
    /// Обновляет данные дохода
    /// </summary>
    /// <param name="id">Id дохода</param>
    /// <param name="income">Доход</param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<int>> Put(int id, [FromBody] IncomeDto income)
    {
        if (id != income.Id)
        {
            return BadRequest();
        }

        if (!await _incomeService.Update(income))
        {
            return NotFound();
        }

        return Ok(income.Id);
    }

    /// <summary>
    /// Удаляет доход
    /// </summary>
    /// <param name="id">Id дохода</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        if (!await _incomeService.Exists(id))
        {
            return NotFound();
        }

        await _incomeService.Delete(id);
        return Ok(id);
    }
}