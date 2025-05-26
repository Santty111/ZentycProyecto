using MauiZentyc.Models;
using MauiZentyc.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiZentyc.Views;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace MauiZentyc.ViewModels;

public class InventarioViewModel : BaseViewModel
{
    private readonly ApiService _apiService;
    private Inventario _selectedItem;
    private Inventario _nuevoProducto = new();

    public ObservableCollection<Inventario> Items { get; } = new();

    public Inventario SelectedItem
    {
        get => _selectedItem;
        set => SetProperty(ref _selectedItem, value);
    }

    public Inventario NuevoProducto
    {
        get => _nuevoProducto;
        set => SetProperty(ref _nuevoProducto, value);
    }

    // Comandos
    public ICommand LoadItemsCommand { get; }
    public ICommand AddItemCommand { get; }
    public ICommand ItemTappedCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand AddProductoCommand { get; }
    public ICommand LoadCommand { get; }

    public InventarioViewModel(ApiService apiService)
    {
        _apiService = apiService;
        Title = "Inventario";

        LoadItemsCommand = new Command(async () => await LoadItemsAsync());
        AddItemCommand = new Command(OnAddItem);
        ItemTappedCommand = new Command<Inventario>(OnItemSelected);
        RefreshCommand = new Command(async () => await LoadItemsAsync());
        AddProductoCommand = new Command(async () => await AddProductoAsync());
        LoadCommand = new Command(async () => await LoadItemsAsync());
    }

    private async Task LoadItemsAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            Items.Clear();

            var items = await _apiService.GetInventariosAsync();

            foreach (var item in items)
            {
                if (item != null)
                {
                    Items.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex}");
            await Shell.Current.DisplayAlert("Error",
                "No se pudieron cargar los items. Intente nuevamente.", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async void OnAddItem()
    {
        if (IsBusy) return;

        await Shell.Current.GoToAsync(nameof(AddInventarioPage), new Dictionary<string, object>
        {
            ["ViewModel"] = this // Pasamos el ViewModel actual
        });
    }

    private async Task AddProductoAsync()
    {
        if (IsBusy) return;

        try
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(NuevoProducto.Nombre))
            {
                await Shell.Current.DisplayAlert("Error", "Ingrese el nombre del producto", "OK");
                return;
            }

            if (NuevoProducto.Precio <= 0)
            {
                await Shell.Current.DisplayAlert("Error", "El precio debe ser mayor a 0", "OK");
                return;
            }

            IsBusy = true;

            bool success = await _apiService.AddInventarioAsync(NuevoProducto);
            if (success)
            {
                await Shell.Current.DisplayAlert("Éxito", "Producto añadido", "OK");
                NuevoProducto = new Inventario(); // Limpiar formulario
                await LoadItemsAsync(); // Actualizar lista
                await Shell.Current.GoToAsync(".."); // Regresar
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

    private async void OnItemSelected(Inventario item)
    {
        if (item == null || IsBusy) return;

        try
        {
            IsBusy = true;
            await Shell.Current.DisplayAlert("Detalles",
                $"Producto: {item.Nombre}\nStock: {item.Cantidad}\nPrecio: {item.Precio:C}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}