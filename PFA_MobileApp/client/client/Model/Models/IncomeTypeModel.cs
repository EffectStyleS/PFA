using CommunityToolkit.Mvvm.ComponentModel;

namespace client.Model.Models;

public partial class IncomeTypeModel : BaseModel
{
    [ObservableProperty] private string _name;
}