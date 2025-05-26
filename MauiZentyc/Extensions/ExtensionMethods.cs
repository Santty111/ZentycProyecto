using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiZentyc.Models;

namespace MauiZentyc.Extensions;

public static class ModelExtensions
{
    public static string ToDisplayInfo(this Inventario item)
    {
        return $"{item.Nombre} - Stock: {item.Cantidad}";
    }

    public static string ToDisplayInfo(this Pedido item)
    {
        return $"{item.Fecha:dd/MM/yyyy} - {item.Producto?.Nombre}";
    }

    public static bool IsValid(this Usuario usuario)
    {
        return !string.IsNullOrWhiteSpace(usuario.Nombre) &&
               !string.IsNullOrWhiteSpace(usuario.Correo);
    }
}
