using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using System.Collections.ObjectModel;

namespace client.ViewModel
{
    public partial class PlannedIncomesPopupViewModel : BaseViewModel
    {
        private readonly IPopupNavigation _popupNavigation;
        private readonly BudgetModel _budget;

        public PlannedIncomesPopupViewModel(IPopupNavigation popupNavigation, BudgetModel budget)
        {
            _popupNavigation = popupNavigation;
            _budget = budget;

            PageTitle = "Planned Incomes";

            PlannedIncomes = new ObservableCollection<PlannedIncomesModel>(_budget.PlannedIncomes);
            if (PlannedIncomes.Any(x => x.Sum == null))
            {
                foreach (var plannedIncomesItem in PlannedIncomes)
                {
                    plannedIncomesItem.Sum = 0;
                }
            }
        }

        [ObservableProperty]
        string _pageTitle;

        [ObservableProperty]
        ObservableCollection<PlannedIncomesModel> _plannedIncomes;

        [RelayCommand]
        async Task Save()
        {
            foreach (var plannedIncomesItem in PlannedIncomes)
            {
                plannedIncomesItem.Sum ??= 0;
            }

            _budget.PlannedIncomes = new List<PlannedIncomesModel>(PlannedIncomes);
            await _popupNavigation.PopAsync();
        }

        [RelayCommand]
        async Task Cancel()
        {
            await _popupNavigation.PopAsync();
        }
    }
}
