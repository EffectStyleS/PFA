using CommunityToolkit.Mvvm.ComponentModel;

namespace client.Model.Models;

public partial class BaseModel : ObservableObject
{
    public int Id { get; set; }
}