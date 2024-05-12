using client.ViewModel;

namespace client.View;

/// <summary>
/// Попап доходов
/// </summary>
public partial class IncomesPopup
{
	/// <summary>
	/// Попап доходов
	/// </summary>
	public IncomesPopup(IncomesPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}