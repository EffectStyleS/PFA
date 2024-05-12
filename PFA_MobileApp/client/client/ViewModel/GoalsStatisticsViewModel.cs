using System.Collections.ObjectModel;
using System.Globalization;
using client.Infrastructure;
using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microcharts;
using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace client.ViewModel;

/// <summary>
/// Модель представления статистики целей
/// </summary>
[QueryProperty(nameof(Goals), "Goals")]
public partial class GoalsStatisticsViewModel : BaseViewModel
{
    /// <summary>
    /// Модель представления статистики целей
    /// </summary>
    public GoalsStatisticsViewModel()
    {
        EventManager.OnUserExit += UserExitHandler;
    }
 
    /// <summary>
    /// Заполнение данных после перехода на страницу
    /// </summary>
    public Task CompleteDataAfterNavigation()
    {
        ChartDateTime = DateTime.Now;
        ChartYear = ChartDateTime.ToString("yyyy", CultureInfo.InvariantCulture);
        
        CompleteEntries();
        
        return Task.CompletedTask;
    }

    /// <summary>
    /// Диаграмма
    /// </summary>
    [ObservableProperty] private DonutChart _chart;
    
    /// <summary>
    /// Данные диаграммы
    /// </summary>
    [ObservableProperty] private ObservableCollection<ChartEntry> _entries;
    
    /// <summary>
    /// Цели
    /// </summary>
    [ObservableProperty] private ObservableCollection<GoalModel> _goals;
    
    /// <summary>
    /// Модели статистики целей
    /// </summary>
    [ObservableProperty] private ObservableCollection<StatisticModel<int>> _goalsStatisticModels;

    /// <summary>
    /// Дата и время статистики
    /// </summary>
    [ObservableProperty] private DateTime _chartDateTime;

    /// <summary>
    /// Год статистики
    /// </summary>
    [ObservableProperty] private string _chartYear;

    /// <summary>
    /// Команда свайпа вправо
    /// </summary>
    [RelayCommand]
    private void SwipeRight()
    {
        ChartDateTime = ChartDateTime.AddYears(-1);
        ChartYear = ChartDateTime.ToString("yyyy", CultureInfo.InvariantCulture);
        
        CompleteEntries();
    }
    
    /// <summary>
    /// Команда свайпа влево
    /// </summary>
    [RelayCommand]
    private void SwipeLeft()
    {
        ChartDateTime = ChartDateTime.AddYears(1);
        ChartYear = ChartDateTime.ToString("yyyy", CultureInfo.InvariantCulture);
        
        CompleteEntries();
    }

    /// <summary>
    /// Заполнение данных
    /// </summary>
    private void CompleteEntries()
    {
        Entries = [];
        GoalsStatisticModels = [];
        
        var goalsStatisticModelsList = Goals
            .Where(x => x.StartDate.Year == ChartDateTime.Year)
            .GroupBy(x => x.Status)
            .Select(x => new StatisticModel<int>
            {
                TypeName = x.Key.ToString(),
                Value = x.Count()
            })
            .ToList();

        GoalsStatisticModels = new ObservableCollection<StatisticModel<int>>(goalsStatisticModelsList);
        
        foreach (var statisticModel in GoalsStatisticModels)
        {
            var entry = new ChartEntry((float)statisticModel.Value)
            {
                Color = GetStatusColor(statisticModel.TypeName),
                Label = statisticModel.TypeName,
                TextColor = GetStatusColor(statisticModel.TypeName),
                ValueLabel = statisticModel.Value.ToString(CultureInfo.CurrentCulture)
            };
            
            Entries.Add(entry);
        }

        if (!GoalsStatisticModels.Any())
        {
            GoalsStatisticModels.Add(new StatisticModel<int>
            {
                TypeName = "Nothing",
                Value = 0
            });
        }
        
        if (!Entries.Any())
        {
            Entries.Add(new ChartEntry(1)
            {
                Color = ChartHelpers.ChartColors[^1],
                Label = "Nothing",
                TextColor = ChartHelpers.ChartColors[^1],
                ValueLabel = "0"
            });
        }
        
        Chart = new DonutChart
        {
            Entries = Entries,
            LabelTextSize = 40,
            HoleRadius = 0.3f,
            BackgroundColor = SKColor.Empty,
            LabelMode = LabelMode.None
        };
    }

    /// <summary>
    /// Получение цвета статуса цели
    /// </summary>
    /// <param name="statusName">Название статуса</param>
    /// <returns></returns>
    private SKColor GetStatusColor(string statusName)
    {
        return statusName switch
        {
            "NotStarted" => Colors.Gray.ToSKColor(),
            "InProgress" => Colors.Gold.ToSKColor(),
            "Completed" => Colors.Green.ToSKColor(),
            "Failed" => Colors.Red.ToSKColor(),
            _ => Colors.Gray.ToSKColor()
        };
    }
    
    /// <summary>
    /// Обработчик выхода пользователя
    /// </summary>
    private Task UserExitHandler()
    {
        Goals = [];
        return Task.CompletedTask;
    }
}