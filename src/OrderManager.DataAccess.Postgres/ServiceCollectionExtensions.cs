using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.DataAccess.IRepository;
using OrderManager.DataAccess.Postgres.Exceptions;

namespace OrderManager.DataAccess.Postgres;

public static class ServiceCollectionExtensions
{
    public static void AddPostgresSqlDataAccessSchemaMigrator(this IServiceCollection collection, string connectionString)
    {
        collection.AddDbContext<DataAccessSchemaMigratorDbContext>(
            builder =>
                builder.UseNpgsql(connectionString, x => x.MigrationsAssembly("OrderManager.DataAccess.Postgres")));
        
        collection.AddScoped<IDataAccessSchemaMigrator, DataAccessSchemaMigrator>();
    }

    public static void AddPostgresSqlContext(this IServiceCollection collection, string connectionString)
    {
        collection.AddDbContextPool<ApplicationDatabaseContext>(
            builder => builder.UseNpgsql(
                    connectionString, optionsBuilder =>
                    {
                        optionsBuilder.EnableRetryOnFailure(3);
                    }));

        collection.AddScoped<IExceptionExtensionsDataAccess, DuplicateException>();

        collection.AddScoped<IProviderRepository, ProviderRepository>();
        collection.AddScoped<IOrderRepository, OrderRepository>();
    }
}