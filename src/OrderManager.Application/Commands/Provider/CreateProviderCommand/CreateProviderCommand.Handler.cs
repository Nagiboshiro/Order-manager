using MediatR;
using OneOf;
using OrderManager.DataAccess.IRepository;
using OrderManager.Domain.Provider;

public sealed partial class CreateProviderCommand
{
    private sealed class Handler : IRequestHandler<CreateProviderCommand, OneOf<
        Results.SuccessResult, Results.FailResult>>
    {
        private readonly IProviderRepository _providerRepository;

        public Handler(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<OneOf<Results.SuccessResult, Results.FailResult>> Handle(
            CreateProviderCommand request, CancellationToken cancellationToken)
        {
            var provider = await _providerRepository.FindByIdAsync(request.ProviderId, cancellationToken);

            if (provider is not null)
            {
                return AlreadyExist(request.ProviderId);
            }
            
            provider = new Provider(request.ProviderId, request.Name!);

            await _providerRepository.SaveAsync(provider);

            return Success();
        }
    }
}