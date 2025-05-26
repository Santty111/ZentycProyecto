using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MauiZentyc.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private const string AuthTokenKey = "auth_token";

    public AuthService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(Constants.ApiBaseUrl);
    }

    public async Task<bool> Login(string username, string password)
    {
        try
        {
            var authData = new { username, password };
            var json = JsonSerializer.Serialize(authData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                await SecureStorage.SetAsync(AuthTokenKey, token);
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> IsAuthenticated()
    {
        var token = await SecureStorage.GetAsync(AuthTokenKey);
        return !string.IsNullOrEmpty(token);
    }

    public async Task Logout()
    {
        SecureStorage.Remove(AuthTokenKey);
    }

    public async Task<string> GetToken()
    {
        return await SecureStorage.GetAsync(AuthTokenKey);
    }
}
