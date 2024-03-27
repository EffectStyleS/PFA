using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Services;

/// <summary>
/// Сервис временных периодов
/// </summary>
public class TimePeriodService : BaseService, ITimePeriodService
{
    /// <summary>
    /// Сервис временных периодов
    /// </summary>
    public TimePeriodService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    #region ICrud Implementation
    
    /// <inheritdoc />
    public async Task<bool> Create(TimePeriodDto itemDto)
    {
        var timePeriod = new TimePeriod
        {
            Id   = itemDto.Id,
            Name = itemDto.Name,
        };

        await UnitOfWork.TimePeriod.Create(timePeriod);
        return await SaveAsync();
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        if (!await UnitOfWork.TimePeriod.Exists(id))
        {
            return false;
        }

        // Период "Месяц" - неудаляемый,
        // TODO: сделать на клиенте неудаляемым, возможно задать имя неудаляемого таймпериода где-нибудь в другом месте?
        var timePeriod = await UnitOfWork.TimePeriod.GetItem(id);
        if (timePeriod!.Name == "Месяц")
        {
            //if (result == false) // добавить лог недудачного удаления "Месяц"
            return false;
        }

        // Заменяем TimePeriodId у бюджетов на id периода "Месяц"
        var allTimePeriods = await UnitOfWork.TimePeriod.GetAll();
        var monthId = allTimePeriods.FirstOrDefault(x => x.Name == "Месяц")!.Id; // период "Месяц" всегда будет найден

        var thisTimePeriodBudgets = await UnitOfWork.Budget.GetAll();
        foreach (var budget in thisTimePeriodBudgets.Where(x => x.TimePeriodId == id).ToList())
        {
            budget.TimePeriodId = monthId;
        }

        await UnitOfWork.TimePeriod.Delete(id);
        //if (result == false) // добавить лог недудачного удаления с id 
        return await SaveAsync();
    }

    /// <inheritdoc />
    public async Task<bool> Exists(int id) => await UnitOfWork.TimePeriod.Exists(id);

    /// <inheritdoc />
    public async Task<List<TimePeriodDto>> GetAll()
    {
        var items = await UnitOfWork.TimePeriod.GetAll();

        var result = items
            .Select(x => new TimePeriodDto(x))
            .ToList();

        return result;
    }

    /// <inheritdoc />
    public async Task<TimePeriodDto?> GetById(int id)
    {
        var item = await UnitOfWork.TimePeriod.GetItem(id);
        return item is null ? null : new TimePeriodDto(item);
    }

    /// <inheritdoc /> 
    public async Task<bool> Update(TimePeriodDto itemDto)
    {
        if (!await UnitOfWork.TimePeriod.Exists(itemDto.Id))
        {
            return false;
        }

        var x = await UnitOfWork.TimePeriod.GetItem(itemDto.Id);

        x!.Id  = itemDto.Id;
        x.Name = itemDto.Name;

        await UnitOfWork.TimePeriod.Update(x);
        return await SaveAsync();
    }
    #endregion
}