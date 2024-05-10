using ApiClient;
using client.Infrastructure;
using client.Model.Interfaces;
using client.Model.Models;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace client.ViewModel;

public partial class BudgetsMenuViewModel : BaseViewModel
{
    private readonly Client _client;
    private readonly IBudgetService _budgetService;

    public BudgetsMenuViewModel(Client client, IBudgetService budgetService)
    {
        _client = client;
        _budgetService = budgetService;
        EventManager.OnUserExit += UserExitHandler;
    }

    private async Task GetAllTimePeriods()
    {
        if (TimePeriods is not null)
        {
            return;
        }

        List<TimePeriodModel> result = [];

        var timePeriodsDto = await _client.TimePeriodAllAsync();

        result.AddRange(timePeriodsDto.Select(timePeriod => new TimePeriodModel
        { 
            Id = timePeriod.Id,
            Name = timePeriod.Name
        }));

        if (result.Count == 0)
        {
            var mainPage = Application.Current!.MainPage;
            if (mainPage != null)
            {
                await mainPage.DisplayAlert("Fail", Resources.NullTimePeriods, "OK");
            }
                
            return;
        }

        TimePeriods = result;
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

        await GetAllTimePeriods();
        await GetAllIncomeTypes();
        await GetAllExpenseTypes();
        Budgets = [];

        ICollection<BudgetDto> budgetResult;

        try
        {
            budgetResult = _client.UserAllAsync(User.Id).Result;
        }
        catch
        {
            var mainPage = Application.Current!.MainPage;
            if (mainPage is not null)
            {
                await mainPage.DisplayAlert("Fail", Resources.GetUserBudgetsFailed, "OK");
            }
                
            return;
        }

        foreach (var budget in budgetResult)
        {
            Budgets.Add(new BudgetModel
            {
                Id = budget.Id,
                Name = budget.Name,
                StartDate = budget.StartDate.DateTime,
                TimePeriodId = budget.TimePeriodId,
                TimePeriod = TimePeriods.FirstOrDefault(x => x.Id == budget.TimePeriodId)!.Name,
                UserId = budget.UserId,
            });
        }

        foreach (var budgetDto in budgetResult)
        {
            ICollection<PlannedExpensesDto> plannedExpensesResult;
            ICollection<PlannedIncomesDto> plannedIncomesResult;
            
            try
            {
                plannedExpensesResult = _client.BudgetAsync(budgetDto.Id).Result;
                plannedIncomesResult = _client.Budget2Async(budgetDto.Id).Result;
            }
            catch
            {
                var mainPage = Application.Current!.MainPage;
                if (mainPage is not null)
                {
                    await mainPage.DisplayAlert("Fail", Resources.GetPlannedIncomesExpensesFailed, "OK");
                }
                    
                return;
            }

            var plannedExpensesModels = plannedExpensesResult.Select(plannedExpenses => new PlannedExpensesModel
                {
                    Id = plannedExpenses.Id,
                    Sum = (decimal?)plannedExpenses.Sum,
                    ExpenseTypeId = plannedExpenses.ExpenseTypeId,
                    ExpenseType = ExpenseTypes.FirstOrDefault(x => x.Id == plannedExpenses.ExpenseTypeId)!.Name,
                    BudgetId = plannedExpenses.BudgetId,
                })
                .ToList();
                
            Budgets.FirstOrDefault(x => x.Id == budgetDto.Id)!.PlannedExpenses = plannedExpensesModels;

            var plannedIncomesModels = plannedIncomesResult.Select(plannedIncomes => new PlannedIncomesModel
                {
                    Id = plannedIncomes.Id,
                    Sum = (decimal?)plannedIncomes.Sum,
                    IncomeTypeId = plannedIncomes.IncomeTypeId,
                    IncomeType = IncomeTypes.FirstOrDefault(x => x.Id == plannedIncomes.IncomeTypeId)!.Name,
                    BudgetId = plannedIncomes.BudgetId,
                })
                .ToList();

            Budgets.FirstOrDefault(x => x.Id == budgetDto.Id)!.PlannedIncomes = plannedIncomesModels;
        }

        foreach (var budget in Budgets)
        {
            budget.Balance = _budgetService.GetBalance(budget);
        }
    }
        

    [ObservableProperty] private ObservableCollection<BudgetModel> _budgets;

    [ObservableProperty] private List<TimePeriodModel> _timePeriods;

    [ObservableProperty] private List<ExpenseTypeModel> _expenseTypes;

    [ObservableProperty] private List<IncomeTypeModel> _incomeTypes;

    [ObservableProperty] private UserModel? _user;

    [RelayCommand]
    private async Task DeleteBudget(BudgetModel budget)
    {
        try
        {
            await _client.BudgetDELETEAsync(budget.Id);
        }
        catch
        {
            var mainPage = Application.Current!.MainPage;
            if (mainPage is not null)
            {
                await mainPage.DisplayAlert("Fail", Resources.DeleteBudgetFailed, "OK");
            }
            return;
        }

        Budgets.Remove(budget);
    }

    [RelayCommand]
    private async Task AddBudget()
    {
        var newPlannedExpenses = ExpenseTypes.Select(expenseTypeModel => new PlannedExpensesModel
        {
            Sum = 0, 
            ExpenseTypeId = expenseTypeModel.Id,
            ExpenseType = ExpenseTypes.FirstOrDefault(x => x.Id == expenseTypeModel.Id)!.Name,
            BudgetId = -1
        }).ToList();

        var newPlannedIncomes = IncomeTypes.Select(incomeTypeModel => new PlannedIncomesModel
        {
            Sum = 0,
            IncomeTypeId = incomeTypeModel.Id,
            IncomeType = IncomeTypes.FirstOrDefault(x => x.Id == incomeTypeModel.Id)!.Name,
            BudgetId = -1
        }).ToList();

        if (User is not null)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                {
                    "Budget",
                    new BudgetModel
                    {
                        UserId = User.Id,
                        PlannedExpenses = newPlannedExpenses,
                        PlannedIncomes = newPlannedIncomes,
                    }
                },

                { "Budgets", Budgets },
                { "ExpenseTypes", ExpenseTypes },
                { "IncomeTypes", IncomeTypes },
                { "TimePeriods", TimePeriods },
                { "IsEdit", false }
            };

            await Shell.Current.GoToAsync($"{nameof(BudgetAddEdit)}", navigationParameter);
        }
    }

    [RelayCommand]
    private async Task EditBudget(BudgetModel budget)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            { "Budget", budget },
            { "Budgets", Budgets },
            { "ExpenseTypes", ExpenseTypes },
            { "IncomeTypes", IncomeTypes },
            { "TimePeriods", TimePeriods },
            { "IsEdit", true }
        };
        await Shell.Current.GoToAsync($"{nameof(BudgetAddEdit)}", navigationParameter);
    }
        
    private Task UserExitHandler()
    {
        User = null;
        Budgets = [];
        return Task.CompletedTask;
    }
}