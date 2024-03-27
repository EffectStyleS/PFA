using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Application.Models;

/// <summary>
/// Объект передачи данных дохода
/// </summary>
public class IncomeDto
{
    /// <summary>
    /// Объект передачи данных дохода
    /// </summary>
    public IncomeDto() {}

    /// <summary>
    /// Объект передачи данных дохода
    /// </summary>
    public IncomeDto(Income income)
    {
        Id           = income.Id;
        Name         = income.Name;
        Value        = income.Value;
        Date         = income.Date;
        UserId       = income.UserId;
        IncomeTypeId = income.IncomeTypeId;
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
    /// Id пользователя
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Id типа дохода
    /// </summary>
    public int IncomeTypeId { get; set; }
}