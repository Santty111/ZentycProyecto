using MauiZentyc.Services;

namespace MauiZentyc;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }

    protected override async void OnStart()
    {
        // Verificar autenticación al iniciar
        var authService = Handler.MauiContext.Services.GetService<AuthService>();
        if (authService != null && !await authService.IsAuthenticated())
        {
            // Redirigir a página de login si no está autenticado
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}