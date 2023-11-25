using client.ViewModel;

namespace client.View;

public partial class IncomesPopup
{
	public IncomesPopup(IncomesPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}