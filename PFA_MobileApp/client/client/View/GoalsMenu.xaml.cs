using client.ViewModel;

namespace client.View;

public partial class GoalsMenu : ContentPage
{
	public GoalsMenu(GoalsMenuViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}