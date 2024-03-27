using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Models;

/// <summary>
/// Объект передачи данных расхода
/// </summary>
public class ExpenseDto
{
    /// <summary>
    /// Объект передачи данных расхода
    /// </summary>
    public ExpenseDto() { }

    /// <summary>
    /// Объект передачи данных расхода
    /// </summary>
    public ExpenseDto(Expense expense)
    {
        Id            = expense.Id;
        Name          = expense.Name;
        Value         = expense.Value;
        Date          = expense.Date;
        ExpenseTypeId = expense.ExpenseTypeId;
        UserId        = expense.UserId;
    }

    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

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
}