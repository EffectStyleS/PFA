namespace PFA_Mobile_v2.Infrastructure.Authentication.Models;

/// <summary>
/// Ответ на запрос аутентификации
/// </summary>
public class AuthResponse
{
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; } = string.Empty;
    
    /// <summary>
    /// Токен доступа
    /// </summary>
    public string Token { get; set; } = string.Empty;
    
    /// <summary>
    /// Рефреш токен
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}