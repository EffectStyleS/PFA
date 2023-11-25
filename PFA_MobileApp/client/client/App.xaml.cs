using client.View;
using client.ViewModel;

namespace client
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell(new AppShellViewModel());
        }
    }
}