using client.View;
using CommunityToolkit.Mvvm.Input;

namespace client.ViewModel
{
    public partial class StartMenuViewModel : BaseViewModel
    {
        public StartMenuViewModel()
        {
        }

        [RelayCommand]
        async Task LoginTap()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginMenu)}");
        }

        [RelayCommand]
        async Task SignUpTap()
        {
            await Shell.Current.GoToAsync($"{nameof(SignUpMenu)}");
        }

    }
}
