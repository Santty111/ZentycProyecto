using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zentyc.Data;
using Zentyc.Models;

[Route("api/[controller]")]
[ApiController]
public class InventariosController : ControllerBase
{
    private readonly WebContext _context;

    public InventariosController(WebContext context)
    {
        _context = context;
    }

    // GET: api/Inventarios
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Inventario>>> GetInventarios()
    {
        return await _context.Inventario.ToListAsync();
    }

    // GET: api/Inventarios/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Inventario>> GetInventario(int id)
    {
        var inventario = await _context.Inventario.FindAsync(id);
        if (inventario == null) return NotFound();
        return inventario;
    }

    // POST: api/Inventarios
    [HttpPost]
    public async Task<ActionResult<Inventario>> PostInventario(Inventario inventario)
    {
        _context.Inventario.Add(inventario);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetInventario), new { id = inventario.InventarioId }, inventario);
    }

    // PUT: api/Inventarios/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutInventario(int id, Inventario inventario)
    {
        if (id != inventario.InventarioId) return BadRequest();
        _context.Entry(inventario).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!InventarioExists(id)) return NotFound();
            else throw;
        }
        return NoContent();
    }

    // DELETE: api/Inventarios/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInventario(int id)
    {
        var inventario = await _context.Inventario.FindAsync(id);
        if (inventario == null) return NotFound();
        _context.Inventario.Remove(inventario);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool InventarioExists(int id) => _context.Inventario.Any(e => e.InventarioId == id);
}