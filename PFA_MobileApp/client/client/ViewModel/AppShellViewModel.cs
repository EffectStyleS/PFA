using ApiClient;
using client.Infrastructure;
using client.View;
using CommunityToolkit.Mvvm.Input;

namespace client.ViewModel
{
    public partial class AppShellViewModel : BaseViewModel
    {
        public delegate Task TaskDelegate();
        public event TaskDelegate OnUserExit;
        
        public AppShellViewModel()
        {
            OnUserExit += EventManager.UserExitHandler;
        }
        
        [RelayCommand]
        private async Task Exit()
        {
            OnUserExit?.Invoke();
            await Shell.Current.GoToAsync($"//{nameof(StartMenu)}");
        }
    }
}
