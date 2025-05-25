using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zentyc.Models;

namespace Zentyc.Data
{
    public class WebContext : DbContext
    {
        public WebContext (DbContextOptions<WebContext> options)
            : base(options)
        {
        }

        public DbSet<Zentyc.Models.Pedido> Pedido { get; set; } = default!;
        public DbSet<Zentyc.Models.Usuario> Usuario { get; set; } = default!;
        public DbSet<Zentyc.Models.Inventario> Inventario { get; set; } = default!;
    }
}
