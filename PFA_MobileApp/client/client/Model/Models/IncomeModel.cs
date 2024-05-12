using CommunityToolkit.Mvvm.ComponentModel;

namespace client.Model.Models;

/// <summary>
/// Модель дохода
/// </summary>
public partial class IncomeModel : BaseModel
{
    /// <summary>
    /// Название
    /// </summary>
    [ObservableProperty] private string _name;
        
    /// <summary>
    /// Значение
    /// </summary>
    [ObservableProperty] private decimal _value;
        
    /// <summary>
    /// Дата
    /// </summary>
    [ObservableProperty] private DateTime _date;
        
    /// <summary>
    /// Id типа дохода
    /// </summary>
    [ObservableProperty] private int _incomeTypeId;
        
    /// <summary>
    /// Тип дохода
    /// </summary>
    [ObservableProperty] private string _incomeType;
        
    /// <summary>
    /// Id пользователя
    /// </summary>
    public int UserId { get; set; }
}