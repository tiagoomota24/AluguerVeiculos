using Microsoft.EntityFrameworkCore;
using AluguerVeiculos.Models;

namespace AluguerVeiculos.Data;

public class DataContext (DbContextOptions<DataContext> options)
    : DbContext (options)

    {
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Contrato> Contrato { get; set; }
        
    }

