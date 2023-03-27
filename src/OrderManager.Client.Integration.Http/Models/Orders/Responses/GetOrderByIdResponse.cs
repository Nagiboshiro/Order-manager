namespace OrderManager.Integration.Http.Models.Orders;

public sealed class GetOrderByIdResponse
{
    public Guid OrderId { get; init; }
    public Guid ProviderId { get; init; }
    public string Number { get; init; } = null!;
    public DateTimeOffset Date { get;init; }
    public IEnumerable<OrderItemView> OrderItems { get; init; } = null!;
    public DateTimeOffset CreatedOn { get; init; }
    public DateTimeOffset UpdatedOn { get; init; }
}

public sealed class OrderItemView
{
    public Guid OrderItemId { get; init; }
    public string Name { get; init; } = null!;
    public decimal Quantity { get; init; }
    public string Unit { get; init; } = null!;
    public DateTimeOffset CreatedOn { get; init; }
    public DateTimeOffset UpdatedOn { get; init; }
}