using client.ViewModel;

namespace client.View;

/// <summary>
/// Меню расходов
/// </summary>
public partial class ExpensesMenu : ContentPage
{
    /// <summary>
    /// Событие перехода на страницу
    /// </summary>
    public event Func<Task> OnNavigatedToEvent = () => Task.CompletedTask;

    /// <summary>
    /// Меню расходов
    /// </summary>
    public ExpensesMenu(ExpensesMenuViewModel vm)
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