using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;

namespace client.ViewModel
{
    public partial class GoalsPopupViewModel : BaseViewModel
    {
        public GoalsPopupViewModel(GoalModel goal = null)
        {
            PageTitle = goal == null ? "Add Goal" : "Edit Goal";

            Id = goal?.Id;
            Name = goal?.Name;
            StartDate = goal?.StartDate;
            EndDate = goal?.EndDate;
            Sum = goal?.Sum;
            IsCompleted = goal != null && goal.IsCompleted;
        }

        [ObservableProperty]
        string _pageTitle;

        [ObservableProperty]
        int? _id;

        [ObservableProperty]
        string _name;

        [ObservableProperty]
        DateTime? _startDate;

        [ObservableProperty]
        DateTime? _endDate;

        [ObservableProperty]
        int? _sum;

        [ObservableProperty]
        bool? _isCompleted;

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
