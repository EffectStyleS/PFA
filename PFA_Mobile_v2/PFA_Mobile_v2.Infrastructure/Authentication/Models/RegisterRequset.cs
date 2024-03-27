using System.ComponentModel.DataAnnotations;

namespace PFA_Mobile_v2.Infrastructure.Authentication.Models;

/// <summary>
/// Запрос регистрации
/// </summary>
public class RegisterRequest
{
    /// <summary>
    /// Логин
    /// </summary>
    [Required]
    public string Login { get; set; } = null!;

    /// <summary>
    /// Пароль
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    /// <summary>
    /// Подтверждение пароля
    /// </summary>
    [Required]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    public string PasswordConfirm { get; set; } = null!;
}