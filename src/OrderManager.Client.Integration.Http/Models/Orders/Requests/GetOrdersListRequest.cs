namespace OrderManager.Integration.Http.Models.Orders;

public sealed class GetOrdersListRequest
{
    public string? Number { get; init; }
    public DateTimeOffset? StartDate { get; init; }
    public DateTimeOffset? EndDate { get; init; }
}