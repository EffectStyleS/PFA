using Microsoft.AspNetCore.Identity;
using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Services;

/// <summary>
/// Сервис пользователей
/// </summary>
public class UserService : BaseService, IUserService
{
    /// <summary>
    /// Сервис пользователей
    /// </summary>
    public UserService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    /// <inheritdoc />
    public async Task AddToRoleAsync(UserDto userDto, string roleName, UserManager<AppUser> userManager)
    {
        var user = await userManager.FindByNameAsync(userDto.Login);

        if (user is null)
        {
            return;
        }
        
        await userManager.AddToRoleAsync(user, roleName);
    }

    /// <inheritdoc />
    public async Task<bool> CheckPasswordAsync(UserDto userDto, string password, UserManager<AppUser> userManager)
    {
        var user = await userManager.FindByNameAsync(userDto.Login);
        
        if (user is null)
        {
            return false;
        }
        
        return await userManager.CheckPasswordAsync(user, password);
    }

    /// <inheritdoc />
    public async Task<IdentityResult> CreateAsync(UserDto userDto, string password, UserManager<AppUser> userManager)
    {
        var newUser = new AppUser
        {
            Login = userDto.Login,
            UserName = userDto.Login, // для нахождения пользователя по имени
            RefreshToken = userDto.RefreshToken,
            RefreshTokenExpiryTime = userDto.RefreshTokenExpiryTime,
        };

        var result = await UnitOfWork.User.Create(newUser, password, userManager);

        if (result.Succeeded) 
        {
            await SaveAsync(); 
        }

        return result;
    }

    /// <inheritdoc />
    public async Task<List<UserDto>> GetAllAsync(UserManager<AppUser> userManager)
    {
        var users = await UnitOfWork.User.GetAll(userManager);

        var result = users
            .Select(u => new UserDto(u))
            .ToList();

        return result;
    }

    /// <inheritdoc />
    public async Task<UserDto?> GetByLoginAsync(string login, UserManager<AppUser> userManager)
    {
        var user = await UnitOfWork.User.GetItem(login, userManager);
        return user is null ? null : new UserDto(user);
    }
        
    /// <inheritdoc />
    public async Task<List<IdentityRole<int>>> GetUserRoles(UserDto userDto, UserManager<AppUser> userManager)
    {
        var user = await userManager.FindByNameAsync(userDto.Login);
        
        if (user is null)
        {
            return [];
        }
        
        return await UnitOfWork.User.GetUserRoles(user);
    }
}