using CommunityToolkit.Mvvm.ComponentModel;

namespace client.Model.Models;

public partial class BudgetModel : BaseModel
{
    [ObservableProperty] private string _name;
        
    [ObservableProperty] private DateTime _startDate;
        
    [ObservableProperty] private int _timePeriodId;
        
    [ObservableProperty] private decimal? _balance;
    [ObservableProperty] private string _timePeriod;
        
    public int UserId { get; set; }
        
    public List<PlannedIncomesModel> PlannedIncomes { get; set; }
        
    public List<PlannedExpensesModel> PlannedExpenses { get; set; }
}