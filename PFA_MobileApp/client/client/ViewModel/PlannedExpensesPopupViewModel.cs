using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using System.Collections.ObjectModel;

namespace client.ViewModel
{
    public partial class PlannedExpensesPopupViewModel : BaseViewModel
    {
        private readonly IPopupNavigation _popupNavigation;
        private readonly BudgetModel _budget;

        public PlannedExpensesPopupViewModel(IPopupNavigation popupNavigation ,BudgetModel budget)
        {
            _popupNavigation = popupNavigation;
            _budget = budget;

            PageTitle = "Planned Expenses";

            PlannedExpenses = new ObservableCollection<PlannedExpensesModel>(_budget.PlannedExpenses);
            if (PlannedExpenses.Any(x => x.Sum == null))
            {
                foreach (var plannedExpensesItem in PlannedExpenses)
                {
                    plannedExpensesItem.Sum = 0;
                }
            }
        }

        [ObservableProperty]
        string _pageTitle;

        [ObservableProperty]
        ObservableCollection<PlannedExpensesModel> _plannedExpenses;

        [RelayCommand]
        async Task Save()
        {
            _budget.PlannedExpenses = new List<PlannedExpensesModel>(PlannedExpenses);
            await _popupNavigation.PopAsync();
        }

        [RelayCommand]
        async Task Cancel()
        {
            await _popupNavigation.PopAsync();
        }
    }
}
