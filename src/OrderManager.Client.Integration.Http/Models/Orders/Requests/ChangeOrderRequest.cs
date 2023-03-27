namespace OrderManager.Integration.Http.Models.Orders;

public sealed class ChangeOrderRequest
{
    public Guid? ProviderId { get; init; }
    public string? Number { get; init; } = null!;
    public DateTimeOffset? Date { get; init; }
}