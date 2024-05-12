using client.Infrastructure;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace client.ViewModel;

/// <summary>
/// Модель представления AppShell
/// </summary>
public partial class AppShellViewModel : BaseViewModel
{
    /// <summary>
    /// Модель представления AppShell
    /// </summary>
    public AppShellViewModel()
    {
        OnUserExit += EventManager.UserExitHandler;
        EventManager.OnUserLogin += UserLoginHandler;
    }
    
    /// <summary>
    /// Делегат с возвращаемым значением Task?
    /// </summary>
    public delegate Task? TaskDelegate();
    
    /// <summary>
    /// Событие выхода пользователя
    /// </summary>
    public event TaskDelegate? OnUserExit;

    /// <summary>
    /// Имя пользователя
    /// </summary>
    [ObservableProperty] private string _userName = "Guest";
    
    /// <summary>
    /// Команда выхода пользователя
    /// </summary>
    [RelayCommand]
    private async Task Exit()
    {
        OnUserExit?.Invoke();
        await Shell.Current.GoToAsync($"//{nameof(StartMenu)}");
    }
    
    /// <summary>
    /// Обработчик входа пользователя
    /// </summary>
    /// <param name="login">Логин</param>
    private void UserLoginHandler(string login)
    {
        UserName = login;
    }
}