namespace OrderManager.Integration.Http.Models.Providers;

public sealed class CreateProviderRequest
{
    public Guid ProviderId { get; init; }
    public string Name { get; init; } = null!;
}