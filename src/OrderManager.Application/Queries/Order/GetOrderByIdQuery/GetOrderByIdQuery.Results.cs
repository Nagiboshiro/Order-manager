
namespace OrderManager.Application.Queries.Order.GetOrderByIdQuery;

public sealed partial class GetOrderByIdQuery
{
    private static Results.SuccessResult Success(OrderView order) 
        => new(order);
    
    private static Results.FailResult NotFound(Guid orderId) => new(
        ApplicationErrors.ORDER_NOT_FOUND, $"Заказ с идентификатором '{orderId}' не найден");
    
    public static class Results
    {
        public sealed class SuccessResult
        {
            public SuccessResult(OrderView order)
            {
                Order = order;
            }

            public OrderView Order { get; }
        }
        
        public sealed class FailResult
        {
            public FailResult(string code, string message)
            {
                Code = code;
                Message = message;
            }

            public string Code { get; }
            public string Message { get; }
        }
    }

    public sealed class OrderView
    {
        public Guid OrderId { get; init; }
        public Guid ProviderId { get; init; }
        public string Number { get; init; } = null!;
        public DateTimeOffset Date { get;init; }
        public IEnumerable<OrderItemView> OrderItems { get; init; }
        public DateTimeOffset CreatedOn { get; init; }
        public DateTimeOffset UpdatedOn { get; init; }
    }
    
    public sealed class OrderItemView
    {
        public Guid OrderItemId { get; init; }
        public string Name { get; init; }
        public decimal Quantity { get; init; }
        public string Unit { get; init; }
        public DateTimeOffset CreatedOn { get; init; }
        public DateTimeOffset UpdatedOn { get; init; }
    }
}