using ApiClient;
using client.Infrastructure;
using client.Infrastructure.Cache;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace client.ViewModel;

/// <summary>
/// Модель представления меню аутентификации
/// </summary>
public partial class LoginMenuViewModel : BaseViewModel
{
    private readonly Client _client;
    private readonly ICacheService _cacheService;
        
    /// <summary>
    /// Модель представления меню аутентификации
    /// </summary>
    /// <param name="client">Клиент</param>
    /// <param name="cacheService">Сервис работы с локальными файлами на девайсе</param>
    public LoginMenuViewModel(Client client, ICacheService cacheService)
    {
        _client = client;
        _cacheService = cacheService;
        EventManager.OnUserExit += UserExitHandler;
        OnUserLogin += EventManager.UserLoginHandler;
    }

    /// <summary>
    /// Логин
    /// </summary>
    [ObservableProperty] private string _login;

    /// <summary>
    /// Пароль
    /// </summary>
    [ObservableProperty] private string _password;

    /// <summary>
    /// Команда входа
    /// </summary>
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

    /// <summary>
    /// Делегат логина пользователя
    /// </summary>
    public delegate void LoginDelegate(string login);
    
    /// <summary>
    /// Событие логина пользователя
    /// </summary>
    public event LoginDelegate? OnUserLogin;

    /// <summary>
    /// Обработчик событие логина пользователя
    /// </summary>
    private void PublishUserLogin() => OnUserLogin?.Invoke(Login);
        
    /// <summary>
    /// Обработчик выхода пользователя
    /// </summary>
    private Task UserExitHandler()
    {
        Login = string.Empty;
        Password = string.Empty;
        return Task.CompletedTask;
    }
}