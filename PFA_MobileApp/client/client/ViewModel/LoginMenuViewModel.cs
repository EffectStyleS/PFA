using ApiClient;
using client.Model.Models;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace client.ViewModel
{
    public partial class LoginMenuViewModel : BaseViewModel
    {
        private readonly Client _client;
        public LoginMenuViewModel(Client client)
        {
            _client = client;
        }

        [ObservableProperty]
        string _login;

        [ObservableProperty]
        string _password;

        [ObservableProperty]
        UserModel _user;

        [RelayCommand]
        async Task SignInTap()
        {
            AuthResponse result;

            try
            {
                result = await _client.LoginAsync(new AuthRequest()
                { 
                    Login = Login,
                    Password = Password
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
    }
}
