using MediatR;
using OneOf;
using OrderManager.DataAccess.IRepository;

namespace OrderManager.Application.Commands.Order;

public sealed partial class DeleteOrderCommand
{
    public sealed class Handler : IRequestHandler<DeleteOrderCommand, OneOf<
        Results.SuccessResult, Results.FailResult>>
    {
        private readonly IOrderRepository _orderRepository;

        public Handler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OneOf<Results.SuccessResult, Results.FailResult>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindByIdAsync(request.OrderId, cancellationToken);

            if (order is null)
            {
                return NotFound(request.OrderId);
            }

            await _orderRepository.Delete(order);

            return Success();
        }
    }
}