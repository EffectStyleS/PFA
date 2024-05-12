using client.ViewModel;

namespace client.View;

/// <summary>
/// Стартовое меню
/// </summary>
public partial class StartMenu : ContentPage
{
    /// <summary>
    /// Событие перехода на страницу
    /// </summary>
    public event Func<Task> OnNavigatedToEvent = () => Task.CompletedTask;
        
    /// <summary>
    /// Стартовое меню
    /// </summary>
    public StartMenu(StartMenuViewModel vm)
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