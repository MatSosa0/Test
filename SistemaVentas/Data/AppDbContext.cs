using Microsoft.EntityFrameworkCore;
using SistemaVentas.Models;

namespace SistemaVentas.Data
{
 public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Venta> Ventas => Set<Venta>();
        public DbSet<VentaDetalle> VentaDetalles => Set<VentaDetalle>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(p => p.Precio)
                    .HasPrecision(18, 2);
            });

            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<VentaDetalle>(entity =>
            {
                entity.Property(d => d.PrecioUnitario)
                    .HasPrecision(18, 2);

                entity.Property(d => d.Subtotal)
                    .HasPrecision(18, 2);
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.Property(v => v.Total)
                    .HasPrecision(18, 2);
            });

        }


    }  
     
}