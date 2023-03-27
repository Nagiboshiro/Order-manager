using Microsoft.EntityFrameworkCore;
using OrderManager.DataAccess.Postgres.Repositories.Configuration;
using OrderManager.Domain.Order;
using OrderManager.Domain.Provider;

namespace OrderManager.DataAccess.Postgres;

public sealed class ApplicationDatabaseContext : DbContext
{
    public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options)
        : base(options)
    {
    }
    
    public DbSet<Provider> Providers { get; set; }
    public DbSet<Order> Orders { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProviderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
    }
}