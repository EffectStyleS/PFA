using client.ViewModel;

namespace client.View
{
    public partial class StartMenu : ContentPage
    {
        public StartMenu(StartMenuViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}