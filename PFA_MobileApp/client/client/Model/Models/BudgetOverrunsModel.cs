namespace client.Model.Models;

/// <summary>
/// Модель для отображения превышений доходов
/// </summary>
public class BudgetOverrunsModel
{
    /// <summary>
    /// Название бюджета
    /// </summary>
    public string BudgetName { get; set; }
    
    /// <summary>
    /// Тип расхода
    /// </summary>
    public string ExpenseType { get; set; }
    
    /// <summary>
    /// Разница
    /// </summary>
    public decimal Difference { get; set; }
}