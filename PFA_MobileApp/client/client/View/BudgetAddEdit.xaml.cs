using client.ViewModel;

namespace client.View;

public partial class BudgetAddEdit : ContentPage
{
	public BudgetAddEdit(BudgetAddEditViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var vm = BindingContext as BudgetAddEditViewModel;
        vm.CompleteDataAfterNavigation();
    }
}