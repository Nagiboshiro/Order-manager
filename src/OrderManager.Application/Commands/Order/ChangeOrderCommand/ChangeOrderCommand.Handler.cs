using MediatR;
using OneOf;
using OrderManager.DataAccess.IRepository;

namespace OrderManager.Application.Commands.Order.ChangeOrderCommand;

public sealed partial class ChangeOrderCommand
{
    public sealed class Handler : IRequestHandler<ChangeOrderCommand, 
        OneOf<Results.SuccessResult, Results.FailResult>>
    {
        private readonly IOrderRepository _orderRepository;

        public Handler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OneOf<Results.SuccessResult, Results.FailResult>> Handle(
            ChangeOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindByIdAsync(request.OrderId, cancellationToken);

            if (order is null)
            {
                return NotFound(request.OrderId);
            }

            if (order.ProviderId == request.ProviderId &&
                order.Number == request.Number &&
                order.Date == request.Date)
            {
                return Success();
            }
            
            order.Change(request.ProviderId, request.Number, request.Date);

            await _orderRepository.SaveAsync(order);

            return Success();
        }
    }
}