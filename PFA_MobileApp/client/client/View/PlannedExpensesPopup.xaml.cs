using client.ViewModel;

namespace client.View;

/// <summary>
/// Попап запланированных расходов
/// </summary>
public partial class PlannedExpensesPopup
{
	/// <summary>
	/// Попап запланированных расходов
	/// </summary>
	public PlannedExpensesPopup(PlannedExpensesPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}