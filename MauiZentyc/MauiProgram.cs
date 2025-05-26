using MauiZentyc.Converts;
using MauiZentyc.Services;
using MauiZentyc.ViewModels;
using MauiZentyc.Views;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace MauiZentyc;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        }).ConfigureMauiHandlers(handlers =>



        {
        // Configuraciones adicionales de handlers si son necesarias
        }).UseMauiCommunityToolkit();
        // Registro de servicios
        builder.Services.AddSingleton<ApiService>();
        // Registro de ViewModels
        builder.Services.AddTransient<InventarioViewModel>();
        builder.Services.AddTransient<PedidoViewModel>();
        builder.Services.AddTransient<UsuarioViewModel>();
        // Registro de páginas
        builder.Services.AddTransient<InventarioPage>();
        builder.Services.AddTransient<AddInventarioPage>();
        builder.Services.AddTransient<EditInventarioPage>();
        builder.Services.AddTransient<PedidosPage>();
        builder.Services.AddTransient<AddPedidoPage>();
        builder.Services.AddTransient<UsuariosPage>();
        builder.Services.AddTransient<AddUsuarioPage>();
        // Registro de convertidores
        builder.Services.AddSingleton<EstadoToColorConverter>();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}