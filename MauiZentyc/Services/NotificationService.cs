using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiZentyc.Services;

public class NotificationService
{
    public async Task ShowSuccess(string message)
    {
        await Shell.Current.DisplayAlert("Éxito", message, "OK");
    }

    public async Task ShowError(string message)
    {
        await Shell.Current.DisplayAlert("Error", message, "OK");
    }

    public async Task<bool> ShowConfirmation(string title, string message)
    {
        return await Shell.Current.DisplayAlert(title, message, "Sí", "No");
    }
}
