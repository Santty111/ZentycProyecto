using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiZentyc.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public Inventario Producto { get; set; }
        public Usuario Cliente { get; set; }
        public int Cantidad { get; set; }
        public string Estado { get; set; }
        public string Notas { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        // Para compatibilidad con API
        public int InventarioId => Producto?.Id ?? 0;
        public int UsuarioId => Cliente?.Id ?? 0;
    }
}