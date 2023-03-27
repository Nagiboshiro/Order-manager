using MediatR;
using OneOf;
using OrderManager.DataAccess.IRepository;

namespace OrderManager.Application.Commands.Order.ChangeOrderItemCommand;

public sealed partial class ChangeOrderItemCommand
{
    public sealed class Handler : IRequestHandler<ChangeOrderItemCommand, OneOf<
        Results.SuccessResult, Results.FailResult>>
    {
        private readonly IOrderRepository _orderRepository;

        public Handler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OneOf<Results.SuccessResult, Results.FailResult>> Handle(
            ChangeOrderItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindByIdAsync(request.OrderId, cancellationToken);

            if (order is null)
            {
                return OrderNotFound(request.OrderId);
            }

            order.ChangeOrderItem(request.OrderItemId, request.Name, request.Quantity, request.Unit);

            await _orderRepository.SaveAsync(order);

            return Success();
        }
    }
}