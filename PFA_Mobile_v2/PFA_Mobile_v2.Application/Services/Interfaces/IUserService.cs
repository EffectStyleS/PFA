using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Services.Interfaces;

/// <summary>
/// Интерфейс сервиса пользователей 
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Возвращает пользователя по логину
    /// </summary>
    /// <param name="login">Логин</param>
    /// <param name="userManager">Менеджер пользователя</param>
    Task<UserDto?> GetByLoginAsync(string login, UserManager<AppUser> userManager);
    
    /// <summary>
    /// Проверяет пароль пользователя
    /// </summary>
    /// <param name="userDto">Объект передачи данных пользователя</param>
    /// <param name="password">Пароль</param>
    /// <param name="userManager">Менеджер пользователя</param>
    /// <returns>True - пароль верный, иначе - false</returns>
    Task<bool> CheckPasswordAsync(UserDto userDto, string password, UserManager<AppUser> userManager);
    
    /// <summary>
    /// Возвращает всех пользователей
    /// </summary>
    /// <param name="userManager">Менеджер пользователя</param>
    Task<List<UserDto>> GetAllAsync(UserManager<AppUser> userManager);
    
    /// <summary>
    /// Создает пользователя
    /// </summary>
    /// <param name="userDto">Объект передачи данных пользователя</param>
    /// <param name="password">Пароль</param>
    /// <param name="userManager">Менеджер пользователя</param>
    Task<IdentityResult> CreateAsync(UserDto userDto, string password, UserManager<AppUser> userManager);       
    
    /// <summary>
    /// Добавляет роль пользователю
    /// </summary>
    /// <param name="userDto">Объект передачи данных пользователя</param>
    /// <param name="roleName">Название роли</param>
    /// <param name="userManager">Менеджер пользователя</param>
    Task AddToRoleAsync(UserDto userDto, string roleName, UserManager<AppUser> userManager);
    
    /// <summary>
    /// Получение ролей пользователя
    /// </summary>
    /// <param name="userDto">Объект передачи данных пользователя</param>
    /// <param name="userManager">Менеджер пользователя</param>
    Task<List<IdentityRole<int>>> GetUserRoles(UserDto userDto, UserManager<AppUser> userManager);
}