using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors]
    [Authorize]
    public class GoalController : ControllerBase
    {
        private readonly IGoalService _goalService;

        public GoalController(IGoalService goalService)
        {
            _goalService = goalService;
        }

        // GET: api/<GoalController>/user/userId
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<GoalDTO>>> GetAllUserGoals(int userId)
        {
            var goals = (await _goalService.GetAllUserGoals(userId)).ToList();

            return Ok(goals);
        }

        // GET api/<GoalController>/id
        [HttpGet("{id}")]
        public async Task<ActionResult<GoalDTO>> GetById(int id)
        {
            var goal = await _goalService.GetById(id);

            if (goal == null)
            {
                return NotFound();
            }

            return Ok(goal);
        }

        // POST api/<GoalController>
        [HttpPost]
        public async Task<ActionResult<GoalDTO>> Create([FromBody] GoalDTO goal)
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
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] GoalDTO goal)
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
        [HttpDelete("{id}")]
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
}
