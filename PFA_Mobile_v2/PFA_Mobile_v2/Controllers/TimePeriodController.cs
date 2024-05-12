using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;

namespace PFA_Mobile_v2.Controllers;

/// <summary>
/// Контроллер временных периодов
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[EnableCors]
[Authorize]
public class TimePeriodController : ControllerBase
{
    private readonly ITimePeriodService _timePeriodService;

    /// <summary>
    /// Контроллер временных периодов
    /// </summary>
    public TimePeriodController(ITimePeriodService timePeriodService)
    {
        _timePeriodService = timePeriodService;
    }

    /// <summary>
    /// Получает все временные периоды
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TimePeriodDto>>> GetAll()
    {
        var timePeriods = (await _timePeriodService.GetAll()).ToList();
        return Ok(timePeriods);
    }

    /// <summary>
    /// Получает временной период по Id
    /// </summary>
    /// <param name="id">Id временного периода</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TimePeriodDto>> GetById(int id)
    {
        var timePeriod = await _timePeriodService.GetById(id);
        if (timePeriod is null)
        {
            return NotFound();
        }

        return Ok(timePeriod);
    }

    /// <summary>
    /// Создает временной период
    /// </summary>
    /// <param name="timePeriod">Временной период</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<TimePeriodDto>> Create([FromBody] TimePeriodDto timePeriod)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!await _timePeriodService.Create(timePeriod))
        {
            return NotFound();
        }

        var createdItem = await _timePeriodService.GetById((await _timePeriodService.GetAll()).Max(x => x.Id));
        return Ok(createdItem);
    }

    /// <summary>
    /// Обновляет данные временного периода
    /// </summary>
    /// <param name="id">Id временного периода</param>
    /// <param name="timePeriod">Временной период</param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<int>> Put(int id, [FromBody] TimePeriodDto timePeriod)
    {
        if (id != timePeriod.Id)
        {
            return BadRequest();
        }

        if (!await _timePeriodService.Update(timePeriod))
        {
            return NotFound();
        }

        return Ok(timePeriod.Id);
    }

    /// <summary>
    /// Удаляет временной период
    /// </summary>
    /// <param name="id">Id временного периода</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        if (!await _timePeriodService.Exists(id))
        {
            return NotFound();
        }

        await _timePeriodService.Delete(id);
        return Ok(id);
    }
}