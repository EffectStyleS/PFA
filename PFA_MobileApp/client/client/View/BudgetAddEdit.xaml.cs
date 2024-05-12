using client.ViewModel;

namespace client.View;

/// <summary>
/// Страница добавления/редактирования бюджета
/// </summary>
public partial class BudgetAddEdit : ContentPage
{
    /// <summary>
    /// Событие перехода на страницу
    /// </summary>
    public event Action OnNavigatedToEvent = delegate { };

    /// <summary>
    /// Страница добавления/редактирования бюджета
    /// </summary>
    public BudgetAddEdit(BudgetAddEditViewModel vm)
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