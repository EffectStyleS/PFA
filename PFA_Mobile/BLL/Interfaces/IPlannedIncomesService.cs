using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IPlannedIncomesService : ICrud<PlannedIncomesDTO>
    {
        Task<List<PlannedIncomesDTO>> GetAllBudgetPlannedIncomes(int budgetId);
    }
}
