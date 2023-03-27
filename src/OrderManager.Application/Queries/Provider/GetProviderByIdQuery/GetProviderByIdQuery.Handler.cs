using MediatR;
using OneOf;
using OrderManager.DataAccess.IRepository;

namespace OrderManager.Application.Queries;

public sealed partial class GetProviderByIdQuery
{
    public sealed class Handler : IRequestHandler<GetProviderByIdQuery, 
        OneOf<Results.SuccessResult, Results.FailResult>>
    {
        private readonly IProviderRepository _providerRepository;

        public Handler(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<OneOf<Results.SuccessResult, Results.FailResult>> Handle(
            GetProviderByIdQuery request, CancellationToken cancellationToken)
        {
            var provider = await _providerRepository.FindByIdAsync(request.ProviderId, cancellationToken);

            if (provider is null)
            {
                return NotFound(request.ProviderId);
            }

            return Success(new ProviderView
            {
                ProviderId = provider.AggregateRootId,
                Name = provider.Name,
                CreatedOn = provider.CreatedOn,
                UpdatedOn = provider.UpdatedOn
            });
        }
    }
}