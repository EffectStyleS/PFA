using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IExpenseService : ICrud<ExpenseDTO>
    {
        Task<List<ExpenseDTO>> GetAllUserExpenses(int userId);

        Task<List<BudgetOverrunDTO>> GetBudgetOverruns(int userId);
    }
}
