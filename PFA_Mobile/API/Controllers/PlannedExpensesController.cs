using BLL.DTOs;
using BLL.Interfaces;
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
    public class PlannedExpensesController : ControllerBase
    {
        private readonly IPlannedExpensesService _plannedExpensesService;

        public PlannedExpensesController(IPlannedExpensesService plannedExpensesService)
        {
            _plannedExpensesService = plannedExpensesService;
        }

        // GET: api/<PlannedExpensesController>/budget/budgetId
        [HttpGet("budget/{budgetId}")]
        public async Task<ActionResult<IEnumerable<PlannedExpensesDTO>>> GetAllBudgetPlannedExpenses(int budgetId)
        {
            var plannedExpenses = (await _plannedExpensesService.GetAllBudgetPlannedExpenses(budgetId)).ToList();

            return Ok(plannedExpenses);
        }

        // GET api/<PlannedExpensesController>/id
        [HttpGet("{id}")]
        public async Task<ActionResult<PlannedExpensesDTO>> GetById(int id)
        {
            var plannedExpenses = await _plannedExpensesService.GetById(id);

            if (plannedExpenses == null)
            {
                return NotFound();
            }

            return Ok(plannedExpenses);
        }

        // POST api/<PlannedExpensesController>
        [HttpPost]
        public async Task<ActionResult<PlannedExpensesDTO>> Create([FromBody] PlannedExpensesDTO plannedExpenses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _plannedExpensesService.Create(plannedExpenses))
            {
                return NotFound();
            }

            return Ok(plannedExpenses);
        }

        // PUT api/<PlannedExpensesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] PlannedExpensesDTO plannedExpenses)
        {
            if (id != plannedExpenses.Id)
            {
                return BadRequest();
            }

            if (!await _plannedExpensesService.Update(plannedExpenses))
            {
                return NotFound();
            }

            return Ok(plannedExpenses.Id);
        }

        // DELETE api/<PlannedExpensesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            if (!await _plannedExpensesService.Exists(id))
            {
                return NotFound();
            }

            await _plannedExpensesService.Delete(id);
            return Ok(id);
        }
    }
}
