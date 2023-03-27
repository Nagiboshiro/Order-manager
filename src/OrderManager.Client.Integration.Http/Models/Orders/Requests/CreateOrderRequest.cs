namespace OrderManager.Integration.Http.Models.Orders;

public sealed class CreateOrderRequest
{
    public Guid OrderId { get; init; }
    public Guid ProviderId { get; init; }
    public string Number { get; init; } = null!;
    public DateTimeOffset Date { get; init; }
}