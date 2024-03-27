using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Models;

/// <summary>
/// Объект передачи данных временного периода
/// </summary>
public class TimePeriodDto
{
    /// <summary>
    /// Объект передачи данных временного периода
    /// </summary>
    public TimePeriodDto() { }

    /// <summary>
    /// Объект передачи данных временного периода
    /// </summary>
    public TimePeriodDto(TimePeriod timePeriod)
    {
        Id   = timePeriod.Id;
        Name = timePeriod.Name;
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