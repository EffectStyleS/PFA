using client.Model.Interfaces;
using client.Model.Models;

namespace client.Model.Services;

/// <summary>
/// Сервис бюджета
/// </summary>
public class BudgetService : IBudgetService
{
    /// <inheritdoc />
    public decimal? GetBalance(BudgetModel budget)
    {
        var sumOfPlannedExpenses = budget.PlannedExpenses.Sum(x => x.Sum);
        var sumOfPlannedIncomes = budget.PlannedIncomes.Sum(x => x.Sum);

        return sumOfPlannedIncomes - sumOfPlannedExpenses;
    }
}