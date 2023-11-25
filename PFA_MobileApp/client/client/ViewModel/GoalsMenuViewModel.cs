using client.Model.Models;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using System.Collections.ObjectModel;

namespace client.ViewModel
{
    public partial class GoalsMenuViewModel : BaseViewModel
    {
        IPopupNavigation _popupNavigation;

        public GoalsMenuViewModel(IPopupNavigation popupNavigation)
        {
            _popupNavigation = popupNavigation;
            // TODO: из сервиса получим потом
            Goals = new ObservableCollection<GoalModel>
            {
                new GoalModel()
                {
                    Name = "Goal 1",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    Sum = 12523,
                    IsCompleted = false,
                },

                new GoalModel()
                {
                    Name = "Goal 2",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    Sum = 12523,
                    IsCompleted = true,
                },

                new GoalModel()
                {
                    Name = "Goal 3",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    Sum = 12523,
                    IsCompleted = false,
                },

                new GoalModel()
                {
                    Name = "Goal 4",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    Sum = 12523,
                    IsCompleted = true,
                },
            };

            PageTitle = "Goals";
        }

        [ObservableProperty]
        string _pageTitle;

        [ObservableProperty]
        ObservableCollection<GoalModel> _goals;

        [RelayCommand]
        void DeleteGoal()
        {
            // TODO: удаление сервисом
        }

        [RelayCommand]
        async Task AddGoal()
        {
            // TODO: добавление сервисом
            await _popupNavigation.PushAsync(new GoalsPopup(new GoalsPopupViewModel()));
        }

        [RelayCommand]
        async Task EditGoal(GoalModel goal)
        {
            // TODO: изменение сервисом
            await _popupNavigation.PushAsync(new GoalsPopup(new GoalsPopupViewModel(goal)));
        }
    }
}
