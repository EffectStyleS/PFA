using Microsoft.Extensions.Configuration;
using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Domain.Entities;
using PFA_Mobile_v2.Infrastructure.Persistence.Repositories;

namespace PFA_Mobile_v2.Infrastructure.Persistence;

/// <summary>
/// Точка работы
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly PfaContext _db;

    private BudgetRepository? _budgetRepository;
    private ExpenseRepository? _expenseRepository;
    private ExpenseTypeRepository? _expenseTypesRepository;
    private GoalRepository? _goalRepository;
    private IncomeRepository? _incomeRepository;
    private IncomeTypeRepository? _incomeTypesRepository;
    private PlannedExpensesRepository? _plannedExpensesRepository;
    private PlannedIncomesRepository? _plannedIncomesRepository;
    private TimePeriodRepository? _timePeriodRepository;
    private UserRepository? _userRepository;

    /// <summary>
    /// Точка работы
    /// </summary>
    public UnitOfWork(IConfiguration configuration)
    {
        _db = new PfaContext(configuration);
    }

    /// <inheritdoc />
    public IRepository<Budget> Budget
    {
        get
        {
            _budgetRepository ??= new BudgetRepository(_db);
            return _budgetRepository;
        }
    }

    /// <inheritdoc />
    public IRepository<PlannedExpenses> PlannedExpenses
    {
        get
        {
            _plannedExpensesRepository ??= new PlannedExpensesRepository(_db);
            return _plannedExpensesRepository;
        }
    }

    /// <inheritdoc />
    public IRepository<PlannedIncomes> PlannedIncomes
    {
        get
        {
            _plannedIncomesRepository ??= new PlannedIncomesRepository(_db);
            return _plannedIncomesRepository;
        }
    }

    /// <inheritdoc />
    public IRepository<Expense> Expense
    {
        get
        {
            _expenseRepository ??= new ExpenseRepository(_db);
            return _expenseRepository;
        }
    }

    /// <inheritdoc />
    public IRepository<Goal> Goal
    {
        get
        {
            _goalRepository ??= new GoalRepository(_db);
            return _goalRepository;
        }
    }

    /// <inheritdoc />
    public IRepository<Income> Income
    {
        get
        {
            _incomeRepository ??= new IncomeRepository(_db);
            return _incomeRepository;
        }
    }

    /// <inheritdoc />
    public IRepository<ExpenseType> ExpenseType
    {
        get
        {
            _expenseTypesRepository ??= new ExpenseTypeRepository(_db);
            return _expenseTypesRepository;
        }
    }

    /// <inheritdoc />
    public IRepository<IncomeType> IncomeType
    {
        get
        {
            _incomeTypesRepository ??= new IncomeTypeRepository(_db);
            return _incomeTypesRepository;
        }
    }

    /// <inheritdoc />
    public IRepository<TimePeriod> TimePeriod
    {
        get
        {
            _timePeriodRepository ??= new TimePeriodRepository(_db);
            return _timePeriodRepository;
        }
    }

    /// <inheritdoc />
    public IUserRepository<AppUser> User
    {
        get
        {
            _userRepository ??= new UserRepository(_db);
            return _userRepository;
        }
    }

    /// <inheritdoc />
    public Task<int> Save()
    {
        return _db.SaveChangesAsync();
    }
}