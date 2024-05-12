using CommunityToolkit.Mvvm.ComponentModel;

namespace client.Model.Models;

/// <summary>
/// Модель запланированных доходов
/// </summary>
public partial class PlannedIncomesModel : BaseModel
{
    /// <summary>
    /// Сумма
    /// </summary>
    [ObservableProperty] private decimal? _sum;
    
    /// <summary>
    /// Id типа дохода
    /// </summary>
    [ObservableProperty] private int _incomeTypeId;
    
    /// <summary>
    /// Id бюджета
    /// </summary>
    [ObservableProperty] private int _budgetId;
    
    /// <summary>
    /// Тип дохода
    /// </summary>
    [ObservableProperty] private string _incomeType;
}