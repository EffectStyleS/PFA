using client.Model.Models;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using System.Collections.ObjectModel;

namespace client.ViewModel
{
    [QueryProperty(nameof(Budget), "Budget")]
    public partial class BudgetAddEditViewModel : BaseViewModel
    {
        IPopupNavigation _popupNavigation;

        public BudgetAddEditViewModel(IPopupNavigation popupNavigation)
        {
            _popupNavigation = popupNavigation;

            // TODO: из сервиса подгружать
            TimePeriods = new ObservableCollection<TimePeriodModel>()
            {
                new TimePeriodModel() { Name = "Period 1" },
                new TimePeriodModel() { Name = "Period 2" },
                new TimePeriodModel() { Name = "Period 3" },
            };
        }

        public void CompleteDataAfterNavigation()
        {
            if (Budget.Id == -1)
            {
                PageTitle = "Add Budget";
                Name = null;
                StartDate = null;
                TimePeriod = null;
                TimePeriodId = 0;

                return;
            }

            PageTitle = "Edit Budget";
            Id = Budget.Id;
            Name = Budget.Name;
            StartDate = Budget.StartDate;
            TimePeriod = Budget.TimePeriod;
            TimePeriodId = Budget.TimePeriodId;
        }

        [ObservableProperty]
        BudgetModel _budget;

        [ObservableProperty]
        string _pageTitle;

        [ObservableProperty]
        int _id;

        [ObservableProperty]
        string _name;

        [ObservableProperty]
        DateTime? _startDate;

        [ObservableProperty]
        string _timePeriod;

        [ObservableProperty]
        int _timePeriodId;

        [ObservableProperty]
        ObservableCollection<TimePeriodModel> _timePeriods;

        [RelayCommand]
        async Task Save()
        {
            // TODO: реализовать сохранение
            await Shell.Current.GoToAsync("..");

        }

        [RelayCommand]
        async Task Cancel()
        {
            // TODO: реализовать отмену
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task PlannedExpenses()
        {
            await _popupNavigation.PushAsync(new PlannedExpensesPopup(new PlannedExpensesPopupViewModel(Budget)));

        }

        [RelayCommand]
        async Task PlannedIncomes()
        {
            await _popupNavigation.PushAsync(new PlannedIncomesPopup(new PlannedIncomesPopupViewModel(Budget)));
        }
    }
}
