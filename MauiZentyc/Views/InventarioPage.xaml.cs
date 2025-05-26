using MauiZentyc.ViewModels;

namespace MauiZentyc.Views;

public partial class InventarioPage : ContentPage
{
    public InventarioPage(InventarioViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is InventarioViewModel vm)
        {
            vm.LoadCommand.Execute(null);
        }
    }
}