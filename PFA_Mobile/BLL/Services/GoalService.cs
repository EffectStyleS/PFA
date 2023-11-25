using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services.Abstract;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class GoalService : BaseService, IGoalService
    {
        public GoalService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #region ICrud Implementation
        public async Task<bool> Create(GoalDTO itemDTO)
        {
            var goal = new Goal()
            {
                Id          = itemDTO.Id,
                Name        = itemDTO.Name,
                StartDate   = itemDTO.StartDate,
                EndDate     = itemDTO.EndDate,
                Sum         = itemDTO.Sum,
                IsCompleted = itemDTO.IsCompleted,
                UserId      = itemDTO.UserId,
            };

            await _unitOfWork.Goal.Create(goal);
            return await SaveAsync();
        }

        public async Task<bool> Delete(int id)
        {
            if (!await _unitOfWork.Goal.Exists(id))
                return false;

            await _unitOfWork.Goal.Delete(id);
            //if (result == false) // добавить лог недудачного удаления с id 
            return await SaveAsync();
        }

        public async Task<bool> Exists(int id) => await _unitOfWork.Goal.Exists(id);

        public async Task<List<GoalDTO>> GetAll()
        {
            var items = await _unitOfWork.Goal.GetAll();

            var result = items
                .Select(x => new GoalDTO(x))
                .ToList();

            return result;
        }

        public async Task<GoalDTO> GetById(int id)
        {
            var item = await _unitOfWork.Goal.GetItem(id);

            return item == null ? null : new GoalDTO(item);
        }

        public async Task<bool> Update(GoalDTO itemDTO)
        {
            if (!await _unitOfWork.Goal.Exists(itemDTO.Id))
                return false;

            Goal x = await _unitOfWork.Goal.GetItem(itemDTO.Id);
            
            x.Id          = itemDTO.Id;
            x.Name        = itemDTO.Name;
            x.StartDate   = itemDTO.StartDate;
            x.EndDate     = itemDTO.EndDate;
            x.Sum         = itemDTO.Sum;
            x.IsCompleted = itemDTO.IsCompleted;
            x.UserId      = itemDTO.UserId;

            await _unitOfWork.Goal.Update(x);
            return await SaveAsync();
        }
        #endregion

        public async Task<List<GoalDTO>> GetAllUserGoals(int userId)
        {
            var items = await _unitOfWork.Goal.GetAll();

            var result = items
                .Select(x => new GoalDTO(x))
                .Where (x => x.UserId == userId)
                .ToList();

            return result;
        }

    }
}
