using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services.Abstract;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ExpenseTypeService : BaseService, IExpenseTypeService
    {
        public ExpenseTypeService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #region ICrud Implementation
        public async Task<bool> Create(ExpenseTypeDTO itemDTO)
        {
            var expenseType = new ExpenseType()
            {
                Id   = itemDTO.Id,
                Name = itemDTO.Name,
            };

            await _unitOfWork.ExpenseType.Create(expenseType);
            return await SaveAsync();
        }

        public async Task<bool> Delete(int id)
        {
            if (!await _unitOfWork.ExpenseType.Exists(id))
                return false;

            // Тип "Другое" - неудаляемый,
            // TODO: сделать на клиенте неудаляемым
            var expenseType = await _unitOfWork.ExpenseType.GetItem(id);
            if (expenseType.Name == "Другое")
            {
                //if (result == false) // добавить лог недудачного удаления "Другое"
                return false;
            }

            // Заменяем ExpenseTypeId у расходов и
            // запланированных расходов на id типа расхода "Другое"
            var allExpenseTypes = await _unitOfWork.ExpenseType.GetAll();
            var typeOtherId = allExpenseTypes.FirstOrDefault(x => x.Name == "Другое").Id; // тип "Другое" всегда будет найден

            var thisExpenseTypeExpenses = await _unitOfWork.Expense.GetAll();
            foreach (var expense in thisExpenseTypeExpenses.Where(x => x.ExpenseTypeId == id).ToList())
            {
                expense.ExpenseTypeId = typeOtherId;
            }

            var thisExpenseTypePlannedExpenses = await _unitOfWork.PlannedExpenses.GetAll();
            foreach (var plannedExpenses in thisExpenseTypePlannedExpenses.Where(x => x.ExpenseTypeId == id).ToList())
            {
                plannedExpenses.ExpenseTypeId = typeOtherId;
            }

            await _unitOfWork.ExpenseType.Delete(id);
            //if (result == false) // добавить лог недудачного удаления с id 
            return await SaveAsync();
        }

        public async Task<bool> Exists(int id) => await _unitOfWork.ExpenseType.Exists(id);

        public async Task<List<ExpenseTypeDTO>> GetAll()
        {
            var items = await _unitOfWork.ExpenseType.GetAll();

            var result = items
                .Select(x => new ExpenseTypeDTO(x))
                .ToList();

            return result;
        }

        public async Task<ExpenseTypeDTO> GetById(int id)
        {
            var item = await _unitOfWork.ExpenseType.GetItem(id);

            return item == null ? null : new ExpenseTypeDTO(item);
        }

        public async Task<bool> Update(ExpenseTypeDTO itemDTO)
        {
            if (!await _unitOfWork.ExpenseType.Exists(itemDTO.Id))
                return false;

            ExpenseType x = await _unitOfWork.ExpenseType.GetItem(itemDTO.Id);

            x.Id   = itemDTO.Id;
            x.Name = itemDTO.Name;

            await _unitOfWork.ExpenseType.Update(x);
            return await SaveAsync();
        }
        #endregion
    }
}
