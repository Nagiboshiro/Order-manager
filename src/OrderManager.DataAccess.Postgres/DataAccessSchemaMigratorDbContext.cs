using Microsoft.EntityFrameworkCore;

namespace OrderManager.DataAccess.Postgres;

public sealed class DataAccessSchemaMigratorDbContext : DbContext
{
    public DataAccessSchemaMigratorDbContext(
        DbContextOptions<DataAccessSchemaMigratorDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataAccessSchemaMigratorDbContext).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}