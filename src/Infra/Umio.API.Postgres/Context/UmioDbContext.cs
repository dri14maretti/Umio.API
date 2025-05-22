using Microsoft.EntityFrameworkCore;
using Umio.API.Entities.Entidades;
using Umio.API.Entities.Entidades.Produtos;


namespace Umio.API.Postgres.Context
{
    public class UmioDbContext : DbContext
    {
        public UmioDbContext(DbContextOptions<UmioDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Pagamento> Pagamentos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Pagamento>()
                .HasDiscriminator<string>("TipoPagamento")
                .HasValue<Dinheiro>("Dinheiro");
        }
    }
}