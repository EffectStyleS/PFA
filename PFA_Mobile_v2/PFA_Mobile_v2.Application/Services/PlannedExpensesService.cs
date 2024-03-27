using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Services;

/// <summary>
/// Сервис запланированных расходов
/// </summary>
public class PlannedExpensesService : BaseService, IPlannedExpensesService
{
    /// <summary>
    /// Сервис запланированных расходов
    /// </summary>
    public PlannedExpensesService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    #region ICrud Implementation
    
    /// <inheritdoc />
    public async Task<bool> Create(PlannedExpensesDto itemDto)
    {
        var plannedExpenses = new PlannedExpenses
        {
            Id            = itemDto.Id,
            Sum           = itemDto.Sum,
            BudgetId      = itemDto.BudgetId,
            ExpenseTypeId = itemDto.ExpenseTypeId,
        };

        await UnitOfWork.PlannedExpenses.Create(plannedExpenses);
        return await SaveAsync();
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        if (!await UnitOfWork.PlannedExpenses.Exists(id))
        {
            return false;
        }

        await UnitOfWork.PlannedExpenses.Delete(id);
        //if (result == false) // добавить лог недудачного удаления с id 
        return await SaveAsync();
    }

    /// <inheritdoc />
    public async Task<bool> Exists(int id) => await UnitOfWork.PlannedExpenses.Exists(id);

    /// <inheritdoc />
    public async Task<List<PlannedExpensesDto>> GetAll()
    {
        var items = await UnitOfWork.PlannedExpenses.GetAll();

        var result = items
            .Select(x => new PlannedExpensesDto(x))
            .ToList();

        return result;
    }

    /// <inheritdoc />
    public async Task<PlannedExpensesDto?> GetById(int id)
    {
        var item = await UnitOfWork.PlannedExpenses.GetItem(id);
        return item is null ? null : new PlannedExpensesDto(item);
    }

    /// <inheritdoc />
    public async Task<bool> Update(PlannedExpensesDto itemDto)
    {
        if (!await UnitOfWork.PlannedExpenses.Exists(itemDto.Id))
        {
            return false;
        }

        var x = await UnitOfWork.PlannedExpenses.GetItem(itemDto.Id);

        x!.Id           = itemDto.Id;
        x.Sum           = itemDto.Sum;
        x.BudgetId      = itemDto.BudgetId;
        x.ExpenseTypeId = itemDto.ExpenseTypeId;

        await UnitOfWork.PlannedExpenses.Update(x);
        return await SaveAsync();
    }
    #endregion

    /// <inheritdoc />
    public async Task<List<PlannedExpensesDto>> GetAllBudgetPlannedExpenses(int budgetId)
    {
        var items = await UnitOfWork.PlannedExpenses.GetAll();

        var result = items
            .Select(x => new PlannedExpensesDto(x))
            .Where(x => x.BudgetId == budgetId)
            .ToList();

        return result;
    }
}