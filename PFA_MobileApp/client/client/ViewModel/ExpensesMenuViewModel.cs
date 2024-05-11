using ApiClient;
using client.Infrastructure;
using client.Model.Models;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using System.Collections.ObjectModel;

namespace client.ViewModel;

[QueryProperty(nameof(User), "User")]
public partial class ExpensesMenuViewModel : BaseViewModel
{
    private readonly IPopupNavigation _popupNavigation;
    private readonly Client _client;

    public ExpensesMenuViewModel(IPopupNavigation popupNavigation, Client client)
    {
        _popupNavigation = popupNavigation;
        _client = client;
        EventManager.OnUserExit += UserExitHandler;
            
    }

    private async Task GetAllExpenseTypes()
    {
        if (ExpenseTypes is not null)
        {
            return;
        }

        List<ExpenseTypeModel> result = [];

        var expenseTypesDto = await _client.ExpenseTypeAllAsync();

        result.AddRange(expenseTypesDto.Select(expenseTypeDto => new ExpenseTypeModel
        {
            Id = expenseTypeDto.Id,
            Name = expenseTypeDto.Name
        }));

        if (result.Count == 0)
        {
            var mainPage = Application.Current!.MainPage;
            if (mainPage is not null)
            {
                await mainPage.DisplayAlert("Fail", Resources.NullExpenseTypes, "OK");
            }
                
            return;
        }

        ExpenseTypes = result;
    }

    public async Task CompleteDataAfterNavigation()
    {
        var userLogin = _client.GetCurrentUserLogin();
        var userDto = await _client.UserAsync(userLogin);
        
        User = new UserModel
        {
            Id = userDto.Id,
            Login = userDto.Login,
            RefreshToken = userDto.RefreshToken,
            RefreshTokenExpireTime = userDto.RefreshTokenExpiryTime.DateTime
        };

        await GetAllExpenseTypes();
        Expenses = [];

        ICollection<ExpenseDto> result;

        try
        {
            result = _client.UserAll2Async(User.Id).Result;
        }
        catch
        {
            var mainPage = Application.Current!.MainPage;
            if (mainPage is not null)
            {
                await mainPage.DisplayAlert("Fail", Resources.GetUserExpensesFailed, "OK");
            }
                
            return;
        }

        foreach (var expense in result)
        {
            Expenses.Add(new ExpenseModel
            {
                Id = expense.Id,
                Name = expense.Name,
                Value = (decimal)expense.Value,
                Date = expense.Date.DateTime,
                ExpenseTypeId = expense.ExpenseTypeId,
                ExpenseType = ExpenseTypes.FirstOrDefault(x => x.Id == expense.ExpenseTypeId)!.Name,
                UserId = expense.UserId
            });
        }
    }

    [ObservableProperty] private ObservableCollection<ExpenseModel> _expenses;

    [ObservableProperty] private List<ExpenseTypeModel> _expenseTypes;

    [ObservableProperty] private UserModel? _user;

    [RelayCommand]
    private async Task DeleteExpense(ExpenseModel expense)
    {
        try
        {
            await _client.ExpenseDELETEAsync(expense.Id);
        }
        catch
        {
            var mainPage = Application.Current!.MainPage;
            if (mainPage != null)
            {
                await mainPage.DisplayAlert("Fail", Resources.DeleteExpenseFailed, "OK");
            }
                
            return;
        }

        Expenses.Remove(expense);
    }

    [RelayCommand]
    private async Task AddExpense()
    {
        if (User is not null)
        {
            ExpenseModel expense = new()
            {
                UserId = User.Id,
            };

            await _popupNavigation.PushAsync(new ExpensesPopup(
                new ExpensesPopupViewModel(_popupNavigation, _client, expense, Expenses, ExpenseTypes, false)));
        }
    }

    [RelayCommand]
    private Task EditExpense(ExpenseModel expense)
    {
        return _popupNavigation.PushAsync(new ExpensesPopup(
            new ExpensesPopupViewModel(_popupNavigation, _client, expense, Expenses, ExpenseTypes, true)));
    }

    [RelayCommand]
    private async Task OpenBudgetOverruns()
    {
        if (User is not null)
        {
            await _popupNavigation.PushAsync(new BudgetOverrunsPopup(
                new BudgetOverrunsPopupViewModel(_client, User.Id)));
        }
    }
    
    [RelayCommand]
    private async Task GoToExpensesStatistics()
    {
        if (User is not null)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Expenses", Expenses }
            };

            await Shell.Current.GoToAsync($"{nameof(ExpensesStatisticsPage)}", navigationParameter);
        }
    }

    private Task UserExitHandler()
    {
        User = null;
        Expenses = [];
        return Task.CompletedTask;
    }
}