using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Conta> Contas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ContaFixa> ContasFixas { get; set; }
        public DbSet<ContaParcelada> ContasParceladas { get; set; }
        public DbSet<ContaVariavel> ContasVariavels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationship between Conta and Usuario
            modelBuilder.Entity<Conta>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Contas)
                .HasForeignKey(c => c.UsuarioId);

            // Configure inheritance with a discriminator column
            modelBuilder.Entity<Conta>()
                .HasDiscriminator<string>("TipoConta")
                .HasValue<ContaFixa>("Fixa")
                .HasValue<ContaParcelada>("Parcelada")
                .HasValue<ContaVariavel>("Variavel");
        }
    }
}