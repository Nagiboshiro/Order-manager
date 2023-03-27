using MediatR;
using OneOf;
using OrderManager.DataAccess.IRepository;

namespace OrderManager.Application.Commands.Order;

public sealed partial class RemoveOrderItemFromOrderCommand
{
    public sealed class Handler : IRequestHandler<RemoveOrderItemFromOrderCommand, OneOf<
        Results.SuccessResult, Results.FailResult>>
    {
        private readonly IOrderRepository _orderRepository;

        public Handler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OneOf<Results.SuccessResult, Results.FailResult>> Handle(
            RemoveOrderItemFromOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindByIdAsync(request.OrderId, cancellationToken);

            if (order is null)
            {
                return OrderNotFound(request.OrderId);
            }
            
            order.RemoveOrderItem(request.OrderItemId);

            await _orderRepository.SaveAsync(order);

            return Success();
        }
    }
}