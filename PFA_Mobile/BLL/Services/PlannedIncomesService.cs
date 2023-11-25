using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services.Abstract;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class PlannedIncomesService : BaseService, IPlannedIncomesService
    {
        public PlannedIncomesService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #region ICrud Implementation
        public async Task<bool> Create(PlannedIncomesDTO itemDTO)
        {
            var plannedIncomes = new PlannedIncomes()
            {
                Id           = itemDTO.Id,
                Sum          = itemDTO.Sum,
                BudgetId     = itemDTO.BudgetId,
                IncomeTypeId = itemDTO.IncomeTypeId,
            };

            await _unitOfWork.PlannedIncomes.Create(plannedIncomes);
            return await SaveAsync();
        }

        public async Task<bool> Delete(int id)
        {
            if (!await _unitOfWork.PlannedIncomes.Exists(id))
                return false;

            await _unitOfWork.PlannedIncomes.Delete(id);
            //if (result == false) // добавить лог недудачного удаления с id 
            return await SaveAsync();
        }

        public async Task<bool> Exists(int id) => await _unitOfWork.PlannedIncomes.Exists(id);

        public async Task<List<PlannedIncomesDTO>> GetAll()
        {
            var items = await _unitOfWork.PlannedIncomes.GetAll();

            var result = items
                .Select(x => new PlannedIncomesDTO(x))
                .ToList();

            return result;
        }

        public async Task<PlannedIncomesDTO> GetById(int id)
        {
            var item = await _unitOfWork.PlannedIncomes.GetItem(id);

            return item == null ? null : new PlannedIncomesDTO(item);
        }

        public async Task<bool> Update(PlannedIncomesDTO itemDTO)
        {
            if (!await _unitOfWork.PlannedIncomes.Exists(itemDTO.Id))
                return false;

            PlannedIncomes x = await _unitOfWork.PlannedIncomes.GetItem(itemDTO.Id);

            x.Id = itemDTO.Id;
            x.Sum = itemDTO.Sum;
            x.BudgetId = itemDTO.BudgetId;
            x.IncomeTypeId = itemDTO.IncomeTypeId;

            await _unitOfWork.PlannedIncomes.Update(x);
            return await SaveAsync();
        }
        #endregion

        public async Task<List<PlannedIncomesDTO>> GetAllBudgetPlannedIncomes(int budgetId)
        {
            var items = await _unitOfWork.PlannedIncomes.GetAll();

            var result = items
                .Select(x => new PlannedIncomesDTO(x))
                .Where(x => x.BudgetId == budgetId)
                .ToList();

            return result;
        }
    }
}
