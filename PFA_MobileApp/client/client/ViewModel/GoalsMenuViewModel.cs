using ApiClient;
using client.Infrastructure;
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
        private readonly IPopupNavigation _popupNavigation;
        private readonly Client _client;

        public GoalsMenuViewModel(IPopupNavigation popupNavigation, Client client)
        {
            _popupNavigation = popupNavigation;
            _client = client;
            EventManager.OnUserExit += UserExitHandler;
        }

        public async Task CompleteDataAfterNavigation()
        {
            var userLogin = _client.GetCurrentUserLogin();
            var userDto = await _client.UserAsync(userLogin);
            User = new UserModel();
            User.Id = userDto.Id;
            User.Login = userDto.Login;
            User.RefreshToken = userDto.RefreshToken;
            User.RefreshTokenExpireTime = userDto.RefreshTokenExpiryTime.DateTime;

            Goals = new ObservableCollection<GoalModel>();

            ICollection<GoalDTO> result = new List<GoalDTO>();

            try
            {
                result = _client.UserAll3Async(User.Id).Result;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Fail", ex.Message, "OK");
                return;
            }

            foreach (var goal in result)
            {
                Goals.Add(new GoalModel()
                {
                    Id = goal.Id,
                    Name = goal.Name,
                    StartDate = goal.StartDate.DateTime,
                    EndDate = goal.EndDate.Value.DateTime,
                    Sum = (decimal?)goal.Sum,
                    IsCompleted = goal.IsCompleted,
                    UserId = goal.UserId
                });
            }
        }
        
        [ObservableProperty] private ObservableCollection<GoalModel> _goals;

        [ObservableProperty] private UserModel _user;

        [RelayCommand]
        private async Task DeleteGoal(GoalModel goal)
        {
            try
            {
                await _client.GoalDELETEAsync(goal.Id);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Fail", ex.Message, "OK");
                return;
            }

            Goals.Remove(goal);
        }

        [RelayCommand]
        private async Task AddGoal()
        {
            GoalModel goal = new()
            {
                UserId = User.Id,
            };

            await _popupNavigation.PushAsync(new GoalsPopup(new GoalsPopupViewModel(_popupNavigation, _client, goal, Goals, false)));
        }

        [RelayCommand]
        private Task EditGoal(GoalModel goal) 
            => _popupNavigation.PushAsync(new GoalsPopup(new GoalsPopupViewModel(_popupNavigation, _client, goal, Goals, true)));
        
        private async Task UserExitHandler()
        {
            User = null;
            Goals = new ObservableCollection<GoalModel>();
        }
    }
}
