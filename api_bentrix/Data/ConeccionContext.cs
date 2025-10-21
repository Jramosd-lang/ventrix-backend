using Microsoft.EntityFrameworkCore;
using api_bentrix.Models;
namespace api_ventrix.Data
{
    public class ConeccionContext : DbContext
    {
        public ConeccionContext(DbContextOptions<ConeccionContext> options)
            : base(options)
        {
        }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Comprador> Compradores { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Negocio> Negocios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }
        public DbSet<Descuento> Descuentos { get; set; }
        public DbSet<Impuesto> Impuestos { get; set; }
        public DbSet<Carrito_Compras> CarritosCompras { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
        public DbSet<Reporte> Reportes { get; set; }
        public DbSet<api_bentrix.Models.ImpuestoDetalle> ImpuestoDetalle { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Desactivar CASCADE en relaciones con Negocio para evitar ciclos
            modelBuilder.Entity<Administrador>()
                .HasOne(a => a.Negocio)
                .WithMany()
                .HasForeignKey(a => a.Id_Negocio)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comprador>()
                .HasOne<Negocio>()
                .WithMany()
                .HasForeignKey(c => c.Id_Negocio)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Vendedor>()
                .HasOne(v => v.Negocio)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Carrito_Compras>()
                .HasOne<Negocio>()
                .WithMany()
                .HasForeignKey(c => c.Id_Negocio)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Producto>()
                .HasOne<Negocio>()
                .WithMany()
                .HasForeignKey(p => p.Id_Negocio)
                .OnDelete(DeleteBehavior.NoAction);

            // Especificar precisión para columnas decimal
            modelBuilder.Entity<Carrito_Compras>()
                .Property(c => c.Subtotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Carrito_Compras>()
                .Property(c => c.Total)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Carrito_Compras>()
                .Property(c => c.Valor_Envio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Descuento>()
                .Property(d => d.Valor_Aplica)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Descuento>()
                .Property(d => d.Valor_Max)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Descuento>()
                .Property(d => d.Valor_Min)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ImpuestoDetalle>()
                .Property(i => i.Valor)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Valor)
                .HasPrecision(18, 2);
        }
    }
}