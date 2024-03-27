using Microsoft.AspNetCore.Identity;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;

/// <summary>
/// Интерфейс репозитория пользователя
/// </summary>
/// <typeparam name="T">Тип пользователя</typeparam>
public interface IUserRepository<T> where T : IdentityUser<int>
{
    /// <summary>
    /// Возвращает всех пользователей
    /// </summary>
    /// <param name="userManager">Менеджер пользователей</param>
    Task<List<T>> GetAll(UserManager<AppUser> userManager);

    /// <summary>
    /// Находит пользователя по логину
    /// </summary>
    /// <param name="login">Логин пользователя</param>
    /// <param name="userManager">Менеджер пользователей</param>
    Task<T?> GetItem(string login, UserManager<T> userManager);
    
    /// <summary>
    /// Создает пользователя
    /// </summary>
    /// <param name="item">Пользователь</param>
    /// <param name="password">Пароль</param>
    /// <param name="userManager">Менеджер пользователей</param>
    Task<IdentityResult> Create(T item, string password, UserManager<T> userManager);
    
    /// <summary>
    /// Обновляет данные пользователя
    /// </summary>
    /// <param name="item">Пользователь</param>
    /// <param name="userManager">Менеджер пользователей</param>
    Task<IdentityResult> Update(T item, UserManager<T> userManager);
    
    /// <summary>
    /// Удаляет пользователя
    /// </summary>
    /// <param name="login">Логин пользователя</param>
    /// <param name="userManager">Менеджер пользователей</param>
    Task<IdentityResult> Delete(string login, UserManager<T> userManager);
    
    /// <summary>
    /// Проверяет существование пользователя
    /// </summary>
    /// <param name="login">Логин пользователя</param>
    /// <param name="userManager">Менеджер пользователей</param>
    /// <returns>True - пользователь существует, иначе false</returns>
    Task<bool> Exists(string login, UserManager<T> userManager);

    /// <summary>
    /// Возвращает роли пользователя
    /// </summary>
    /// <param name="item">Пользователь</param>
    Task<List<IdentityRole<int>>> GetUserRoles(T item);
}