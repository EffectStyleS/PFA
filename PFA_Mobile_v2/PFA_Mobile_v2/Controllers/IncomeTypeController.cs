using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;

namespace PFA_Mobile_v2.Controllers;

/// <summary>
/// Контроллер типов доходов
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[EnableCors]
[Authorize]
public class IncomeTypeController : ControllerBase
{
    private readonly IIncomeTypeService _incomeTypeService;

    /// <summary>
    /// Контроллер типов доходов
    /// </summary>
    public IncomeTypeController(IIncomeTypeService incomeTypeService)
    {
        _incomeTypeService = incomeTypeService;
    }

    /// <summary>
    /// Получает все типы доходов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IncomeTypeDto>>> GetAll()
    {
        var incomeTypes = (await _incomeTypeService.GetAll()).ToList();
        return Ok(incomeTypes);
    }

    /// <summary>
    /// Получает тип дохода по Id
    /// </summary>
    /// <param name="id">Id дохода</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<IncomeTypeDto>> GetById(int id)
    {
        var incomeType = await _incomeTypeService.GetById(id);
        if (incomeType is null)
        {
            return NotFound();
        }

        return Ok(incomeType);
    }

    /// <summary>
    /// Создает тип дохода
    /// </summary>
    /// <param name="incomeType">Тип дохода</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<IncomeTypeDto>> Create([FromBody] IncomeTypeDto incomeType)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!await _incomeTypeService.Create(incomeType))
        {
            return NotFound();
        }

        var createdItem = await _incomeTypeService.GetById((await _incomeTypeService.GetAll()).Max(x => x.Id));
        return Ok(createdItem);
    }

    /// <summary>
    /// Обновляет данные типа дохода
    /// </summary>
    /// <param name="id">Id типа дохода</param>
    /// <param name="incomeType">Тип дохода</param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<int>> Put(int id, [FromBody] IncomeTypeDto incomeType)
    {
        if (id != incomeType.Id)
        {
            return BadRequest();
        }

        if (!await _incomeTypeService.Update(incomeType))
        {
            return NotFound();
        }

        return Ok(incomeType.Id);
    }

    /// <summary>
    /// Удаляет тип дохода
    /// </summary>
    /// <param name="id">Id типа дохода</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        if (!await _incomeTypeService.Exists(id))
        {
            return NotFound();
        }

        await _incomeTypeService.Delete(id);
        return Ok(id);
    }
}