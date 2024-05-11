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

[QueryProperty(nameof(Goals), "Goals")]
public partial class GoalsStatisticsViewModel : BaseViewModel
{
    public GoalsStatisticsViewModel()
    {
        EventManager.OnUserExit += UserExitHandler;
    }
 
    public Task CompleteDataAfterNavigation()
    {
        ChartDateTime = DateTime.Now;
        ChartYear = ChartDateTime.ToString("yyyy", CultureInfo.InvariantCulture);
        
        CompleteEntries();
        
        return Task.CompletedTask;
    }

    [ObservableProperty] private DonutChart _chart;
    
    [ObservableProperty] private ObservableCollection<ChartEntry> _entries;
    
    [ObservableProperty] private ObservableCollection<GoalModel> _goals;
    
    [ObservableProperty] private ObservableCollection<StatisticModel<int>> _goalsStatisticModels;

    [ObservableProperty] private DateTime _chartDateTime;

    [ObservableProperty] private string _chartYear;

    [RelayCommand]
    private void SwipeRight()
    {
        ChartDateTime = ChartDateTime.AddYears(-1);
        ChartYear = ChartDateTime.ToString("yyyy", CultureInfo.InvariantCulture);
        
        CompleteEntries();
    }
    
    [RelayCommand]
    private void SwipeLeft()
    {
        ChartDateTime = ChartDateTime.AddYears(1);
        ChartYear = ChartDateTime.ToString("yyyy", CultureInfo.InvariantCulture);
        
        CompleteEntries();
    }

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
    
    private Task UserExitHandler()
    {
        Goals = [];
        return Task.CompletedTask;
    }
}