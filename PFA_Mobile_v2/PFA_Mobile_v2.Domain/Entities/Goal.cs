namespace PFA_Mobile_v2.Domain.Entities;

/// <summary>
/// Цель
/// </summary>
public class Goal : BaseEntity
{
    /// <summary>
    /// Название цели
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Начальная дата
    /// </summary>
    public DateTime StartDate { get; set; }
    
    /// <summary>
    /// Конечная дата
    /// </summary>
    public DateTime? EndDate { get; set; }
    
    /// <summary>
    /// Сумма цели
    /// </summary>
    public decimal? Sum { get; set; }
    
    /// <summary>
    /// Признак выполнения
    /// </summary>
    public bool IsCompleted { get; set; }
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Пользователь
    /// </summary>
    public virtual AppUser User { get; set; }
}