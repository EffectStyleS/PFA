using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services;
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
    public class IncomeTypeController : ControllerBase
    {
        private readonly IIncomeTypeService _incomeTypeService;

        public IncomeTypeController(IIncomeTypeService incomeTypeService)
        {
            _incomeTypeService = incomeTypeService;
        }

        // GET: api/<IncomeTypeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncomeTypeDTO>>> GetAll()
        {
            var incomeTypes = (await _incomeTypeService.GetAll()).ToList();

            return Ok(incomeTypes);
        }

        // GET api/<IncomeTypeController>/id
        [HttpGet("{id}")]
        public async Task<ActionResult<IncomeTypeDTO>> GetById(int id)
        {
            var incomeType = await _incomeTypeService.GetById(id);

            if (incomeType == null)
            {
                return NotFound();
            }

            return Ok(incomeType);
        }

        // POST api/<IncomeTypeController>
        [HttpPost]
        public async Task<ActionResult<IncomeTypeDTO>> Create([FromBody] IncomeTypeDTO incomeType)
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

        // PUT api/<IncomeTypeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] IncomeTypeDTO incomeType)
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

        // DELETE api/<IncomeTypeController>/5
        [HttpDelete("{id}")]
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
}
