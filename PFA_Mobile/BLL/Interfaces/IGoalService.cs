using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IGoalService : ICrud<GoalDTO>
    {
        Task<List<GoalDTO>> GetAllUserGoals(int userId);
    }
}
