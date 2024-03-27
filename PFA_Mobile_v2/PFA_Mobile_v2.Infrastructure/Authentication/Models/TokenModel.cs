namespace PFA_Mobile_v2.Infrastructure.Authentication.Models;

/// <summary>
/// Модель токена
/// </summary>
public class TokenModel
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}