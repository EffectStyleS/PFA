using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IPlannedExpensesService : ICrud<PlannedExpensesDTO>
    {
        Task<List<PlannedExpensesDTO>> GetAllBudgetPlannedExpenses(int budgetId);
    }
}
