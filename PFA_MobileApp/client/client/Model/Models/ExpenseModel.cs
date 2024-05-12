using CommunityToolkit.Mvvm.ComponentModel;

namespace client.Model.Models;

/// <summary>
/// Модель расхода
/// </summary>
public partial class ExpenseModel : BaseModel
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
    /// Id типа расхода
    /// </summary>
    [ObservableProperty] private int _expenseTypeId;
        
    /// <summary>
    /// Тип расхода
    /// </summary>
    [ObservableProperty] private string _expenseType;
        
    /// <summary>
    /// Id пользователя
    /// </summary>
    public int UserId { get; set; }
}