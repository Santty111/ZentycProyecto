using MauiZentyc.ViewModels;

namespace MauiZentyc.Views
{
    public partial class AddPedidoPage : ContentPage
    {
        public AddPedidoPage(PedidoViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}