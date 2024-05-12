using CommunityToolkit.Mvvm.ComponentModel;

namespace client.Model.Models;

/// <summary>
/// Базовая модель
/// </summary>
public partial class BaseModel : ObservableObject
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
}