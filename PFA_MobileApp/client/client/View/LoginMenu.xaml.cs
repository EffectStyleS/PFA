using client.ViewModel;

namespace client.View
{
	public partial class LoginMenu : ContentPage
	{
        public LoginMenu(LoginMenuViewModel vm)
		{
			InitializeComponent();
			BindingContext = vm;
		}
	}
}