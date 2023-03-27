using MediatR;
using OneOf;
using OrderManager.DataAccess.IRepository;

namespace OrderManager.Application.Commands.Provider;

public sealed partial class ChangeProviderCommand
{
    public sealed class Handler : IRequestHandler<ChangeProviderCommand, 
        OneOf<Results.SuccessResult, Results.FailResult>>
    {
        private readonly IProviderRepository _providerRepository;

        public Handler(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<OneOf<Results.SuccessResult, Results.FailResult>> Handle(
            ChangeProviderCommand request, CancellationToken cancellationToken)
        {
            var provider = await _providerRepository.FindByIdAsync(request.ProviderId, cancellationToken);

            if (provider is null)
            {
                return NotFound(request.ProviderId);
            }

            if (provider.Name == request.Name)
            {
                return Success();
            }
            
            provider.Change(request.Name);

            await _providerRepository.SaveAsync(provider);

            return Success();

        }
    }
}