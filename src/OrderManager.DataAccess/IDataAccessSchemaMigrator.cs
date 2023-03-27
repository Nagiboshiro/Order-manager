namespace OrderManager.DataAccess;

public interface IDataAccessSchemaMigrator
{
    ValueTask MigrateAsync();
}