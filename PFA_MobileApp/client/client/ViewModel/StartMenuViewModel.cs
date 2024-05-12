using ApiClient;
using client.Infrastructure;
using client.Infrastructure.Cache;
using client.View;
using CommunityToolkit.Mvvm.Input;

namespace client.ViewModel;

/// <summary>
/// Модель представления стартового меню
/// </summary>
public partial class StartMenuViewModel : BaseViewModel
{
    private readonly Client _client;
    private readonly ICacheService _cacheService;
        
    /// <summary>
    /// Модель представления стартового меню
    /// </summary>
    /// <param name="client">Клиент</param>
    /// <param name="cacheService">Сервис работы с локальными файлами на девайсе</param>
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
    private void PublishUserLogin(string login) => OnUserLogin?.Invoke(login);
        
    /// <summary>
    /// Обработчик выхода пользователя
    /// </summary>
    private async Task UserExitHandler()
    {
        _cacheService.DeleteCredentialsFile();
            
        await _client.RevokeAsync(_client.GetCurrentUserLogin());
        _client.ResetBearerToken();
    }
  
    /// <summary>
    /// Команда перехода на страницу аутентификации
    /// </summary>
    [RelayCommand]
    private async Task LoginTap()
    {
        await Shell.Current.GoToAsync($"{nameof(LoginMenu)}");
    }

    /// <summary>
    /// Команда перехода на страницу регистрации
    /// </summary>
    [RelayCommand]
    private async Task SignUpTap()
    {
        await Shell.Current.GoToAsync($"{nameof(SignUpMenu)}");
    }

}