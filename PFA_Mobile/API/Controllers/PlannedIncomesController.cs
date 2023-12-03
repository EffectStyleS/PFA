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
    public class PlannedIncomesController : ControllerBase
    {
        private readonly IPlannedIncomesService _plannedIncomesService;

        public PlannedIncomesController(IPlannedIncomesService plannedIncomesService)
        {
            _plannedIncomesService = plannedIncomesService;
        }

        // GET: api/<PlannedIncomesController>/budget/budgetId
        [HttpGet("budget/{budgetId}")]
        public async Task<ActionResult<IEnumerable<PlannedIncomesDTO>>> GetAllBudgetPlannedIncomes(int budgetId)
        {
            var plannedIncomes = (await _plannedIncomesService.GetAllBudgetPlannedIncomes(budgetId)).ToList();

            return Ok(plannedIncomes);
        }

        // GET api/<PlannedIncomesController>/id
        [HttpGet("{id}")]
        public async Task<ActionResult<PlannedIncomesDTO>> GetById(int id)
        {
            var plannedIncomes = await _plannedIncomesService.GetById(id);

            if (plannedIncomes == null)
            {
                return NotFound();
            }

            return Ok(plannedIncomes);
        }

        // POST api/<PlannedIncomesController>
        [HttpPost]
        public async Task<ActionResult<PlannedIncomesDTO>> Create([FromBody] PlannedIncomesDTO plannedIncomes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _plannedIncomesService.Create(plannedIncomes))
            {
                return NotFound();
            }

            var createdItem = await _plannedIncomesService.GetById((await _plannedIncomesService.GetAllBudgetPlannedIncomes(plannedIncomes.BudgetId)).Max(x => x.Id));
            return Ok(createdItem);
        }

        // PUT api/<PlannedIncomesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] PlannedIncomesDTO plannedIncomes)
        {
            if (id != plannedIncomes.Id)
            {
                return BadRequest();
            }

            if (!await _plannedIncomesService.Update(plannedIncomes))
            {
                return NotFound();
            }

            return Ok(plannedIncomes.Id);
        }

        // DELETE api/<PlannedIncomesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            if (!await _plannedIncomesService.Exists(id))
            {
                return NotFound();
            }

            await _plannedIncomesService.Delete(id);
            return Ok(id);
        }
    }
}
