using OrderManager.Domain.Provider;

namespace OrderManager.DataAccess.IRepository;

public interface IProviderRepository
{
    public IQueryable<Provider> Queryable { get; }
    public Task<IEnumerable<Provider>> GetListAsync(CancellationToken cancellationToken = default);
    public Task<Provider?> FindByIdAsync(Guid providerId, CancellationToken cancellationToken = default);
    public Task Delete(Provider provider);
    public Task SaveAsync(Provider provider);

}