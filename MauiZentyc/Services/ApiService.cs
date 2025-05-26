using MauiZentyc.Models;
using System.Net.Http.Json;
using System.Net;

namespace MauiZentyc.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://localhost:7183/api/";

    public ApiService()
    {
        _httpClient = new HttpClient(new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        })
        {
            BaseAddress = new Uri(BaseUrl),
            Timeout = TimeSpan.FromSeconds(30)
        };
    }

    // -------------------- INVENTARIO --------------------
    public async Task<List<Inventario>> GetInventariosAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<Inventario>>("inventario");
        }
        catch
        {
            return new List<Inventario>();
        }
    }

    public async Task<bool> AddInventarioAsync(Inventario item)
    {
        var response = await _httpClient.PostAsJsonAsync("inventario", item);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateInventarioAsync(Inventario item)
    {
        var response = await _httpClient.PutAsJsonAsync($"inventario/{item.Id}", item);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteInventarioAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"inventario/{id}");
        return response.IsSuccessStatusCode;
    }

    // -------------------- PEDIDOS --------------------
    public async Task<List<Pedido>> GetPedidosAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<Pedido>>("pedidos");
        }
        catch
        {
            return new List<Pedido>();
        }
    }

    public async Task<bool> AddPedidoAsync(Pedido item)
    {
        var response = await _httpClient.PostAsJsonAsync("pedidos", item);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdatePedidoAsync(Pedido item)
    {
        var response = await _httpClient.PutAsJsonAsync($"pedidos/{item.Id}", item);
        return response.IsSuccessStatusCode;
    }

    // -------------------- USUARIOS --------------------
    public async Task<List<Usuario>> GetUsuariosAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<Usuario>>("usuarios");
        }
        catch
        {
            return new List<Usuario>();
        }
    }

    public async Task<bool> AddUsuarioAsync(Usuario item)
    {
        var response = await _httpClient.PostAsJsonAsync("usuarios", item);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateUsuarioAsync(Usuario item)
    {
        var response = await _httpClient.PutAsJsonAsync($"usuarios/{item.Id}", item);
        return response.IsSuccessStatusCode;
    }
}