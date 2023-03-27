using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OrderManager.DataAccess.IRepository;
using OrderManager.Domain.Provider;

namespace OrderManager.DataAccess.Postgres;

public sealed class ProviderRepository : IProviderRepository
{
    private readonly ApplicationDatabaseContext _context;

    public ProviderRepository(ApplicationDatabaseContext context)
    {
        _context = context;
    }

    public IQueryable<Provider> Queryable => _context.Providers.AsQueryable();

    public async Task<IEnumerable<Provider>> GetListAsync(CancellationToken cancellationToken = default)
        => await _context.Providers.ToListAsync(cancellationToken);


    public async Task<Provider?> FindByIdAsync(Guid providerId, CancellationToken cancellationToken = default)
        => await _context.Providers
            .FirstOrDefaultAsync(p => p.AggregateRootId == providerId, cancellationToken);

    public async Task Delete(Provider provider)
    {
        _context.Providers.Remove(provider);
        
        if (_context.Entry(provider).State == EntityState.Deleted)
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


    public async Task SaveAsync(Provider provider)
    {
        if (_context.Entry(provider).State == EntityState.Detached)
        {
            await _context.Providers.AddAsync(provider);
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