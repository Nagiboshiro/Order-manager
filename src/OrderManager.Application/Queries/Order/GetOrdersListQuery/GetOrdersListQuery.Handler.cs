using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OrderManager.Application.Extensions;
using OrderManager.DataAccess.IRepository;

namespace OrderManager.Application.Queries;

public sealed partial class GetOrdersListQuery
{
    public sealed class Handler : IRequestHandler<GetOrdersListQuery,
        OneOf<Results.SuccessResult, Results.FailResult>>
    {
        private readonly IOrderRepository _orderRepository;

        public Handler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OneOf<Results.SuccessResult, Results.FailResult>> Handle(
            GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var query = _orderRepository.Queryable;

            if (string.IsNullOrWhiteSpace(request.Number) is false)
            {
                query = query.Where(n => n.Number.StartsWith(request.Number));
            }

            if (request.StartDate != default && request.EndDate != default)
            {
                query = query.Where(d => d.Date >= request.StartDate && d.Date < request.EndDate);
            }


            var ordersList = await query
                .OrderBy(i => i.CreatedOn)
                .Select(o => new OrderView
                {
                    OrderId = o.AggregateRootId,
                    ProviderId = o.ProviderId,
                    Date = o.Date,
                    Number = o.Number,
                    CreatedOn = o.CreatedOn,
                    UpdatedOn = o.UpdatedOn
                }).ToListAsync(cancellationToken);


            return Success(ordersList);
        }
    }
}