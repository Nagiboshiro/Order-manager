namespace OrderManager.Domain.Order;

public sealed class Order : AggregateRoot
{
    private Order() { }

    public Order(Guid orderId, Guid providerId, string number, DateTimeOffset date)
        :base(orderId)
    {
        ProviderId = providerId;
        Number = number;
        Date = date;
        CreatedOn = UpdatedOn = DateTimeOffset.UtcNow;
    }
    
    private readonly List<OrderItem> _orderItems = new();
    
    public Guid ProviderId { get; private set; }
    public string Number { get; private set; }
    public DateTimeOffset Date { get; private set; }
    public DateTimeOffset CreatedOn { get; private set; }
    public DateTimeOffset UpdatedOn { get; private set; }
    
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public void AddOrderItem(Guid orderItemId, string name, decimal quantity, string unit)
    {
        var orderItem = _orderItems.FirstOrDefault(i => i.OrderItemId == orderItemId);

        if (orderItem is not null)
        {
            if (orderItem.Quantity != quantity ||
                orderItem.Unit != unit)
            {
                return; 
            }
            
            return; ///TODO Already exist
        }

        _orderItems.Add(new OrderItem(orderItemId, name, quantity, unit));
    }

    public void Change(Guid? providerId, string? number, DateTimeOffset? date)
    {
        if (providerId is not null && providerId == ProviderId &&
            number is not null && number == Number &&
            date is not null && date == Date)
        {
            return;
        }

        ProviderId = providerId ?? ProviderId;
        Number = number ?? Number;
        Date = date ?? Date;
        UpdatedOn = DateTimeOffset.UtcNow;
    }

    public void ChangeOrderItem(Guid orderItemId, string? name, decimal? quantity, string? unit)
    {
        var orderItem = _orderItems.FirstOrDefault(i => i.OrderItemId == orderItemId);

        if (orderItem is null)
        {
            return; ///TODO Exception Not Found
        }
        
        if (name is not null && name == orderItem.Name &&
            quantity is not null && quantity == orderItem.Quantity &&
            unit is not null && unit == orderItem.Unit)
        {
            return;
        }

        orderItem.Change(name, quantity, unit);
        
        UpdatedOn = DateTimeOffset.UtcNow;
    }

    public void RemoveOrderItem(Guid orderItemId)
    {
        var orderItem = _orderItems.FirstOrDefault(i => i.OrderItemId == orderItemId);

        if (orderItem is null)
        {
            return;
        }

        _orderItems.Remove(orderItem);
    }
}