using ApiClient;
using client.Infrastructure;
using client.Infrastructure.Cache;
using client.View;
using CommunityToolkit.Mvvm.Input;

namespace client.ViewModel
{
    public partial class StartMenuViewModel : BaseViewModel
    {
        private readonly Client _client;
        private readonly ICacheService _cacheService;
        
        public StartMenuViewModel(Client client, ICacheService cacheService)
        {
            _client = client;
            _cacheService = cacheService;
            EventManager.OnUserExit += UserExitHandler;
            OnUserLogin += EventManager.UserLoginHandler;
        }

        public async Task CompleteDataAfterNavigation()
        {
            var authCacheModel = _cacheService.GetCredentialsFromFile();

            try
            {
                var result = await _client.LoginAsync(new AuthRequest
                { 
                    Login = authCacheModel.Login,
                    Password = authCacheModel.Password
                });
                
                _client.AddBearerToken(result.Token);
                PublishUserLogin(authCacheModel.Login);
                
                await Shell.Current.GoToAsync($"//{nameof(HistoryPage)}");
            }
            catch
            {
                // ignored
            }
        }
        
        public delegate void StringDelegate(string login);
        public event StringDelegate? OnUserLogin;

        private void PublishUserLogin(string login) => OnUserLogin?.Invoke(login);
        
        private async Task UserExitHandler()
        {
            _cacheService.DeleteCredentialsFile();
            
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
