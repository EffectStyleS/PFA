using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using System.Collections.ObjectModel;

namespace client.ViewModel;

/// <summary>
/// Модель представления попапа запланированных расходов
/// </summary>
public partial class PlannedExpensesPopupViewModel : BaseViewModel
{
    private readonly IPopupNavigation _popupNavigation;
    private readonly BudgetModel _budget;

    /// <summary>
    /// Модель представления попапа запланированных расходов
    /// </summary>
    /// <param name="popupNavigation">Навигация попапов</param>
    /// <param name="budget">Бюджет</param>
    public PlannedExpensesPopupViewModel(IPopupNavigation popupNavigation, BudgetModel budget)
    {
        _popupNavigation = popupNavigation;
        _budget = budget;

        PageTitle = "Planned Expenses";

        PlannedExpenses = new ObservableCollection<PlannedExpensesModel>(_budget.PlannedExpenses);
        if (PlannedExpenses.All(x => x.Sum != null))
        {
            return;
        }
            
        foreach (var plannedExpensesItem in PlannedExpenses)
        {
            plannedExpensesItem.Sum = 0;
        }
    }

    /// <summary>
    /// Название страницы
    /// </summary>
    [ObservableProperty] private string _pageTitle;

    /// <summary>
    /// Запланированные доходы
    /// </summary>
    [ObservableProperty] private ObservableCollection<PlannedExpensesModel> _plannedExpenses;

    /// <summary>
    /// Команда сохранения
    /// </summary>
    [RelayCommand]
    private async Task Save()
    {
        foreach (var plannedExpensesItem in PlannedExpenses)
        {
            plannedExpensesItem.Sum ??= 0;
        }

        _budget.PlannedExpenses = [..PlannedExpenses];
        await _popupNavigation.PopAsync();
    }

    /// <summary>
    /// Команда отмены
    /// </summary>
    [RelayCommand]
    private async Task Cancel()
    {
        await _popupNavigation.PopAsync();
    }
}