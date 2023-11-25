using BLL.Interfaces;
using BLL.Interfaces.Identity;
using BLL.Services;
using BLL.Services.Identity;
using DAL.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesScoped(this IServiceCollection services)
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

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }

        public static IServiceCollection AddUnitOfWorkScoped(this IServiceCollection services)
        {
            services.AddUnitOfWork();
            return services;
        }

        //public static IServiceCollection AddDbContext(this IServiceCollection services, DbContextOptionsBuilder opt)
        //{
        //    services.DALAddDbContext(opt);
        //    return services;
        //}
    }
}
