using client.ViewModel;

namespace client.View;

public partial class BudgetsMenu : ContentPage
{
    public delegate Task TaskDelegate();
    public event TaskDelegate OnNavigatedToEvent;

    public BudgetsMenu(BudgetsMenuViewModel vm)
	{
		InitializeComponent();
        OnNavigatedToEvent += vm.CompleteDataAfterNavigation;
        BindingContext = vm;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        OnNavigatedToEvent();
    }
}