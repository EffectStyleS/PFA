using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Services;

/// <summary>
/// Сервис расходов
/// </summary>
public class ExpenseService : BaseService, IExpenseService
{
    /// <summary>
    /// Сервис расходов
    /// </summary>
    public ExpenseService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    #region ICrud Implementation
    
    /// <inheritdoc />
    public async Task<bool> Create(ExpenseDto itemDto)
    {
        var expense = new Expense
        {
            Id            = itemDto.Id,
            Name          = itemDto.Name,
            Value         = itemDto.Value,
            Date          = itemDto.Date,
            ExpenseTypeId = itemDto.ExpenseTypeId,
            UserId        = itemDto.UserId
        };

        await UnitOfWork.Expense.Create(expense);
        return await SaveAsync();
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        if (!await UnitOfWork.Expense.Exists(id))
        {
            return false;
        }

        await UnitOfWork.Expense.Delete(id);
        return await SaveAsync();
    }

    /// <inheritdoc />
    public async Task<bool> Exists(int id) => await UnitOfWork.Expense.Exists(id);

    /// <inheritdoc />
    public async Task<List<ExpenseDto>> GetAll()
    {
        var items = await UnitOfWork.Expense.GetAll();

        var result = items
            .Select(x => new ExpenseDto(x))
            .ToList();

        return result;
    }

    /// <inheritdoc />
    public async Task<ExpenseDto?> GetById(int id)
    {
        var item = await UnitOfWork.Expense.GetItem(id);
        return item is null ? null : new ExpenseDto(item);
    }

    /// <inheritdoc />
    public async Task<bool> Update(ExpenseDto itemDto)
    {
        if (!await UnitOfWork.Expense.Exists(itemDto.Id))
        {
            return false;
        }

        var x = await UnitOfWork.Expense.GetItem(itemDto.Id);

        x!.Id           = itemDto.Id;
        x.Name          = itemDto.Name;
        x.Value         = itemDto.Value;
        x.Date          = itemDto.Date;
        x.ExpenseTypeId = itemDto.ExpenseTypeId;
        x.UserId        = itemDto.UserId;

        await UnitOfWork.Expense.Update(x);
        return await SaveAsync();
    }
    #endregion

    /// <summary>
    /// Получение конечной даты бюджета
    /// </summary>
    /// <param name="budget"></param>
    /// <returns></returns>
    private DateTime GetEndDate(Budget budget)
    {
        var endDate = budget.TimePeriodId switch
        {
            1 => budget.StartDate.AddMonths(1),
            2 => budget.StartDate.AddMonths(3),
            3 => budget.StartDate.AddYears(1),
            _ => new DateTime()
        };

        return endDate;
    }

    /// <inheritdoc />
    public async Task<List<ExpenseDto>> GetAllUserExpenses(int userId)
    {
        var items = await UnitOfWork.Expense.GetAll();

        var result = items
            .Select(x => new ExpenseDto(x))
            .Where(x => x.UserId == userId)
            .ToList();

        return result;
    }

    /// <inheritdoc />
    public async Task<List<BudgetOverrunDto>> GetBudgetOverruns(int userId)
    {
        List<BudgetOverrunDto> overruns = [];

        var userBudgets = (await UnitOfWork.Budget.GetAll()).Where(x => x.UserId == userId).ToList();

        foreach (var budget in userBudgets)
        {
            var plannedExpenses = (await UnitOfWork.PlannedExpenses.GetAll())
                .Where(x => x.BudgetId == budget.Id)
                .ToList();
            
            foreach (var plannedExpensesItem in plannedExpenses)
            {
                var expensesByType = (await UnitOfWork.Expense.GetAll())
                    .Where(x => x.UserId == userId &&
                        x.ExpenseTypeId == plannedExpensesItem.ExpenseTypeId &&
                        x.Date > budget.StartDate &&
                        x.Date < GetEndDate(budget))
                    .ToList();

                var sumOfExpensesByType = expensesByType.Sum(x => x.Value);
                var difference = sumOfExpensesByType - plannedExpensesItem.Sum;

                if (difference > 0) 
                {
                    overruns.Add(new BudgetOverrunDto
                    {
                        BudgetName = budget.Name,
                        ExpenseType = (await UnitOfWork.ExpenseType.GetAll())
                            .FirstOrDefault(x => x.Id == plannedExpensesItem.ExpenseTypeId)!.Name,
                        Difference = difference
                    });
                }
            }           
        }

        return overruns;
    }
}