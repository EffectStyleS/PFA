using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services.Abstract;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class PlannedExpensesService : BaseService, IPlannedExpensesService
    {
        public PlannedExpensesService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #region ICrud Implementation
        public async Task<bool> Create(PlannedExpensesDTO itemDTO)
        {
            var plannedExpenses = new PlannedExpenses()
            {
                Id            = itemDTO.Id,
                Sum           = itemDTO.Sum,
                BudgetId      = itemDTO.BudgetId,
                ExpenseTypeId = itemDTO.ExpenseTypeId,
            };

            await _unitOfWork.PlannedExpenses.Create(plannedExpenses);
            return await SaveAsync();
        }

        public async Task<bool> Delete(int id)
        {
            if (!await _unitOfWork.PlannedExpenses.Exists(id))
                return false;

            await _unitOfWork.PlannedExpenses.Delete(id);
            //if (result == false) // добавить лог недудачного удаления с id 
            return await SaveAsync();
        }

        public async Task<bool> Exists(int id) => await _unitOfWork.PlannedExpenses.Exists(id);

        public async Task<List<PlannedExpensesDTO>> GetAll()
        {
            var items = await _unitOfWork.PlannedExpenses.GetAll();

            var result = items
                .Select(x => new PlannedExpensesDTO(x))
                .ToList();

            return result;
        }

        public async Task<PlannedExpensesDTO> GetById(int id)
        {
            var item = await _unitOfWork.PlannedExpenses.GetItem(id);

            return item == null ? null : new PlannedExpensesDTO(item);
        }

        public async Task<bool> Update(PlannedExpensesDTO itemDTO)
        {
            if (!await _unitOfWork.PlannedExpenses.Exists(itemDTO.Id))
                return false;

            PlannedExpenses x = await _unitOfWork.PlannedExpenses.GetItem(itemDTO.Id);

            x.Id            = itemDTO.Id;
            x.Sum           = itemDTO.Sum;
            x.BudgetId      = itemDTO.BudgetId;
            x.ExpenseTypeId = itemDTO.ExpenseTypeId;

            await _unitOfWork.PlannedExpenses.Update(x);
            return await SaveAsync();
        }
        #endregion

        public async Task<List<PlannedExpensesDTO>> GetAllBudgetPlannedExpenses(int budgetId)
        {
            var items = await _unitOfWork.PlannedExpenses.GetAll();

            var result = items
                .Select(x => new PlannedExpensesDTO(x))
                .Where(x => x.BudgetId == budgetId)
                .ToList();

            return result;
        }
    }
}
