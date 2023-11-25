using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Infrastructure.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        //public static IServiceCollection DALAddDbContext(this IServiceCollection services, DbContextOptionsBuilder opt)
        //{
        //    services.AddDbContext<PFAContext>(opt);
        //    return services;
        //}
    }
}
