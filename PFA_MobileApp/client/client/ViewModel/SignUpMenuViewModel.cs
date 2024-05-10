using ApiClient;
using client.Infrastructure;
using client.Infrastructure.Cache;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace client.ViewModel;

public partial class SignUpMenuViewModel : BaseViewModel
{
    private readonly Client _client;
    private readonly ICacheService _cacheService;

    public SignUpMenuViewModel(Client client, ICacheService cacheService)
    {
        _client = client;
        _cacheService = cacheService;
        EventManager.OnUserExit += UserExitHandler;
        OnUserLogin += EventManager.UserLoginHandler;
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
        catch
        {
            var mainPage = Application.Current!.MainPage;
            if (mainPage is not null)
            {
                await mainPage.DisplayAlert("Fail", Resources.RegisterFailed, "OK");
            }
                
            return;
        }

        if (result == null)
        {
            var mainPage = Application.Current!.MainPage;
            if (mainPage != null)
            {
                await mainPage.DisplayAlert("Fail", Resources.WrongCredentials, "OK");
            }
                
            return;
        }

        await _cacheService.SaveCredentialsToFile(new AuthCacheModel
        {
            Login = Login,
            Password = Password
        });
            
        _client.AddBearerToken(result.Token);
        PublishUserLogin();
            
        await Shell.Current.GoToAsync($"//{nameof(HistoryPage)}");
    }
        
    public delegate void StringDelegate(string login);
    public event StringDelegate? OnUserLogin;

    private void PublishUserLogin() => OnUserLogin?.Invoke(Login);
        
    private Task UserExitHandler()
    {
        Login = string.Empty;
        Password = string.Empty;
        PasswordConfirm = string.Empty;
        return Task.CompletedTask;
    }
}