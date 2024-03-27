using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;

namespace PFA_Mobile_v2.Application.Services;

/// <summary>
/// Базовый класс сервиса
/// </summary>
public abstract class BaseService
{
    protected readonly IUnitOfWork UnitOfWork;
    
    /// <summary>
    /// Базовый класс сервиса
    /// </summary>
    protected BaseService(IUnitOfWork unitOfWork) => UnitOfWork = unitOfWork;
    
    /// <summary>
    /// Сохраняет изменения
    /// </summary>
    /// <returns>True - успешно, иначе false</returns>
    protected virtual async Task<bool> SaveAsync() => await UnitOfWork.Save() > 0;
}