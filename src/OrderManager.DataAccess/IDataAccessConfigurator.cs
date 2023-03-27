using Microsoft.Extensions.DependencyInjection;

namespace OrderManager.DataAccess;

public interface IDataAccessConfigurator
{

    public static IDataAccessConfigurator Instance(IServiceCollection serviceCollection)
    {
        return new DefaultDataAccessConfigurator(serviceCollection);
    }

    void Configure(Action<IServiceCollection> action);

}
internal sealed class DefaultDataAccessConfigurator : IDataAccessConfigurator
{

    private readonly IServiceCollection serviceCollection;

    public DefaultDataAccessConfigurator(IServiceCollection serviceCollection)
    {
        this.serviceCollection = serviceCollection;
    }

    public void Configure(Action<IServiceCollection> action)
    {
        action.Invoke(serviceCollection);
    }

}