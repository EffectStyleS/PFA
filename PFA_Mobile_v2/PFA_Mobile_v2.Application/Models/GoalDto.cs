using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Models;

/// <summary>
/// Объект передачи данных цели
/// </summary>
public class GoalDto
{
    /// <summary>
    /// Объект передачи данных цели
    /// </summary>
    public GoalDto() { }

    /// <summary>
    /// Объект передачи данных цели
    /// </summary>
    public GoalDto(Goal goal) 
    {
        Id          = goal.Id;
        Name        = goal.Name;
        StartDate   = goal.StartDate;
        EndDate     = goal.EndDate;
        Sum         = goal.Sum;
        IsCompleted = goal.IsCompleted;
        UserId      = goal.UserId;
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
    /// Начальная дата
    /// </summary>
    public DateTime StartDate { get; set; }
    
    /// <summary>
    /// Конечная дата
    /// </summary>
    public DateTime? EndDate { get; set; }
    
    /// <summary>
    /// Сумма
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
}