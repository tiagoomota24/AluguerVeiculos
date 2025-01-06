using Microsoft.EntityFrameworkCore;
using AluguerVeiculos.Models;

namespace AluguerVeiculos.Data;

public class DataContext (DbContextOptions<DataContext> options)
    : DbContext (options)

    {
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Contrato> Contrato { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Veiculo>()
                .HasIndex(v => v.Matricula)
                .IsUnique();

            // Configurar a constraint UNIQUE para a coluna Email
            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Email)
                .IsUnique();
        }
        
    }

