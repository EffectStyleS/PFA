using client.ViewModel;

namespace client.View
{
	public partial class IncomesMenu : ContentPage
	{
		public IncomesMenu(IncomesMenuViewModel vm)
		{
			InitializeComponent();
			BindingContext = vm;
		}
	}
}