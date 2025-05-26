using MauiZentyc.Views;

namespace MauiZentyc;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Registro de rutas
        Routing.RegisterRoute(nameof(AddInventarioPage), typeof(AddInventarioPage));
        Routing.RegisterRoute(nameof(EditInventarioPage), typeof(EditInventarioPage));
        Routing.RegisterRoute(nameof(AddPedidoPage), typeof(AddPedidoPage));
        Routing.RegisterRoute(nameof(AddUsuarioPage), typeof(AddUsuarioPage));
    }
}