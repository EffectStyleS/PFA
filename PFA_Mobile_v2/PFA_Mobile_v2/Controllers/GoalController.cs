using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;

namespace PFA_Mobile_v2.Controllers;

/// <summary>
/// Контроллер целей
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[EnableCors]
[Authorize]
public class GoalController : ControllerBase
{
    private readonly IGoalService _goalService;

    /// <summary>
    /// Контроллер целей
    /// </summary>
    public GoalController(IGoalService goalService)
    {
        _goalService = goalService;
    }

    // GET: api/<GoalController>/user/userId
    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<IEnumerable<GoalDto>>> GetAllUserGoals(int userId)
    {
        var goals = (await _goalService.GetAllUserGoals(userId)).ToList();
        return Ok(goals);
    }

    // GET api/<GoalController>/id
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GoalDto>> GetById(int id)
    {
        var goal = await _goalService.GetById(id);
        if (goal is null)
        {
            return NotFound();
        }

        return Ok(goal);
    }

    // POST api/<GoalController>
    [HttpPost]
    public async Task<ActionResult<GoalDto>> Create([FromBody] GoalDto goal)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!await _goalService.Create(goal))
        {
            return NotFound();
        }

        var createdItem = await _goalService.GetById((await _goalService.GetAllUserGoals(goal.UserId)).Max(x => x.Id));
        return Ok(createdItem);
    }

    // PUT api/<GoalController>/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult<int>> Put(int id, [FromBody] GoalDto goal)
    {
        if (id != goal.Id)
        {
            return BadRequest();
        }

        if (!await _goalService.Update(goal))
        {
            return NotFound();
        }

        return Ok(goal.Id);
    }

    // DELETE api/<GoalController>/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        if (!await _goalService.Exists(id))
        {
            return NotFound();
        }

        await _goalService.Delete(id);
        return Ok(id);
    }
}