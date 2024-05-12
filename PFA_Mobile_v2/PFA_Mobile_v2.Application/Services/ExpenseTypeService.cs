using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Services;

/// <summary>
/// Сервис типов расходов
/// </summary>
public class ExpenseTypeService : BaseService, IExpenseTypeService
{
    /// <summary>
    /// Сервис типов расходов
    /// </summary>
    public ExpenseTypeService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    #region ICrud Implementation
    
    /// <inheritdoc />
    public async Task<bool> Create(ExpenseTypeDto itemDto)
    {
        var expenseType = new ExpenseType
        {
            Id   = itemDto.Id,
            Name = itemDto.Name,
        };

        await UnitOfWork.ExpenseType.Create(expenseType);
        return await SaveAsync();
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        if (!await UnitOfWork.ExpenseType.Exists(id))
        {
            return false;
        }

        // Тип "Другое" - неудаляемый,
        var expenseType = await UnitOfWork.ExpenseType.GetItem(id);
        if (expenseType!.Name == "Другое")
        {
            return false;
        }

        // Заменяем ExpenseTypeId у расходов и
        // запланированных расходов на id типа расхода "Другое"
        var allExpenseTypes = await UnitOfWork.ExpenseType.GetAll();
        var typeOtherId = allExpenseTypes.FirstOrDefault(x => x.Name == "Другое")!.Id;

        var thisExpenseTypeExpenses = await UnitOfWork.Expense.GetAll();
        foreach (var expense in thisExpenseTypeExpenses.Where(x => x.ExpenseTypeId == id).ToList())
        {
            expense.ExpenseTypeId = typeOtherId;
        }

        var thisExpenseTypePlannedExpenses = await UnitOfWork.PlannedExpenses.GetAll();
        foreach (var plannedExpenses in thisExpenseTypePlannedExpenses.Where(x => x.ExpenseTypeId == id).ToList())
        {
            plannedExpenses.ExpenseTypeId = typeOtherId;
        }

        await UnitOfWork.ExpenseType.Delete(id);
        return await SaveAsync();
    }

    /// <inheritdoc />
    public async Task<bool> Exists(int id) => await UnitOfWork.ExpenseType.Exists(id);

    /// <inheritdoc />
    public async Task<List<ExpenseTypeDto>> GetAll()
    {
        var items = await UnitOfWork.ExpenseType.GetAll();

        var result = items
            .Select(x => new ExpenseTypeDto(x))
            .ToList();

        return result;
    }

    /// <inheritdoc />
    public async Task<ExpenseTypeDto?> GetById(int id)
    {
        var item = await UnitOfWork.ExpenseType.GetItem(id);
        return item is null ? null : new ExpenseTypeDto(item);
    }

    /// <inheritdoc />
    public async Task<bool> Update(ExpenseTypeDto itemDto)
    {
        if (!await UnitOfWork.ExpenseType.Exists(itemDto.Id))
            return false;

        var x = await UnitOfWork.ExpenseType.GetItem(itemDto.Id);

        x!.Id  = itemDto.Id;
        x.Name = itemDto.Name;

        await UnitOfWork.ExpenseType.Update(x);
        return await SaveAsync();
    }
    #endregion
}