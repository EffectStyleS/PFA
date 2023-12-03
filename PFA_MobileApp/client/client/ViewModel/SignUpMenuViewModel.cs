using ApiClient;
using client.Model.Models;
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
        }

        [ObservableProperty]
        string _login;

        [ObservableProperty]
        string _password;

        [ObservableProperty]
        string _passwordConfirm;

        [RelayCommand]
        async Task SignUpTap()
        {
            AuthResponse result;

            try
            {
                result = await _client.RegisterAsync(new RegisterRequest()
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
    }
}
