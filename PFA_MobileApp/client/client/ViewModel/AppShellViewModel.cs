using client.View;
using CommunityToolkit.Mvvm.Input;

namespace client.ViewModel
{
    public partial class AppShellViewModel : BaseViewModel
    {
        [RelayCommand]
        async Task Exit()
        {
            await Shell.Current.GoToAsync($"//{nameof(StartMenu)}");
        }
    }
}
