using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using System.Collections.ObjectModel;

namespace client.ViewModel;

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
        if (PlannedIncomes.All(x => x.Sum != null))
        {
            return;
        }
            
        foreach (var plannedIncomesItem in PlannedIncomes)
        {
            plannedIncomesItem.Sum = 0;
        }
    }

    [ObservableProperty] private string _pageTitle;

    [ObservableProperty] private ObservableCollection<PlannedIncomesModel> _plannedIncomes;

    [RelayCommand]
    private async Task Save()
    {
        foreach (var plannedIncomesItem in PlannedIncomes)
        {
            plannedIncomesItem.Sum ??= 0;
        }

        _budget.PlannedIncomes = [..PlannedIncomes];
        await _popupNavigation.PopAsync();
    }

    [RelayCommand]
    private async Task Cancel()
    {
        await _popupNavigation.PopAsync();
    }
}