using ApiClient;
using client.Infrastructure;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace client.ViewModel
{
    public partial class StartMenuViewModel : BaseViewModel
    {
        private readonly Client _client;
        public StartMenuViewModel(Client client)
        {
            _client = client;
            EventManager.OnUserExit += UserExitHandler;
        }

        private async Task UserExitHandler()
        {
            await _client.RevokeAsync(_client.GetCurrentUserLogin());
            _client.ResetBearerToken();
        }

        [RelayCommand]
        private async Task LoginTap()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginMenu)}");
        }

        [RelayCommand]
        private async Task SignUpTap()
        {
            await Shell.Current.GoToAsync($"{nameof(SignUpMenu)}");
        }

    }
}
