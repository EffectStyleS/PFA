using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Models;

/// <summary>
/// Объект передачи данных пользователя
/// </summary>
public class UserDto
{
    /// <summary>
    /// Объект передачи данных пользователя
    /// </summary>
    public UserDto() { }

    /// <summary>
    /// Объект передачи данных пользователя
    /// </summary>
    public UserDto(AppUser user)
    {
        Id       = user.Id;
        Login    = user.Login;

        RefreshToken           = user.RefreshToken;
        RefreshTokenExpiryTime = user.RefreshTokenExpiryTime;
    }

    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Рефреш токен
    /// </summary>
    public string? RefreshToken { get; set; }
    
    /// <summary>
    /// Время жизни рефреш токена
    /// </summary>
    public DateTime RefreshTokenExpiryTime { get; set; }
}
