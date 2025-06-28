using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheFoodHood.Web.Data.Entities;

namespace TheFoodHood.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

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
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Pedido → DetallePedidos
            builder.Entity<Pedido>()
                .HasMany(p => p.Detalles)
                .WithOne(dp => dp.Pedido)
                .HasForeignKey(dp => dp.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación DetallePedido → Articulo
            builder.Entity<DetallePedido>()
                .HasOne(dp => dp.Articulo)
                .WithMany(a => a.DetallesPedido!)
                .HasForeignKey(dp => dp.ArticuloId)
                .OnDelete(DeleteBehavior.Restrict); // evita eliminar artículo si hay pedidos

            // Clave primaria explícita
            builder.Entity<DetallePedido>()
                .HasKey(dp => dp.Id);
        }
    }
}
