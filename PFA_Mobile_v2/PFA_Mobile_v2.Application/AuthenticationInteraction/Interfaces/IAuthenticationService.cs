using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.AuthenticationInteraction.Interfaces;

/// <summary>
/// Интерфейс сервиса аутентификации
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Создание рефреш токена пользователя
    /// </summary>
    /// <param name="userDto">Объект передачи данных пользователя</param>
    /// <param name="userManager">Менеджер пользователя</param>
    /// <param name="configuration">Конфигурация</param>
    Task<bool> CreateRefreshToken(UserDto userDto, UserManager<AppUser> userManager, IConfiguration configuration);
    
    /// <summary>
    /// Сброс рефреш токена пользователя
    /// </summary>
    /// <param name="userDto">Объект передачи данных пользователя</param>
    /// <param name="userManager">Менеджер пользователя</param>
    /// <returns>True - успешно, иначе false</returns>
    Task<bool> ResetRefreshToken(UserDto userDto, UserManager<AppUser> userManager);
    
    /// <summary>
    /// Сброс рефреш токенов всех пользователей
    /// </summary>
    /// <param name="userManager">Менеджер пользователя</param>
    /// <returns>True - успешно, иначе false</returns>
    Task<bool> ResetAllRefreshTokens(UserManager<AppUser> userManager);
}