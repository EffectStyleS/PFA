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
    public class ExpenseTypeController : ControllerBase
    {
        private readonly IExpenseTypeService _expenseTypeService;

        public ExpenseTypeController(IExpenseTypeService expenseTypeService)
        {
            _expenseTypeService = expenseTypeService;
        }

        // GET: api/<ExpenseTypeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseTypeDTO>>> GetAll()
        {
            var expenseTypes = (await _expenseTypeService.GetAll()).ToList();

            return Ok(expenseTypes);
        }

        // GET api/<ExpenseTypeController>/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseTypeDTO>> GetById(int id)
        {
            var expenseType = await _expenseTypeService.GetById(id);

            if (expenseType == null)
            {
                return NotFound();
            }

            return Ok(expenseType);
        }

        // POST api/<ExpenseTypeController>
        [HttpPost]
        public async Task<ActionResult<ExpenseTypeDTO>> Create([FromBody] ExpenseTypeDTO expenseType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _expenseTypeService.Create(expenseType))
            {
                return NotFound();
            }

            return Ok(expenseType);
        }

        // PUT api/<ExpenseTypeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] ExpenseTypeDTO expenseType)
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

        // DELETE api/<ExpenseTypeController>/5
        [HttpDelete("{id}")]
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
}
