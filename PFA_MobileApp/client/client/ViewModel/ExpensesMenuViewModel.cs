using client.Model.Models;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using System.Collections.ObjectModel;

namespace client.ViewModel
{
    public partial class ExpensesMenuViewModel : BaseViewModel
    {
        IPopupNavigation _popupNavigation;

        public ExpensesMenuViewModel(IPopupNavigation popupNavigation)
        {
            _popupNavigation = popupNavigation;
            // TODO: из сервиса получим потом
            Expenses = new ObservableCollection<ExpenseModel>
            {
                new ExpenseModel()
                {
                    Name = "Расход 1",
                    Date = DateTime.Now,
                    Value = 12523,
                    ExpenseTypeId = 0,
                    ExpenseType = "бубубу"
                },

                new ExpenseModel()
                {
                    Name = "Расход 2",
                    Date = DateTime.Now,
                    Value = 12523,
                    ExpenseTypeId = 0,
                    ExpenseType = "бубубу"
                },

                new ExpenseModel()
                {
                    Name = "Расход 3",
                    Date = DateTime.Now,
                    Value = 12523,
                    ExpenseTypeId = 0,
                    ExpenseType = "бубубу"
                },

                new ExpenseModel()
                {
                    Name = "Расход 4",
                    Date = DateTime.Now,
                    Value = 12523,
                    ExpenseTypeId = 0,
                    ExpenseType = "бубубу"
                },
            };

            PageTitle = "Expenses";
        }

        [ObservableProperty]
        string _pageTitle;

        [ObservableProperty]
        ObservableCollection<ExpenseModel> _expenses;

        [RelayCommand]
        void DeleteExpense()
        {
            // TODO: удаление сервисом
        }

        [RelayCommand]
        async Task AddExpense()
        {
            // TODO: добавление сервисом
            await _popupNavigation.PushAsync(new ExpensesPopup(new ExpensesPopupViewModel()));
        }

        [RelayCommand]
        async Task EditExpense(ExpenseModel expense)
        {
            // TODO: изменение сервисом
            await _popupNavigation.PushAsync(new ExpensesPopup(new ExpensesPopupViewModel(expense)));
        }


        [RelayCommand]
        async Task OpenBudgetOverruns()
        {
            // TODO: удаление сервисом
            var user = new UserModel();
            await _popupNavigation.PushAsync(new BudgetOverrunsPopup(new BudgetOverrunsPopupViewModel(user)));
        }

    }
}
