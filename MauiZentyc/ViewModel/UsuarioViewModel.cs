using MauiZentyc.Models;
using MauiZentyc.Views;
using MauiZentyc.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiZentyc.ViewModels
{
    public class UsuarioViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;
        private Usuario _nuevoUsuario = new();

        public ObservableCollection<Usuario> Items { get; } = new();
        public ObservableCollection<string> TiposUsuario { get; } = new ObservableCollection<string>
        {
            "Administrador",
            "Empleado",
            "Cliente"
        };

        public Usuario NuevoUsuario
        {
            get => _nuevoUsuario;
            set => SetProperty(ref _nuevoUsuario, value);
        }

        // Comandos actualizados
        public ICommand LoadCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand SaveCommand { get; }

        public UsuarioViewModel(ApiService apiService)
        {
            _apiService = apiService;
            Title = "Usuarios";

            // Inicialización de comandos
            LoadCommand = new Command(async () => await LoadUsuariosAsync());
            RefreshCommand = new Command(async () => await LoadUsuariosAsync());
            AddCommand = new Command(OnAddUsuario);
            SaveCommand = new Command(async () => await SaveUsuarioAsync());
        }

        private async Task LoadUsuariosAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                Items.Clear();
                var usuarios = await _apiService.GetUsuariosAsync();

                foreach (var usuario in usuarios)
                {
                    Items.Add(usuario);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnAddUsuario()
        {
            await Shell.Current.GoToAsync(nameof(AddUsuarioPage));
        }

        private async Task SaveUsuarioAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                // Validaciones
                if (string.IsNullOrWhiteSpace(NuevoUsuario.Nombre))
                {
                    await Shell.Current.DisplayAlert("Error", "Ingrese el nombre completo", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(NuevoUsuario.Correo))
                {
                    await Shell.Current.DisplayAlert("Error", "Ingrese el correo electrónico", "OK");
                    return;
                }

                bool success = await _apiService.AddUsuarioAsync(NuevoUsuario);
                if (success)
                {
                    await Shell.Current.DisplayAlert("Éxito", "Usuario registrado", "OK");
                    NuevoUsuario = new Usuario();
                    await LoadUsuariosAsync();
                    await Shell.Current.GoToAsync("..");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}