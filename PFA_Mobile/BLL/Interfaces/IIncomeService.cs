using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IIncomeService : ICrud<IncomeDTO>
    {
        Task<List<IncomeDTO>> GetAllUserIncomes(int userId);
    }
}
