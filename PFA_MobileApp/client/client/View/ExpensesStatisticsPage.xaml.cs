using client.ViewModel;

namespace client.View;

/// <summary>
/// Страница статистики расходов
/// </summary>
public partial class ExpensesStatisticsPage : ContentPage
{
    /// <summary>
    /// Событие перехода на страницу
    /// </summary>
    public event Func<Task> OnNavigatedToEvent = () => Task.CompletedTask;
    
    /// <summary>
    /// Страница статистики расходов
    /// </summary>
    public ExpensesStatisticsPage(ExpensesStatisticsViewModel vm)
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