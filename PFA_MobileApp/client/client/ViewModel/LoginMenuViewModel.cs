using ApiClient;
using client.Infrastructure;
using client.Infrastructure.Cache;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace client.ViewModel;

public partial class LoginMenuViewModel : BaseViewModel
{
    private readonly Client _client;
    private readonly ICacheService _cacheService;
        
    public LoginMenuViewModel(Client client, ICacheService cacheService)
    {
        _client = client;
        _cacheService = cacheService;
        EventManager.OnUserExit += UserExitHandler;
        OnUserLogin += EventManager.UserLoginHandler;
    }

    [ObservableProperty] private string _login;

    [ObservableProperty] private string _password;

    [RelayCommand]
    private async Task SignInTap()
    {
        AuthResponse result;

        try
        {
            result = await _client.LoginAsync(new AuthRequest
            { 
                Login = Login,
                Password = Password
            });
        }
        catch
        {
            var mainPage = Application.Current!.MainPage;
            if (mainPage is not null)
            {
                await mainPage.DisplayAlert("Fail", Resources.AuthFailed, "OK");
            }
                
            return;
        }

        if (result is null)
        {
            var mainPage = Application.Current!.MainPage;
            if (mainPage is not null)
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
        return Task.CompletedTask;
    }
}