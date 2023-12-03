using client.ViewModel;

namespace client.View;

public partial class BudgetAddEdit : ContentPage
{
    public event Action OnNavigatedToEvent = delegate { };

    public BudgetAddEdit(BudgetAddEditViewModel vm)
	{
		InitializeComponent();
        OnNavigatedToEvent += vm.CompleteDataAfterNavigation;
        BindingContext = vm;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        OnNavigatedToEvent();
    }
}