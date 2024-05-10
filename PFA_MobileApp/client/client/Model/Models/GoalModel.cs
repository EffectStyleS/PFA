using client.Model.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

namespace client.Model.Models;

public partial class GoalModel : BaseModel
{
    [ObservableProperty] private string _name;
    
    [ObservableProperty] private DateTime _startDate;
    
    [ObservableProperty] private DateTime _endDate;
    
    [ObservableProperty] private decimal? _sum;
    
    [ObservableProperty] private bool _isCompleted;
    
    [ObservableProperty] private GoalStatuses _status;
    
    public int UserId { get; set; }
}