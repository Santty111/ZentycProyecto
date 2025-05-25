using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zentyc.Data;
using Zentyc.Models;

namespace Web.Controllers
{
    public class PedidosController : Controller
    {
        private readonly WebContext _context;

        public PedidosController(WebContext context)
        {
            _context = context;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            var webContext = _context.Pedido.Include(p => p.Inventario).Include(p => p.Usuario);
            return View(await webContext.ToListAsync());
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Inventario)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.PedidoId == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            ViewData["InventarioId"] = new SelectList(_context.Set<Inventario>(), "InventarioId", "Nombre");
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "UsuarioId", "Nombre");
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PedidoId,FechaPedido,CantidadSolicitada,UsuarioId,InventarioId")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InventarioId"] = new SelectList(_context.Set<Inventario>(), "InventarioId", "Nombre", pedido.InventarioId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "UsuarioId", "Nombre", pedido.UsuarioId);
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["InventarioId"] = new SelectList(_context.Set<Inventario>(), "InventarioId", "Nombre", pedido.InventarioId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "UsuarioId", "Nombre", pedido.UsuarioId);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PedidoId,FechaPedido,CantidadSolicitada,UsuarioId,InventarioId")] Pedido pedido)
        {
            if (id != pedido.PedidoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.PedidoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["InventarioId"] = new SelectList(_context.Set<Inventario>(), "InventarioId", "Nombre", pedido.InventarioId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "UsuarioId", "Nombre", pedido.UsuarioId);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Inventario)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.PedidoId == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedido.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedido.Any(e => e.PedidoId == id);
        }
    }
}
