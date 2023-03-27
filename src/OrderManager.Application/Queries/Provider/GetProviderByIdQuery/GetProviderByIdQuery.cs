using MediatR;
using OneOf;

namespace OrderManager.Application.Queries;

public sealed partial class GetProviderByIdQuery : IRequest<OneOf<
    GetProviderByIdQuery.Results.SuccessResult, GetProviderByIdQuery.Results.FailResult>>
{
    public Guid ProviderId { get; init; }
}