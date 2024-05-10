using ApiClient;
using client.Infrastructure;
using client.Model.Models;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using System.Collections.ObjectModel;

namespace client.ViewModel;

public partial class IncomesMenuViewModel : BaseViewModel
{
    private readonly IPopupNavigation _popupNavigation;
    private readonly Client _client;

    public IncomesMenuViewModel(IPopupNavigation popupNavigation, Client client)
    {
        _popupNavigation = popupNavigation;
        _client = client;
        EventManager.OnUserExit += UserExitHandler;
    }

    private async Task GetAllIncomeTypes()
    {
        if (IncomeTypes is not null)
        {
            return;
        }

        List<IncomeTypeModel> result = [];

        var incomeTypesDto = await _client.IncomeTypeAllAsync();

        result.AddRange(incomeTypesDto.Select(incomeType => new IncomeTypeModel
        {
            Id = incomeType.Id,
            Name = incomeType.Name
        }));

        if (result.Count == 0)
        {
            var mainPage = Application.Current!.MainPage;
            if (mainPage is not null)
            {
                await mainPage.DisplayAlert("Fail", Resources.NullIncomeTypes, "OK");
            }
            
            return;
        }

        IncomeTypes = result;
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

        await GetAllIncomeTypes().ConfigureAwait(false);
        Incomes = [];

        ICollection<IncomeDto> result;

        try
        {
            result = _client.UserAll4Async(User.Id).Result;
        }
        catch
        {
            var mainPage = Application.Current!.MainPage;
            if (mainPage is not null)
            {
                await mainPage.DisplayAlert("Fail", Resources.GetUserIncomesFailed, "OK");
            }
            
            return;
        }

        foreach (var income in result)
        {
            Incomes.Add(new IncomeModel
            {
                Id = income.Id,
                Name = income.Name,
                Value = (decimal)income.Value,
                Date = income.Date.DateTime,
                IncomeTypeId = income.IncomeTypeId,
                IncomeType = IncomeTypes.FirstOrDefault(x => x.Id == income.IncomeTypeId)!.Name,
                UserId = income.UserId
            });
        }
    }

    [ObservableProperty] private ObservableCollection<IncomeModel> _incomes;

    [ObservableProperty] private List<IncomeTypeModel> _incomeTypes;

    [ObservableProperty] private UserModel? _user;

    [RelayCommand]
    private async Task DeleteIncome(IncomeModel income)
    {
        try
        {
            await _client.IncomeDELETEAsync(income.Id);
        }
        catch
        {
            var mainPage = Application.Current!.MainPage;
            if (mainPage is not null)
            {
                await mainPage.DisplayAlert("Fail", Resources.DeleteIncomeFailed, "OK");
            }
            
            return;
        }

        Incomes.Remove(income);
    }

    [RelayCommand]
    private async Task AddIncome()
    {
        if (User is not null)
        {
            IncomeModel income = new()
            {
                UserId = User.Id,
            };
        
            await _popupNavigation.PushAsync(new IncomesPopup(
                new IncomesPopupViewModel(_popupNavigation, _client, income, Incomes, IncomeTypes, false)));
        }
    }

    [RelayCommand]
    private async Task EditIncome(IncomeModel income)
    {
        await _popupNavigation.PushAsync(new IncomesPopup(
            new IncomesPopupViewModel(_popupNavigation, _client, income, Incomes, IncomeTypes, true)));
    }
        
    private Task UserExitHandler()
    {
        User = null;
        Incomes = [];
        return Task.CompletedTask;
    }
}