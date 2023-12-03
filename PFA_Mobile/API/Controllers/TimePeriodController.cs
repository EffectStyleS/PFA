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
    public class TimePeriodController : ControllerBase
    {
        private readonly ITimePeriodService _timePeriodService;

        public TimePeriodController(ITimePeriodService timePeriodService)
        {
            _timePeriodService = timePeriodService;
        }

        // GET: api/<TimePeriodController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimePeriodDTO>>> GetAll()
        {
            var timePeriods = (await _timePeriodService.GetAll()).ToList();

            return Ok(timePeriods);
        }

        // GET api/<TimePeriodController>/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TimePeriodDTO>> GetById(int id)
        {
            var timePeriod = await _timePeriodService.GetById(id);

            if (timePeriod == null)
            {
                return NotFound();
            }

            return Ok(timePeriod);
        }

        // POST api/<TimePeriodController>
        [HttpPost]
        public async Task<ActionResult<TimePeriodDTO>> Create([FromBody] TimePeriodDTO timePeriod)
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
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] TimePeriodDTO timePeriod)
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
        [HttpDelete("{id}")]
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
}
