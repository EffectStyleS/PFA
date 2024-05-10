using client.ViewModel;
using Microsoft.Maui.Controls;

namespace client.View;

public partial class SignUpMenu : ContentPage
{
    public SignUpMenu(SignUpMenuViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}