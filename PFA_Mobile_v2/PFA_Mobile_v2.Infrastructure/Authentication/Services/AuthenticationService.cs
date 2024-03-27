using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PFA_Mobile_v2.Application.AuthenticationInteraction.Interfaces;
using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services;
using PFA_Mobile_v2.Domain.Entities;
using PFA_Mobile_v2.Infrastructure.Authentication.Extensions;

namespace PFA_Mobile_v2.Infrastructure.Authentication.Services;

/// <summary>
/// Сервис аутентификации
/// </summary>
public class AuthenticationService : BaseService, IAuthenticationService
{
    /// <summary>
    /// Сервис аутентификации
    /// </summary>
    public AuthenticationService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    
    /// <inheritdoc />
    public async Task<bool> ResetAllRefreshTokens(UserManager<AppUser> userManager)
    {
        var userList = await userManager.Users.ToListAsync();

        foreach (var user in userList)
        {
            user.RefreshToken = null;
            var foundUser = await UnitOfWork.User.GetItem(user.Login, userManager);
            var userDto = foundUser is null ? null : new UserDto(user);
            
            if (userDto is null)
            {
                return false;
            }
            
            userDto.RefreshToken = null;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return false;
            }
        }

        return await SaveAsync();
    }

    /// <inheritdoc />
    public async Task<bool> ResetRefreshToken(UserDto userDto, UserManager<AppUser> userManager)
    {
        userDto.RefreshToken = null;

        var user = await userManager.FindByNameAsync(userDto.Login);
        
        if (user is null)
        {
            return false;
        }
        
        user.RefreshToken = null;

        var result = await UnitOfWork.User.Update(user, userManager);
        return result.Succeeded && await SaveAsync();
    }
    
    /// <inheritdoc />
    public async Task<bool> CreateRefreshToken(UserDto userDto, UserManager<AppUser> userManager, IConfiguration configuration)
    {
        userDto.RefreshToken = configuration.GenerateRefreshToken();
        
        var refreshTokenValidityInDays = configuration.GetSection("Jwt:RefreshTokenValidityInDays").Value;
        if (refreshTokenValidityInDays is null)
        {
            return false;
        }
        
        userDto.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(int.Parse(refreshTokenValidityInDays));

        var user = await userManager.FindByNameAsync(userDto.Login);
        if (user is null)
        {
            return false;
        }
        
        user.RefreshToken = userDto.RefreshToken;
        user.RefreshTokenExpiryTime = userDto.RefreshTokenExpiryTime;

        var result = await UnitOfWork.User.Update(user, userManager);
        return result.Succeeded && await SaveAsync();
    }
}