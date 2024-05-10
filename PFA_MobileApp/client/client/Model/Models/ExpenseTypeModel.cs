using CommunityToolkit.Mvvm.ComponentModel;

namespace client.Model.Models;

public partial class ExpenseTypeModel : BaseModel
{
    [ObservableProperty] private string _name;
}