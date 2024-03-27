using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Services;

/// <summary>
/// Сервис бюджетов
/// </summary>
public class BudgetService : BaseService, IBudgetService
{
    /// <summary>
    /// Сервис бюджетов
    /// </summary>
    public BudgetService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    #region ICrud Implementation

    /// <inheritdoc />
    public async Task<bool> Create(BudgetDto itemDto)
    {
        var budget = new Budget
        {
            Id              = itemDto.Id,
            Name            = itemDto.Name,
            StartDate       = itemDto.StartDate,
            TimePeriodId    = itemDto.TimePeriodId,
            UserId          = itemDto.UserId,
        };

        await UnitOfWork.Budget.Create(budget);
        return await SaveAsync();
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        if (!await UnitOfWork.Budget.Exists(id))
        {
            return false;
        }

        // Сначала удаляем запланированные расходы и запланированные доходы
        var plannedExpenses = await UnitOfWork.PlannedExpenses.GetAll();
        foreach (var budgetPlannedExpenses in plannedExpenses.Where(x => x.BudgetId ==id).ToList())
        {
            /*var result = */await UnitOfWork.PlannedExpenses.Delete(budgetPlannedExpenses.Id);
            //if (result == false) // добавить лог недудачного удаления с id 
        }

        var plannedIncomes = await UnitOfWork.PlannedIncomes.GetAll();
        foreach (var budgetPlannedIncomes in plannedIncomes.Where(x => x.BudgetId == id).ToList())
        {
            await UnitOfWork.PlannedIncomes.Delete(budgetPlannedIncomes.Id);
            //if (result == false) // добавить лог недудачного удаления с id 
        }

        await UnitOfWork.Budget.Delete(id);
        //if (result == false) // добавить лог недудачного удаления с id 
        return await SaveAsync();
    }

    /// <inheritdoc />
    public async Task<bool> Exists(int id) => await UnitOfWork.Budget.Exists(id);

    /// <inheritdoc />
    public async Task<List<BudgetDto>> GetAll()
    {
        var items = await UnitOfWork.Budget.GetAll();

        var result = items
            .Select(x => new BudgetDto(x))
            .ToList();

        return result;
    }

    /// <inheritdoc />
    public async Task<BudgetDto?> GetById(int id)
    {
        var item = await UnitOfWork.Budget.GetItem(id);
        return item is null ? null : new BudgetDto(item);
    }

    /// <inheritdoc />
    public async Task<bool> Update(BudgetDto itemDto)
    {
        if (!await UnitOfWork.Budget.Exists(itemDto.Id))
        {
            return false;
        }

        var b = await UnitOfWork.Budget.GetItem(itemDto.Id);

        b!.Id             = itemDto.Id;
        b.Name            = itemDto.Name;
        b.UserId          = itemDto.UserId;
        b.StartDate       = itemDto.StartDate;
        b.TimePeriodId    = itemDto.TimePeriodId;

        await UnitOfWork.Budget.Update(b);
        return await SaveAsync();
    }
    #endregion

    /// <inheritdoc />
    public async Task<List<BudgetDto>> GetAllUserBudgets(int userId)
    {
        var items = await UnitOfWork.Budget.GetAll();

        var result = items
            .Select(x => new BudgetDto(x))
            .Where(x => x.UserId == userId)
            .ToList();

        return result;
    }
}