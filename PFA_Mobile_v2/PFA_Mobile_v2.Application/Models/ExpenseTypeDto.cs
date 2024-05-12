using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Models;

/// <summary>
/// Объект передачи данных типа расхода
/// </summary>
public class ExpenseTypeDto
{
    /// <summary>
    /// Объект передачи данных типа расхода
    /// </summary>
    public ExpenseTypeDto() { }
    
    /// <summary>
    /// Объект передачи данных типа расхода
    /// </summary>
    public ExpenseTypeDto(ExpenseType expenseType)
    {
        Id   = expenseType.Id;
        Name = expenseType.Name;
    }

    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = string.Empty;
}