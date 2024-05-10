using CommunityToolkit.Mvvm.ComponentModel;

namespace client.Model.Models;

public partial class TimePeriodModel : BaseModel
{
    [ObservableProperty] private string _name;
}