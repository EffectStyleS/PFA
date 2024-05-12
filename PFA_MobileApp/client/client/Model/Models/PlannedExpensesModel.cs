using CommunityToolkit.Mvvm.ComponentModel;

namespace client.Model.Models;

/// <summary>
/// Модель запланированных расходов
/// </summary>
public partial class PlannedExpensesModel : BaseModel
{
    /// <summary>
    /// Сумма
    /// </summary>
    [ObservableProperty] private decimal? _sum;
    
    /// <summary>
    /// Id типа расхода
    /// </summary>
    [ObservableProperty] private int _expenseTypeId;
    
    /// <summary>
    /// Id бюджета
    /// </summary>
    [ObservableProperty] private int _budgetId;

    /// <summary>
    /// Тип расхода
    /// </summary>
    [ObservableProperty] private string _expenseType;
}