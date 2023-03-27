namespace OrderManager.Integration.Http.Models.Orders;

public sealed class GetOrdersListResponse
{
    public Guid OrderId { get; init; }
    public Guid ProviderId { get; init; }
    public string Number { get; init; } = null!;
    public DateTimeOffset Date { get;init; }
    public DateTimeOffset CreatedOn { get; init; }
    public DateTimeOffset UpdatedOn { get; init; }
}