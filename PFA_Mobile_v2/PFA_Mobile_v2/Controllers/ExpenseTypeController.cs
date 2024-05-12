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

    /// <summary>
    /// Получает все типы расходов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExpenseTypeDto>>> GetAll()
    {
        var expenseTypes = (await _expenseTypeService.GetAll()).ToList();
        return Ok(expenseTypes);
    }

    /// <summary>
    /// Получает тип расхода по Id
    /// </summary>
    /// <param name="id">Id типа расхода</param>
    /// <returns></returns>
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

    /// <summary>
    /// Создает тип расхода
    /// </summary>
    /// <param name="expenseType">Тип расхода</param>
    /// <returns></returns>
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

    /// <summary>
    /// Обновляет данные типа расхода
    /// </summary>
    /// <param name="id">Id типа расхода</param>
    /// <param name="expenseType">Тип расхода</param>
    /// <returns></returns>
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

    /// <summary>
    /// Удаляет тип расхода
    /// </summary>
    /// <param name="id">Id типа расхода</param>
    /// <returns></returns>
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