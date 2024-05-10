using client.Infrastructure;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace client.ViewModel;

public partial class AppShellViewModel : BaseViewModel
{
    public AppShellViewModel()
    {
        OnUserExit += EventManager.UserExitHandler;
        EventManager.OnUserLogin += UserLoginHandler;
    }
    
    public delegate Task? TaskDelegate();
    public event TaskDelegate? OnUserExit;

    [ObservableProperty] private string _userName = "Guest";
    
    [RelayCommand]
    private async Task Exit()
    {
        OnUserExit?.Invoke();
        await Shell.Current.GoToAsync($"//{nameof(StartMenu)}");
    }
    
    private void UserLoginHandler(string login)
    {
        UserName = login;
    }
}