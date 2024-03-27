using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Models;

/// <summary>
/// Объект передачи данных типа дохода
/// </summary>
public class IncomeTypeDto
{
    /// <summary>
    /// Объект передачи данных типа дохода
    /// </summary>
    public IncomeTypeDto() {}

    /// <summary>
    /// Объект передачи данных типа дохода
    /// </summary>
    public IncomeTypeDto(IncomeType incomeType)
    {
        Id   = incomeType.Id;
        Name = incomeType.Name;
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