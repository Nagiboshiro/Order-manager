using MediatR;
using OneOf;

namespace OrderManager.Application.Queries.Order.GetOrderByIdQuery;

public sealed partial class GetOrderByIdQuery : IRequest<OneOf<
    GetOrderByIdQuery.Results.SuccessResult, GetOrderByIdQuery.Results.FailResult>>
{
    public Guid OrderId { get; init; }
    public string? Name { get; init; }
    public string? Unit { get; init; }

}