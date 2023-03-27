using MediatR;
using OneOf;

namespace OrderManager.Application.Queries;

public sealed partial class GetProvidersListQuery : IRequest<OneOf<
    GetProvidersListQuery.Results.SuccessResult, GetProvidersListQuery.Results.FailResult>>
{
    public string? Name { get; init; }
}