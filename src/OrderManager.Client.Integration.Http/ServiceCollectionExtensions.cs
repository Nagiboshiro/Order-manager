using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace OrderManager.Integration.Http.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddHttpOrderManagerServer(this IServiceCollection collection,
        string address)
    {
        collection.AddRefitClient<IOrderManager>().ConfigureHttpClient(client =>
        {
            client.BaseAddress = new Uri(address);
        });
    }
}