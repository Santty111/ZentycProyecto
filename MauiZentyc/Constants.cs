using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiZentyc;

public static class Constants
{
    public const string ApiBaseUrl = "https://tuservicioapi.com/api/";

    public static class Inventario
    {
        public const int StockMinimo = 5;
    }

    public static class Pedidos
    {
        public static List<string> Estados = new()
        {
            "Pendiente",
            "En Proceso",
            "Completado",
            "Cancelado"
        };
    }

    public static class Usuarios
    {
        public static List<string> Tipos = new()
        {
            "Administrador",
            "Empleado",
            "Cliente"
        };
    }
}
