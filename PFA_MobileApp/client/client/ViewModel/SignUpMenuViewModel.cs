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

        [ObservableProperty]
        UserModel _user;

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

            var userDto = await _client.UserAsync(result.Login);
            User = new UserModel();
            User.Id = userDto.Id;
            User.Login = userDto.Login;
            User.RefreshToken = userDto.RefreshToken;
            User.RefreshTokenExpireTime = userDto.RefreshTokenExpiryTime.DateTime;

            // TODO: мб на null проверять?

            var navigationParameter = new Dictionary<string, object>
            {
                { "User", User }
            };
            await Shell.Current.GoToAsync($"//{nameof(IncomesMenu)}", navigationParameter);
        }
    }
}
