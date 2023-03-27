using MediatR;
using OneOf;
using OrderManager.DataAccess.IRepository;
using OrderManager.Domain.Order;
using OrderManager.Domain.Provider;


public sealed partial class CreateOrderCommand
{
    public sealed class Handler : IRequestHandler<CreateOrderCommand,
        OneOf<Results.SuccessResult, Results.FailResult>>
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IOrderRepository _orderRepository;

        public Handler(IOrderRepository orderRepository, IProviderRepository providerRepository)
        {
            _orderRepository = orderRepository;
            _providerRepository = providerRepository;
        }

        public async Task<OneOf<Results.SuccessResult, Results.FailResult>> Handle(
            CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var provider = await _providerRepository.FindByIdAsync(request.ProviderId, cancellationToken);

            if (provider is null)
            {
                return ProviderNotFound(request.ProviderId);
            }

            var order = await _orderRepository.FindByIdAsync(request.OrderId, cancellationToken);

            if (order is not null)
            {
                if (order.ProviderId != request.ProviderId ||
                    order.Number != request.Number ||
                    order.Date != request.Date)
                    return AlreadyExist(request.OrderId);

                return Success();
            }

            order = new Order(request.OrderId, request.ProviderId, request.Number, request.Date);

            try
            {
                await _orderRepository.SaveAsync(order);
            }
            catch (Exception e)
            {
                return AlreadyExist();
            }
            

            return Success();

        }
    }
}