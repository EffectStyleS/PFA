using client.ViewModel;

namespace client.View;

public partial class ExpensesPopup
{
	public ExpensesPopup(ExpensesPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;	
	}
}