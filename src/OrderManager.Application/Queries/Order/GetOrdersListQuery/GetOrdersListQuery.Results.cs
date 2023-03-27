namespace OrderManager.Application.Queries;

public sealed partial class GetOrdersListQuery
{
    private static Results.SuccessResult Success(IEnumerable<OrderView> orders) 
        => new(orders);
    
    public static class Results
    {
        public sealed class SuccessResult
        {
            public SuccessResult(IEnumerable<OrderView> orders)
            {
                Orders = orders;
            }

            public IEnumerable<OrderView> Orders { get; }
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
        public DateTimeOffset CreatedOn { get; init; }
        public DateTimeOffset UpdatedOn { get; init; }
    }
    
    
}