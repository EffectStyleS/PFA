namespace client.Infrastructure.Cache;

/// <summary>
/// Модель данных пользователя для аутентификации из кэша
/// </summary>
public class AuthCacheModel
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