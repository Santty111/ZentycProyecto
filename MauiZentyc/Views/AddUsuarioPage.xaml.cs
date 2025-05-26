using MauiZentyc.ViewModels;

namespace MauiZentyc.Views
{
    public partial class AddUsuarioPage : ContentPage
    {
        public AddUsuarioPage(UsuarioViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}