using MauiZentyc.ViewModels;

namespace MauiZentyc.Views
{
    public partial class UsuariosPage : ContentPage
    {
        public UsuariosPage(UsuarioViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is UsuarioViewModel viewModel)
            {
                viewModel.LoadCommand.Execute(null);
            }
        }
    }
}