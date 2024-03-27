namespace PFA_Mobile_v2.Domain.Entities;

/// <summary>
/// Расход
/// </summary>
public class Expense : BaseEntity
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
    /// Id типа расхода
    /// </summary>
    public int ExpenseTypeId { get; set; }
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Тип расхода
    /// </summary>
    public virtual ExpenseType ExpenseType { get; set; }

    /// <summary>
    /// Пользователь
    /// </summary>
    public virtual AppUser User { get; set; }
}