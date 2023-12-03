using ApiClient;
using client.Model.Models;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace client.ViewModel
{
    public partial class BudgetsMenuViewModel : BaseViewModel
    {
        private readonly Client _client;

        public BudgetsMenuViewModel(Client client)
        {
            _client = client;

            PageTitle = "Budgets";
        }

        private async Task GetAllTimePeriods()
        {
            if (TimePeriods != null)
                return;

            List<TimePeriodModel> result = new();

            var timePeriodsDto = await _client.TimePeriodAllAsync();

            foreach (var timePeriod in timePeriodsDto)
            {
                result.Add(new TimePeriodModel()
                {
                    Id = timePeriod.Id,
                    Name = timePeriod.Name,
                });
            }

            if (result.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Fail", "Null Time Periods", "OK");
                return;
            }

            TimePeriods = result;
        }

        private async Task GetAllExpenseTypes()
        {
            if (ExpenseTypes != null)
                return;

            List<ExpenseTypeModel> result = new();

            var expenseTypesDto = await _client.ExpenseTypeAllAsync();

            foreach (var expenseTypeDto in expenseTypesDto)
            {
                result.Add(new ExpenseTypeModel()
                {
                    Id = expenseTypeDto.Id,
                    Name = expenseTypeDto.Name
                });
            }

            if (result.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Fail", "Null Expense Types", "OK");
                return;
            }

            ExpenseTypes = result;
        }

        private async Task GetAllIncomeTypes()
        {
            if (IncomeTypes != null)
                return;

            List<IncomeTypeModel> result = new();

            var incomeTypesDto = await _client.IncomeTypeAllAsync();

            foreach (var incomeType in incomeTypesDto)
            {
                result.Add(new IncomeTypeModel()
                {
                    Id = incomeType.Id,
                    Name = incomeType.Name
                });
            }

            if (result.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Fail", "Null Income Types", "OK");
                return;
            }

            IncomeTypes = result;
        }

        public async Task CompleteDataAfterNavigation()
        {
            var userLogin = _client.GetCurrentUserLogin();
            var userDto = await _client.UserAsync(userLogin);
            User = new UserModel();
            User.Id = userDto.Id;
            User.Login = userDto.Login;
            User.RefreshToken = userDto.RefreshToken;
            User.RefreshTokenExpireTime = userDto.RefreshTokenExpiryTime.DateTime;

            await GetAllTimePeriods();
            await GetAllIncomeTypes();
            await GetAllExpenseTypes();
            Budgets = new ObservableCollection<BudgetModel>();

            ICollection<BudgetDTO> budgetResult = new List<BudgetDTO>();

            try
            {
                budgetResult = _client.UserAllAsync(User.Id).Result;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Fail", ex.Message, "OK");
                return;
            }

            foreach (var budget in budgetResult)
            {
                Budgets.Add(new BudgetModel()
                {
                    Id = budget.Id,
                    Name = budget.Name,
                    StartDate = budget.StartDate.DateTime,
                    TimePeriodId = budget.TimePeriodId,
                    TimePeriod = TimePeriods.Where(x => x.Id == budget.TimePeriodId).FirstOrDefault().Name,
                    UserId = budget.UserId,
                });
            }

            ICollection<PlannedExpensesDTO> plannedExpensesResult = new List<PlannedExpensesDTO>();
            ICollection<PlannedIncomesDTO> plannedIncomesResult = new List<PlannedIncomesDTO>();
            foreach (var budgetDto in budgetResult)
            {
                try
                {
                    plannedExpensesResult = _client.BudgetAsync(budgetDto.Id).Result;
                    plannedIncomesResult = _client.Budget2Async(budgetDto.Id).Result;
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Fail", ex.Message, "OK");
                    return;
                }

                var plannedExpensesModels = new List<PlannedExpensesModel>();
                foreach (var plannedExpenses in plannedExpensesResult)
                {
                    plannedExpensesModels.Add(new PlannedExpensesModel()
                    {
                        Id = plannedExpenses.Id,
                        Sum = (decimal?)plannedExpenses.Sum,
                        ExpenseTypeId = plannedExpenses.ExpenseTypeId,
                        ExpenseType = ExpenseTypes.Where(x => x.Id == plannedExpenses.ExpenseTypeId).FirstOrDefault().Name,
                        BudgetId = plannedExpenses.BudgetId,
                    });
                }
                Budgets.Where(x => x.Id == budgetDto.Id).FirstOrDefault().PlannedExpenses = plannedExpensesModels;

                var plannedIncomesModels = new List<PlannedIncomesModel>();
                foreach (var plannedIncomes in plannedIncomesResult)
                {
                    plannedIncomesModels.Add(new PlannedIncomesModel()
                    {
                        Id = plannedIncomes.Id,
                        Sum = (decimal?)plannedIncomes.Sum,
                        IncomeTypeId = plannedIncomes.IncomeTypeId,
                        IncomeType = IncomeTypes.Where(x => x.Id == plannedIncomes.IncomeTypeId).FirstOrDefault().Name,
                        BudgetId = plannedIncomes.BudgetId,
                    });
                }

                Budgets.Where(x => x.Id == budgetDto.Id).FirstOrDefault().PlannedIncomes = plannedIncomesModels;
            }
        }

        [ObservableProperty]
        string _pageTitle;

        [ObservableProperty]
        ObservableCollection<BudgetModel> _budgets;

        [ObservableProperty]
        List<TimePeriodModel> _timePeriods;

        [ObservableProperty]
        List<ExpenseTypeModel> _expenseTypes;

        [ObservableProperty]
        List<IncomeTypeModel> _incomeTypes;

        [ObservableProperty]
        UserModel _user;

        [RelayCommand]
        async Task DeleteBudget(BudgetModel budget)
        {
            try
            {
                await _client.BudgetDELETEAsync(budget.Id);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Fail", ex.Message, "OK");
                return;
            }

            Budgets.Remove(budget);
        }

        [RelayCommand]
        async Task AddBudget()
        {
            var newPlannedExpenses = new List<PlannedExpensesModel>();
            for (int i = 0; i < ExpenseTypes.Count; i++)
            {
                newPlannedExpenses.Add(new PlannedExpensesModel()
                {
                    Sum = 0,
                    ExpenseTypeId = ExpenseTypes[i].Id,
                    ExpenseType = ExpenseTypes.Where(x => x.Id == ExpenseTypes[i].Id).FirstOrDefault().Name,
                    BudgetId = -1
                });
            }

            var newPlannedIncomes = new List<PlannedIncomesModel>();
            for (int i = 0; i < IncomeTypes.Count; i++)
            {
                newPlannedIncomes.Add(new PlannedIncomesModel()
                {
                    Sum = 0,
                    IncomeTypeId = IncomeTypes[i].Id,
                    IncomeType = IncomeTypes.Where(x => x.Id == IncomeTypes[i].Id).FirstOrDefault().Name,
                    BudgetId = -1
                });
            }
              
            var navigationParameter = new Dictionary<string, object>
            {
                {
                    "Budget",
                    new BudgetModel()
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
                { "IsEdited", false }
            };

            await Shell.Current.GoToAsync($"{nameof(BudgetAddEdit)}", navigationParameter);
        }

        [RelayCommand]
        async Task EditBudget(BudgetModel budget)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Budget", budget },
                { "Budgets", Budgets },
                { "ExpenseTypes", ExpenseTypes },
                { "IncomeTypes", IncomeTypes },
                { "TimePeriods", TimePeriods },
                { "IsEdited", true }
            };
            await Shell.Current.GoToAsync($"{nameof(BudgetAddEdit)}", navigationParameter);
        }

    }
}
