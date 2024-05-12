using System.Collections.ObjectModel;
using ApiClient;
using client.Infrastructure;
using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace client.ViewModel;

/// <summary>
/// Модель представления страницы истории
/// </summary>
public partial class HistoryPageViewModel : BaseViewModel
{
    private readonly Client _client;

    /// <summary>
    /// Модель представления страницы истории
    /// </summary>
    /// <param name="client">Клиент</param>
    public HistoryPageViewModel(Client client)
    {
        _client = client;
        EventManager.OnUserExit += UserExitHandler;
    }

    /// <summary>
    /// Заполнение данных после перехода на страницу
    /// </summary>
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
        
        Incomes = [];
        ICollection<IncomeDto> incomesResult;
        try
        {
            incomesResult = _client.UserAll4Async(User.Id).Result;
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

        foreach (var income in incomesResult)
        {
            Incomes.Add(new IncomeModel
            {
                Id = income.Id,
                Name = income.Name,
                Value = (decimal)income.Value,
                Date = income.Date.DateTime,
                IncomeTypeId = income.IncomeTypeId,
                IncomeType = string.Empty,
                UserId = income.UserId
            });
        }
        
        Expenses = [];
        ICollection<ExpenseDto> expensesResult;
        try
        {
            expensesResult = _client.UserAll2Async(User.Id).Result;
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

        foreach (var expense in expensesResult)
        {
            Expenses.Add(new ExpenseModel
            {
                Id = expense.Id,
                Name = expense.Name,
                Value = (decimal)expense.Value,
                Date = expense.Date.DateTime,
                ExpenseTypeId = expense.ExpenseTypeId,
                ExpenseType = string.Empty,
                UserId = expense.UserId
            });
        }

        HistoryRecords = [];
        var historyList = Incomes.Select(income => new HistoryRecordModel
        {
            Date = income.Date.Date,
            Name = income.Name,
            Value = income.Value
        }).ToList();
        
        historyList.AddRange(Expenses.Select(expense => new HistoryRecordModel
        {
            Date = expense.Date.Date,
            Name = expense.Name,
            Value = -expense.Value
        }));

        var groups = historyList
            .GroupBy(x => x.Date)
            .OrderByDescending(g => g.Key)
            .Select(g => new Grouping<DateTime, HistoryRecordModel>(g.Key, g));
        
        HistoryRecords = new ObservableCollection<Grouping<DateTime, HistoryRecordModel>>(groups);
    }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    [ObservableProperty] private UserModel? _user;
    
    // Привязка адекватно не работает почему-то, видимо баг
    /// <summary>
    /// Ключ
    /// </summary>
    [ObservableProperty] private DateTime _key;
    
    /// <summary>
    /// Доходы
    /// </summary>
    [ObservableProperty] private ObservableCollection<IncomeModel> _incomes;
    
    /// <summary>
    /// Расходы
    /// </summary>
    [ObservableProperty] private ObservableCollection<ExpenseModel> _expenses;
    
    /// <summary>
    /// Записи истории
    /// </summary>
    [ObservableProperty] private ObservableCollection<Grouping<DateTime, HistoryRecordModel>> _historyRecords;
    
    /// <summary>
    /// Обработчик выхода пользователя
    /// </summary>
    private Task UserExitHandler()
    {
        User = null;
        Incomes = [];
        Expenses = [];
        HistoryRecords = [];
        return Task.CompletedTask;
    }
}