using client.ViewModel;

namespace client.View;

public partial class ExpensesMenu : ContentPage
{
	public ExpensesMenu(ExpensesMenuViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}