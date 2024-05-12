using client.ViewModel;

namespace client.View;

/// <summary>
/// Страница статистики доходов
/// </summary>
public partial class IncomesStatisticsPage : ContentPage
{
    /// <summary>
    /// Событие перехода на страницу
    /// </summary>
    public event Func<Task> OnNavigatedToEvent = () => Task.CompletedTask;
    
    /// <summary>
    /// Страница статистики доходов
    /// </summary>
    public IncomesStatisticsPage(IncomesStatisticsViewModel vm)
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