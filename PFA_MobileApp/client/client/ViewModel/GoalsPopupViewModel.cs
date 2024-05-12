using ApiClient;
using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using System.Collections.ObjectModel;
using client.Infrastructure;

namespace client.ViewModel;

/// <summary>
/// Модель представления попапа целей
/// </summary>
public partial class GoalsPopupViewModel : BaseViewModel
{
    private readonly Client _client;
    private readonly IPopupNavigation _popupNavigation;
    private readonly ObservableCollection<GoalModel> _goals;
    private readonly GoalModel _goal;
    private readonly bool _isEdit;

    /// <summary>
    /// Модель представления попапа целей
    /// </summary>
    /// <param name="popupNavigation">Навигация попапов</param>
    /// <param name="client">Клиент</param>
    /// <param name="goal">Цель</param>
    /// <param name="goals">Цели</param>
    /// <param name="isEdit">Признак редактирования</param>
    public GoalsPopupViewModel(IPopupNavigation popupNavigation, Client client, GoalModel goal,
        ObservableCollection<GoalModel> goals, bool isEdit)
    {
        _client = client;
        _popupNavigation = popupNavigation;

        _goal = goal;
        _goals = goals;
        _isEdit = isEdit;
        
        OnGoalChange += EventManager.GoalChangeHandler;

        if (_isEdit)
        {
            PageTitle = "Edit Goal";

            Name = goal.Name;
            StartDate = goal.StartDate;
            EndDate = goal.EndDate;
            Sum = goal.Sum.GetValueOrDefault(0);
            IsCompleted = goal.IsCompleted;

            return;
        }

        PageTitle = "Add Goal";

        StartDate = DateTime.Today;
        EndDate = DateTime.Today.AddMonths(1);
        Sum = 0;
        IsCompleted = false;
    }

    /// <summary>
    /// Название страницы
    /// </summary>
    [ObservableProperty] private string _pageTitle;

    /// <summary>
    /// Название
    /// </summary>
    [ObservableProperty] private string _name;

    /// <summary>
    /// Сумма
    /// </summary>
    [ObservableProperty] private decimal? _sum;

    /// <summary>
    /// Начальная дата
    /// </summary>
    [ObservableProperty] private DateTime _startDate;

    /// <summary>
    /// Конечная дата
    /// </summary>
    [ObservableProperty] private DateTime _endDate;

    /// <summary>
    /// Признак выполненности
    /// </summary>
    [ObservableProperty] private bool _isCompleted;

    /// <summary>
    /// Команда сохранения
    /// </summary>
    [RelayCommand]
    private async Task Save()
    {
        _goal.Name = Name ?? "New Goal";
        _goal.Sum = Sum ?? 0;
        _goal.StartDate = StartDate;
        _goal.EndDate = EndDate;
        _goal.IsCompleted = IsCompleted;

        GoalDto postResult = new();

        try
        {
            if (_isEdit)
            {
                var goalRequest = new GoalDto
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
                    new GoalDto
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
        catch
        {
            var mainPage = Application.Current!.MainPage;
            if (mainPage is not null)
            {
                await mainPage.DisplayAlert("Fail", Resources.AddEditGoalFailed, "OK");
            }
                
            return;
        }
        
        if (_isEdit)
        {
            PublishGoalChange(_goal);
            
            var found = _goals.FirstOrDefault(x => x.Id == _goal.Id);
            if (found is not null)
            {
                var i = _goals.IndexOf(found);
                _goals[i] = _goal;
            }
        }
        else
        {
            var newGoal = new GoalModel
            {
                Id = postResult.Id,
                Name = postResult.Name,
                StartDate = postResult.StartDate.DateTime,
                EndDate = postResult.EndDate.GetValueOrDefault(postResult.StartDate.DateTime).DateTime,
                Sum = (decimal?)postResult.Sum,
                IsCompleted = postResult.IsCompleted,
                UserId = postResult.UserId,
            };

            PublishGoalChange(newGoal);
            _goals.Add(newGoal);
        }
        
        await _popupNavigation.PopAsync();
    }

    /// <summary>
    /// Делегат цели
    /// </summary>
    public delegate void GoalDelegate(GoalModel goal);
    
    /// <summary>
    /// Событие изменения цели
    /// </summary>
    public event GoalDelegate? OnGoalChange;
    
    /// <summary>
    /// Обработчик изменения цели
    /// </summary>
    /// <param name="goal"></param>
    private void PublishGoalChange(GoalModel goal) => OnGoalChange?.Invoke(goal);
    
    /// <summary>
    /// Команда отмены
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private Task Cancel()
    {
        return _popupNavigation.PopAsync();
    }
}