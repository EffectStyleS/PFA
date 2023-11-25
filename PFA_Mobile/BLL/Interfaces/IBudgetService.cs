using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IBudgetService : ICrud<BudgetDTO>
    {
        Task<List<BudgetDTO>> GetAllUserBudgets(int userId);
    }
}
