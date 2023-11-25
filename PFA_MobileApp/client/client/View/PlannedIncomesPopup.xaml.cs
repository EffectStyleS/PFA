using client.ViewModel;

namespace client.View;

public partial class PlannedIncomesPopup
{
	public PlannedIncomesPopup(PlannedIncomesPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}