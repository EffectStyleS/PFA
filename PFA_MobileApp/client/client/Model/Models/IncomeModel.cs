using CommunityToolkit.Mvvm.ComponentModel;

namespace client.Model.Models;

public partial class IncomeModel : BaseModel
{
    [ObservableProperty] private string _name;
        
    [ObservableProperty] private decimal _value;
        
    [ObservableProperty] private DateTime _date;
        
    [ObservableProperty] private int _incomeTypeId;
        
    [ObservableProperty] private string _incomeType; // для отображения
        
    public int UserId { get; set; }
}