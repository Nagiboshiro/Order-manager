using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OrderManager.DataAccess.IRepository;

namespace OrderManager.Application.Queries;

public sealed partial class GetProvidersListQuery
{
    public sealed class Handler : IRequestHandler<GetProvidersListQuery, OneOf<
        Results.SuccessResult, Results.FailResult>>
    {
        private readonly IProviderRepository _providerRepository;

        public Handler(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<OneOf<Results.SuccessResult, Results.FailResult>> Handle(
            GetProvidersListQuery request, CancellationToken cancellationToken)
        {
            var query = _providerRepository.Queryable;
            
            if (string.IsNullOrWhiteSpace(request.Name) is false)
            {
                query = query.Where(n => n.Name.StartsWith(request.Name));
            }

            var providersList = await query
                .OrderBy(i => i.CreatedOn)
                .Select(p => new ProviderView
                {
                    ProviderId = p.AggregateRootId,
                    Name = p.Name,
                    CreatedOn = p.CreatedOn,
                    UpdatedOn = p.UpdatedOn
                }).ToListAsync(cancellationToken);
            

            return Success(providersList);
        }
    }
}