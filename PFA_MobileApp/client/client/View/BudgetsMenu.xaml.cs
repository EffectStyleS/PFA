using client.ViewModel;

namespace client.View;

public partial class BudgetsMenu : ContentPage
{
	public BudgetsMenu(BudgetsMenuViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}