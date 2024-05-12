using client.ViewModel;

namespace client.View;

/// <summary>
/// Меню аутентификации
/// </summary>
public partial class LoginMenu : ContentPage
{
	/// <summary>
	/// Меню аутентификации
	/// </summary>
	public LoginMenu(LoginMenuViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}