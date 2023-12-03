using ApiClient;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace client.ViewModel
{
    public partial class StartMenuViewModel : BaseViewModel
    {
        public readonly Client _client;
        public StartMenuViewModel(Client client)
        {
            _client = client;
            _isRevoke = false;
        }

        [ObservableProperty]
        bool _isRevoke;

        public async Task CompleteDataAfterNavigation()
        {
            if (!IsRevoke)
                return;


            await _client.RevokeAsync(_client.GetCurrentUserLogin());
            _client.ResetBearerToken();
        }

        [RelayCommand]
        async Task LoginTap()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginMenu)}");
        }

        [RelayCommand]
        async Task SignUpTap()
        {
            await Shell.Current.GoToAsync($"{nameof(SignUpMenu)}");
        }

    }
}
