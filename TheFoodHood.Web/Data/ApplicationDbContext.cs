using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheFoodHood.Web.Data.Entities;

namespace TheFoodHood.Web.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Articulo> Articulos { get; set; } = null!;
        public DbSet<Pedido> Pedidos { get; set; } = null!;
        public DbSet<DetallePedido> DetallePedidos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Relación Pedido → Usuario
            builder.Entity<Pedido>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Pedidos)
                .HasForeignKey(p => p.UsuarioId);

            // Relación DetallePedido → Pedido
            builder.Entity<DetallePedido>()
                .HasOne(dp => dp.Pedido)
                .WithMany()
                .HasForeignKey(dp => dp.PedidoId);

            // Relación DetallePedido → Articulo
            builder.Entity<DetallePedido>()
                .HasOne(dp => dp.Articulo)
                .WithMany()
                .HasForeignKey(dp => dp.ArticuloId);

            // Clave primaria de DetallePedido (si usas Id)
            builder.Entity<DetallePedido>()
                .HasKey(dp => dp.Id);
        }

    }
}
