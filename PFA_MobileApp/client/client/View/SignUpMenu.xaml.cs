using client.ViewModel;
using Microsoft.Maui.Controls;

namespace client.View;

/// <summary>
/// Меню регистрации
/// </summary>
public partial class SignUpMenu : ContentPage
{
    /// <summary>
    /// Меню регистрации
    /// </summary>
    public SignUpMenu(SignUpMenuViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}