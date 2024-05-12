using Microsoft.Extensions.DependencyInjection;
using PFA_Mobile_v2.Application.AuthenticationInteraction.Interfaces;
using PFA_Mobile_v2.Application.Services;
using PFA_Mobile_v2.Application.Services.Interfaces;

namespace PFA_Mobile_v2.Application;

/// <summary>
/// Внедрение зависимостей слоя Application
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Внедряет зависимости слоя Application
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <returns>Коллекция сервисов</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IBudgetService, BudgetService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<IExpenseTypeService, ExpenseTypeService>();
        services.AddScoped<IGoalService, GoalService>();
        services.AddScoped<IIncomeService, IncomeService>();
        services.AddScoped<IIncomeTypeService, IncomeTypeService>();
        services.AddScoped<IPlannedExpensesService, PlannedExpensesService>();
        services.AddScoped<IPlannedIncomesService, PlannedIncomesService>();
        services.AddScoped<ITimePeriodService, TimePeriodService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IStatisticsService, StatisticsService>();

        return services;
    }
}