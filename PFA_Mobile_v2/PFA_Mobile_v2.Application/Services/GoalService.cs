using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Services;

/// <summary>
/// Сервис целей
/// </summary>
public class GoalService : BaseService, IGoalService
{
    /// <summary>
    /// Сервис целей
    /// </summary>
    public GoalService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    #region ICrud Implementation
    
    /// <inheritdoc />
    public async Task<bool> Create(GoalDto itemDto)
    {
        var goal = new Goal
        {
            Id          = itemDto.Id,
            Name        = itemDto.Name,
            StartDate   = itemDto.StartDate,
            EndDate     = itemDto.EndDate,
            Sum         = itemDto.Sum,
            IsCompleted = itemDto.IsCompleted,
            UserId      = itemDto.UserId,
        };

        await UnitOfWork.Goal.Create(goal);
        return await SaveAsync();
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        if (!await UnitOfWork.Goal.Exists(id))
        {
            return false;
        }

        await UnitOfWork.Goal.Delete(id);
        //if (result == false) // добавить лог недудачного удаления с id 
        return await SaveAsync();
    }

    /// <inheritdoc />
    public async Task<bool> Exists(int id) => await UnitOfWork.Goal.Exists(id);

    /// <inheritdoc />
    public async Task<List<GoalDto>> GetAll()
    {
        var items = await UnitOfWork.Goal.GetAll();

        var result = items
            .Select(x => new GoalDto(x))
            .ToList();

        return result;
    }

    /// <inheritdoc />
    public async Task<GoalDto?> GetById(int id)
    {
        var item = await UnitOfWork.Goal.GetItem(id);
        return item is null ? null : new GoalDto(item);
    }

    /// <inheritdoc />
    public async Task<bool> Update(GoalDto itemDto)
    {
        if (!await UnitOfWork.Goal.Exists(itemDto.Id))
        {
            return false;
        }

        var x = await UnitOfWork.Goal.GetItem(itemDto.Id);
        
        x!.Id         = itemDto.Id;
        x.Name        = itemDto.Name;
        x.StartDate   = itemDto.StartDate;
        x.EndDate     = itemDto.EndDate;
        x.Sum         = itemDto.Sum;
        x.IsCompleted = itemDto.IsCompleted;
        x.UserId      = itemDto.UserId;

        await UnitOfWork.Goal.Update(x);
        return await SaveAsync();
    }
    #endregion

    /// <inheritdoc />
    public async Task<List<GoalDto>> GetAllUserGoals(int userId)
    {
        var items = await UnitOfWork.Goal.GetAll();

        var result = items
            .Select(x => new GoalDto(x))
            .Where (x => x.UserId == userId)
            .ToList();

        return result;
    }
}