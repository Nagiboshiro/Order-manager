namespace OrderManager.Integration.Http.Models.OrderItems.Request;

public sealed class ChangeOrderItemRequest
{
    public string? Name { get; init; } = null!;
    public decimal? Quantity { get; init; } = null!;
    public string? Unit { get; init; } = null!;
}