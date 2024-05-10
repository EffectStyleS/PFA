using CommunityToolkit.Mvvm.ComponentModel;

namespace client.Model.Models;

public partial class ExpenseModel : BaseModel
{
    [ObservableProperty] private string _name;
        
    [ObservableProperty] private decimal _value;

    [ObservableProperty] private DateTime _date;
        
    [ObservableProperty] private int _expenseTypeId;
        
    [ObservableProperty] private string _expenseType; // для отображения
        
    public int UserId { get; set; }
}