using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services.Abstract;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class BudgetService : BaseService, IBudgetService
    {
        public BudgetService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #region ICrud Implementation
        public async Task<bool> Create(BudgetDTO itemDTO)
        {
            var budget = new Budget()
            {
                Id              = itemDTO.Id,
                StartDate       = itemDTO.StartDate,
                TimePeriodId    = itemDTO.TimePeriodId,
                UserId          = itemDTO.UserId,
            };

            await _unitOfWork.Budget.Create(budget);
            return await SaveAsync();
        }

        public async Task<bool> Delete(int id)
        {
            if (!await _unitOfWork.Budget.Exists(id))
                return false;

            // Сначала удаляем запланированные расходы и запланированные доходы
            var plannedExpenses = await _unitOfWork.PlannedExpenses.GetAll();
            foreach (var budgetPlannedExpenses in plannedExpenses.Where(x => x.BudgetId ==id).ToList())
            {
                /*var result = */await _unitOfWork.PlannedExpenses.Delete(budgetPlannedExpenses.Id);
                //if (result == false) // добавить лог недудачного удаления с id 
            }

            var plannedIncomes = await _unitOfWork.PlannedIncomes.GetAll();
            foreach (var budgetPlannedIncomes in plannedIncomes.Where(x => x.BudgetId == id).ToList())
            {
                await _unitOfWork.PlannedIncomes.Delete(budgetPlannedIncomes.Id);
                //if (result == false) // добавить лог недудачного удаления с id 
            }

            await _unitOfWork.Budget.Delete(id);
            //if (result == false) // добавить лог недудачного удаления с id 
            return await SaveAsync();
        }

        public async Task<bool> Exists(int id) => await _unitOfWork.Budget.Exists(id);

        public async Task<List<BudgetDTO>> GetAll()
        {
            var items = await _unitOfWork.Budget.GetAll();

            var result = items
                .Select(x => new BudgetDTO(x))
                .ToList();

            return result;
        }

        public async Task<BudgetDTO> GetById(int id)
        {
            var item = await _unitOfWork.Budget.GetItem(id);

            return item == null ? null : new BudgetDTO(item);
        }

        public async Task<bool> Update(BudgetDTO itemDTO)
        {
            if (!await _unitOfWork.Budget.Exists(itemDTO.Id))
                return false;

            Budget b = await _unitOfWork.Budget.GetItem(itemDTO.Id);

            b.Id              = itemDTO.Id;
            b.UserId          = itemDTO.UserId;
            b.StartDate       = itemDTO.StartDate;
            b.TimePeriodId    = itemDTO.TimePeriodId;

            await _unitOfWork.Budget.Update(b);
            return await SaveAsync();
        }
        #endregion

        public async Task<List<BudgetDTO>> GetAllUserBudgets(int userId)
        {
            var items = await _unitOfWork.Budget.GetAll();

            var result = items
                .Select(x => new BudgetDTO(x))
                .Where(x => x.UserId == userId)
                .ToList();

            return result;
        }
    }
}
