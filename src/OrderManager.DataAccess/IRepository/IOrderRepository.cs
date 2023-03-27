using OrderManager.Domain.Order;

namespace OrderManager.DataAccess.IRepository;

public interface IOrderRepository
{
    public IQueryable<Order> Queryable { get; }
    public Task<Order?> FindByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    public Task Delete(Order order);
    public Task SaveAsync(Order order);

}