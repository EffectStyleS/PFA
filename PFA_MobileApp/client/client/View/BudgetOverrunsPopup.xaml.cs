using client.ViewModel;

namespace client.View;

/// <summary>
/// Попап превышений бюджетов
/// </summary>
public partial class BudgetOverrunsPopup
{
	/// <summary>
	/// Попап превышений бюджетов
	/// </summary>
	public BudgetOverrunsPopup(BudgetOverrunsPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}