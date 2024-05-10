using CommunityToolkit.Mvvm.ComponentModel;

namespace client.Model.Models;

public partial class PlannedExpensesModel : BaseModel
{
    [ObservableProperty] private decimal? _sum;
    
    [ObservableProperty] private int _expenseTypeId;
    
    [ObservableProperty] private int _budgetId;

    [ObservableProperty] private string _expenseType;
}