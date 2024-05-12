using CommunityToolkit.Mvvm.ComponentModel;

namespace client.Model.Models;

/// <summary>
/// Модель бюджета
/// </summary>
public partial class BudgetModel : BaseModel
{
    /// <summary>
    /// Название
    /// </summary>
    [ObservableProperty] private string _name;
        
    /// <summary>
    /// Начальная дата
    /// </summary>
    [ObservableProperty] private DateTime _startDate;
        
    /// <summary>
    /// Id временного периода
    /// </summary>
    [ObservableProperty] private int _timePeriodId;

    /// <summary>
    /// Сальдо
    /// </summary>
    [ObservableProperty] private decimal? _balance;
    
    /// <summary>
    /// Временной период
    /// </summary>
    [ObservableProperty] private string _timePeriod;
        
    /// <summary>
    /// Id пользователя
    /// </summary>
    public int UserId { get; set; }
        
    /// <summary>
    /// Запланированные доходы
    /// </summary>
    public List<PlannedIncomesModel> PlannedIncomes { get; set; }
        
    /// <summary>
    /// Запланированные расходы
    /// </summary>
    public List<PlannedExpensesModel> PlannedExpenses { get; set; }
}