using MediatR;
using OneOf;

namespace OrderManager.Application.Queries;

public sealed partial class GetOrdersListQuery : IRequest<OneOf<
    GetOrdersListQuery.Results.SuccessResult, GetOrdersListQuery.Results.FailResult>>
{
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public string? Number { get; init; }
}