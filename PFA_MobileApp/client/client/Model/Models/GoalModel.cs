using client.Model.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

namespace client.Model.Models;

/// <summary>
/// Модель цели
/// </summary>
public partial class GoalModel : BaseModel
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
    /// Конечная дата
    /// </summary>
    [ObservableProperty] private DateTime _endDate;
    
    /// <summary>
    /// Сумма
    /// </summary>
    [ObservableProperty] private decimal? _sum;
    
    /// <summary>
    /// Признак выполненности
    /// </summary>
    [ObservableProperty] private bool _isCompleted;
    
    /// <summary>
    /// Статус
    /// </summary>
    [ObservableProperty] private GoalStatuses _status;
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    public int UserId { get; set; }
}