using client.Model.Models;

namespace client.Model.Interfaces
{
    public interface IBudgetService
    {
        decimal? GetSaldo(BudgetModel budget);
    }
}
