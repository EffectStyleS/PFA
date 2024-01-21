using ApiClient;
using client.Infrastructure;
using client.Model.Models;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using System.Collections.ObjectModel;

namespace client.ViewModel
{
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
            Expenses = new ObservableCollection<ExpenseModel>();

            ICollection<ExpenseDTO> result = new List<ExpenseDTO>();

            try
            {
                result = _client.UserAll2Async(User.Id).Result;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Fail", ex.Message, "OK");
                return;
            }

            foreach (var expense in result)
            {
                Expenses.Add(new ExpenseModel()
                {
                    Id = expense.Id,
                    Name = expense.Name,
                    Value = (decimal)expense.Value,
                    Date = expense.Date.DateTime,
                    ExpenseTypeId = expense.ExpenseTypeId,
                    ExpenseType = ExpenseTypes.Where(x => x.Id == expense.ExpenseTypeId).FirstOrDefault().Name,
                    UserId = expense.UserId
                });
            }
        }

        [ObservableProperty] private ObservableCollection<ExpenseModel> _expenses;

        [ObservableProperty] private List<ExpenseTypeModel> _expenseTypes;

        [ObservableProperty] private UserModel _user;

        [RelayCommand]
        private async Task DeleteExpense(ExpenseModel expense)
        {
            try
            {
                await _client.ExpenseDELETEAsync(expense.Id);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Fail", ex.Message, "OK");
                return;
            }

            Expenses.Remove(expense);
        }

        [RelayCommand]
        private async Task AddExpense()
        {
            ExpenseModel expense = new()
            {
                UserId = User.Id,
            };
            
            await _popupNavigation.PushAsync(new ExpensesPopup(new ExpensesPopupViewModel(_popupNavigation, _client, expense, Expenses, ExpenseTypes, false)));
        }

        [RelayCommand]
        private Task EditExpense(ExpenseModel expense) 
            => _popupNavigation.PushAsync(new ExpensesPopup(new ExpensesPopupViewModel(_popupNavigation, _client, expense, Expenses, ExpenseTypes, true)));

        [RelayCommand]
        private Task OpenBudgetOverruns() => _popupNavigation.PushAsync(new BudgetOverrunsPopup(new BudgetOverrunsPopupViewModel(_client, User.Id)));
        
        private async Task UserExitHandler()
        {
            User = null;
            Expenses = new ObservableCollection<ExpenseModel>();
        }
    }
}
