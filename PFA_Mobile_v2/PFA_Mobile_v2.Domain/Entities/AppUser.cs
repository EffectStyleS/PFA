using Microsoft.AspNetCore.Identity;

namespace PFA_Mobile_v2.Domain.Entities;

/// <summary>
/// Пользователь
/// </summary>
public class AppUser : IdentityUser<int>
{
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

    /// <summary>
    /// Коллекция расходов
    /// </summary>
    public ICollection<Expense> Expenses { get; set; } = new HashSet<Expense>();
    
    /// <summary>
    /// Коллекция доходов
    /// </summary>
    public ICollection<Income> Incomes { get; set; } = new HashSet<Income>();
    
    /// <summary>
    /// Коллекция бюджетов
    /// </summary>
    public ICollection<Budget> Budgets { get; set; } = new HashSet<Budget>();
    
    /// <summary>
    /// Коллекция целей
    /// </summary>
    public ICollection<Goal> Goals { get; set; } = new HashSet<Goal>();
}