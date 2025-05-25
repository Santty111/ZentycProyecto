namespace Zentyc.Models
{
    public class Inventario
    {
        public int InventarioId { get; set; }
        public string Nombre { get; set; }
        public string Talla { get; set; }
        public string Color { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
