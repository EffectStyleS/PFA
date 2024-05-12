using client.ViewModel;

namespace client.View;

/// <summary>
/// Попап расходов
/// </summary>
public partial class ExpensesPopup
{
	/// <summary>
	/// Попап расходов
	/// </summary>
	public ExpensesPopup(ExpensesPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;	
	}
}