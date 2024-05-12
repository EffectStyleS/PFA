using client.ViewModel;

namespace client.View;

/// <summary>
/// Меню бюджетов
/// </summary>
public partial class BudgetsMenu : ContentPage
{
    /// <summary>
    /// Событие перехода на страницу
    /// </summary>
    public event Func<Task> OnNavigatedToEvent = () => Task.CompletedTask;

    /// <summary>
    /// Меню бюджетов
    /// </summary>
    public BudgetsMenu(BudgetsMenuViewModel vm)
	{
		InitializeComponent();
        OnNavigatedToEvent += vm.CompleteDataAfterNavigation;
        BindingContext = vm;
	}

    /// <inheritdoc />
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        OnNavigatedToEvent();
    }
}