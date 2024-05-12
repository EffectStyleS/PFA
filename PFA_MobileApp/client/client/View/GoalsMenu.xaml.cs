using client.ViewModel;

namespace client.View;

/// <summary>
/// Меню целей
/// </summary>
public partial class GoalsMenu : ContentPage
{
    /// <summary>
    /// Событие перехода на страницу
    /// </summary>
    public event Func<Task> OnNavigatedToEvent = () => Task.CompletedTask;
    
    /// <summary>
    /// Меню целей
    /// </summary>
    public GoalsMenu(GoalsMenuViewModel vm)
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