using CommunityToolkit.Mvvm.ComponentModel;

namespace client.Model.Models;

/// <summary>
/// Модель временной периода
/// </summary>
public partial class TimePeriodModel : BaseModel
{
    /// <summary>
    /// Название
    /// </summary>
    [ObservableProperty] private string _name;
}