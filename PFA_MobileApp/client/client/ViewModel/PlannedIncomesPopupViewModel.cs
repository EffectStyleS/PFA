using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using System.Collections.ObjectModel;

namespace client.ViewModel;

/// <summary>
/// Модель представления попапа запланированных доходов
/// </summary>
public partial class PlannedIncomesPopupViewModel : BaseViewModel
{
    private readonly IPopupNavigation _popupNavigation;
    private readonly BudgetModel _budget;

    /// <summary>
    /// Модель представления попапа запланированных доходов
    /// </summary>
    /// <param name="popupNavigation">Навигация попапов</param>
    /// <param name="budget">Бюджет</param>
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

    /// <summary>
    /// Название страницы
    /// </summary>
    [ObservableProperty] private string _pageTitle;

    /// <summary>
    /// Запланированные расходы
    /// </summary>
    [ObservableProperty] private ObservableCollection<PlannedIncomesModel> _plannedIncomes;

    /// <summary>
    /// Команда сохранения
    /// </summary>
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

    /// <summary>
    /// Команда отмены
    /// </summary>
    [RelayCommand]
    private async Task Cancel()
    {
        await _popupNavigation.PopAsync();
    }
}