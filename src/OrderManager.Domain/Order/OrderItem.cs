namespace OrderManager.Domain.Order;

public sealed class OrderItem
{
    private OrderItem() {}
    
    public OrderItem(Guid orderItemId, string name, decimal quantity, string unit)
    {
        OrderItemId = orderItemId;
        Name = name;
        Quantity = quantity;
        Unit = unit;
        CreatedOn = UpdatedOn = DateTimeOffset.UtcNow;
    }
    
    public Guid OrderItemId { get; private set; }
    public string Name { get; private set; }
    public decimal Quantity { get; private set; }
    public string Unit { get; private set; }
    public DateTimeOffset CreatedOn { get; private set; }
    public DateTimeOffset UpdatedOn { get; private set; }

    public void Change(string? name, decimal? quantity, string? unit)
    {
        Name = name ?? Name;
        Quantity = quantity ?? Quantity;
        Unit = unit ?? Unit;
        UpdatedOn = DateTimeOffset.UtcNow;
    }
    
}