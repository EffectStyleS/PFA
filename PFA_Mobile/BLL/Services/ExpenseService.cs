using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services.Abstract;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ExpenseService : BaseService, IExpenseService
    {
        public ExpenseService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #region ICrud Implementation
        public async Task<bool> Create(ExpenseDTO itemDTO)
        {
            var expense = new Expense()
            {
                Id            = itemDTO.Id,
                Name          = itemDTO.Name,
                Value         = itemDTO.Value,
                Date          = itemDTO.Date,
                ExpenseTypeId = itemDTO.ExpenseTypeId,
                UserId        = itemDTO.UserId
            };

            await _unitOfWork.Expense.Create(expense);
            return await SaveAsync();
        }

        public async Task<bool> Delete(int id)
        {
            if (!await _unitOfWork.Expense.Exists(id))
                return false;

            await _unitOfWork.Expense.Delete(id);
            //if (result == false) // добавить лог недудачного удаления с id 
            return await SaveAsync();
        }

        public async Task<bool> Exists(int id) => await _unitOfWork.Expense.Exists(id);

        public async Task<List<ExpenseDTO>> GetAll()
        {
            var items = await _unitOfWork.Expense.GetAll();

            var result = items
                .Select(x => new ExpenseDTO(x))
                .ToList();

            return result;
        }

        public async Task<ExpenseDTO> GetById(int id)
        {
            var item = await _unitOfWork.Expense.GetItem(id);

            return item == null ? null : new ExpenseDTO(item);
        }

        public async Task<bool> Update(ExpenseDTO itemDTO)
        {
            if (!await _unitOfWork.Expense.Exists(itemDTO.Id))
                return false;

            Expense x = await _unitOfWork.Expense.GetItem(itemDTO.Id);

            x.Id            = itemDTO.Id;
            x.Name          = itemDTO.Name;
            x.Value         = itemDTO.Value;
            x.Date          = itemDTO.Date;
            x.ExpenseTypeId = itemDTO.ExpenseTypeId;
            x.UserId        = itemDTO.UserId;

            await _unitOfWork.Expense.Update(x);
            return await SaveAsync();
        }
        #endregion

        public async Task<List<ExpenseDTO>> GetAllUserExpenses(int userId)
        {
            var items = await _unitOfWork.Expense.GetAll();

            var result = items
                .Select(x => new ExpenseDTO(x))
                .Where(x => x.UserId == userId)
                .ToList();

            return result;
        }
    }
}
