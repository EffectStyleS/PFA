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
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        // GET: api/<IncomeController>/user/userId
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<IncomeDTO>>> GetAllUserIncomes(int userId)
        {
            var incomes = (await _incomeService.GetAllUserIncomes(userId)).ToList();

            return Ok(incomes);
        }

        // GET api/<IncomeController>/id
        [HttpGet("{id}")]
        public async Task<ActionResult<IncomeDTO>> GetById(int id)
        {
            var income = await _incomeService.GetById(id);

            if (income == null)
            {
                return NotFound();
            }

            return Ok(income);
        }

        // POST api/<IncomeController>
        [HttpPost]
        public async Task<ActionResult<IncomeDTO>> Create([FromBody] IncomeDTO income)
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

        // PUT api/<IncomeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] IncomeDTO income)
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

        // DELETE api/<IncomeController>/5
        [HttpDelete("{id}")]
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
}
