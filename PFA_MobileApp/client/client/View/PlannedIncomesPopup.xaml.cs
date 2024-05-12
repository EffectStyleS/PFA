using client.ViewModel;

namespace client.View;

/// <summary>
/// Попап запланированных доходов
/// </summary>
public partial class PlannedIncomesPopup
{
	/// <summary>
	/// Попап запланированных доходов
	/// </summary>
	public PlannedIncomesPopup(PlannedIncomesPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}