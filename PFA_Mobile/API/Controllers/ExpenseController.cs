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
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        // GET: api/<ExpenseController>/user/userId
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ExpenseDTO>>> GetAllUserExpenses(int userId)
        {
            var expenses = (await _expenseService.GetAllUserExpenses(userId)).ToList();

            return Ok(expenses);
        }

        // GET api/<ExpenseController>/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseDTO>> GetById(int id)
        {
            var expense = await _expenseService.GetById(id);

            if (expense == null)
            {
                return NotFound();
            }

            return Ok(expense);
        }

        // POST api/<ExpenseController>
        [HttpPost]
        public async Task<ActionResult<ExpenseDTO>> Create([FromBody] ExpenseDTO expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _expenseService.Create(expense))
            {
                return NotFound();
            }

            var createdItem = await _expenseService.GetById((await _expenseService.GetAllUserExpenses(expense.UserId)).Max(x => x.Id));
            return Ok(createdItem);
        }

        // PUT api/<ExpenseController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] ExpenseDTO expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }

            if (!await _expenseService.Update(expense))
            {
                return NotFound();
            }

            return Ok(expense.Id);
        }

        // DELETE api/<ExpenseController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            if (!await _expenseService.Exists(id))
            {
                return NotFound();
            }

            await _expenseService.Delete(id);
            return Ok(id);
        }
    }
}
