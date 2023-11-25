using client.ViewModel;

namespace client.View;

public partial class BudgetOverrunsPopup
{
	public BudgetOverrunsPopup(BudgetOverrunsPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}