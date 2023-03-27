using Microsoft.EntityFrameworkCore;
using OrderManager.DataAccess;
using OrderManager.DataAccess.IRepository;
using OrderManager.DataAccess.Postgres;
using OrderManager.Domain.Order;

public sealed class OrderRepository : IOrderRepository
{
    private readonly ApplicationDatabaseContext _context;

    public OrderRepository(ApplicationDatabaseContext context)
    {
        _context = context;
    }

    public IQueryable<Order> Queryable => _context.Orders.AsQueryable();

    public async Task<IEnumerable<Order>> GetListAsync(CancellationToken cancellationToken = default)
        => await _context.Orders.ToListAsync(cancellationToken);

    public async Task<Order?> FindByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
        => await _context.Orders
            .Include(i => i.OrderItems)
            .FirstOrDefaultAsync(o => o.AggregateRootId == orderId, cancellationToken);

    public async Task Delete(Order order)
    {
        _context.Orders.Remove(order);

        if (_context.Entry(order).State == EntityState.Deleted)
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    public async Task SaveAsync(Order order)
    {
        if (_context.Entry(order).State == EntityState.Detached)
        {
            await _context.Orders.AddAsync(order);
        }
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _context.ChangeTracker.Clear();
            throw new OptimisticConcurrencyException(ex.Message, ex);
        }
        catch (DbUpdateException ex)
        {
            throw new AggregateUpdateException(ex.Message, ex);
        }
    }
}