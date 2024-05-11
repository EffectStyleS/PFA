using client.ViewModel;

namespace client.View;

public partial class GoalsStatisticsPage : ContentPage
{
    public delegate Task TaskDelegate();
    public event TaskDelegate OnNavigatedToEvent;
    
    public GoalsStatisticsPage(GoalsStatisticsViewModel vm)
    {
        InitializeComponent();
        OnNavigatedToEvent += vm.CompleteDataAfterNavigation;
        BindingContext = vm;
    }
    
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        OnNavigatedToEvent?.Invoke();
    }
}