namespace OrderManager.Integration.Http.Models.Providers;

public sealed class GetProvidersList
{
    public Guid ProviderId { get; init; }
    public string Name { get; init; } = null!;
    public DateTimeOffset CreatedOn { get; init; }
    public DateTimeOffset UpdatedOn { get; init; }
}