using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zentyc.Data;
using Zentyc.Models;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly WebContext _context;

    public UsuariosController(WebContext context)
    {
        _context = context;
    }

    // GET: api/Usuarios
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
    {
        return await _context.Usuario.ToListAsync();
    }

    // GET: api/Usuarios/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetUsuario(int id)
    {
        var usuario = await _context.Usuario.FindAsync(id);
        if (usuario == null) return NotFound();
        return usuario;
    }

    // POST: api/Usuarios
    [HttpPost]
    public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
    {
        _context.Usuario.Add(usuario);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUsuario), new { id = usuario.UsuarioId }, usuario);
    }

    // PUT: api/Usuarios/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
    {
        if (id != usuario.UsuarioId) return BadRequest();
        _context.Entry(usuario).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UsuarioExists(id)) return NotFound();
            else throw;
        }
        return NoContent();
    }

    // DELETE: api/Usuarios/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var usuario = await _context.Usuario.FindAsync(id);
        if (usuario == null) return NotFound();
        _context.Usuario.Remove(usuario);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool UsuarioExists(int id) => _context.Usuario.Any(e => e.UsuarioId == id);
}