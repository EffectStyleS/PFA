using ApiClient;
using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace client.ViewModel
{
    public partial class GoalsPopupViewModel : BaseViewModel
    {
        private readonly Client _client;
        private readonly IPopupNavigation _popupNavigation;
        private readonly ObservableCollection<GoalModel> _goals;
        private readonly GoalModel _goal;
        private readonly bool _isEdited;

        public GoalsPopupViewModel(IPopupNavigation popupNavigation, Client client, GoalModel goal, ObservableCollection<GoalModel> goals, bool isEdited)
        {
            _client = client;
            _popupNavigation = popupNavigation;

            _goal = goal;
            _goals = goals;
            _isEdited = isEdited;

            if (_isEdited)
            {
                PageTitle = "Edit Goal";

                Name = goal.Name;
                StartDate = goal.StartDate;
                EndDate = goal.EndDate;
                Sum = goal.Sum.Value;
                IsCompleted = goal.IsCompleted;

                return;
            }

            PageTitle = "Add Goal";

            StartDate = DateTime.Today;
            EndDate = DateTime.Today.AddMonths(1);
            Sum = 0;
            IsCompleted = false;      
        }

        [ObservableProperty]
        string _pageTitle;

        [ObservableProperty]
        string _name;

        [ObservableProperty]
        decimal _sum;

        [ObservableProperty]
        DateTime _startDate;

        [ObservableProperty]
        DateTime _endDate;

        [ObservableProperty]
        bool _isCompleted;

        [RelayCommand]
        async Task Save()
        {
            _goal.Name = Name;
            _goal.Sum = Sum;
            _goal.StartDate = StartDate;
            _goal.EndDate = EndDate;
            _goal.IsCompleted = IsCompleted;

            GoalDTO postResult = new();

            try
            {
                if (_isEdited)
                {
                    var goalRequest = new GoalDTO()
                    {
                        Id = _goal.Id,
                        Name = _goal.Name,
                        StartDate = _goal.StartDate,
                        EndDate = _goal.EndDate,
                        Sum = (double?)_goal.Sum,
                        IsCompleted = _goal.IsCompleted,
                        UserId = _goal.UserId,
                    };

                    await _client.GoalPUTAsync(_goal.Id, goalRequest);
                }
                else
                {
                    postResult = await _client.GoalPOSTAsync(
                        new GoalDTO()
                        {
                            Name = _goal.Name,
                            StartDate = _goal.StartDate,
                            EndDate = _goal.EndDate,
                            Sum = (double?)_goal.Sum,
                            IsCompleted = _goal.IsCompleted,
                            UserId = _goal.UserId,
                        });
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Fail", ex.Message, "OK");
                return;
            }

            // обновление списка доходов
            if (_isEdited)
            {
                var found = _goals.FirstOrDefault(x => x.Id == _goal.Id);
                int i = _goals.IndexOf(found);
                _goals[i] = _goal;
            }
            else
            {
                _goals.Add(new GoalModel()
                {
                    Id = postResult.Id,
                    Name = postResult.Name,
                    StartDate = postResult.StartDate.DateTime,
                    EndDate = postResult.EndDate.Value.DateTime,
                    Sum = (decimal?)postResult.Sum,
                    IsCompleted = postResult.IsCompleted,
                    UserId = postResult.UserId,
                });
            }

            await _popupNavigation.PopAsync();
        }

        [RelayCommand]
        Task Cancel()
        {
            return _popupNavigation.PopAsync();
        }
    }
}
