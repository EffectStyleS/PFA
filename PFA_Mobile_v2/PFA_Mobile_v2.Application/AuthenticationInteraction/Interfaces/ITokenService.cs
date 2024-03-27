using Microsoft.AspNetCore.Identity;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.AuthenticationInteraction.Interfaces;

/// <summary>
/// Интерфейс сервиса JWT
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Создает JWT
    /// </summary>
    /// <param name="userDto">Объект переноса данных пользователя</param>
    /// <param name="userManager">Менеджер пользователя</param>
    /// <param name="role">Роли пользователя</param>
    Task<string> CreateToken(UserDto userDto, UserManager<AppUser> userManager, List<IdentityRole<int>> role);
}