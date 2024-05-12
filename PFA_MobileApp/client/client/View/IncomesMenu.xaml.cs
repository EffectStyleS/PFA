using client.ViewModel;

namespace client.View;

/// <summary>
/// Меню доходов
/// </summary>
public partial class IncomesMenu : ContentPage
{
	/// <summary>
	/// Событие перехода на страницу
	/// </summary>
	public event Func<Task> OnNavigatedToEvent = () => Task.CompletedTask;

	/// <summary>
	/// Меню доходов
	/// </summary>
	public IncomesMenu(IncomesMenuViewModel vm)
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