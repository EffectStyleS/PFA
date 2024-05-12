using ApiClient;
using client.Model.Models;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using System.Collections.ObjectModel;

namespace client.ViewModel;

/// <summary>
/// Модель представления страница добавления/редактирования бюджета
/// </summary>
[QueryProperty(nameof(Budget), "Budget")]
[QueryProperty(nameof(Budgets), "Budgets")]
[QueryProperty(nameof(ExpenseTypes), "ExpenseTypes")]
[QueryProperty(nameof(IncomeTypes), "IncomeTypes")]
[QueryProperty(nameof(TimePeriods), "TimePeriods")]
[QueryProperty(nameof(IsEdit), "IsEdit")]
public partial class BudgetAddEditViewModel : BaseViewModel
{
    private readonly IPopupNavigation _popupNavigation;
    private readonly Client _client;

    /// <summary>
    /// Модель представления страница добавления/редактирования бюджета
    /// </summary>
    /// <param name="popupNavigation">Навигация попапов</param>
    /// <param name="client">Клиент</param>
    public BudgetAddEditViewModel(IPopupNavigation popupNavigation, Client client)
    {
        _popupNavigation = popupNavigation;
        _client = client;
    }

    /// <summary>
    /// Заполнение данных после перехода на страницу
    /// </summary>
    public void CompleteDataAfterNavigation()
    {
        if (IsEdit)
        {
            PageTitle = "Edit Budget";

            Name = Budget.Name;
            StartDate = Budget.StartDate;
            TimePeriod = TimePeriods.FirstOrDefault(x => x.Id == Budget.TimePeriodId)!;
                
            return;
        }

        PageTitle = "Add Budget";

        StartDate = DateTime.Today;
        TimePeriod = TimePeriods[0];
    }

    /// <summary>
    /// Бюджет
    /// </summary>
    [ObservableProperty] private BudgetModel _budget;

    /// <summary>
    /// Название страницы
    /// </summary>
    [ObservableProperty] private string _pageTitle;

    /// <summary>
    /// Название
    /// </summary>
    [ObservableProperty] private string _name;

    /// <summary>
    /// Начальная дата
    /// </summary>
    [ObservableProperty] private DateTime _startDate;

    /// <summary>
    /// Временной период
    /// </summary>
    [ObservableProperty] private TimePeriodModel _timePeriod;

    /// <summary>
    /// Временные периоды
    /// </summary>
    [ObservableProperty] private List<TimePeriodModel> _timePeriods;  

    /// <summary>
    /// Типы расходов
    /// </summary>
    [ObservableProperty] private List<ExpenseTypeModel> _expenseTypes;

    /// <summary>
    /// Типы доходов
    /// </summary>
    [ObservableProperty] private List<IncomeTypeModel> _incomeTypes;

    /// <summary>
    /// Бюджетов
    /// </summary>
    [ObservableProperty] private ObservableCollection<BudgetModel> _budgets;

    /// <summary>
    /// Признак редактирования
    /// </summary>
    public bool IsEdit { get; set; }

    /// <summary>
    /// Команда сохранения
    /// </summary>
    [RelayCommand]
    private async Task Save()
    {
        Budget.Name = Name ?? "New Budget";
        Budget.StartDate = StartDate;
        Budget.TimePeriodId = TimePeriod.Id;
        Budget.TimePeriod = TimePeriod.Name;

        BudgetRequestModel postResult = new();

        try
        {
            var plannedExpensesRequest = new List<PlannedExpensesDto>();
            var plannedIncomesRequest = new List<PlannedIncomesDto>();

            if (IsEdit)
            {
                plannedExpensesRequest.AddRange(Budget.PlannedExpenses.Select(plannedExpenses =>
                    new PlannedExpensesDto
                    {
                        Id = plannedExpenses.Id, Sum = (double?)plannedExpenses.Sum,
                        BudgetId = plannedExpenses.BudgetId, ExpenseTypeId = plannedExpenses.ExpenseTypeId
                    }));

                plannedIncomesRequest.AddRange(Budget.PlannedIncomes.Select(plannedIncomes => new PlannedIncomesDto
                {
                    Id = plannedIncomes.Id, Sum = (double?)plannedIncomes.Sum, BudgetId = plannedIncomes.BudgetId,
                    IncomeTypeId = plannedIncomes.IncomeTypeId
                }));

                var budgetRequest = new BudgetRequestModel
                {
                    Budget = new BudgetDto
                    {
                        Id = Budget.Id,
                        Name = Budget.Name,
                        StartDate = Budget.StartDate,
                        TimePeriodId = Budget.TimePeriodId,
                        UserId = Budget.UserId,
                    },
                    PlannedExpenses = plannedExpensesRequest,
                    PlannedIncomes = plannedIncomesRequest
                };

                await _client.BudgetPUTAsync(Budget.Id, budgetRequest);
            }
            else
            {
                plannedExpensesRequest.AddRange(Budget.PlannedExpenses.Select(plannedExpenses =>
                    new PlannedExpensesDto
                    {
                        Sum = (double?)plannedExpenses.Sum, BudgetId = plannedExpenses.BudgetId,
                        ExpenseTypeId = plannedExpenses.ExpenseTypeId
                    }));

                plannedIncomesRequest.AddRange(Budget.PlannedIncomes.Select(plannedIncomes => new PlannedIncomesDto
                {
                    Sum = (double?)plannedIncomes.Sum, BudgetId = plannedIncomes.BudgetId,
                    IncomeTypeId = plannedIncomes.IncomeTypeId
                }));

                postResult = await _client.BudgetPOSTAsync(
                    new BudgetRequestModel
                    {
                        Budget = new BudgetDto
                        {
                            Name = Budget.Name,
                            StartDate = Budget.StartDate,
                            TimePeriodId = Budget.TimePeriodId,
                            UserId = Budget.UserId,
                        },
                        PlannedExpenses = plannedExpensesRequest,
                        PlannedIncomes = plannedIncomesRequest
                    });
            }
        }
        catch
        {
            var mainPage = Application.Current!.MainPage;
            if (mainPage is not null)
            {
                await mainPage.DisplayAlert("Fail", Resources.AddEditBudgetFailed, "OK");
            }
                
            return;
        }

        // обновление списка бюджетов
        if (IsEdit)
        {
            var found = Budgets.FirstOrDefault(x => x.Id == Budget.Id);
            if (found is not null)
            {
                var i = Budgets.IndexOf(found);
                Budgets[i] = Budget;
            }
        }
        else
        {
            var plannedExpensesResponse = postResult.PlannedExpenses.Select(plannedExpenses => new PlannedExpensesModel
                {
                    Id = plannedExpenses.Id,
                    Sum = (decimal?)plannedExpenses.Sum,
                    ExpenseTypeId = plannedExpenses.ExpenseTypeId,
                    ExpenseType = ExpenseTypes.FirstOrDefault(x => x.Id == plannedExpenses.ExpenseTypeId)!.Name,
                    BudgetId = plannedExpenses.BudgetId,
                })
                .ToList();

            var plannedIncomesResponse = postResult.PlannedIncomes.Select(plannedIncomes => new PlannedIncomesModel
                {
                    Id = plannedIncomes.Id,
                    Sum = (decimal?)plannedIncomes.Sum,
                    IncomeTypeId = plannedIncomes.IncomeTypeId,
                    IncomeType = IncomeTypes.FirstOrDefault(x => x.Id == plannedIncomes.IncomeTypeId)!.Name,
                    BudgetId = plannedIncomes.BudgetId,
                })
                .ToList();

            Budgets.Add(new BudgetModel
            {
                Id = postResult.Budget.Id,
                Name = postResult.Budget.Name,
                StartDate = postResult.Budget.StartDate.DateTime,
                TimePeriodId = postResult.Budget.TimePeriodId,
                TimePeriod = TimePeriods.FirstOrDefault(x => x.Id == postResult.Budget.TimePeriodId)!.Name,
                UserId = postResult.Budget.UserId,
                PlannedExpenses = plannedExpensesResponse,
                PlannedIncomes = plannedIncomesResponse,
            });
        }

        await Shell.Current.GoToAsync("..");
    }

    /// <summary>
    /// Команда отмены
    /// </summary>
    [RelayCommand]
    private async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }

    /// <summary>
    /// Команда открытия попапа запланированных расходов
    /// </summary>
    [RelayCommand]
    private async Task PlannedExpenses()
    {
        await _popupNavigation.PushAsync(new PlannedExpensesPopup(
            new PlannedExpensesPopupViewModel(_popupNavigation, Budget)));
    }
    
    /// <summary>
    /// Команда открытия попапа запланированных доходов
    /// </summary>
    [RelayCommand]
    private async Task PlannedIncomes()
    {
        await _popupNavigation.PushAsync(new PlannedIncomesPopup(
            new PlannedIncomesPopupViewModel(_popupNavigation, Budget)));
    }
}