using DAL.Entities;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;


namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PFAContext _db;

        private BudgetRepository _budgetRepository;
        private ExpenseRepository _expenseRepository;
        private ExpenseTypeRepository _expenseTypesRepository;
        private GoalRepository _goalRepository;
        private IncomeRepository _incomeRepository;
        private IncomeTypeRepository _incomeTypesRepository;
        private PlannedExpensesRepository _plannedExpensesRepository;
        private PlannedIncomesRepository _plannedIncomesRepository;
        private TimePeriodRepository _timePeriodRepository;
        private UserRepository _userRepository;

        public UnitOfWork(IConfiguration configuration)
        {
            _db = new PFAContext(configuration);
        }

        public IRepository<Budget> Budget
        {
            get
            {
                _budgetRepository ??= new BudgetRepository(_db);
                return _budgetRepository;
            }
        }

        public IRepository<PlannedExpenses> PlannedExpenses
        {
            get
            {
                _plannedExpensesRepository ??= new PlannedExpensesRepository(_db);
                return _plannedExpensesRepository;
            }
        }

        public IRepository<PlannedIncomes> PlannedIncomes
        {
            get
            {
                _plannedIncomesRepository ??= new PlannedIncomesRepository(_db);
                return _plannedIncomesRepository;
            }
        }

        public IRepository<Expense> Expense
        {
            get
            {
                _expenseRepository ??= new ExpenseRepository(_db);
                return _expenseRepository;
            }
        }

        public IRepository<Goal> Goal
        {
            get
            {
                _goalRepository ??= new GoalRepository(_db);
                return _goalRepository;
            }
        }

        public IRepository<Income> Income
        {
            get
            {
                _incomeRepository ??= new IncomeRepository(_db);
                return _incomeRepository;
            }
        }

        public IRepository<ExpenseType> ExpenseType
        {
            get
            {
                _expenseTypesRepository ??= new ExpenseTypeRepository(_db);
                return _expenseTypesRepository;
            }
        }

        public IRepository<IncomeType> IncomeType
        {
            get
            {
                _incomeTypesRepository ??= new IncomeTypeRepository(_db);
                return _incomeTypesRepository;
            }
        }

        public IRepository<TimePeriod> TimePeriod
        {
            get
            {
                _timePeriodRepository ??= new TimePeriodRepository(_db);
                return _timePeriodRepository;
            }
        }

        public IUserRepository<AppUser> User
        {
            get
            {
                _userRepository ??= new UserRepository(_db);
                return _userRepository;
            }
        }

        public async Task<int> Save()
        {
            try
            {
                return await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
