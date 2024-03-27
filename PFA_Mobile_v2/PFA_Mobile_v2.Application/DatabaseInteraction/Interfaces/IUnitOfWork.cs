using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;

/// <summary>
///     Интерфейс для реализации паттерна UnitOfWork
///     для работы репозиториев с одним контекстом данных
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Репозиторий бюджета
    /// </summary>
    IRepository<Budget> Budget { get; }
    
    /// <summary>
    /// Репозиторий расхода
    /// </summary>
    IRepository<Expense> Expense { get; }

    /// <summary>
    /// Репозиторий типа расхода
    /// </summary>
    IRepository<ExpenseType> ExpenseType { get; }
    
    /// <summary>
    /// Репоизиторий цели
    /// </summary>
    IRepository<Goal> Goal { get; }
    
    /// <summary>
    /// Репозиторий дохода
    /// </summary>
    IRepository<Income> Income { get; }
    
    /// <summary>
    /// Репозиторий типа дохода
    /// </summary>
    IRepository<IncomeType> IncomeType { get; }
    
    /// <summary>
    /// Репоизторий запланированных расходов
    /// </summary>
    IRepository<PlannedExpenses> PlannedExpenses { get; }
    
    /// <summary>
    /// Репозиторий запланированных доходов
    /// </summary>
    IRepository<PlannedIncomes> PlannedIncomes { get; }
    
    /// <summary>
    /// Репозиторий временного периода
    /// </summary>
    IRepository<TimePeriod> TimePeriod { get; }
    
    /// <summary>
    /// Репозиторий пользователя
    /// </summary>
    IUserRepository<AppUser> User { get; }

    /// <summary>
    ///     Сохраняет изменения в бд
    /// </summary>
    /// <returns>
    ///     При успешном выполнени возвращает количество
    ///     записей состояния, записанных в базу данных.
    ///     Иначе возвращает минимальное значение Int32.
    /// </returns>
    Task<int> Save();
}