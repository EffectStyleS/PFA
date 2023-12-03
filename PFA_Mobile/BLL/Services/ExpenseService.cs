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

        private DateTime GetEndDate(Budget budget)
        {
            DateTime endDate = new DateTime();
            switch (budget.TimePeriodId)
            {
                case 1:
                    endDate = budget.StartDate.AddMonths(1);
                    break;
                case 2:
                    endDate = budget.StartDate.AddMonths(3);
                    break;
                case 3:
                    endDate = budget.StartDate.AddYears(1);
                    break;
            }

            return endDate;
        }

        public async Task<List<ExpenseDTO>> GetAllUserExpenses(int userId)
        {
            var items = await _unitOfWork.Expense.GetAll();

            var result = items
                .Select(x => new ExpenseDTO(x))
                .Where(x => x.UserId == userId)
                .ToList();

            return result;
        }

        public async Task<List<BudgetOverrunDTO>> GetBudgetOverruns(int userId)
        {
            List<BudgetOverrunDTO> overruns = new();

            List<Budget> userBudgets = 
                (await _unitOfWork.Budget.GetAll()).Where(x => x.UserId == userId)
                                                   .ToList();

            foreach (var budget in userBudgets) 
            {
                var plannedExpenses = (await _unitOfWork.PlannedExpenses.GetAll()).Where(x => x.BudgetId == budget.Id).ToList();
                
                foreach (var plannedExpensesItem in plannedExpenses)
                {
                    var expensesByType = (await _unitOfWork.Expense.GetAll())
                        .Where(x => x.UserId == userId &&
                            x.ExpenseTypeId == plannedExpensesItem.ExpenseTypeId &&
                            x.Date > budget.StartDate &&
                            x.Date < GetEndDate(budget))
                        .ToList();

                    var sumOfExpensesByType = expensesByType.Sum(x => x.Value);
                    var difference = sumOfExpensesByType - plannedExpensesItem.Sum;

                    if (difference > 0) 
                    {
                        overruns.Add(new BudgetOverrunDTO()
                        {
                            BudgetName = budget.Name,
                            ExpenseType = (await _unitOfWork.ExpenseType.GetAll()).FirstOrDefault(x => x.Id == plannedExpensesItem.ExpenseTypeId).Name,
                            Difference = difference
                        });
                    }
                }           
            }

            return overruns;
        }
    }
}
