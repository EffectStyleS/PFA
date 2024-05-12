using client.ViewModel;

namespace client.View;

/// <summary>
/// Попап целей
/// </summary>
public partial class GoalsPopup
{
	/// <summary>
	/// Попап целей
	/// </summary>
	public GoalsPopup(GoalsPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}