using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zentyc.Data;
using Zentyc.Models;
using ZentycApi.Models;

[Route("api/[controller]")]
[ApiController]
public class PedidosController : ControllerBase
{
    private readonly WebContext _context;

    public PedidosController(WebContext context)
    {
        _context = context;
    }

    // GET: api/Pedidos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PedidoDto>>> GetPedidos()
    {
        return await _context.Pedido
            .Include(p => p.Usuario)
            .Include(p => p.Inventario)
            .Select(p => new PedidoDto
            {
                PedidoId = p.PedidoId,
                FechaPedido = p.FechaPedido,
                CantidadSolicitada = p.CantidadSolicitada,
                Usuario = new UsuarioDto
                {
                    UsuarioId = p.Usuario.UsuarioId,
                    Nombre = p.Usuario.Nombre,
                    Correo = p.Usuario.Correo
                },
                Inventario = new InventarioDto
                {
                    InventarioId = p.Inventario.InventarioId,
                    Nombre = p.Inventario.Nombre,
                    Talla = p.Inventario.Talla,
                    Precio = p.Inventario.Precio
                }
            })
            .ToListAsync();
    }

    // GET: api/Pedidos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Pedido>> GetPedido(int id)
    {
        var pedido = await _context.Pedido
            .Include(p => p.Inventario)
            .Include(p => p.Usuario)
            .FirstOrDefaultAsync(p => p.PedidoId == id);
        if (pedido == null) return NotFound();
        return pedido;
    }

    // POST: api/Pedidos
    [HttpPost]
    public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
    {
        _context.Pedido.Add(pedido);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPedido), new { id = pedido.PedidoId }, pedido);
    }

    // PUT: api/Pedidos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPedido(int id, Pedido pedido)
    {
        if (id != pedido.PedidoId) return BadRequest();
        _context.Entry(pedido).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PedidoExists(id)) return NotFound();
            else throw;
        }
        return NoContent();
    }

    // DELETE: api/Pedidos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePedido(int id)
    {
        var pedido = await _context.Pedido.FindAsync(id);
        if (pedido == null) return NotFound();
        _context.Pedido.Remove(pedido);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool PedidoExists(int id) => _context.Pedido.Any(e => e.PedidoId == id);
}