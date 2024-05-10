using CommunityToolkit.Mvvm.ComponentModel;

namespace client.Model.Models;

public partial class PlannedIncomesModel : BaseModel
{
    [ObservableProperty] private decimal? _sum;
    
    [ObservableProperty] private int _incomeTypeId;
    
    [ObservableProperty] private int _budgetId;
    
    [ObservableProperty] private string _incomeType;
}