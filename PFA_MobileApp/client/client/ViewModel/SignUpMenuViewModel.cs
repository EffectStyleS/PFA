using ApiClient;
using client.Infrastructure;
using client.Infrastructure.Cache;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace client.ViewModel;

/// <summary>
/// Модель представления меню регистрации
/// </summary>
public partial class SignUpMenuViewModel : BaseViewModel
{
    private readonly Client _client;
    private readonly ICacheService _cacheService;

    /// <summary>
    /// Модель представления меню регистрации
    /// </summary>
    /// <param name="client">Клиент</param>
    /// <param name="cacheService">Сервис работы с локальными файлами на девайсе</param>
    public SignUpMenuViewModel(Client client, ICacheService cacheService)
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
    /// Подтверждение пароля
    /// </summary>
    [ObservableProperty] private string _passwordConfirm;

    /// <summary>
    /// Команда регистрации
    /// </summary>
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
        PasswordConfirm = string.Empty;
        return Task.CompletedTask;
    }
}