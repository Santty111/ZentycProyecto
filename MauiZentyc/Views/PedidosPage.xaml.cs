using MauiZentyc.ViewModels;

namespace MauiZentyc.Views;

public partial class PedidosPage : ContentPage
{
    public PedidosPage(PedidoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is PedidoViewModel vm)
        {
            vm.LoadCommand.Execute(null);
        }
    }
}