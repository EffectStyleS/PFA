using client.ViewModel;

namespace client.View;

/// <summary>
/// Страница статистики целей
/// </summary>
public partial class GoalsStatisticsPage : ContentPage
{
    /// <summary>
    /// Событие перехода на страницу
    /// </summary>
    public event Func<Task> OnNavigatedToEvent = () => Task.CompletedTask;
    
    /// <summary>
    /// Страница статистики целей
    /// </summary>
    public GoalsStatisticsPage(GoalsStatisticsViewModel vm)
    {
        InitializeComponent();
        OnNavigatedToEvent += vm.CompleteDataAfterNavigation;
        BindingContext = vm;
    }
    
    /// <inheritdoc />
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        OnNavigatedToEvent?.Invoke();
    }
}