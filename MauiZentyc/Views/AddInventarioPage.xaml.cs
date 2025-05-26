using MauiZentyc.ViewModels;

namespace MauiZentyc.Views;

public partial class AddInventarioPage : ContentPage
{
    public AddInventarioPage(InventarioViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}