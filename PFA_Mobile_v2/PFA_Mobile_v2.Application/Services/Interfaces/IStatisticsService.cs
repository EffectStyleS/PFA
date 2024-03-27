using Microsoft.AspNetCore.Identity;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Services.Interfaces;

public interface IStatisticsService
{
    Task<List<CountStatisticRequestModel>> GetUsersExpensesCount(UserManager<AppUser> userManager);
    
    Task<List<CountStatisticRequestModel>> GetExpensesByTypeCount();
    
    Task<List<CountStatisticRequestModel>> GetUsersIncomesCount(UserManager<AppUser> userManager);
    
    Task<List<CountStatisticRequestModel>> GetIncomesByTypeCount();
    
    Task<List<CountStatisticRequestModel>> GetUsersBudgetsCount(UserManager<AppUser> userManager);
    
    Task<List<CountStatisticRequestModel>> GetBudgetsByTimePeriodsCount();

    Task<List<CountStatisticRequestModel>> GetUsersGoalsCount(UserManager<AppUser> userManager);
}