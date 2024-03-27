using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Models;

/// <summary>
/// Объект передачи данных бюджета
/// </summary>
public class BudgetDto
{
    /// <summary>
    /// Объект передачи данных бюджета
    /// </summary>
    public BudgetDto() { }

    /// <summary>
    /// Объект передачи данных бюджета
    /// </summary>
    public BudgetDto(Budget budget)
    {
        Id              = budget.Id;
        Name            = budget.Name;
        StartDate       = budget.StartDate;
        TimePeriodId    = budget.TimePeriodId;
        UserId          = budget.UserId;
    }

    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Начальная дата
    /// </summary>
    public DateTime StartDate { get; set; }
    
    /// <summary>
    /// Id временного периода
    /// </summary>
    public int TimePeriodId { get; set; }
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    public int UserId { get; set; }
}