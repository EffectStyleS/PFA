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

    // TODO: возможно, не нужно хранить и создавать расходы и запланированные расходы для каждого типа расхода, это же просто справочник
    // Если мы вдруг удалим тип расхода, то просто в expense поставим тип "Другое", а не удалим все расходы с таким типом
    // и пересчитаем сумму расходов и запланированных расходов для типа "Другое", тип "Другое" - неудаляемый
    // с временным периодом бюджета то же самое, просто в бюджет ставим временнной период Месяц, а месяц делаем неудаляемы
}