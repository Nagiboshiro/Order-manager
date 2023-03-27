namespace OrderManager.Integration.Http.Models.Orders;

public sealed class GetOrderByIdRequest
{
    public string? Name { get; init; }
    public string? Unit { get; init; }
}