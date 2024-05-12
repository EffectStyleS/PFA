using client.ViewModel;

namespace client.View;

/// <summary>
/// Страница истории
/// </summary>
public partial class HistoryPage : ContentPage
{
    /// <summary>
    /// Событие перехода на страницу
    /// </summary>
    public event Func<Task> OnNavigatedToEvent = () => Task.CompletedTask;
    
    /// <summary>
    /// Страница истории
    /// </summary>
    public HistoryPage(HistoryPageViewModel vm)
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