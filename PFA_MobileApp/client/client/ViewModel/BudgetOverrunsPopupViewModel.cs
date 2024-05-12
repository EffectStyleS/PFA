using ApiClient;
using client.Infrastructure;
using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace client.ViewModel;

/// <summary>
/// Модель представления попапа превышения бюджетов
/// </summary>
public partial class BudgetOverrunsPopupViewModel : BaseViewModel
{
    private readonly Client _client;
    private readonly int _userId;

    /// <summary>
    /// Модель представления попапа превышения бюджетов
    /// </summary>
    /// <param name="client">Клиент</param>
    /// <param name="userId">Id пользователя</param>
    public BudgetOverrunsPopupViewModel(Client client, int userId)
    {
        _client = client;
        _userId = userId;
        BudgetOverruns = [];
            
        EventManager.OnUserExit += UserExitHandler;
        GetBudgetsDifferences().Wait();

        PageTitle = "Budget Overruns";
    }

    /// <summary>
    /// Получение превышений бюджетов
    /// </summary>
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

    /// <summary>
    /// Название страницы
    /// </summary>
    [ObservableProperty] private string _pageTitle;

    /// <summary>
    /// Превышения бюджетов
    /// </summary>
    [ObservableProperty] private ObservableCollection<BudgetOverrunsModel> _budgetOverruns;
        
    /// <summary>
    /// Обработчик выхода пользователя из аккаунта
    /// </summary>
    /// <returns></returns>
    private Task UserExitHandler()
    {
        BudgetOverruns = [];
        return Task.CompletedTask;
    }
}