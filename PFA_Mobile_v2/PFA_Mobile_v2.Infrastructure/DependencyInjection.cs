using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PFA_Mobile_v2.Application.AuthenticationInteraction.Interfaces;
using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Infrastructure.Authentication.Services;
using PFA_Mobile_v2.Infrastructure.Persistence;

namespace PFA_Mobile_v2.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddDbContext<PfaContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        return services;
    }
}