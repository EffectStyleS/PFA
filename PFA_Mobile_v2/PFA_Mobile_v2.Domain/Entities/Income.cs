namespace PFA_Mobile_v2.Domain.Entities;

/// <summary>
/// Доход
/// </summary>
public class Income : BaseEntity
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Значение
    /// </summary>
    public decimal Value { get; set; }
    
    /// <summary>
    /// Дата
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Id типа дохода
    /// </summary>
    public int IncomeTypeId { get; set; }
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Тип дохода
    /// </summary>
    public virtual IncomeType IncomeType { get; set; }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public virtual AppUser User { get; set; }
}