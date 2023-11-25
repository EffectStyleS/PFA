using client.View;
using CommunityToolkit.Mvvm.Input;

namespace client.ViewModel
{
    public partial class LoginMenuViewModel : BaseViewModel
    {
        [RelayCommand]
        async Task SignInTap()
        {
            // TODO: прикрутить авторизацию
            // передавать user'а query параметром
            await Shell.Current.GoToAsync($"//{nameof(IncomesMenu)}");
        }
    }
}
