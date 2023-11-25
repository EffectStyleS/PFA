using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace client.ViewModel
{
    public partial class PlannedExpensesPopupViewModel : BaseViewModel
    {
        private BudgetModel _budget;

        public PlannedExpensesPopupViewModel(BudgetModel budget)
        {
            PageTitle = "Planned Expenses";
            _budget = budget;

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
            // TODO: реализовать сохранение
            await MopupService.Instance.PopAsync();
        }

        [RelayCommand]
        async Task Cancel()
        {
            // TODO: реализовать отмену
            await MopupService.Instance.PopAsync();
        }
    }
}
