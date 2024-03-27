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

    // GET: api/<TimePeriodController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TimePeriodDto>>> GetAll()
    {
        var timePeriods = (await _timePeriodService.GetAll()).ToList();
        return Ok(timePeriods);
    }

    // GET api/<TimePeriodController>/id
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

    // POST api/<TimePeriodController>
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

    // PUT api/<TimePeriodController>/5
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

    // DELETE api/<TimePeriodController>/5
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