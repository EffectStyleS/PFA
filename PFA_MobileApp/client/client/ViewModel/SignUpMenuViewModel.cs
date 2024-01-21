using ApiClient;
using client.Infrastructure;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace client.ViewModel
{
    public partial class SignUpMenuViewModel : BaseViewModel
    {
        private readonly Client _client;

        public SignUpMenuViewModel(Client client)
        {
            _client = client;
            EventManager.OnUserExit += UserExitHandler;
        }

        [ObservableProperty] private string _login;

        [ObservableProperty] private string _password;

        [ObservableProperty] private string _passwordConfirm;

        [RelayCommand]
        private async Task SignUpTap()
        {
            AuthResponse result;

            try
            {
                result = await _client.RegisterAsync(new RegisterRequest
                {
                    Login = Login,
                    Password = Password,
                    PasswordConfirm = PasswordConfirm,
                });
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Fail", ex.Message, "OK");
                return;
            }

            if (result == null)
            {
                await Application.Current.MainPage.DisplayAlert("Fail", "Login or password isn't valid", "OK");
                return;
            }

            _client.AddBearerToken(result.Token);

            await Shell.Current.GoToAsync($"//{nameof(IncomesMenu)}");
        }
        
        private async Task UserExitHandler()
        {
            Login = string.Empty;
            Password = string.Empty;
            PasswordConfirm = string.Empty;
        }
    }
}
