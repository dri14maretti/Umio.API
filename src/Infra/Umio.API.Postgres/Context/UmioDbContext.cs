using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Umio.API.Entities.Entidades;


namespace Umio.API.Postgres.Context
{
    public class UmioDbContext : DbContext

    {
        public UmioDbContext(DbContextOptions<UmioDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Pagamento> Pagamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Pagamento>()
                .HasDiscriminator<string>("TipoPagamento")
                .HasValue<Dinheiro>("Dinheiro");
        }
    }
}