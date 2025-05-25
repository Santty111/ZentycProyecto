using System.ComponentModel.DataAnnotations.Schema;

namespace Zentyc.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public DateTime FechaPedido { get; set; }
        public int CantidadSolicitada { get; set; }
        // Relación con Usuario
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }

        // Relación con Producto
        public int InventarioId { get; set; }
        [ForeignKey("InventarioId")]
        public Inventario? Inventario { get; set; }
    }
}
