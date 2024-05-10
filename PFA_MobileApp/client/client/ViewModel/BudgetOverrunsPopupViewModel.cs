using ApiClient;
using client.Infrastructure;
using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;


namespace client.ViewModel;

public partial class BudgetOverrunsPopupViewModel : BaseViewModel
{
    private readonly Client _client;
    private readonly int _userId;

    public BudgetOverrunsPopupViewModel(Client client, int userId)
    {
        _client = client;
        _userId = userId;
        BudgetOverruns = [];
            
        EventManager.OnUserExit += UserExitHandler;
        GetBudgetsDifferences().Wait();

        PageTitle = "Budget Overruns";
    }

    private async Task GetBudgetsDifferences()
    {
        ICollection<BudgetOverrunDto> result;

        try
        {
            result = _client.DifferenceAsync(_userId).Result;
        }
        catch
        {
            var mainPage = Application.Current!.MainPage;
            if (mainPage is not null)
            {
                await mainPage.DisplayAlert("Fail", Resources.GetDifferenceFailed, "OK");
            }
                
            return;
        }
            
        foreach (var item in result)
        {
            BudgetOverruns.Add(new BudgetOverrunsModel
            {
                BudgetName = item.BudgetName,
                ExpenseType = item.ExpenseType,
                Difference = (decimal)item.Difference!,
            });
        }
    }

    [ObservableProperty] private string _pageTitle;

    [ObservableProperty] private ObservableCollection<BudgetOverrunsModel> _budgetOverruns;
        
    private Task UserExitHandler()
    {
        BudgetOverruns = [];
        return Task.CompletedTask;
    }
}