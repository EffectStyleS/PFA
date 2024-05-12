namespace client.Model.Models;

/// <summary>
/// Модель пользователя
/// </summary>
public class UserModel : BaseModel
{
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Refresh токен
    /// </summary>
    public string RefreshToken { get; set; }
    
    /// <summary>
    /// Время истечения refresh токена
    /// </summary>
    public DateTime RefreshTokenExpireTime { get; set; }
}