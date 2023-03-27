using MediatR;
using OneOf;
using OrderManager.DataAccess.IRepository;

namespace OrderManager.Application.Commands.Provider;

public sealed partial class DeleteProviderCommand
{
    public sealed class Handler : IRequestHandler<DeleteProviderCommand, 
        OneOf<Results.SuccessResult, Results.FailResult>>
    {
        private readonly IProviderRepository _providerRepository;

        public Handler(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<OneOf<Results.SuccessResult, Results.FailResult>> Handle(DeleteProviderCommand request, CancellationToken cancellationToken)
        {
            var provider = await _providerRepository.FindByIdAsync(request.ProviderId, cancellationToken);

            if (provider is null)
            {
                return NotFound(request.ProviderId);
            }

            await _providerRepository.Delete(provider);
            
            return Success();
        }
    }
}