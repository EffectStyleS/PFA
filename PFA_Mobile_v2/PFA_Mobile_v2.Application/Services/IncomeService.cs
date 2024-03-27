using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Services;

/// <summary>
/// Сервис доходов
/// </summary>
public class IncomeService : BaseService, IIncomeService
{
    /// <summary>
    /// Сервис доходов
    /// </summary>
    public IncomeService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    #region ICrud Implementation
    
    /// <inheritdoc />
    public async Task<bool> Create(IncomeDto itemDto)
    {
        var income = new Income
        {
            Id           = itemDto.Id,
            Name         = itemDto.Name,
            Value        = itemDto.Value,
            Date         = itemDto.Date,
            IncomeTypeId = itemDto.IncomeTypeId,
            UserId       = itemDto.UserId
        };

        await UnitOfWork.Income.Create(income);
        return await SaveAsync();
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        if (!await UnitOfWork.Income.Exists(id))
        {
            return false;
        }

        await UnitOfWork.Income.Delete(id);
        //if (result == false) // добавить лог недудачного удаления с id 
        return await SaveAsync();
    }

    /// <inheritdoc />
    public async Task<bool> Exists(int id) => await UnitOfWork.Income.Exists(id);

    /// <inheritdoc />
    public async Task<List<IncomeDto>> GetAll()
    {
        var items = await UnitOfWork.Income.GetAll();

        var result = items
            .Select(x => new IncomeDto(x))
            .ToList();

        return result;
    }

    /// <inheritdoc />
    public async Task<IncomeDto?> GetById(int id)
    {
        var item = await UnitOfWork.Income.GetItem(id);
        return item is null ? null : new IncomeDto(item);
    }

    /// <inheritdoc />
    public async Task<bool> Update(IncomeDto itemDto)
    {
        if (!await UnitOfWork.Income.Exists(itemDto.Id))
        {
            return false;
        }

        var x = await UnitOfWork.Income.GetItem(itemDto.Id);

        x!.Id          = itemDto.Id;
        x.Name         = itemDto.Name;
        x.Value        = itemDto.Value;
        x.Date         = itemDto.Date;
        x.IncomeTypeId = itemDto.IncomeTypeId;
        x.UserId       = itemDto.UserId;

        await UnitOfWork.Income.Update(x);
        return await SaveAsync();
    }
    #endregion

    /// <inheritdoc />
    public async Task<List<IncomeDto>> GetAllUserIncomes(int userId)
    {
        var items = await UnitOfWork.Income.GetAll();

        var result = items
            .Select(x => new IncomeDto(x))
            .Where(x => x.UserId == userId) 
            .ToList();

        return result;
    }
}