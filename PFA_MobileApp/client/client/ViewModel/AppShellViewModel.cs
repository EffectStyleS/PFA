using ApiClient;
using client.View;
using CommunityToolkit.Mvvm.Input;

namespace client.ViewModel
{
    public partial class AppShellViewModel : BaseViewModel
    {
        [RelayCommand]
        async Task Exit()
        {
            var navigationParameter = new Dictionary<string, object>()
            {
                { "IsRevoke", true },
            };

            await Shell.Current.GoToAsync($"//{nameof(StartMenu)}", navigationParameter);
        }
    }
}
