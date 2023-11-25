using client.ViewModel;

namespace client.View;

public partial class PlannedExpensesPopup
{
	public PlannedExpensesPopup(PlannedExpensesPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}