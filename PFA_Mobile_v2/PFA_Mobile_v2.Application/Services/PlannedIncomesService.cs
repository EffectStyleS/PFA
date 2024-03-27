using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Services;

/// <summary>
/// Сервис запланированных доходов
/// </summary>
public class PlannedIncomesService : BaseService, IPlannedIncomesService
{
    /// <summary>
    /// Сервис запланированных доходов
    /// </summary>
    public PlannedIncomesService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    #region ICrud Implementation
    
    /// <inheritdoc />
    public async Task<bool> Create(PlannedIncomesDto itemDto)
    {
        var plannedIncomes = new PlannedIncomes
        {
            Id           = itemDto.Id,
            Sum          = itemDto.Sum,
            BudgetId     = itemDto.BudgetId,
            IncomeTypeId = itemDto.IncomeTypeId,
        };

        await UnitOfWork.PlannedIncomes.Create(plannedIncomes);
        return await SaveAsync();
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        if (!await UnitOfWork.PlannedIncomes.Exists(id))
        {
            return false;
        }

        await UnitOfWork.PlannedIncomes.Delete(id);
        //if (result == false) // добавить лог недудачного удаления с id 
        return await SaveAsync();
    }

    /// <inheritdoc />
    public async Task<bool> Exists(int id) => await UnitOfWork.PlannedIncomes.Exists(id);

    /// <inheritdoc />
    public async Task<List<PlannedIncomesDto>> GetAll()
    {
        var items = await UnitOfWork.PlannedIncomes.GetAll();

        var result = items
            .Select(x => new PlannedIncomesDto(x))
            .ToList();

        return result;
    }

    /// <inheritdoc />
    public async Task<PlannedIncomesDto?> GetById(int id)
    {
        var item = await UnitOfWork.PlannedIncomes.GetItem(id);
        return item is null ? null : new PlannedIncomesDto(item);
    }

    /// <inheritdoc />
    public async Task<bool> Update(PlannedIncomesDto itemDto)
    {
        if (!await UnitOfWork.PlannedIncomes.Exists(itemDto.Id))
        {
            return false;
        }

        var x = await UnitOfWork.PlannedIncomes.GetItem(itemDto.Id);

        x!.Id = itemDto.Id;
        x.Sum = itemDto.Sum;
        x.BudgetId = itemDto.BudgetId;
        x.IncomeTypeId = itemDto.IncomeTypeId;

        await UnitOfWork.PlannedIncomes.Update(x);
        return await SaveAsync();
    }
    #endregion

    /// <inheritdoc />
    public async Task<List<PlannedIncomesDto>> GetAllBudgetPlannedIncomes(int budgetId)
    {
        var items = await UnitOfWork.PlannedIncomes.GetAll();

        var result = items
            .Select(x => new PlannedIncomesDto(x))
            .Where(x => x.BudgetId == budgetId)
            .ToList();

        return result;
    }
}