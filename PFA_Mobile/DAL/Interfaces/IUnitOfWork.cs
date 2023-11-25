using DAL.Entities;

namespace DAL.Interfaces
{
    /// <summary>
    ///     Интерфейс для реализации паттерна UnitOfWork
    ///     для работы репозиториев с одним контекстом данных
    /// </summary>
    public interface IUnitOfWork
    {
        IUserRepository<AppUser> User { get; }
        IRepository<Budget> Budget { get; }
        IRepository<PlannedExpenses> PlannedExpenses { get; }
        IRepository<PlannedIncomes> PlannedIncomes { get; }
        IRepository<Expense> Expense { get; }
        IRepository<Income> Income { get; }
        IRepository<ExpenseType> ExpenseType { get; }
        IRepository<IncomeType> IncomeType { get; }
        IRepository<TimePeriod> TimePeriod { get; }
        IRepository<Goal> Goal { get; }

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
}
