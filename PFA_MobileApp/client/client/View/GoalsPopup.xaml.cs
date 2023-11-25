using client.ViewModel;

namespace client.View;

public partial class GoalsPopup
{
	public GoalsPopup(GoalsPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}