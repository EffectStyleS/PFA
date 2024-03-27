using Microsoft.AspNetCore.Identity;
using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Services;

public class StatisticsService : BaseService, IStatisticsService
{
    public StatisticsService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    
    public async Task<List<CountStatisticRequestModel>> GetUsersExpensesCount(UserManager<AppUser> userManager)
    {
        var result = new List<CountStatisticRequestModel>();
        
        var users = await UnitOfWork.User.GetAll(userManager);

        foreach (var user in users)
        {
            var expenses = await UnitOfWork.Expense.GetAll();
            var userExpensesCount = expenses.Count(x => x.UserId == user.Id);

            var userExpensesCountStatistic = new CountStatisticRequestModel
            {
                Base = $"{user.Login} ({user.Id})",
                Value = userExpensesCount
            };
            
            result.Add(userExpensesCountStatistic);
        }

        return result;
    }

    public async Task<List<CountStatisticRequestModel>> GetExpensesByTypeCount()
    {
        var result = new List<CountStatisticRequestModel>();

        var expenseTypes = await UnitOfWork.ExpenseType.GetAll();

        foreach (var expenseType in expenseTypes)
        {
            var expenses = await UnitOfWork.Expense.GetAll();
            var expensesByTypeCount = expenses.Count(x => x.ExpenseTypeId == expenseType.Id);

            var expensesByTypeCountStatistic = new CountStatisticRequestModel
            {
                Base = expenseType.Name,
                Value = expensesByTypeCount
            };
            
            result.Add(expensesByTypeCountStatistic);
        }
        
        return result;
    }

    public async Task<List<CountStatisticRequestModel>> GetUsersIncomesCount(UserManager<AppUser> userManager)
    {
        var result = new List<CountStatisticRequestModel>();
        
        var users = await UnitOfWork.User.GetAll(userManager);

        foreach (var user in users)
        {
            var incomes = await UnitOfWork.Income.GetAll();
            var userIncomesCount = incomes.Count(x => x.UserId == user.Id);

            var userIncomesCountStatistic = new CountStatisticRequestModel
            {
                Base = $"{user.Login} ({user.Id})",
                Value = userIncomesCount
            };
            
            result.Add(userIncomesCountStatistic);
        }

        return result;
    }

    public async Task<List<CountStatisticRequestModel>> GetIncomesByTypeCount()
    {
        var result = new List<CountStatisticRequestModel>();

        var incomeTypes = await UnitOfWork.IncomeType.GetAll();

        foreach (var incomeType in incomeTypes)
        {
            var incomes = await UnitOfWork.Income.GetAll();
            var incomesByTypeCount = incomes.Count(x => x.IncomeTypeId == incomeType.Id);

            var incomesByTypeCountStatistic = new CountStatisticRequestModel
            {
                Base = incomeType.Name,
                Value = incomesByTypeCount
            };
            
            result.Add(incomesByTypeCountStatistic);
        }
        
        return result;
    }

    public async Task<List<CountStatisticRequestModel>> GetUsersBudgetsCount(UserManager<AppUser> userManager)
    {
        var result = new List<CountStatisticRequestModel>();
        
        var users = await UnitOfWork.User.GetAll(userManager);

        foreach (var user in users)
        {
            var budgets = await UnitOfWork.Budget.GetAll();
            var userBudgetsCount = budgets.Count(x => x.UserId == user.Id);

            var userBudgetsCountStatistic = new CountStatisticRequestModel
            {
                Base = $"{user.Login} ({user.Id})",
                Value = userBudgetsCount
            };
            
            result.Add(userBudgetsCountStatistic);
        }

        return result;
    }

    public async Task<List<CountStatisticRequestModel>> GetBudgetsByTimePeriodsCount()
    {
        var result = new List<CountStatisticRequestModel>();

        var timePeriods = await UnitOfWork.TimePeriod.GetAll();

        foreach (var timePeriod in timePeriods)
        {
            var budgets = await UnitOfWork.Budget.GetAll();
            var budgetsByTimePeriodCount = budgets.Count(x => x.TimePeriodId == timePeriod.Id);

            var budgetsByTimePeriodCountStatistic = new CountStatisticRequestModel
            {
                Base = timePeriod.Name,
                Value = budgetsByTimePeriodCount
            };
            
            result.Add(budgetsByTimePeriodCountStatistic);
        }
        
        return result;
    }

    public async Task<List<CountStatisticRequestModel>> GetUsersGoalsCount(UserManager<AppUser> userManager)
    {
        var result = new List<CountStatisticRequestModel>();
        
        var users = await UnitOfWork.User.GetAll(userManager);

        foreach (var user in users)
        {
            var goals = await UnitOfWork.Goal.GetAll();
            var userGoalsCount = goals.Count(x => x.UserId == user.Id);

            var userGoalsCountStatistic = new CountStatisticRequestModel
            {
                Base = $"{user.Login} ({user.Id})",
                Value = userGoalsCount
            };
            
            result.Add(userGoalsCountStatistic);
        }

        return result;
    }
}