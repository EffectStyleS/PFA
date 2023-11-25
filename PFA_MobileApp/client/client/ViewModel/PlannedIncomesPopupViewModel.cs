using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace client.ViewModel
{
    public partial class PlannedIncomesPopupViewModel : BaseViewModel
    {
        private BudgetModel _budget;

        public PlannedIncomesPopupViewModel(BudgetModel budget)
        {
            PageTitle = "Planned Incomes";
            _budget = budget;

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
