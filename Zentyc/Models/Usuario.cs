namespace Zentyc.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
