using client.Model.Interfaces;
using client.Model.Models;

namespace client.Model.Services
{
    public class BudgetService : IBudgetService
    {
        public decimal? GetSaldo(BudgetModel budget)
        {
            var sumOfPlannedExpenses = budget.PlannedExpenses.Sum(x => x.Sum);
            var sumOfPlannedIncomes = budget.PlannedIncomes.Sum(x => x.Sum);

            return sumOfPlannedIncomes - sumOfPlannedExpenses;
        }
    }
}
