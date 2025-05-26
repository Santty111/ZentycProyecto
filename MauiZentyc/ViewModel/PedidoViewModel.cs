using MauiZentyc.Models;
using MauiZentyc.Views;
using MauiZentyc.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Diagnostics;


namespace MauiZentyc.ViewModels
{
    public class PedidoViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;
        private Pedido _selectedItem;
        private Pedido _nuevoPedido = new();

        public ObservableCollection<Pedido> Items { get; } = new();
        public ObservableCollection<Inventario> Productos { get; } = new();
        public ObservableCollection<Usuario> Clientes { get; } = new();
        public ObservableCollection<string> Estados { get; } = new ObservableCollection<string>
        {
            "Pendiente",
            "En proceso",
            "Completado",
            "Cancelado"
        };

        public Pedido SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public Pedido NuevoPedido
        {
            get => _nuevoPedido;
            set => SetProperty(ref _nuevoPedido, value);
        }

        public ICommand LoadCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand ProductoSeleccionadoCommand { get; }

        public PedidoViewModel(ApiService apiService)
        {
            _apiService = apiService;
            Title = "Pedidos";

            LoadCommand = new Command(async () => await LoadData());
            AddCommand = new Command(OnAddItem);
            SaveCommand = new Command(async () => await SavePedido());
            RefreshCommand = new Command(async () => await LoadData());
            ProductoSeleccionadoCommand = new Command(OnProductoSeleccionado);
        }

        private async Task LoadData()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                await Task.WhenAll(
                    LoadPedidos(),
                    LoadProductos(),
                    LoadClientes()
                );
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error al cargar: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadPedidos()
        {
            Items.Clear();
            var items = await _apiService.GetPedidosAsync();
            foreach (var item in items.OrderByDescending(p => p.Fecha))
            {
                Items.Add(item);
            }
        }

        private async Task LoadProductos()
        {
            Productos.Clear();
            var items = await _apiService.GetInventariosAsync();
            foreach (var item in items.Where(i => i.Cantidad > 0).OrderBy(p => p.Nombre))
            {
                Productos.Add(item);
            }
        }

        private async Task LoadClientes()
        {
            Clientes.Clear();
            var items = await _apiService.GetUsuariosAsync();
            foreach (var item in items.OrderBy(u => u.Nombre))
            {
                Clientes.Add(item);
            }
        }

        private void OnProductoSeleccionado()
        {
            if (NuevoPedido.Producto != null)
            {
                Debug.WriteLine($"Producto seleccionado: {NuevoPedido.Producto.Nombre}");
            }
        }

        private async void OnAddItem()
        {
            NuevoPedido = new Pedido { Fecha = DateTime.Now };
            await Shell.Current.GoToAsync(nameof(AddPedidoPage));
        }

        private async Task SavePedido()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                // Validaciones
                if (NuevoPedido.Producto == null)
                {
                    await Shell.Current.DisplayAlert("Error", "Seleccione un producto", "OK");
                    return;
                }

                if (NuevoPedido.Cliente == null)
                {
                    await Shell.Current.DisplayAlert("Error", "Seleccione un cliente", "OK");
                    return;
                }

                if (NuevoPedido.Cantidad <= 0)
                {
                    await Shell.Current.DisplayAlert("Error", "Cantidad inválida", "OK");
                    return;
                }

                bool success = await _apiService.AddPedidoAsync(NuevoPedido);
                if (success)
                {
                    await Shell.Current.DisplayAlert("Éxito", "Pedido registrado", "OK");
                    await LoadData();
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