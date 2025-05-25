namespace ZentycApi.Models
{
    public class PedidoDto
    {
        public int PedidoId { get; set; }
        public DateTime FechaPedido { get; set; }
        public int CantidadSolicitada { get; set; }

        // Relaciones (usando DTOs)
        public UsuarioDto Usuario { get; set; }
        public InventarioDto Inventario { get; set; }
    }

    public class UsuarioDto
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        // No incluir 'Pedidos' para evitar ciclos
    }

    public class InventarioDto
    {
        public int InventarioId { get; set; }
        public string Nombre { get; set; }
        public string Talla { get; set; }
        public decimal Precio { get; set; }
        // No incluir 'Pedidos' para evitar ciclos
    }
}
