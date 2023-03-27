using MediatR;
using OneOf;
using OrderManager.DataAccess.IRepository;

namespace OrderManager.Application.Commands.Order.AddOrderItemToOrderCommand;

public sealed partial class AddOrderItemToOrderCommand
{
    public sealed class Handler : IRequestHandler<AddOrderItemToOrderCommand, 
        OneOf<Results.SuccessResult, Results.FailResult>>
    {
        private readonly IOrderRepository _orderRepository;

        public Handler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OneOf<Results.SuccessResult, Results.FailResult>> Handle(
            AddOrderItemToOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindByIdAsync(request.OrderId, cancellationToken);

            if (order is null)
            {
                return OrderNotFound(request.OrderId);
            }

            if (order.Number == request.Name)
            {
                return OrderNumberEqualOrderItemName();
            }

            order.AddOrderItem(request.OrderItemId, request.Name, request.Quantity, request.Unit);

            await _orderRepository.SaveAsync(order);

            return Success();
        }
    }
}