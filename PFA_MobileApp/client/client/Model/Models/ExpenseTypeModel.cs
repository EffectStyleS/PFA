using CommunityToolkit.Mvvm.ComponentModel;

namespace client.Model.Models;

/// <summary>
/// Модель типа расхода
/// </summary>
public partial class ExpenseTypeModel : BaseModel
{
    /// <summary>
    /// Название
    /// </summary>
    [ObservableProperty] private string _name;
}