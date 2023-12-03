using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.DTOs;
using API.RequestsModels;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors]
    [Authorize]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;
        private readonly IPlannedExpensesService _plannedExpensesService;
        private readonly IPlannedIncomesService _plannedIncomesService;
        
        public BudgetController(IBudgetService budgetService, IPlannedExpensesService plannedExpensesService, IPlannedIncomesService plannedIncomesService)
        {
            _budgetService = budgetService;
            _plannedExpensesService = plannedExpensesService;
            _plannedIncomesService = plannedIncomesService;
        }

        // GET: api/<BudgetController>/user/userId
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<BudgetDTO>>> GetAllUserBudgets(int userId)
        {
            var budgets = (await _budgetService.GetAllUserBudgets(userId)).ToList();

            return Ok(budgets);
        }

        // GET api/<BudgetController>/id
        [HttpGet("{id}")]
        public Task<BudgetDTO> GetById(int id)
        {
            return _budgetService.GetById(id);
        }

        // POST api/<BudgetController>
        [HttpPost]
        public async Task<ActionResult<BudgetRequestModel>> Create([FromBody]BudgetRequestModel budget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _budgetService.Create(budget.Budget))
            {
                return NotFound();
            }

            var budgetId = (await _budgetService.GetById((await _budgetService.GetAllUserBudgets(budget.Budget.UserId)).Max(x => x.Id))).Id;
            foreach (var plannedExpenses in budget.PlannedExpenses)
            {
                plannedExpenses.BudgetId = budgetId;
                if (!await _plannedExpensesService.Create(plannedExpenses))
                {
                    return NotFound();
                }
            }

            foreach (var plannedIncomes in budget.PlannedIncomes)
            {
                plannedIncomes.BudgetId = budgetId;
                if (!await _plannedIncomesService.Create(plannedIncomes))
                {
                    return NotFound();
                }
            }

            var createdBudgetRequestModel = new BudgetRequestModel()
            {
                Budget = await _budgetService.GetById((await _budgetService.GetAllUserBudgets(budget.Budget.UserId)).Max(x => x.Id)),
                PlannedExpenses = await _plannedExpensesService.GetAllBudgetPlannedExpenses(budget.Budget.UserId),
                PlannedIncomes = await _plannedIncomesService.GetAllBudgetPlannedIncomes(budget.Budget.UserId)
            };

            return Ok(createdBudgetRequestModel);
        }

        // PUT api/<BudgetController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody]BudgetRequestModel budget)
        {
            if (id != budget.Budget.Id)
            {
                return BadRequest();
            }

            if (!await _budgetService.Update(budget.Budget))
            {
                return NotFound();
            }

            foreach (var plannedExpenses in budget.PlannedExpenses)
            {
                if (!await _plannedExpensesService.Update(plannedExpenses))
                {
                    return NotFound();
                }
            }

            foreach (var plannedIncomes in budget.PlannedIncomes)
            {
                if (!await _plannedIncomesService.Update(plannedIncomes))
                {
                    return NotFound();
                }
            }

            return Ok(budget.Budget.Id);
        }

        // DELETE api/<BudgetController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            if (!await _budgetService.Exists(id))
            {
                return NotFound();
            }

            await _budgetService.Delete(id);
            return Ok(id);
        }
    }
}

