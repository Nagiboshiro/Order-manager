using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OrderManager.DataAccess.IRepository;

namespace OrderManager.Application.Queries.Order.GetOrderByIdQuery;

public sealed partial class GetOrderByIdQuery
{
    public sealed class Handler : IRequestHandler<GetOrderByIdQuery,
        OneOf<Results.SuccessResult, Results.FailResult>>
    {
        private readonly IOrderRepository _orderRepository;

        public Handler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OneOf<Results.SuccessResult, Results.FailResult>> Handle(
            GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var query = _orderRepository.Queryable;

            query = query.Where(q => q.AggregateRootId == request.OrderId);

            query = query.Include(d => d.OrderItems.Where(i =>
                    (string.IsNullOrWhiteSpace(request.Name) || i.Name.StartsWith(request.Name))
                    && 
                    (string.IsNullOrWhiteSpace(request.Unit) || i.Unit.StartsWith(request.Unit))));


            var item = await query.FirstOrDefaultAsync(cancellationToken);

            if (item is null)
            {
                return NotFound(request.OrderId);
            }

            return Success(new OrderView
            {
                ProviderId = item.ProviderId,
                OrderId = item.AggregateRootId,
                Number = item.Number,
                Date = item.Date,
                OrderItems = item.OrderItems.Select(i => new OrderItemView
                {
                    OrderItemId = i.OrderItemId,
                    Name = i.Name,
                    Quantity = i.Quantity,
                    Unit = i.Unit,
                    CreatedOn = i.CreatedOn,
                    UpdatedOn = i.UpdatedOn
                }),
                CreatedOn = item.CreatedOn,
                UpdatedOn = item.UpdatedOn
            });
        }
    }
}