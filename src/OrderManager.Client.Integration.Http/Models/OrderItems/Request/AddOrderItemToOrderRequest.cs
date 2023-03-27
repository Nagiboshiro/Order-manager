namespace OrderManager.Integration.Http.Models.OrderItems.Request;

public sealed class AddOrderItemToOrderRequest
{
    public Guid OrderItemId { get; init; }
    public decimal Quantity { get; init; }
    public string Unit { get; init; } = null!;
    public string Name { get; init; } = null!;
}