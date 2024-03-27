namespace PFA_Mobile_v2.Infrastructure.Authentication.Models;

/// <summary>
/// Запрос аутентификации
/// </summary>
public class AuthRequest
{
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; } = string.Empty;
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } = string.Empty;
}