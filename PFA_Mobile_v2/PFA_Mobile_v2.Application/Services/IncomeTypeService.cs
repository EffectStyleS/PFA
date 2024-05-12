using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Services;

/// <summary>
/// Сервис типов доходов
/// </summary>
public class IncomeTypeService : BaseService, IIncomeTypeService
{
    /// <summary>
    /// Сервис типов доходов
    /// </summary>
    public IncomeTypeService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    #region ICrud Implementation
    
    /// <inheritdoc />
    public async Task<bool> Create(IncomeTypeDto itemDto)
    {
        var incomeType = new IncomeType
        {
            Id   = itemDto.Id,
            Name = itemDto.Name,
        };

        await UnitOfWork.IncomeType.Create(incomeType);
        return await SaveAsync();
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        if (!await UnitOfWork.IncomeType.Exists(id))
        {
            return false;
        }

        // Тип "Другое" - неудаляемый
        var incomeType = await UnitOfWork.IncomeType.GetItem(id);
        if (incomeType!.Name == "Другое")
        {
            return false;
        }

        // Заменяем IncomeTypeId у доходов и
        // запланированных доходов на id типа дохода "Другое"
        var allIncomesTypes = await UnitOfWork.IncomeType.GetAll();
        var typeOtherId = allIncomesTypes.FirstOrDefault(x => x.Name == "Другое")!.Id;

        var thisIncomeTypeIncomes = await UnitOfWork.Income.GetAll();
        foreach (var income in thisIncomeTypeIncomes.Where(x => x.IncomeTypeId == id).ToList())
        {
            income.IncomeTypeId = typeOtherId;
        }

        var thisIncomeTypePlannedIncomes = await UnitOfWork.PlannedIncomes.GetAll();
        foreach (var plannedIncomes in thisIncomeTypePlannedIncomes.Where(x => x.IncomeTypeId == id).ToList())
        {
            plannedIncomes.IncomeTypeId = typeOtherId;
        }

        await UnitOfWork.IncomeType.Delete(id);
        return await SaveAsync();
    }

    /// <inheritdoc />
    public async Task<bool> Exists(int id) => await UnitOfWork.IncomeType.Exists(id);

    /// <inheritdoc />
    public async Task<List<IncomeTypeDto>> GetAll()
    {
        var items = await UnitOfWork.IncomeType.GetAll();

        var result = items
            .Select(x => new IncomeTypeDto(x))
            .ToList();

        return result;
    }

    /// <inheritdoc />
    public async Task<IncomeTypeDto?> GetById(int id)
    {
        var item = await UnitOfWork.IncomeType.GetItem(id);
        return item == null ? null : new IncomeTypeDto(item);
    }

    /// <inheritdoc />
    public async Task<bool> Update(IncomeTypeDto itemDto)
    {
        if (!await UnitOfWork.IncomeType.Exists(itemDto.Id))
        {
            return false;
        }

        var x = await UnitOfWork.IncomeType.GetItem(itemDto.Id);

        x!.Id  = itemDto.Id;
        x.Name = itemDto.Name;

        await UnitOfWork.IncomeType.Update(x);
        return await SaveAsync();
    }
    #endregion
}