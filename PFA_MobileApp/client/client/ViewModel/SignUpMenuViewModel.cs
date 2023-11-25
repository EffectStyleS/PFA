using client.View;
using CommunityToolkit.Mvvm.Input;

namespace client.ViewModel
{
    public partial class SignUpMenuViewModel : BaseViewModel
    {
        [RelayCommand]
        async Task SignUpTap()
        {
            // TODO: прикрутить регистрацию
            // передавать user'а query параметром
            await Shell.Current.GoToAsync($"//{nameof(IncomesMenu)}");
        }
    }
}
